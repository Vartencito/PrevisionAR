using PrevisionAR.Motor.Requisitos;

namespace PrevisionAR.Motor.Tests.Requisitos;

/// <summary>Cálculo de edad cumplida "de fecha a fecha" (Código Civil y Comercial, art. 6), usado por R002.</summary>
public class EdadTests
{
    [Theory]
    [InlineData("1960-03-15", "2026-01-01", 789)] // 65a 9m
    [InlineData("1961-01-01", "2026-01-01", 780)] // cumpleaños exacto
    [InlineData("1961-01-02", "2026-01-01", 779)] // un día antes del cumpleaños
    [InlineData("1964-02-29", "2026-02-28", 744)] // nacido un 29/02: en año no bisiesto cumple el 28/02 (CCyC art. 6)
    [InlineData("1964-02-29", "2026-02-27", 743)] // un día antes
    [InlineData("1990-01-31", "1990-04-30", 3)]   // nacido un 31: en un mes de 30 días cumple el 30
    public void MesesCumplidos_Fechas_CuentaMesesCumplidos(string nacimiento, string fecha, int esperado)
    {
        Assert.Equal(esperado, Edad.MesesCumplidos(DateOnly.Parse(nacimiento), DateOnly.Parse(fecha)));
    }
}
