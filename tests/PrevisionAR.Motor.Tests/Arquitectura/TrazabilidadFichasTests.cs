using System.Text.RegularExpressions;

namespace PrevisionAR.Motor.Tests.Arquitectura;

/// <summary>
/// Hace cumplir la convención de CLAUDE.md: en cada ficha implementada, cada caso de prueba y cada
/// criterio de aceptación tiene al menos un test. Los tests se vinculan con comentarios
/// "// Caso N" y "// Criterio N" (o "Criterios 3 y 7") dentro de una clase cuyo resumen nombra la ficha ("ficha R001").
/// </summary>
public class TrazabilidadFichasTests
{
    public static TheoryData<string> FichasImplementadas()
    {
        var datos = new TheoryData<string>();
        var fichas = Directory.EnumerateFiles(Repositorio.Ruta("docs", "normativa"), "R*.md")
            .Where(ruta => Regex.IsMatch(Path.GetFileName(ruta), @"^R\d{3}-"))
            .Order();
        foreach (var ruta in fichas)
        {
            var estado = File.ReadLines(ruta).FirstOrDefault(l => l.StartsWith("- **Estado:**", StringComparison.Ordinal)) ?? string.Empty;
            if (estado.Contains("implementada", StringComparison.OrdinalIgnoreCase))
            {
                datos.Add(Path.GetFileName(ruta));
            }
        }

        return datos;
    }

    [Fact]
    public void Fichas_HayAlMenosUnaImplementada()
    {
        Assert.NotEmpty(FichasImplementadas());
    }

    [Theory]
    [MemberData(nameof(FichasImplementadas))]
    public void Ficha_CadaCasoDePrueba_TieneSuTest(string archivo)
    {
        var (codigo, ficha) = LeerFicha(archivo);
        var casos = NumerosDeFila(Seccion(ficha, "## Casos de prueba"));
        var cubiertos = ReferenciasEnTests(codigo, "Caso");

        var faltan = casos.Except(cubiertos).Order().ToList();
        Assert.True(faltan.Count == 0, $"{codigo}: casos sin test: {string.Join(", ", faltan)}. Agregar un test con el comentario \"// Caso N\".");
    }

    [Theory]
    [MemberData(nameof(FichasImplementadas))]
    public void Ficha_CadaCriterioDeAceptacion_TieneSuTest(string archivo)
    {
        var (codigo, ficha) = LeerFicha(archivo);
        var criterios = NumerosDeLista(Seccion(ficha, "## Criterios de aceptación"));
        var cubiertos = ReferenciasEnTests(codigo, "Criterio");

        var faltan = criterios.Except(cubiertos).Order().ToList();
        Assert.True(faltan.Count == 0, $"{codigo}: criterios sin test: {string.Join(", ", faltan)}. Agregar \"// Criterio N\" al test que lo prueba.");
    }

    private static (string Codigo, string Contenido) LeerFicha(string archivo)
    {
        var codigo = archivo[..4]; // "R001"
        return (codigo, File.ReadAllText(Repositorio.Ruta("docs", "normativa", archivo)));
    }

    private static string Seccion(string ficha, string titulo)
    {
        var inicio = ficha.IndexOf(titulo, StringComparison.Ordinal);
        Assert.True(inicio >= 0, $"Falta la sección \"{titulo}\".");
        var fin = ficha.IndexOf("\n## ", inicio + titulo.Length, StringComparison.Ordinal);
        return fin < 0 ? ficha[inicio..] : ficha[inicio..fin];
    }

    /// <summary>Filas de tabla que empiezan con "| N |".</summary>
    private static HashSet<int> NumerosDeFila(string seccion) =>
        Regex.Matches(seccion, @"^\|\s*(\d+)\s*\|", RegexOptions.Multiline)
            .Select(m => int.Parse(m.Groups[1].Value)).ToHashSet();

    /// <summary>Ítems de lista numerada "N. ".</summary>
    private static HashSet<int> NumerosDeLista(string seccion) =>
        Regex.Matches(seccion, @"^(\d+)\.\s", RegexOptions.Multiline)
            .Select(m => int.Parse(m.Groups[1].Value)).ToHashSet();

    /// <summary>Números citados como "Caso 3", "Criterio 5" o "Criterios 3 y 7" en los tests de la ficha.</summary>
    private static HashSet<int> ReferenciasEnTests(string codigo, string palabra)
    {
        var numeros = new HashSet<int>();
        var patron = new Regex($@"\b{palabra}s?\s+(\d+(?:\s*(?:,|y)\s*\d+)*)");
        foreach (var archivo in Repositorio.ArchivosCs("tests"))
        {
            var texto = File.ReadAllText(archivo);
            if (!texto.Contains($"ficha {codigo}", StringComparison.Ordinal))
            {
                continue;
            }

            foreach (Match m in patron.Matches(texto))
            {
                foreach (Match n in Regex.Matches(m.Groups[1].Value, @"\d+"))
                {
                    numeros.Add(int.Parse(n.Value));
                }
            }
        }

        return numeros;
    }
}
