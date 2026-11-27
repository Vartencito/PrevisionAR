using PrevisionAR.Motor.Computo;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Tests.Computo;

/// <summary>
/// Bordes del cómputo de servicios (ficha R001) que los casos principales no cubren.
/// Datos ficticios.
/// </summary>
public class ComputoDeServiciosBordesTests
{
    private static readonly DateOnly FechaDeCalculo = new(2026, 1, 1);

    private static PeriodoDeServicio Periodo(string empleador, string desde, string? hasta) =>
        new(empleador, DateOnly.Parse(desde), hasta is null ? null : DateOnly.Parse(hasta), TipoDeServicio.RelacionDeDependencia, ConAportes: true);

    private static ResultadoDelComputo Calcular(DateOnly fecha, params PeriodoDeServicio[] periodos) =>
        ComputoDeServicios.Calcular(new HistoriaLaboral(periodos), fecha);

    // Caso 12 — Superposición triple: el tramo común se cuenta una vez y se informa por pares.
    [Fact]
    public void Calcular_TresPeriodosSuperpuestos_CuentaUnaVezEInformaTresPares()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "2000-01-01", "2000-12-31"),
            Periodo("B", "2000-06-01", "2000-12-31"),
            Periodo("C", "2000-10-01", "2000-12-31"));

        Assert.Equal(12, resultado.Total.TotalMeses);
        Assert.Equal(366, resultado.Total.DiasCalendario); // 2000 es bisiesto
        Assert.Equal(3, resultado.Superposiciones.Count);
        Assert.Equal(new DateOnly(2000, 6, 1), resultado.Superposiciones[0].Desde); // ordenadas por fecha
    }

    [Fact]
    public void Calcular_MismoEmpleadorDosVeces_LasSuperposicionesIdentificanCadaPeriodo()
    {
        var primero = Periodo("Empresa A", "2000-01-01", "2005-12-31");
        var segundo = Periodo("Empresa A", "2005-01-01", "2009-12-31");

        var superposicion = Assert.Single(Calcular(FechaDeCalculo, primero, segundo).Superposiciones);

        Assert.Equal(primero, superposicion.PeriodoA);
        Assert.Equal(segundo, superposicion.PeriodoB);
    }

    [Fact]
    public void Calcular_PeriodoDeUnSoloDia_CuentaUnDia()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2010-05-10", "2010-05-10"));

        Assert.Equal((0, 0, 1), (resultado.Total.Anios, resultado.Total.Meses, resultado.Total.Dias));
    }

    [Fact]
    public void Calcular_PeriodoQueEmpiezaElDiaDeCalculo_CuentaEseDia()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2026-01-01", null));

        Assert.Equal(1, resultado.Total.DiasCalendario);
    }

    [Fact]
    public void Calcular_FebreroBisiestoCompleto_CuentaUnMes()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2024-02-01", "2024-02-29"));

        Assert.Equal((0, 1, 0), (resultado.Total.Anios, resultado.Total.Meses, resultado.Total.Dias));
    }

    [Fact]
    public void Calcular_PeriodosPegadosEnElCorteDe1994_CadaUnoQuedaEnSuTramo()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "1994-01-01", "1994-06-30"),
            Periodo("B", "1994-07-01", "1994-12-31"));

        Assert.Equal(6, resultado.HastaJunio1994.Total.TotalMeses);
        Assert.Equal(6, resultado.DesdeJulio1994.Total.TotalMeses);
        Assert.Empty(resultado.Superposiciones);
    }

    [Fact]
    public void Calcular_HistoriaVacia_DevuelveCero()
    {
        var resultado = ComputoDeServicios.Calcular(new HistoriaLaboral(), FechaDeCalculo);

        Assert.Equal(TiempoDeServicio.Cero, resultado.Total);
        Assert.Equal(DesgloseDeTramo.Vacio, resultado.HastaJunio1994);
        Assert.Empty(resultado.Superposiciones);
    }

    [Fact]
    public void Calcular_HistoriaNula_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => ComputoDeServicios.Calcular(null!, FechaDeCalculo));
    }

    [Fact]
    public void Calcular_TipoDeServicioInvalido_LanzaError()
    {
        var invalido = new PeriodoDeServicio("A", new DateOnly(2000, 1, 1), new DateOnly(2000, 12, 31), (TipoDeServicio)99, true);

        Assert.Throws<HistoriaLaboralInvalidaException>(() => Calcular(FechaDeCalculo, invalido));
    }

    [Fact]
    public void HistoriaLaboral_ListaOriginalModificada_NoCambiaLaHistoria()
    {
        var lista = new List<PeriodoDeServicio> { Periodo("A", "2000-01-01", "2000-12-31") };
        var historia = new HistoriaLaboral(lista);

        lista.Add(Periodo("B", "2001-01-01", "2001-12-31"));

        Assert.Single(historia.Periodos);
    }
}
