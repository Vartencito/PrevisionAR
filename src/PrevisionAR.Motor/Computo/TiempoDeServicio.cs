namespace PrevisionAR.Motor.Computo;

/// <summary>
/// Tiempo de servicios expresado en años, meses y días, más los días calendario exactos.
/// Se construye siempre con <see cref="ConvencionDeComputo"/>.
/// </summary>
/// <param name="Anios">Años completos.</param>
/// <param name="Meses">Meses restantes (0 a 11).</param>
/// <param name="Dias">Días restantes que no llegan a formar un mes.</param>
/// <param name="DiasCalendario">Cantidad exacta de días del calendario computados. Es el dato sin convenciones.</param>
public sealed record TiempoDeServicio(int Anios, int Meses, int Dias, int DiasCalendario)
{
    public static readonly TiempoDeServicio Cero = new(0, 0, 0, 0);

    /// <summary>Total en meses completos (la unidad que usa el Decreto 679/95: 30 años = 360 meses).</summary>
    public int TotalMeses => (Anios * 12) + Meses;

    public override string ToString() => $"{Anios}a {Meses}m {Dias}d";
}
