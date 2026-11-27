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
        // Regla 3: sin disco, red, azar ni entorno (todo entra como parámetro).
        { @"\b(File|Directory|Path)\.", "El motor no accede a disco: los datos entran como parámetros." },
        { @"\b(HttpClient|WebClient|Socket)\b", "El motor no accede a la red." },
        { @"\b(Random|Guid\.NewGuid|Environment\.|TimeProvider|Stopwatch)\b", "El motor es determinístico: sin azar, entorno ni reloj." },
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

    private static IEnumerable<string> ArchivosDelMotor() => Repositorio.ArchivosCs("src", "PrevisionAR.Motor");
}
