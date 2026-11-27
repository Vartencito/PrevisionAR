using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Web.Casos;

/// <summary>
/// Lo que el abogado tiene cargado en la pantalla, completo o no. Es lo que se guarda en el archivo del caso
/// (ADR 0007). No calcula nada: para calcular se pasa al motor con <see cref="APersona"/> y <see cref="AHistoriaLaboral"/>.
/// </summary>
public sealed class CasoEnEdicion
{
    /// <summary>Texto libre y opcional para reconocer el caso (ADR 0007, decisión 3). Nunca llega al motor.</summary>
    public string Referencia { get; set; } = string.Empty;

    public DateOnly FechaDeNacimiento { get; set; } = new(1960, 1, 1);

    public Sexo Sexo { get; set; } = Sexo.Hombre;

    public DateOnly FechaDeCalculo { get; set; }

    public List<FilaPeriodo> Filas { get; } = [];

    public Persona APersona() => new(FechaDeNacimiento, Sexo);

    public HistoriaLaboral AHistoriaLaboral() => new(Filas.Select(f => f.APeriodo()));
}

/// <summary>Fila editable de la historia laboral. Solo traslada datos al motor; no calcula nada.</summary>
public sealed class FilaPeriodo
{
    public string Empleador { get; set; } = string.Empty;

    public DateOnly Desde { get; set; } = new(2000, 1, 1);

    public DateOnly? Hasta { get; set; }

    public TipoDeServicio Tipo { get; set; } = TipoDeServicio.RelacionDeDependencia;

    public bool ConAportes { get; set; } = true;

    public static FilaPeriodo DesdePeriodo(PeriodoDeServicio periodo)
    {
        ArgumentNullException.ThrowIfNull(periodo);
        return new()
        {
            Empleador = periodo.Empleador,
            Desde = periodo.Desde,
            Hasta = periodo.Hasta,
            Tipo = periodo.Tipo,
            ConAportes = periodo.ConAportes,
        };
    }

    public PeriodoDeServicio APeriodo() =>
        new(string.IsNullOrWhiteSpace(Empleador) ? "(sin nombre)" : Empleador.Trim(), Desde, Hasta, Tipo, ConAportes);
}
