using PrevisionAR.Motor.Computo;

namespace PrevisionAR.Motor.Requisitos;

/// <summary>Resultado de la verificación de requisitos de la PBU (R002). Todos los tiempos en meses.</summary>
/// <param name="FechaDeCalculo">Fecha a la que se verificó.</param>
/// <param name="EdadCumplidaMeses">Edad cumplida a la fecha de cálculo.</param>
/// <param name="EdadRequeridaMeses">Edad requerida según el sexo (Ley 24.241, art. 19 inc. a y b).</param>
/// <param name="ServiciosAcreditadosMeses">Meses completos de servicios con aportes (R001).</param>
/// <param name="ServiciosRequeridosMeses">360 meses (Ley 24.241, art. 19 inc. c; Decreto 679/95).</param>
/// <param name="MesesDeEdadExcedente">Meses de edad por encima de la requerida (0 si no la cumple).</param>
/// <param name="MesesCompensados">Meses de servicios cubiertos con edad excedente (2 a 1).</param>
/// <param name="MesesDeServicioFaltantesTrasCompensar">Lo que sigue faltando después de compensar.</param>
/// <param name="ComputoDeServicios">Cómputo de servicios usado (R001), para auditar.</param>
/// <param name="Explicacion">Pasos del razonamiento, en lenguaje claro y con la norma de cada uno.</param>
public sealed record ResultadoRequisitosPbu(
    DateOnly FechaDeCalculo,
    int EdadCumplidaMeses,
    int EdadRequeridaMeses,
    int ServiciosAcreditadosMeses,
    int ServiciosRequeridosMeses,
    int MesesDeEdadExcedente,
    int MesesCompensados,
    int MesesDeServicioFaltantesTrasCompensar,
    ResultadoDelComputo ComputoDeServicios,
    IReadOnlyList<string> Explicacion)
{
    public bool CumpleEdad => EdadCumplidaMeses >= EdadRequeridaMeses;

    /// <summary>Meses de edad que faltan para la requerida (0 si ya la cumple).</summary>
    public int MesesDeEdadFaltantes => Math.Max(0, EdadRequeridaMeses - EdadCumplidaMeses);

    /// <summary>Meses de servicios que faltan antes de compensar.</summary>
    public int MesesDeServicioFaltantes => Math.Max(0, ServiciosRequeridosMeses - ServiciosAcreditadosMeses);

    public bool CumpleServicios => MesesDeServicioFaltantesTrasCompensar == 0;

    /// <summary>Cumple edad y servicios (con o sin compensación).</summary>
    public bool TieneDerecho => CumpleEdad && CumpleServicios;

    /// <summary>Ficha normativa con la que se verificó.</summary>
    public string Regla => Reglas.RequisitosPbu;
}
