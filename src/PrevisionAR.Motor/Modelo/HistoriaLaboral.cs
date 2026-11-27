namespace PrevisionAR.Motor.Modelo;

/// <summary>Conjunto de períodos de servicio de una persona. Guarda su propia copia de la lista.</summary>
public sealed record HistoriaLaboral
{
    public HistoriaLaboral(IEnumerable<PeriodoDeServicio> periodos)
    {
        ArgumentNullException.ThrowIfNull(periodos);
        Periodos = [.. periodos];
    }

    public HistoriaLaboral(params PeriodoDeServicio[] periodos)
        : this((IEnumerable<PeriodoDeServicio>)periodos)
    {
    }

    public IReadOnlyList<PeriodoDeServicio> Periodos { get; }
}
