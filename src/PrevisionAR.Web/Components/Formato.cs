using System.Globalization;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Web.Components;

/// <summary>Formatos de presentación comunes a las pantallas y al informe. No calcula nada.</summary>
internal static class Formato
{
    /// <summary>Fecha dd/MM/yyyy, sin depender de la configuración regional de la PC.</summary>
    public static string Fecha(DateOnly fecha) => fecha.ToString("dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);

    public static string Fecha(DateOnly? fecha, string siVacia) => fecha is { } f ? Fecha(f) : siVacia;

    public static string Sexo(Sexo sexo) => sexo switch
    {
        Motor.Modelo.Sexo.Hombre => "Hombre",
        Motor.Modelo.Sexo.Mujer => "Mujer",
        _ => throw new ArgumentOutOfRangeException(nameof(sexo), sexo, "Sexo no contemplado."),
    };

    public static string Tipo(TipoDeServicio tipo) => tipo switch
    {
        TipoDeServicio.RelacionDeDependencia => "Relación de dependencia",
        TipoDeServicio.Autonomo => "Autónomo",
        _ => throw new ArgumentOutOfRangeException(nameof(tipo), tipo, "Tipo de servicio no contemplado."),
    };
}
