namespace PrevisionAR.Motor.Computo;

/// <summary>Tramo en el que dos períodos con aportes se pisan. Ese tramo se computa una sola vez.</summary>
/// <param name="PeriodoA">Empleador del primer período.</param>
/// <param name="PeriodoB">Empleador del segundo período.</param>
/// <param name="Desde">Inicio del tramo superpuesto (inclusive).</param>
/// <param name="Hasta">Fin del tramo superpuesto (inclusive).</param>
/// <param name="DiasCalendario">Días del tramo superpuesto.</param>
public sealed record Superposicion(string PeriodoA, string PeriodoB, DateOnly Desde, DateOnly Hasta, int DiasCalendario);
