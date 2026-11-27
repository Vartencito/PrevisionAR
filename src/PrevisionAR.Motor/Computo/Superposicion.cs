using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Computo;

/// <summary>
/// Tramo en el que dos períodos con aportes se pisan. Ese tramo se computa una sola vez.
/// Se informa por pares: si tres períodos se pisan, aparecen tres superposiciones.
/// </summary>
/// <param name="PeriodoA">Primer período.</param>
/// <param name="PeriodoB">Segundo período.</param>
/// <param name="Desde">Inicio del tramo común (inclusive).</param>
/// <param name="Hasta">Fin del tramo común (inclusive).</param>
/// <param name="DiasCalendario">Días del tramo común entre este par (no los días descontados del total).</param>
public sealed record Superposicion(PeriodoDeServicio PeriodoA, PeriodoDeServicio PeriodoB, DateOnly Desde, DateOnly Hasta, int DiasCalendario);
