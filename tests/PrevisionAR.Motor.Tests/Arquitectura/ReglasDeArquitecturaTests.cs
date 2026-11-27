using System.Reflection;

namespace PrevisionAR.Motor.Tests.Arquitectura;

/// <summary>
/// Verifica automáticamente las reglas de arquitectura de CLAUDE.md,
/// para que no se rompan sin que nadie lo note.
/// </summary>
public class ReglasDeArquitecturaTests
{
    private static readonly Assembly EnsambladoMotor = typeof(MarcadorMotor).Assembly;

    [Fact]
    public void Motor_Referencias_SoloUsaEnsambladosDelFramework()
    {
        // Regla 3: el motor no depende de otros proyectos ni de paquetes externos.
        var externas = EnsambladoMotor
            .GetReferencedAssemblies()
            .Select(r => r.Name ?? string.Empty)
            .Where(nombre => !EsDelFramework(nombre))
            .ToList();

        Assert.Empty(externas);
    }

    private static bool EsDelFramework(string nombre) =>
        nombre == "netstandard"
        || nombre == "mscorlib"
        || nombre == "System"
        || nombre.StartsWith("System.", StringComparison.Ordinal)
        || nombre.StartsWith("Microsoft.Win32.", StringComparison.Ordinal);

    [Fact]
    public void Motor_TiposResultado_IndicanLaReglaQueLosProdujo()
    {
        // Convención de trazabilidad: todo resultado público del motor dice con qué ficha se calculó.
        var sinRegla = EnsambladoMotor.GetExportedTypes()
            .Where(t => t.Name.StartsWith("Resultado", StringComparison.Ordinal))
            .Where(t => t.GetProperty("Regla", typeof(string)) is null)
            .Select(t => t.FullName)
            .ToList();

        Assert.Empty(sinRegla);
    }
}
