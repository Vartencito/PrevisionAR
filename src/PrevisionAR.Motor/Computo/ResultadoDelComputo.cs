namespace PrevisionAR.Motor.Computo;

/// <summary>Resultado del cómputo de servicios con aportes (R001).</summary>
/// <param name="FechaDeCalculo">Fecha hasta la que se computó (inclusive).</param>
/// <param name="Total">Tiempo total de servicios con aportes, sin contar dos veces los tramos superpuestos.</param>
/// <param name="HastaJunio1994">Desglose del tiempo hasta el 30/06/1994 (lo usará la PC).</param>
/// <param name="DesdeJulio1994">Desglose del tiempo desde el 01/07/1994 (lo usará la PAP).</param>
/// <param name="Superposiciones">Tramos en que dos períodos con aportes se pisan.</param>
/// <param name="NoComputados">Períodos que no sumaron y por qué.</param>
public sealed record ResultadoDelComputo(
    DateOnly FechaDeCalculo,
    TiempoDeServicio Total,
    DesgloseDeTramo HastaJunio1994,
    DesgloseDeTramo DesdeJulio1994,
    IReadOnlyList<Superposicion> Superposiciones,
    IReadOnlyList<PeriodoNoComputado> NoComputados)
{
    /// <summary>Ficha normativa con la que se calculó.</summary>
    public string Regla => Reglas.ComputoDeServicios;
}
