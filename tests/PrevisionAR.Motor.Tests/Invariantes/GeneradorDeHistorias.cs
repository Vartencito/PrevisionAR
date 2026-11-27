using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Tests.Invariantes;

/// <summary>
/// Genera historias laborales ficticias al azar pero reproducibles: la misma semilla da siempre las mismas historias,
/// así un test que falla se puede repetir exactamente.
/// </summary>
internal sealed class GeneradorDeHistorias(int semilla)
{
    private static readonly DateOnly Minimo = new(1970, 1, 1);
    private static readonly DateOnly Maximo = new(2030, 12, 31);

    private readonly Random _azar = new(semilla);

    public HistoriaLaboral Historia(int maximoDePeriodos = 6)
    {
        var cantidad = _azar.Next(0, maximoDePeriodos + 1);
        var periodos = Enumerable.Range(0, cantidad).Select(i => Periodo($"P{i}")).ToArray();
        return new HistoriaLaboral(periodos);
    }

    public PeriodoDeServicio Periodo(string empleador)
    {
        var desde = Fecha(Minimo, Maximo);
        DateOnly? hasta = _azar.Next(10) == 0 ? null : desde.AddDays(_azar.Next(0, 365 * 12));
        if (hasta > Maximo)
        {
            hasta = Maximo;
        }

        var tipo = _azar.Next(3) == 0 ? TipoDeServicio.Autonomo : TipoDeServicio.RelacionDeDependencia;
        var conAportes = _azar.Next(5) != 0;
        return new PeriodoDeServicio(empleador, desde, hasta, tipo, conAportes);
    }

    public Sexo Sexo() => _azar.Next(2) == 0 ? Modelo.Sexo.Hombre : Modelo.Sexo.Mujer;

    public DateOnly Fecha(DateOnly desde, DateOnly hasta) =>
        DateOnly.FromDayNumber(_azar.Next(desde.DayNumber, hasta.DayNumber + 1));

    public T[] Mezclar<T>(IEnumerable<T> elementos)
    {
        var copia = elementos.ToArray();
        _azar.Shuffle(copia);
        return copia;
    }
}
