namespace PrevisionAR.Motor.Modelo;

/// <summary>Conjunto de períodos de servicio de una persona.</summary>
public sealed record HistoriaLaboral(IReadOnlyList<PeriodoDeServicio> Periodos)
{
    public HistoriaLaboral(params PeriodoDeServicio[] periodos)
        : this((IReadOnlyList<PeriodoDeServicio>)periodos)
    {
    }
}
