namespace PrevisionAR.Motor.Requisitos;

/// <summary>Cálculo de edad cumplida.</summary>
public static class Edad
{
    /// <summary>
    /// Meses cumplidos entre la fecha de nacimiento y la fecha dada, contados "de fecha a fecha".
    /// Si el mes de la fecha no tiene el día del nacimiento (por ejemplo, un 29/02 en año no bisiesto),
    /// se cumple el último día de ese mes (Código Civil y Comercial, art. 6).
    /// El día del cumpleaños ya cuenta como cumplido (R002, convención a validar: pregunta 15).
    /// </summary>
    public static int MesesCumplidos(DateOnly nacimiento, DateOnly fecha)
    {
        var meses = ((fecha.Year - nacimiento.Year) * 12) + (fecha.Month - nacimiento.Month);
        var diaEquivalente = Math.Min(nacimiento.Day, DateTime.DaysInMonth(fecha.Year, fecha.Month));
        if (fecha.Day < diaEquivalente)
        {
            meses--;
        }

        return meses;
    }
}
