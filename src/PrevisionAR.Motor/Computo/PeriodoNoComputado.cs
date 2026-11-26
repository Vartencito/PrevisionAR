using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Computo;

/// <summary>Por qué un período no suma al cómputo.</summary>
public enum MotivoNoComputado
{
    /// <summary>El período no tiene aportes (Ley 24.241, art. 19 inc. c: se exigen servicios con aportes).</summary>
    SinAportes,

    /// <summary>El período empieza después de la fecha de cálculo (Ley 24.241, art. 22).</summary>
    PosteriorALaFechaDeCalculo,
}

/// <summary>Período de la historia laboral que no se computó, con el motivo.</summary>
public sealed record PeriodoNoComputado(PeriodoDeServicio Periodo, MotivoNoComputado Motivo);
