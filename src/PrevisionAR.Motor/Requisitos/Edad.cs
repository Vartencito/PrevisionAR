namespace PrevisionAR.Motor.Requisitos;

/// <summary>Cálculo de edad cumplida.</summary>
public static class Edad
{
    /// <summary>
    /// Meses cumplidos entre la fecha de nacimiento y la fecha dada.
    /// Un mes se cumple el mismo número de día (R002, convención a validar).
    /// El día del cumpleaños ya cuenta como cumplido.
    /// </summary>
    public static int MesesCumplidos(DateOnly nacimiento, DateOnly fecha)
    {
        var meses = ((fecha.Year - nacimiento.Year) * 12) + (fecha.Month - nacimiento.Month);
        if (fecha.Day < nacimiento.Day)
        {
            meses--;
        }

        return meses;
    }
}
