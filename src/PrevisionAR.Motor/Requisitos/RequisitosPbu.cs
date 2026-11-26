using PrevisionAR.Motor.Computo;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Requisitos;

/// <summary>
/// R002 — Requisitos de edad y servicios para la PBU del régimen general.
/// Ficha: docs/normativa/R002-requisitos-pbu.md
/// </summary>
public static class RequisitosPbu
{
    /// <summary>Ley 24.241, art. 19 inc. a): hombres, 65 años.</summary>
    public const int EdadHombresMeses = 65 * 12;

    /// <summary>Ley 24.241, art. 19 inc. b): mujeres, 60 años.</summary>
    public const int EdadMujeresMeses = 60 * 12;

    /// <summary>Ley 24.241, art. 19 inc. c) y Decreto 679/95: 30 años = 360 meses con aportes.</summary>
    public const int ServiciosRequeridosMeses = 360;

    /// <summary>Ley 24.241, art. 19: 2 años de edad excedente compensan 1 año de servicios faltante.</summary>
    public const int MesesDeEdadPorMesDeServicio = 2;

    /// <summary>Verifica si la persona cumple los requisitos de la PBU a la fecha de cálculo.</summary>
    /// <exception cref="DatosPersonalesInvalidosException">Si nació después de la fecha de cálculo.</exception>
    /// <exception cref="HistoriaLaboralInvalidaException">Si la historia laboral es inválida (R001).</exception>
    public static ResultadoRequisitosPbu Verificar(Persona persona, HistoriaLaboral historia, DateOnly fechaDeCalculo)
    {
        ArgumentNullException.ThrowIfNull(persona);
        if (persona.FechaDeNacimiento > fechaDeCalculo)
        {
            throw new DatosPersonalesInvalidosException("La fecha de nacimiento es posterior a la fecha de cálculo.");
        }

        var computo = ComputoDeServicios.Calcular(historia, fechaDeCalculo);

        var edadCumplida = Edad.MesesCumplidos(persona.FechaDeNacimiento, fechaDeCalculo);
        var edadRequerida = persona.Sexo == Sexo.Hombre ? EdadHombresMeses : EdadMujeresMeses;
        var acreditados = computo.Total.TotalMeses; // Solo meses completos (convención a validar).

        var faltantes = Math.Max(0, ServiciosRequeridosMeses - acreditados);
        var excedente = Math.Max(0, edadCumplida - edadRequerida);

        // La compensación solo sirve para completar servicios, y solo con edad de más (nunca al revés).
        // Se toma la parte entera de excedente ÷ 2 (redondeo a validar).
        var compensables = excedente / MesesDeEdadPorMesDeServicio;
        var compensados = Math.Min(compensables, faltantes);
        var faltantesTrasCompensar = faltantes - compensados;

        var explicacion = Explicar(persona, edadCumplida, edadRequerida, acreditados, faltantes, excedente, compensados, faltantesTrasCompensar);

        return new ResultadoRequisitosPbu(
            fechaDeCalculo,
            edadCumplida,
            edadRequerida,
            acreditados,
            ServiciosRequeridosMeses,
            excedente,
            compensados,
            faltantesTrasCompensar,
            computo,
            explicacion);
    }

    private static List<string> Explicar(
        Persona persona,
        int edadCumplida,
        int edadRequerida,
        int acreditados,
        int faltantes,
        int excedente,
        int compensados,
        int faltantesTrasCompensar)
    {
        var inciso = persona.Sexo == Sexo.Hombre ? "a" : "b";
        var lineas = new List<string>
        {
            $"Edad cumplida: {AniosYMeses(edadCumplida)}. Edad requerida: {AniosYMeses(edadRequerida)} (Ley 24.241, art. 19 inc. {inciso}).",
            edadCumplida >= edadRequerida
                ? "Cumple la edad."
                : $"No cumple la edad: faltan {AniosYMeses(edadRequerida - edadCumplida)}. Los servicios de más no compensan edad.",
            $"Servicios con aportes: {acreditados} meses (R001). Requeridos: {ServiciosRequeridosMeses} meses (Ley 24.241, art. 19 inc. c; Decreto 679/95).",
        };

        if (faltantes == 0)
        {
            lineas.Add("Cumple los servicios.");
        }
        else if (excedente == 0)
        {
            lineas.Add($"Faltan {faltantes} meses de servicios y no hay edad excedente para compensar.");
        }
        else
        {
            lineas.Add(
                $"Faltan {faltantes} meses de servicios. Edad excedente: {excedente} meses, que compensan {excedente / MesesDeEdadPorMesDeServicio} meses " +
                "(2 de edad por 1 de servicios, Ley 24.241, art. 19).");
            lineas.Add(faltantesTrasCompensar == 0
                ? $"Con la compensación de {compensados} meses, cumple los servicios."
                : $"Aun compensando, faltan {faltantesTrasCompensar} meses de servicios.");
        }

        return lineas;
    }

    private static string AniosYMeses(int meses) => (meses / 12, meses % 12) switch
    {
        (0, var m) => $"{m} meses",
        (var a, 0) => $"{a} años",
        (var a, var m) => $"{a} años y {m} meses",
    };
}
