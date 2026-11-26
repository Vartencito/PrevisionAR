namespace PrevisionAR.Motor.Tests.Arquitectura;

/// <summary>Ubica archivos del repositorio desde los tests (para revisar código y documentación).</summary>
internal static class Repositorio
{
    public static string Raiz { get; } = BuscarRaiz();

    public static string Ruta(params string[] partes) => Path.Combine([Raiz, .. partes]);

    /// <summary>Archivos .cs de una carpeta, sin los generados en bin/ y obj/.</summary>
    public static IEnumerable<string> ArchivosCs(params string[] carpeta)
    {
        var separador = Path.DirectorySeparatorChar;
        return Directory.EnumerateFiles(Ruta(carpeta), "*.cs", SearchOption.AllDirectories)
            .Where(ruta => !ruta.Contains($"{separador}obj{separador}", StringComparison.Ordinal)
                        && !ruta.Contains($"{separador}bin{separador}", StringComparison.Ordinal));
    }

    private static string BuscarRaiz()
    {
        var carpeta = new DirectoryInfo(AppContext.BaseDirectory);
        while (carpeta is not null && !File.Exists(Path.Combine(carpeta.FullName, "PrevisionAR.slnx")))
        {
            carpeta = carpeta.Parent;
        }

        return carpeta?.FullName ?? throw new InvalidOperationException("No se encontró PrevisionAR.slnx.");
    }
}
