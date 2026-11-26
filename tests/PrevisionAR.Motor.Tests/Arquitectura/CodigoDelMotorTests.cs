using System.Text.RegularExpressions;

namespace PrevisionAR.Motor.Tests.Arquitectura;

/// <summary>
/// Revisa el código fuente del motor para hacer cumplir convenciones de CLAUDE.md
/// que el compilador no controla.
/// </summary>
public class CodigoDelMotorTests
{
    public static TheoryData<string, string> Prohibidos => new()
    {
        // Regla 3 y 4: el motor no lee el reloj del sistema (la fecha de cálculo es un parámetro).
        { @"\bDateTime\.(Now|Today|UtcNow)\b", "Usar la fecha de cálculo recibida como parámetro." },
        { @"\bDateTimeOffset\.(Now|UtcNow)\b", "Usar la fecha de cálculo recibida como parámetro." },
        // Convención: importes y cálculos con decimal.
        { @"\b(double|float)\b", "Usar decimal (o int para conteos)." },
    };

    [Theory]
    [MemberData(nameof(Prohibidos))]
    public void CodigoDelMotor_NoUsaConstruccionesProhibidas(string patron, string alternativa)
    {
        var infracciones = ArchivosDelMotor()
            .SelectMany(archivo => File.ReadLines(archivo)
                .Select((linea, indice) => (archivo, linea, numero: indice + 1)))
            .Where(x => !EsComentario(x.linea) && Regex.IsMatch(x.linea, patron))
            .Select(x => $"{Path.GetFileName(x.archivo)}:{x.numero}: {x.linea.Trim()}")
            .ToList();

        Assert.True(infracciones.Count == 0, $"{alternativa}\n{string.Join('\n', infracciones)}");
    }

    private static bool EsComentario(string linea) => linea.TrimStart().StartsWith("//", StringComparison.Ordinal);

    private static IEnumerable<string> ArchivosDelMotor()
    {
        var carpeta = Path.Combine(RaizDelRepositorio(), "src", "PrevisionAR.Motor");
        return Directory.EnumerateFiles(carpeta, "*.cs", SearchOption.AllDirectories)
            .Where(ruta => !ruta.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.Ordinal)
                        && !ruta.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.Ordinal));
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
