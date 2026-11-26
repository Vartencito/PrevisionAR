namespace PrevisionAR.Motor.Modelo;

/// <summary>
/// Un período de trabajo de la historia laboral. Las fechas son inclusive.
/// </summary>
/// <param name="Empleador">Empleador o actividad. Identifica el período en errores e informes.</param>
/// <param name="Desde">Fecha de inicio (inclusive).</param>
/// <param name="Hasta">Fecha de cese (inclusive). <c>null</c> si sigue trabajando.</param>
/// <param name="Tipo">Relación de dependencia o autónomo.</param>
/// <param name="ConAportes">Si el período tiene aportes (Decreto 679/95, art. 2).</param>
public sealed record PeriodoDeServicio(
    string Empleador,
    DateOnly Desde,
    DateOnly? Hasta,
    TipoDeServicio Tipo,
    bool ConAportes);
