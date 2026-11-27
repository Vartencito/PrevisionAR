using System.Text.Json;
using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Web.Casos;

/// <summary>Resultado de abrir un archivo: el caso, o un mensaje para el abogado (nunca las dos cosas).</summary>
public sealed record AperturaDeCaso
{
    private AperturaDeCaso(CasoEnEdicion? caso, string? error)
    {
        Caso = caso;
        Error = error;
    }

    public CasoEnEdicion? Caso { get; }

    public string? Error { get; }

    public static AperturaDeCaso Exitosa(CasoEnEdicion caso) => new(caso ?? throw new ArgumentNullException(nameof(caso)), null);

    public static AperturaDeCaso Fallida(string error) => new(null, error);
}

/// <summary>
/// Convierte un caso en edición a archivo JSON y viceversa (ADR 0007). Es pura: no toca disco, Blazor ni JavaScript;
/// la descarga y la subida las hace la pantalla.
/// </summary>
public static class ArchivoDeCasos
{
    public const string Formato = "PrevisionAR.Caso";

    public const int VersionActual = 1;

    /// <summary>Tamaño máximo que se acepta al abrir (1 MB): un caso real ocupa pocos KB.</summary>
    public const long TamanioMaximoEnBytes = 1024 * 1024;

    public const string MensajeArchivoInvalido = "El archivo no es un caso de Previsión AR o está dañado.";

    public const string MensajeVersionPosterior =
        "Este caso se guardó con una versión más nueva de Previsión AR. Actualice el programa para abrirlo.";

    private static readonly JsonSerializerOptions Opciones = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    /// <summary>Nombre sugerido para la descarga. No incluye la referencia ni datos del cliente.</summary>
    public static string NombreSugerido(DateOnly hoy) => $"caso-{hoy:yyyy-MM-dd}.previsionar.json";

    /// <summary>Arma el contenido del archivo. Guarda el caso tal cual, aunque esté incompleto o tenga errores.</summary>
    public static string Guardar(CasoEnEdicion caso, string versionDeLaApp)
    {
        ArgumentNullException.ThrowIfNull(caso);
        var archivo = new ArchivoDeCaso(
            Formato: Formato,
            VersionDelFormato: VersionActual,
            GuardadoConLaVersion: versionDeLaApp,
            Referencia: caso.Referencia,
            Persona: new PersonaEnArchivo(caso.FechaDeNacimiento, SexoATexto(caso.Sexo)),
            FechaDeCalculo: caso.FechaDeCalculo,
            Periodos: [.. caso.Filas.Select(f => new PeriodoEnArchivo(f.Empleador, f.Desde, f.Hasta, TipoATexto(f.Tipo), OrigenATexto(f.ConAportes)))]);
        return JsonSerializer.Serialize(archivo, Opciones);
    }

    /// <summary>Lee un archivo. Nunca lanza por el contenido: si algo no cierra, devuelve el motivo y ningún dato.</summary>
    public static AperturaDeCaso Abrir(string contenido)
    {
        if (string.IsNullOrWhiteSpace(contenido))
        {
            return AperturaDeCaso.Fallida(MensajeArchivoInvalido);
        }

        ArchivoDeCaso? archivo;
        try
        {
            archivo = JsonSerializer.Deserialize<ArchivoDeCaso>(contenido, Opciones);
        }
        catch (JsonException ex)
        {
            var donde = string.IsNullOrEmpty(ex.Path) ? string.Empty : $" (problema en {ex.Path})";
            return AperturaDeCaso.Fallida(MensajeArchivoInvalido + donde);
        }

        if (archivo is null || archivo.Formato != Formato || archivo.VersionDelFormato < 1)
        {
            return AperturaDeCaso.Fallida(MensajeArchivoInvalido);
        }

        if (archivo.VersionDelFormato > VersionActual)
        {
            return AperturaDeCaso.Fallida(MensajeVersionPosterior);
        }

        return ConvertirVersion1(archivo);
    }

    private static AperturaDeCaso ConvertirVersion1(ArchivoDeCaso archivo)
    {
        if (archivo.Persona?.FechaDeNacimiento is not { } nacimiento)
        {
            return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Falta la fecha de nacimiento.");
        }

        if (TextoASexo(archivo.Persona.Sexo) is not { } sexo)
        {
            return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} El sexo \"{Recortar(archivo.Persona.Sexo)}\" no es válido.");
        }

        if (archivo.FechaDeCalculo is not { } fechaDeCalculo || archivo.Periodos is null)
        {
            return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Falta la fecha de cálculo o la historia laboral.");
        }

        var caso = new CasoEnEdicion
        {
            Referencia = archivo.Referencia ?? string.Empty,
            FechaDeNacimiento = nacimiento,
            Sexo = sexo,
            FechaDeCalculo = fechaDeCalculo,
        };

        for (var i = 0; i < archivo.Periodos.Count; i++)
        {
            var p = archivo.Periodos[i];
            var fila = i + 1;
            if (p is null)
            {
                return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Período {fila}: está vacío.");
            }

            if (p.Desde is not { } desde)
            {
                return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Período {fila}: falta la fecha desde.");
            }

            if (TextoATipo(p.Tipo) is not { } tipo)
            {
                return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Período {fila}: el tipo \"{Recortar(p.Tipo)}\" no es válido.");
            }

            if (TextoAConAportes(p.Origen) is not { } conAportes)
            {
                return AperturaDeCaso.Fallida($"{MensajeArchivoInvalido} Período {fila}: el origen \"{Recortar(p.Origen)}\" no es válido.");
            }

            caso.Filas.Add(new FilaPeriodo { Empleador = p.Empleador ?? string.Empty, Desde = desde, Hasta = p.Hasta, Tipo = tipo, ConAportes = conAportes });
        }

        return AperturaDeCaso.Exitosa(caso);
    }

    // Recorta un valor leído del archivo antes de mostrarlo en un mensaje (el archivo puede traer cualquier cosa).
    private static string Recortar(string? valor) =>
        valor is null ? "(vacío)" : valor.Length <= 40 ? valor : string.Concat(valor.AsSpan(0, 40), "…");

    private static string SexoATexto(Sexo sexo) => sexo switch
    {
        Sexo.Hombre => "hombre",
        Sexo.Mujer => "mujer",
        _ => throw new ArgumentOutOfRangeException(nameof(sexo), sexo, "Sexo no contemplado en el archivo de caso."),
    };

    private static Sexo? TextoASexo(string? texto) => texto switch
    {
        "hombre" => Sexo.Hombre,
        "mujer" => Sexo.Mujer,
        _ => null,
    };

    private static string TipoATexto(TipoDeServicio tipo) => tipo switch
    {
        TipoDeServicio.RelacionDeDependencia => "relacionDeDependencia",
        TipoDeServicio.Autonomo => "autonomo",
        _ => throw new ArgumentOutOfRangeException(nameof(tipo), tipo, "Tipo no contemplado en el archivo de caso."),
    };

    private static TipoDeServicio? TextoATipo(string? texto) => texto switch
    {
        "relacionDeDependencia" => TipoDeServicio.RelacionDeDependencia,
        "autonomo" => TipoDeServicio.Autonomo,
        _ => null,
    };

    private static string OrigenATexto(bool conAportes) => conAportes ? "conAportes" : "sinAportes";

    private static bool? TextoAConAportes(string? texto) => texto switch
    {
        "conAportes" => true,
        "sinAportes" => false,
        _ => null,
    };
}
