namespace PrevisionAR.Web.Tests.Arquitectura;

/// <summary>
/// ADR 0007: la conversión del archivo de caso es pura (sin disco, JavaScript ni Blazor), para poder probarla
/// y para que la app no deje copias de casos en la PC.
/// </summary>
public class ArchivoDeCasosPuroTests
{
    [Theory]
    [InlineData("File.")]
    [InlineData("Directory.")]
    [InlineData("IJSRuntime")]
    [InlineData("Microsoft.AspNetCore")]
    public void CodigoDeCasos_PatronProhibido_NoAparece(string prohibido)
    {
        var archivos = Directory.GetFiles(Path.Combine(RaizDelRepositorio(), "src", "PrevisionAR.Web", "Casos"), "*.cs");
        Assert.NotEmpty(archivos);

        var infracciones = archivos.Where(a => File.ReadAllText(a).Contains(prohibido, StringComparison.Ordinal)).ToList();

        Assert.True(infracciones.Count == 0, $"{prohibido} en: {string.Join(", ", infracciones.Select(Path.GetFileName))}");
    }

    private static string RaizDelRepositorio()
    {
        var carpeta = new DirectoryInfo(AppContext.BaseDirectory);
        while (carpeta is not null && !File.Exists(Path.Combine(carpeta.FullName, "PrevisionAR.slnx")))
        {
            carpeta = carpeta.Parent;
        }

        return carpeta?.FullName ?? throw new InvalidOperationException("No se encontró PrevisionAR.slnx.");
    }
}
