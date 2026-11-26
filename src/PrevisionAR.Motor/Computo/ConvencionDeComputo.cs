namespace PrevisionAR.Motor.Computo;

/// <summary>
/// Cómo se pasan meses y días sueltos a años, meses y días.
/// </summary>
/// <remarks>
/// PENDIENTE DE VALIDAR (R001, docs/conocimiento/preguntas-previsionalista.md, pregunta 1):
/// no hay norma nacional explícita sobre cuántos días sueltos forman un mes.
/// Se usa 30 días = 1 mes como propuesta. Si el previsionalista indica otra cosa,
/// se cambia solo esta clase y sus tests.
/// </remarks>
public static class ConvencionDeComputo
{
    /// <summary>Días sueltos que equivalen a un mes.</summary>
    public const int DiasPorMes = 30;

    public const int MesesPorAnio = 12;

    /// <summary>Arma el tiempo a partir de meses calendario completos y días sueltos.</summary>
    /// <param name="mesesCompletos">Meses calendario cubiertos enteros.</param>
    /// <param name="diasSueltos">Días de meses cubiertos en forma parcial.</param>
    /// <param name="diasCalendario">Total exacto de días computados.</param>
    public static TiempoDeServicio Armar(int mesesCompletos, int diasSueltos, int diasCalendario)
    {
        var meses = mesesCompletos + (diasSueltos / DiasPorMes);
        var dias = diasSueltos % DiasPorMes;
        return new TiempoDeServicio(meses / MesesPorAnio, meses % MesesPorAnio, dias, diasCalendario);
    }
}
