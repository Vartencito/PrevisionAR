using PrevisionAR.Motor.Modelo;
using PrevisionAR.Motor.Requisitos;

namespace PrevisionAR.Motor.Tests.Invariantes;

/// <summary>Reglas que tienen que cumplirse para cualquier persona e historia (R002).</summary>
public class RequisitosPbuInvariantesTests
{
    private const int Casos = 400;

    public static TheoryData<int> Semillas => [5, 99, 31415];

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Verificar_CualquierCaso_LaCompensacionRespetaLaProporcionYNoSobra(int semilla)
    {
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Casos; i++)
        {
            var (persona, historia, fecha) = Caso(generador);

            var r = RequisitosPbu.Verificar(persona, historia, fecha);

            Assert.True(r.MesesCompensados * 2 <= r.MesesDeEdadExcedente);
            Assert.True(r.MesesCompensados <= r.MesesDeServicioFaltantes);
            Assert.Equal(r.MesesDeServicioFaltantes - r.MesesCompensados, r.MesesDeServicioFaltantesTrasCompensar);
            Assert.Equal(r.CumpleEdad && r.CumpleServicios, r.TieneDerecho);
            if (!r.CumpleEdad)
            {
                Assert.Equal(0, r.MesesCompensados); // Sin edad excedente no hay compensación.
            }
        }
    }

    [Theory]
    [MemberData(nameof(Semillas))]
    public void Verificar_HistoriaTerminada_SiTieneDerechoLoSigueTeniendoMasAdelante(int semilla)
    {
        // Con una historia ya cerrada, pasar el tiempo solo suma edad: el derecho no se puede perder.
        var generador = new GeneradorDeHistorias(semilla);
        for (var i = 0; i < Casos; i++)
        {
            var (persona, historia, fecha) = Caso(generador);
            var cerrada = new HistoriaLaboral(historia.Periodos
                .Where(p => p.Hasta is { } h && h <= fecha)
                .ToList());

            var ahora = RequisitosPbu.Verificar(persona, cerrada, fecha);
            var despues = RequisitosPbu.Verificar(persona, cerrada, fecha.AddYears(3));

            if (ahora.TieneDerecho)
            {
                Assert.True(despues.TieneDerecho);
            }
        }
    }

    private static (Persona Persona, HistoriaLaboral Historia, DateOnly Fecha) Caso(GeneradorDeHistorias generador)
    {
        var fecha = generador.Fecha(new DateOnly(2000, 1, 1), new DateOnly(2030, 12, 31));
        var nacimiento = generador.Fecha(new DateOnly(1930, 1, 1), fecha);
        var sexo = generador.Sexo();
        return (new Persona(nacimiento, sexo), generador.Historia(maximoDePeriodos: 8), fecha);
    }
}
