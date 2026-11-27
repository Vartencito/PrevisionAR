using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Motor.Computo;

/// <summary>
/// R001 — Cómputo de servicios con aportes del régimen general.
/// Ficha: docs/normativa/R001-computo-de-servicios.md
/// </summary>
/// <remarks>
/// Cómo funciona: se marca, día por día del calendario, qué tipo de servicio con aportes lo cubre
/// (dependencia, autónomo o ambos). Así un día trabajado en dos lugares cuenta una sola vez
/// y nunca se computa más tiempo que el calendario (Ley 18.037; Decreto-Ley 9.316/46, art. 2).
/// Después se cuentan meses calendario completos y días sueltos, y se convierten con
/// <see cref="ConvencionDeComputo"/>.
/// </remarks>
public static class ComputoDeServicios
{
    /// <summary>
    /// Día en que empieza el segundo tramo: la PAP toma los meses desde julio de 1994 inclusive
    /// (Ley 24.241, art. 30 inc. b; Decreto 679/95, reglamentación del art. 30). El primer tramo lo usará la PC.
    /// </summary>
    /// <remarks>
    /// A VALIDAR (pregunta 8): para la PC, el art. 23 toma servicios hasta la vigencia del Libro I,
    /// que el Decreto 56/94 fijó el 15/07/1994. Si el previsionalista lo confirma, la PC usaría otro corte.
    /// </remarks>
    public static readonly DateOnly CorteDeTramos = new(1994, 7, 1);

    /// <summary>Calcula el tiempo de servicios con aportes hasta la fecha de cálculo (inclusive).</summary>
    /// <exception cref="HistoriaLaboralInvalidaException">Si algún período tiene fechas invertidas.</exception>
    public static ResultadoDelComputo Calcular(HistoriaLaboral historia, DateOnly fechaDeCalculo)
    {
        ArgumentNullException.ThrowIfNull(historia);
        Validar(historia);

        var (computables, noComputados) = Clasificar(historia, fechaDeCalculo);
        var superposiciones = DetectarSuperposiciones(computables);

        if (computables.Count == 0)
        {
            return new ResultadoDelComputo(
                fechaDeCalculo, TiempoDeServicio.Cero, DesgloseDeTramo.Vacio, DesgloseDeTramo.Vacio, superposiciones, noComputados);
        }

        var calendario = CalendarioDeCobertura.Armar(computables);
        var finTramo1 = CorteDeTramos.AddDays(-1);

        return new ResultadoDelComputo(
            fechaDeCalculo,
            Total: calendario.Contar(calendario.Inicio, calendario.Fin, c => c != Cobertura.Ninguna),
            HastaJunio1994: Desglosar(calendario, calendario.Inicio, Min(calendario.Fin, finTramo1)),
            DesdeJulio1994: Desglosar(calendario, Max(calendario.Inicio, CorteDeTramos), calendario.Fin),
            superposiciones,
            noComputados);
    }

    private static void Validar(HistoriaLaboral historia)
    {
        foreach (var periodo in historia.Periodos)
        {
            if (periodo.Hasta is { } hasta && hasta < periodo.Desde)
            {
                throw new HistoriaLaboralInvalidaException(periodo, "la fecha hasta es anterior a la fecha desde.");
            }
        }
    }

    private static (List<TramoComputable> Computables, List<PeriodoNoComputado> NoComputados) Clasificar(
        HistoriaLaboral historia, DateOnly fechaDeCalculo)
    {
        var computables = new List<TramoComputable>();
        var noComputados = new List<PeriodoNoComputado>();

        foreach (var periodo in historia.Periodos)
        {
            if (!periodo.ConAportes)
            {
                noComputados.Add(new PeriodoNoComputado(periodo, MotivoNoComputado.SinAportes));
                continue;
            }

            if (periodo.Desde > fechaDeCalculo)
            {
                noComputados.Add(new PeriodoNoComputado(periodo, MotivoNoComputado.PosteriorALaFechaDeCalculo));
                continue;
            }

            // Sin fecha de cese, o con cese posterior: se computa hasta la fecha de cálculo (Ley 24.241, art. 22).
            var hasta = periodo.Hasta is { } h && h < fechaDeCalculo ? h : fechaDeCalculo;
            computables.Add(new TramoComputable(periodo, periodo.Desde, hasta));
        }

        return (computables, noComputados);
    }

    private static List<Superposicion> DetectarSuperposiciones(List<TramoComputable> tramos)
    {
        var superposiciones = new List<Superposicion>();
        for (var i = 0; i < tramos.Count; i++)
        {
            for (var j = i + 1; j < tramos.Count; j++)
            {
                var desde = Max(tramos[i].Desde, tramos[j].Desde);
                var hasta = Min(tramos[i].Hasta, tramos[j].Hasta);
                if (desde <= hasta)
                {
                    superposiciones.Add(new Superposicion(
                        tramos[i].Periodo,
                        tramos[j].Periodo,
                        desde,
                        hasta,
                        hasta.DayNumber - desde.DayNumber + 1));
                }
            }
        }

        // Orden estable para el informe: por fecha de inicio del tramo común (el orden de carga no importa).
        return [.. superposiciones.OrderBy(s => s.Desde).ThenBy(s => s.Hasta)];
    }

    private static DesgloseDeTramo Desglosar(CalendarioDeCobertura calendario, DateOnly desde, DateOnly hasta) =>
        new(
            Total: calendario.Contar(desde, hasta, c => c != Cobertura.Ninguna),
            SoloDependencia: calendario.Contar(desde, hasta, c => c == Cobertura.Dependencia),
            SoloAutonomo: calendario.Contar(desde, hasta, c => c == Cobertura.Autonomo),
            Simultaneo: calendario.Contar(desde, hasta, c => c == Cobertura.Ambos));

    private static DateOnly Min(DateOnly a, DateOnly b) => a < b ? a : b;

    private static DateOnly Max(DateOnly a, DateOnly b) => a > b ? a : b;

    private sealed record TramoComputable(PeriodoDeServicio Periodo, DateOnly Desde, DateOnly Hasta);

    [Flags]
    private enum Cobertura : byte
    {
        Ninguna = 0,
        Dependencia = 1,
        Autonomo = 2,
        Ambos = Dependencia | Autonomo,
    }

    /// <summary>Qué tipo de servicio con aportes cubre cada día, entre el primer y el último día computable.</summary>
    private sealed class CalendarioDeCobertura
    {
        private readonly Cobertura[] _dias;

        private CalendarioDeCobertura(DateOnly inicio, DateOnly fin)
        {
            Inicio = inicio;
            Fin = fin;
            _dias = new Cobertura[fin.DayNumber - inicio.DayNumber + 1];
        }

        public DateOnly Inicio { get; }

        public DateOnly Fin { get; }

        public static CalendarioDeCobertura Armar(List<TramoComputable> tramos)
        {
            var calendario = new CalendarioDeCobertura(tramos.Min(t => t.Desde), tramos.Max(t => t.Hasta));
            foreach (var tramo in tramos)
            {
                var marca = tramo.Periodo.Tipo switch
                {
                    TipoDeServicio.RelacionDeDependencia => Cobertura.Dependencia,
                    TipoDeServicio.Autonomo => Cobertura.Autonomo,
                    _ => throw new HistoriaLaboralInvalidaException(tramo.Periodo, "el tipo de servicio no es válido."),
                };
                for (var dia = tramo.Desde.DayNumber; dia <= tramo.Hasta.DayNumber; dia++)
                {
                    calendario._dias[dia - calendario.Inicio.DayNumber] |= marca;
                }
            }

            return calendario;
        }

        /// <summary>
        /// Cuenta los días entre <paramref name="desde"/> y <paramref name="hasta"/> (inclusive) que cumplen la condición.
        /// Un mes calendario cubierto entero suma un mes; si está cubierto en parte, sus días suman como días sueltos.
        /// </summary>
        public TiempoDeServicio Contar(DateOnly desde, DateOnly hasta, Func<Cobertura, bool> cumple)
        {
            if (desde > hasta)
            {
                return TiempoDeServicio.Cero;
            }

            var mesesCompletos = 0;
            var diasSueltos = 0;
            var diasCalendario = 0;

            var primerDiaDelMes = new DateOnly(desde.Year, desde.Month, 1);
            while (primerDiaDelMes <= hasta)
            {
                var diasDelMes = DateTime.DaysInMonth(primerDiaDelMes.Year, primerDiaDelMes.Month);
                var ultimoDiaDelMes = primerDiaDelMes.AddDays(diasDelMes - 1);
                var tramoDesde = Max(primerDiaDelMes, desde);
                var tramoHasta = Min(ultimoDiaDelMes, hasta);

                var cubiertos = 0;
                for (var dia = tramoDesde.DayNumber; dia <= tramoHasta.DayNumber; dia++)
                {
                    if (cumple(_dias[dia - Inicio.DayNumber]))
                    {
                        cubiertos++;
                    }
                }

                diasCalendario += cubiertos;
                if (cubiertos == diasDelMes)
                {
                    mesesCompletos++;
                }
                else
                {
                    diasSueltos += cubiertos;
                }

                primerDiaDelMes = primerDiaDelMes.AddMonths(1);
            }

            return ConvencionDeComputo.Armar(mesesCompletos, diasSueltos, diasCalendario);
        }
    }
}
