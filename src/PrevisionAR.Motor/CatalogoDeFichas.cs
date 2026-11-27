namespace PrevisionAR.Motor;

/// <summary>Resumen de una ficha normativa para mostrar en informes: título, normas y puntos sin validar.</summary>
/// <param name="Codigo">Código de la ficha (ver <see cref="Reglas"/>).</param>
/// <param name="Titulo">Título tal como figura en el encabezado de la ficha en docs/normativa/.</param>
/// <param name="Normas">Normas que respaldan la regla, citadas en forma breve.</param>
/// <param name="PuntosAValidar">Convenciones propuestas que todavía no confirmó un previsionalista.</param>
public sealed record FichaNormativa(
    string Codigo,
    string Titulo,
    IReadOnlyList<string> Normas,
    IReadOnlyList<string> PuntosAValidar);

/// <summary>
/// Catálogo de las fichas que usa el motor. Es la única fuente de las citas que muestra el informe,
/// para que la Web no repita (y desactualice) texto normativo.
/// </summary>
/// <remarks>
/// Si se cambia una ficha en docs/normativa/, se actualiza acá en el mismo cambio.
/// Un test (CatalogoDeFichasTests) verifica que cada código tenga su ficha y que el título coincida.
/// </remarks>
public static class CatalogoDeFichas
{
    public static readonly FichaNormativa ComputoDeServicios = new(
        Reglas.ComputoDeServicios,
        "Cómputo de servicios con aportes (régimen general)",
        [
            "Ley 24.241, art. 19 inc. c) y art. 22: servicios con aportes computables",
            "Decreto 679/95, art. 2: definición de servicios con aportes",
            "Decreto 679/95, reglamentación del art. 19: 30 años = 360 meses",
            "Decreto-Ley 9.316/46, art. 2: en servicios simultáneos no se acumulan los tiempos",
            "Ley 18.037 (t.o. 1976), arts. 14 a 16, en forma supletoria: no se computa más tiempo que el calendario",
            "Ley 24.241, arts. 23, 24 y 30; Decreto 679/95: separación de tramos antes y desde el 01/07/1994",
        ],
        [
            "Cómo se convierten los días sueltos en meses (hoy: 30 días = 1 mes, sumando días de distintos meses)",
            "Si las fechas desde y hasta se cuentan las dos (hoy: sí)",
            "Corte de tramos para la PC y la PAP en julio de 1994",
        ]);

    public static readonly FichaNormativa RequisitosPbu = new(
        Reglas.RequisitosPbu,
        "Requisitos de edad y servicios para la PBU (régimen general)",
        [
            "Ley 24.241, art. 19 inc. a) y b): 65 años los hombres, 60 años las mujeres",
            "Ley 24.241, art. 19 inc. c): 30 años de servicios con aportes",
            "Ley 24.241, art. 19: compensación de 2 años de edad excedente por 1 de servicios faltante",
            "Decreto 679/95, reglamentación del art. 19: 360 meses; las fracciones de edad también compensan",
            "Código Civil y Comercial, art. 6: cómputo de plazos de fecha a fecha",
        ],
        [
            "Redondeo de la compensación (hoy: meses de edad excedente ÷ 2, sin redondear hacia arriba)",
            "Si los días sueltos ayudan a llegar a 360 meses (hoy: no)",
            "Cómo se cumple la edad un 29 de febrero (hoy: el 28/02 en años no bisiestos)",
        ]);

    /// <summary>Todas las fichas del catálogo, en orden de código.</summary>
    public static IReadOnlyList<FichaNormativa> Todas { get; } = [ComputoDeServicios, RequisitosPbu];

    /// <summary>Busca una ficha por código.</summary>
    /// <exception cref="KeyNotFoundException">Si el código no está en el catálogo.</exception>
    public static FichaNormativa Obtener(string codigo) =>
        Todas.FirstOrDefault(f => f.Codigo == codigo)
        ?? throw new KeyNotFoundException($"La ficha {codigo} no está en el catálogo.");
}
