using System.Globalization;
using PrevisionAR.Motor.Computo;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Tests.Computo;

/// <summary>
/// Tests de la ficha R001 (docs/normativa/R001-computo-de-servicios.md).
/// Los casos se numeran igual que en la ficha. Datos ficticios.
/// </summary>
public class ComputoDeServiciosTests
{
    private static readonly DateOnly FechaDeCalculo = new(2026, 1, 1);

    private static PeriodoDeServicio Periodo(
        string empleador,
        string desde,
        string? hasta,
        bool conAportes = true,
        TipoDeServicio tipo = TipoDeServicio.RelacionDeDependencia) =>
        new(empleador, DateOnly.Parse(desde, CultureInfo.InvariantCulture), hasta is null ? null : DateOnly.Parse(hasta, CultureInfo.InvariantCulture), tipo, conAportes);

    private static ResultadoDelComputo Calcular(DateOnly fecha, params PeriodoDeServicio[] periodos) =>
        ComputoDeServicios.Calcular(new HistoriaLaboral(periodos), fecha);

    private static void AssertTiempo(TiempoDeServicio tiempo, int anios, int meses, int dias)
    {
        Assert.Equal((anios, meses, dias), (tiempo.Anios, tiempo.Meses, tiempo.Dias));
    }

    // Caso 1 — Criterio 1: período único.
    [Fact]
    public void Calcular_PeriodoUnico_DevuelveDiezAnios()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2000-01-01", "2009-12-31"));

        AssertTiempo(resultado.Total, 10, 0, 0);
        Assert.Equal(120, resultado.Total.TotalMeses);
    }

    // Caso 2 — Criterio 2: períodos sucesivos.
    [Fact]
    public void Calcular_PeriodosSucesivos_SumaAmbos()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "2000-01-01", "2004-12-31"),
            Periodo("B", "2005-01-01", "2009-12-31"));

        AssertTiempo(resultado.Total, 10, 0, 0);
        Assert.Equal(120, resultado.Total.TotalMeses);
        Assert.Empty(resultado.Superposiciones);
    }

    // Caso 3 — Criterio 3: período sin aportes.
    [Fact]
    public void Calcular_PeriodoSinAportes_NoSumaYSeInforma()
    {
        var sinAportes = Periodo("B", "2005-01-01", "2009-12-31", conAportes: false);

        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2000-01-01", "2004-12-31"), sinAportes);

        AssertTiempo(resultado.Total, 5, 0, 0);
        Assert.Equal(60, resultado.Total.TotalMeses);
        var noComputado = Assert.Single(resultado.NoComputados);
        Assert.Equal(sinAportes, noComputado.Periodo);
        Assert.Equal(MotivoNoComputado.SinAportes, noComputado.Motivo);
    }

    // Caso 4 — Criterio 4: superposición total.
    [Fact]
    public void Calcular_SuperposicionTotal_CuentaUnaVezEInforma()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "2000-01-01", "2009-12-31"),
            Periodo("B", "2003-01-01", "2004-12-31"));

        AssertTiempo(resultado.Total, 10, 0, 0);
        var superposicion = Assert.Single(resultado.Superposiciones);
        Assert.Equal(("A", "B"), (superposicion.PeriodoA.Empleador, superposicion.PeriodoB.Empleador));
        Assert.Equal(new DateOnly(2003, 1, 1), superposicion.Desde);
        Assert.Equal(new DateOnly(2004, 12, 31), superposicion.Hasta);
        Assert.Equal(731, superposicion.DiasCalendario); // 2003 (365) + 2004 bisiesto (366)
    }

    // Caso 5 — Criterio 5: superposición parcial.
    [Fact]
    public void Calcular_SuperposicionParcial_CuentaTramoComunUnaVez()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "2000-01-01", "2005-12-31"),
            Periodo("B", "2004-01-01", "2009-12-31"));

        AssertTiempo(resultado.Total, 10, 0, 0);
        Assert.Equal(120, resultado.Total.TotalMeses);
        var superposicion = Assert.Single(resultado.Superposiciones);
        Assert.Equal(new DateOnly(2004, 1, 1), superposicion.Desde);
        Assert.Equal(new DateOnly(2005, 12, 31), superposicion.Hasta);
    }

    // Caso 6 — Criterio 6: trabajo actual (sin fecha de fin).
    [Fact]
    public void Calcular_PeriodoSinFechaDeFin_ComputaHastaFechaDeCalculo()
    {
        var resultado = Calcular(new DateOnly(2025, 12, 31), Periodo("A", "2016-01-01", null));

        AssertTiempo(resultado.Total, 10, 0, 0);
        Assert.Equal(120, resultado.Total.TotalMeses);
    }

    // Caso 7 — Criterio 7: fecha de cálculo en medio de un período.
    [Fact]
    public void Calcular_PeriodoQueTerminaDespuesDeLaFechaDeCalculo_ComputaHastaLaFecha()
    {
        var resultado = Calcular(new DateOnly(2025, 12, 31), Periodo("A", "2016-01-01", "2030-12-31"));

        AssertTiempo(resultado.Total, 10, 0, 0);
        Assert.Equal(120, resultado.Total.TotalMeses);
    }

    // Caso 8 — Criterio 8: fechas inválidas.
    [Fact]
    public void Calcular_FechasInvertidas_LanzaErrorQueIdentificaElPeriodo()
    {
        var invalido = Periodo("A", "2010-01-01", "2009-12-31");

        var error = Assert.Throws<HistoriaLaboralInvalidaException>(() => Calcular(FechaDeCalculo, invalido));

        Assert.Equal(invalido, error.Periodo);
        Assert.Contains("\"A\"", error.Message, StringComparison.Ordinal);
    }

    // Caso 9 — Convención de días sueltos (A VALIDAR con previsionalista).
    [Fact]
    public void Calcular_MesIncompleto_DevuelveDiasSueltos()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2000-03-01", "2000-03-15"));

        AssertTiempo(resultado.Total, 0, 0, 15);
        Assert.Equal(0, resultado.Total.TotalMeses);
        Assert.Equal(15, resultado.Total.DiasCalendario);
    }

    // Caso 10 — Criterio 11: desglose por tramo (período que cruza el 01/07/1994).
    [Fact]
    public void Calcular_PeriodoQueCruzaJulio1994_SeParteEnDosTramos()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "1990-01-01", "1999-12-31"));

        Assert.Equal(120, resultado.Total.TotalMeses);
        Assert.Equal(54, resultado.HastaJunio1994.Total.TotalMeses);
        Assert.Equal(54, resultado.HastaJunio1994.SoloDependencia.TotalMeses);
        Assert.Equal(66, resultado.DesdeJulio1994.Total.TotalMeses);
        Assert.Equal(66, resultado.DesdeJulio1994.SoloDependencia.TotalMeses);
        Assert.Equal(
            resultado.Total.DiasCalendario,
            resultado.HastaJunio1994.Total.DiasCalendario + resultado.DesdeJulio1994.Total.DiasCalendario);
    }

    // Caso 11 — Criterio 12: desglose por tipo (n, m, p).
    [Fact]
    public void Calcular_DependenciaYAutonomoSuperpuestos_DesglosaEnNmp()
    {
        var resultado = Calcular(
            FechaDeCalculo,
            Periodo("A", "2000-01-01", "2009-12-31"),
            Periodo("B", "2005-01-01", "2014-12-31", tipo: TipoDeServicio.Autonomo));

        Assert.Equal(180, resultado.Total.TotalMeses);
        Assert.Equal(0, resultado.HastaJunio1994.Total.TotalMeses);
        var tramo = resultado.DesdeJulio1994;
        Assert.Equal(60, tramo.SoloDependencia.TotalMeses); // n
        Assert.Equal(60, tramo.SoloAutonomo.TotalMeses);    // m
        Assert.Equal(60, tramo.Simultaneo.TotalMeses);      // p
        Assert.Equal(
            tramo.Total.DiasCalendario,
            tramo.SoloDependencia.DiasCalendario + tramo.SoloAutonomo.DiasCalendario + tramo.Simultaneo.DiasCalendario);
    }

    // Criterio 9: trazabilidad.
    [Fact]
    public void Calcular_CualquierHistoria_IndicaReglaR001()
    {
        var resultado = Calcular(FechaDeCalculo, Periodo("A", "2000-01-01", "2009-12-31"));

        Assert.Equal("R001", resultado.Regla);
    }

    // Criterio 10: determinismo.
    [Fact]
    public void Calcular_MismosDatos_MismoResultado()
    {
        var periodos = new[]
        {
            Periodo("A", "1990-03-10", "2001-08-20"),
            Periodo("B", "1999-01-15", null, tipo: TipoDeServicio.Autonomo),
            Periodo("C", "2003-05-01", "2004-02-29", conAportes: false),
        };

        var primero = Calcular(FechaDeCalculo, periodos);
        var segundo = Calcular(FechaDeCalculo, periodos);

        Assert.Equal(primero.Total, segundo.Total);
        Assert.Equal(primero.HastaJunio1994, segundo.HastaJunio1994);
        Assert.Equal(primero.DesdeJulio1994, segundo.DesdeJulio1994);
        Assert.Equal(primero.Superposiciones, segundo.Superposiciones);
        Assert.Equal(primero.NoComputados, segundo.NoComputados);
    }

    // Validación de la ficha: período que empieza después de la fecha de cálculo.
    [Fact]
    public void Calcular_PeriodoPosteriorALaFechaDeCalculo_NoComputaYSeInforma()
    {
        var futuro = Periodo("A", "2027-01-01", "2027-12-31");

        var resultado = Calcular(FechaDeCalculo, futuro);

        Assert.Equal(TiempoDeServicio.Cero, resultado.Total);
        var noComputado = Assert.Single(resultado.NoComputados);
        Assert.Equal(MotivoNoComputado.PosteriorALaFechaDeCalculo, noComputado.Motivo);
    }
}
