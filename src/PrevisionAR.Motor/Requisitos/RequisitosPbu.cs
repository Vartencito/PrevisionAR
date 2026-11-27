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
    public const int EdadHombresMeses = 65 * ConvencionDeComputo.MesesPorAnio;

    /// <summary>Ley 24.241, art. 19 inc. b): mujeres, 60 años.</summary>
    public const int EdadMujeresMeses = 60 * ConvencionDeComputo.MesesPorAnio;

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
        var edadRequerida = EdadRequerida(persona.Sexo);
        var acreditados = computo.Total.TotalMeses; // Meses según R001; los días que no forman un mes no suman (a validar).

        var faltantes = Math.Max(0, ServiciosRequeridosMeses - acreditados);
        var excedente = Math.Max(0, edadCumplida - edadRequerida);

        // La compensación solo sirve para completar servicios, y solo con edad de más (nunca al revés).
        // Se toma la parte entera de excedente ÷ 2 (redondeo a validar: pregunta 13).
        var compensados = Math.Min(MesesCompensables(excedente), faltantes);

        var resultado = new ResultadoRequisitosPbu(
            FechaDeCalculo: fechaDeCalculo,
            EdadCumplidaMeses: edadCumplida,
            EdadRequeridaMeses: edadRequerida,
            ServiciosAcreditadosMeses: acreditados,
            ServiciosRequeridosMeses: ServiciosRequeridosMeses,
            MesesDeEdadExcedente: excedente,
            MesesCompensados: compensados,
            MesesDeServicioFaltantesTrasCompensar: faltantes - compensados,
            ComputoDeServicios: computo,
            Explicacion: []);

        return resultado with { Explicacion = Explicar(persona, resultado) };
    }

    private static int EdadRequerida(Sexo sexo) => sexo switch
    {
        Sexo.Hombre => EdadHombresMeses,
        Sexo.Mujer => EdadMujeresMeses,
        _ => throw new DatosPersonalesInvalidosException("El sexo indicado no es válido."),
    };

    private static int MesesCompensables(int mesesDeEdadExcedente) => mesesDeEdadExcedente / MesesDeEdadPorMesDeServicio;

    /// <summary>Pasos del razonamiento, en lenguaje claro, con la norma de cada uno.</summary>
    private static List<string> Explicar(Persona persona, ResultadoRequisitosPbu r)
    {
        var inciso = persona.Sexo == Sexo.Hombre ? "a" : "b";
        var lineas = new List<string>
        {
            $"Edad cumplida: {AniosYMeses(r.EdadCumplidaMeses)}. Edad requerida: {AniosYMeses(r.EdadRequeridaMeses)} (Ley 24.241, art. 19 inc. {inciso}).",
            r.CumpleEdad
                ? "Cumple la edad."
                : $"No cumple la edad: faltan {AniosYMeses(r.MesesDeEdadFaltantes)}. Los servicios de más no compensan edad.",
            $"Servicios con aportes: {r.ServiciosAcreditadosMeses} meses (R001). Requeridos: {r.ServiciosRequeridosMeses} meses (Ley 24.241, art. 19 inc. c; Decreto 679/95).",
        };

        if (r.MesesDeServicioFaltantes == 0)
        {
            lineas.Add("Cumple los servicios.");
        }
        else if (r.MesesDeEdadExcedente == 0)
        {
            lineas.Add($"Faltan {r.MesesDeServicioFaltantes} meses de servicios y no hay edad excedente para compensar.");
        }
        else
        {
            lineas.Add(
                $"Faltan {r.MesesDeServicioFaltantes} meses de servicios. Edad excedente: {r.MesesDeEdadExcedente} meses, " +
                $"que compensan {MesesCompensables(r.MesesDeEdadExcedente)} meses (2 de edad por 1 de servicios, Ley 24.241, art. 19).");
            lineas.Add(r.CumpleServicios
                ? $"Con la compensación de {r.MesesCompensados} meses, cumple los servicios."
                : $"Aun compensando, faltan {r.MesesDeServicioFaltantesTrasCompensar} meses de servicios.");
        }

        return lineas;
    }

    private static string AniosYMeses(int meses) => (meses / ConvencionDeComputo.MesesPorAnio, meses % ConvencionDeComputo.MesesPorAnio) switch
    {
        (0, var m) => $"{m} meses",
        (var a, 0) => $"{a} años",
        (var a, var m) => $"{a} años y {m} meses",
    };
}
