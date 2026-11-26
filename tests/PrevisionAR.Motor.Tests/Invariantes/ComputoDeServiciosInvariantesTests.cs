using PrevisionAR.Motor.Computo;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Tests.Invariantes;

/// <summary>
/// Reglas que tienen que cumplirse para CUALQUIER historia laboral (R001), probadas con cientos de historias generadas.
/// Complementan los casos de la ficha: los casos prueban ejemplos; estas pruebas buscan casos raros que nadie pensó.
/// </summary>
public class ComputoDeServiciosInvariantesTests
{
    private const int Historias = 400;

    public static TheoryData<int> Semillas => [11, 2024, 777];

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_CualquierHistoria_CuentaCadaDiaConAportesUnaSolaVez(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var historia = generador.Historia();
            var fecha = generador.Fecha(new DateOnly(1975, 1, 1), new DateOnly(2030, 12, 31));

            var resultado = ComputoDeServicios.Calcular(historia, fecha);

            Assert.Equal(DiasCubiertosConAportes(historia, fecha).Count, resultado.Total.DiasCalendario);
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_CualquierHistoria_LosDesglosesSumanElTotalEnDias(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var resultado = ComputoDeServicios.Calcular(generador.Historia(), generador.Fecha(new DateOnly(1975, 1, 1), new DateOnly(2030, 12, 31)));

            Assert.Equal(
                resultado.Total.DiasCalendario,
                resultado.HastaJunio1994.Total.DiasCalendario + resultado.DesdeJulio1994.Total.DiasCalendario);
            foreach (var tramo in new[] { resultado.HastaJunio1994, resultado.DesdeJulio1994 })
            {
                Assert.Equal(
                    tramo.Total.DiasCalendario,
                    tramo.SoloDependencia.DiasCalendario + tramo.SoloAutonomo.DiasCalendario + tramo.Simultaneo.DiasCalendario);
            }
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_CualquierHistoria_NuncaSuperaElTiempoCalendario(int semilla)
    {
        // Ley 18.037: no más tiempo que el calendario ni más de 12 meses por año calendario.
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var historia = generador.Historia();
            var fecha = generador.Fecha(new DateOnly(1975, 1, 1), new DateOnly(2030, 12, 31));

            var resultado = ComputoDeServicios.Calcular(historia, fecha);

            var dias = DiasCubiertosConAportes(historia, fecha);
            if (dias.Count == 0)
            {
                Assert.Equal(TiempoDeServicio.Cero, resultado.Total);
                continue;
            }

            var primero = DateOnly.FromDayNumber(dias.Min());
            var ultimo = DateOnly.FromDayNumber(dias.Max());
            var mesesCalendario = ((ultimo.Year - primero.Year) * 12) + (ultimo.Month - primero.Month) + 1;
            Assert.True(resultado.Total.TotalMeses <= mesesCalendario);
            Assert.True(resultado.Total.DiasCalendario <= ultimo.DayNumber - primero.DayNumber + 1);
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_OrdenDeLosPeriodos_NoCambiaElResultado(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var historia = generador.Historia();
            var mezclada = new HistoriaLaboral(generador.Mezclar(historia.Periodos));
            var fecha = new DateOnly(2026, 1, 1);

            var original = ComputoDeServicios.Calcular(historia, fecha);
            var otraVez = ComputoDeServicios.Calcular(mezclada, fecha);

            Assert.Equal(original.Total, otraVez.Total);
            Assert.Equal(original.HastaJunio1994, otraVez.HastaJunio1994);
            Assert.Equal(original.DesdeJulio1994, otraVez.DesdeJulio1994);
            Assert.Equal(original.Superposiciones.Count, otraVez.Superposiciones.Count);
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_AgregarPeriodoSinAportes_NoCambiaElTotal(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var historia = generador.Historia();
            var sinAportes = generador.Periodo("Extra") with { ConAportes = false };
            var ampliada = new HistoriaLaboral([.. historia.Periodos, sinAportes]);
            var fecha = new DateOnly(2026, 1, 1);

            Assert.Equal(
                ComputoDeServicios.Calcular(historia, fecha).Total,
                ComputoDeServicios.Calcular(ampliada, fecha).Total);
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Calcular_AgregarPeriodoConAportes_NuncaDisminuyeElTotal(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Historias; i++)
        {
            var historia = generador.Historia();
            var extra = generador.Periodo("Extra") with { ConAportes = true };
            var ampliada = new HistoriaLaboral([.. historia.Periodos, extra]);
            var fecha = new DateOnly(2026, 1, 1);

            Assert.True(
                ComputoDeServicios.Calcular(ampliada, fecha).Total.DiasCalendario >=
                ComputoDeServicios.Calcular(historia, fecha).Total.DiasCalendario);
        }
    }

    /// <summary>Cálculo "a lo bruto", independiente del motor: el conjunto de días con aportes hasta la fecha.</summary>
    private static HashSet<int> DiasCubiertosConAportes(HistoriaLaboral historia, DateOnly fecha)
    {
        var dias = new HashSet<int>();
        foreach (var periodo in historia.Periodos.Where(p => p.ConAportes && p.Desde <= fecha))
        {
            var hasta = periodo.Hasta is { } h && h < fecha ? h : fecha;
            for (var dia = periodo.Desde.DayNumber; dia <= hasta.DayNumber; dia++)
            {
                dias.Add(dia);
            }
        }

        return dias;
    }
}
