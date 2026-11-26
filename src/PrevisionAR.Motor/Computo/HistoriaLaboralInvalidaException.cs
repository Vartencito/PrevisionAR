using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Computo;

/// <summary>La historia laboral tiene datos que no permiten calcular (por ejemplo, fechas invertidas).</summary>
public sealed class HistoriaLaboralInvalidaException : Exception
{
    public HistoriaLaboralInvalidaException(PeriodoDeServicio periodo, string motivo)
        : base($"Período \"{periodo.Empleador}\": {motivo}")
    {
        Periodo = periodo;
    }

    /// <summary>El período con el problema.</summary>
    public PeriodoDeServicio Periodo { get; }
}
