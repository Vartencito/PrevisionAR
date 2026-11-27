using PrevisionAR.Motor.Modelo;
using PrevisionAR.Motor.Requisitos;

namespace PrevisionAR.Motor.Tests.Requisitos;

/// <summary>
/// Tests de la ficha R002 (docs/normativa/R002-requisitos-pbu.md).
/// Los casos se numeran igual que en la ficha. Datos ficticios.
/// </summary>
public class RequisitosPbuTests
{
    private static readonly DateOnly FechaDeCalculo = new(2026, 1, 1);

    private static ResultadoRequisitosPbu Verificar(Sexo sexo, string nacimiento, string desde, string hasta) =>
        RequisitosPbu.Verificar(
            new Persona(DateOnly.Parse(nacimiento), sexo),
            new HistoriaLaboral(new PeriodoDeServicio(
                "Empresa (ficticia)", DateOnly.Parse(desde), DateOnly.Parse(hasta), TipoDeServicio.RelacionDeDependencia, ConAportes: true)),
            FechaDeCalculo);

    // Caso 1 — Criterio 1.
    [Fact]
    public void Verificar_HombreConEdadYServicios_TieneDerechoSinCompensar()
    {
        var resultado = Verificar(Sexo.Hombre, "1960-03-15", "1990-01-01", "2019-12-31");

        Assert.True(resultado.TieneDerecho);
        Assert.Equal(65 * 12 + 9, resultado.EdadCumplidaMeses);
        Assert.Equal(360, resultado.ServiciosAcreditadosMeses);
        Assert.Equal(0, resultado.MesesCompensados);
    }

    // Caso 2 — Criterio 2.
    [Fact]
    public void Verificar_MujerConEdadYServicios_TieneDerecho()
    {
        var resultado = Verificar(Sexo.Mujer, "1965-05-10", "1990-01-01", "2019-12-31");

        Assert.True(resultado.TieneDerecho);
        Assert.Equal(60 * 12 + 7, resultado.EdadCumplidaMeses);
        Assert.Equal(60 * 12, resultado.EdadRequeridaMeses);
    }

    // Caso 3 — Criterios 3 y 7: falta edad; los servicios de más no compensan.
    [Fact]
    public void Verificar_FaltaEdadConServiciosDeMas_NoTieneDerecho()
    {
        var resultado = Verificar(Sexo.Hombre, "1961-06-01", "1985-01-01", "2019-12-31");

        Assert.False(resultado.TieneDerecho);
        Assert.False(resultado.CumpleEdad);
        Assert.Equal(5, resultado.MesesDeEdadFaltantes);
        Assert.Equal(420, resultado.ServiciosAcreditadosMeses);
    }

    // Caso 4 — Criterio 4.
    [Fact]
    public void Verificar_FechaDeCalculoEsElCumpleanios_CumpleLaEdad()
    {
        var resultado = Verificar(Sexo.Hombre, "1961-01-01", "1990-01-01", "2019-12-31");

        Assert.True(resultado.CumpleEdad);
        Assert.True(resultado.TieneDerecho);
    }

    // Caso 5 — Criterio 5.
    [Fact]
    public void Verificar_CompensacionSuficiente_TieneDerechoEInformaMesesCompensados()
    {
        var resultado = Verificar(Sexo.Hombre, "1955-01-01", "1980-01-01", "2006-12-31");

        Assert.True(resultado.TieneDerecho);
        Assert.Equal(72, resultado.MesesDeEdadExcedente);
        Assert.Equal(36, resultado.MesesDeServicioFaltantes);
        Assert.Equal(36, resultado.MesesCompensados);
        Assert.Equal(0, resultado.MesesDeServicioFaltantesTrasCompensar);
    }

    // Caso 6 — Criterio 6.
    [Fact]
    public void Verificar_CompensacionInsuficiente_NoTieneDerechoEInformaLoQueFalta()
    {
        var resultado = Verificar(Sexo.Hombre, "1958-01-01", "1980-01-01", "2006-12-31");

        Assert.False(resultado.TieneDerecho);
        Assert.Equal(18, resultado.MesesCompensados);
        Assert.Equal(18, resultado.MesesDeServicioFaltantesTrasCompensar);
    }

    // Caso 7 — Convención de redondeo (A VALIDAR): 65 meses de exceso compensan 32.
    [Fact]
    public void Verificar_ExcedenteImpar_CompensaLaParteEntera()
    {
        var resultado = Verificar(Sexo.Mujer, "1960-08-01", "1990-01-01", "2016-12-31");

        Assert.False(resultado.TieneDerecho);
        Assert.Equal(65, resultado.MesesDeEdadExcedente);
        Assert.Equal(32, resultado.MesesCompensados);
        Assert.Equal(4, resultado.MesesDeServicioFaltantesTrasCompensar);
    }

    // Caso 8 — Criterio 8.
    [Fact]
    public void Verificar_NacimientoPosteriorALaFechaDeCalculo_LanzaError()
    {
        Assert.Throws<DatosPersonalesInvalidosException>(
            () => Verificar(Sexo.Hombre, "2026-02-01", "1990-01-01", "2019-12-31"));
    }

    // Criterio 9 — Trazabilidad.
    [Fact]
    public void Verificar_CualquierCaso_IndicaR002YUsaR001()
    {
        var resultado = Verificar(Sexo.Hombre, "1960-03-15", "1990-01-01", "2019-12-31");

        Assert.Equal("R002", resultado.Regla);
        Assert.Equal("R001", resultado.ComputoDeServicios.Regla);
        Assert.NotEmpty(resultado.Explicacion);
    }

    // Caso 9 — Convención (A VALIDAR): los días que no forman un mes no suman para los 360 meses.
    [Fact]
    public void Verificar_359MesesY20Dias_NoAlcanzanLos360()
    {
        var historia = new HistoriaLaboral(
            new PeriodoDeServicio("A", new DateOnly(1990, 1, 1), new DateOnly(2019, 11, 30), TipoDeServicio.RelacionDeDependencia, true),
            new PeriodoDeServicio("B", new DateOnly(2019, 12, 1), new DateOnly(2019, 12, 20), TipoDeServicio.RelacionDeDependencia, true));

        // Cumple 65 justo el día de cálculo: sin edad excedente para compensar.
        var resultado = RequisitosPbu.Verificar(new Persona(new DateOnly(1961, 1, 1), Sexo.Hombre), historia, FechaDeCalculo);

        Assert.Equal(359, resultado.ServiciosAcreditadosMeses);
        Assert.Equal(20, resultado.ComputoDeServicios.Total.Dias);
        Assert.False(resultado.TieneDerecho);
    }

    [Fact]
    public void Verificar_SexoInvalido_LanzaError()
    {
        Assert.Throws<DatosPersonalesInvalidosException>(() => RequisitosPbu.Verificar(
            new Persona(new DateOnly(1960, 1, 1), (Sexo)99), new HistoriaLaboral(), FechaDeCalculo));
    }

    [Fact]
    public void Verificar_PersonaNula_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => RequisitosPbu.Verificar(null!, new HistoriaLaboral(), FechaDeCalculo));
    }
}
