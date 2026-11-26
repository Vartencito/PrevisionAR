using PrevisionAR.Motor.Computo;

namespace PrevisionAR.Motor.Tests.Computo;

/// <summary>
/// Convención propuesta en R001: 30 días sueltos = 1 mes, 12 meses = 1 año.
/// PENDIENTE DE VALIDAR con previsionalista (pregunta 1).
/// </summary>
public class ConvencionDeComputoTests
{
    [Theory]
    [InlineData(0, 29, 0, 0, 29)]
    [InlineData(0, 30, 0, 1, 0)]
    [InlineData(11, 45, 1, 0, 15)]
    [InlineData(360, 0, 30, 0, 0)]
    public void Armar_MesesYDiasSueltos_ConvierteSegunConvencion(
        int mesesCompletos, int diasSueltos, int anios, int meses, int dias)
    {
        var tiempo = ConvencionDeComputo.Armar(mesesCompletos, diasSueltos, diasCalendario: 0);

        Assert.Equal((anios, meses, dias), (tiempo.Anios, tiempo.Meses, tiempo.Dias));
    }
}
