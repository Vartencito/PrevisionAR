using System.Reflection;

namespace PrevisionAR.Motor.Tests.Arquitectura;

/// <summary>
/// El catálogo de fichas que muestra el informe debe coincidir con docs/normativa/ y con <see cref="Reglas"/>.
/// </summary>
public class CatalogoDeFichasTests
{
    public static TheoryData<string> CodigosDelCatalogo()
    {
        var datos = new TheoryData<string>();
        foreach (var ficha in CatalogoDeFichas.Todas)
        {
            datos.Add(ficha.Codigo);
        }

        return datos;
    }

    [Theory]
    [MemberData(nameof(CodigosDelCatalogo))]
    public void Catalogo_CadaFicha_TieneSuArchivoConElMismoTitulo(string codigo)
    {
        var ficha = CatalogoDeFichas.Obtener(codigo);
        var archivos = Directory.GetFiles(Repositorio.Ruta("docs", "normativa"), $"{codigo}-*.md");

        var archivo = Assert.Single(archivos);
        var encabezado = File.ReadLines(archivo).First();
        Assert.Equal($"# {codigo} — {ficha.Titulo}", encabezado);
    }

    [Theory]
    [MemberData(nameof(CodigosDelCatalogo))]
    public void Catalogo_CadaFicha_CitaAlMenosUnaNorma(string codigo)
    {
        Assert.NotEmpty(CatalogoDeFichas.Obtener(codigo).Normas);
    }

    [Fact]
    public void Catalogo_CadaCodigoDeReglas_EstaEnElCatalogo()
    {
        var codigos = typeof(Reglas).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(campo => campo.IsLiteral)
            .Select(campo => (string)campo.GetRawConstantValue()!);

        foreach (var codigo in codigos)
        {
            Assert.Contains(CatalogoDeFichas.Todas, f => f.Codigo == codigo);
        }
    }

    [Fact]
    public void Obtener_CodigoInexistente_LanzaKeyNotFound()
    {
        Assert.Throws<KeyNotFoundException>(() => CatalogoDeFichas.Obtener("R999"));
    }
}
