using PrevisionAR.Motor.Modelo;
using PrevisionAR.Web.Casos;

namespace PrevisionAR.Web.Tests.Casos;

/// <summary>Guardar y abrir casos en un archivo local (ADR 0007, criterios de aceptación). Todos los datos son ficticios.</summary>
public class ArchivoDeCasosTests
{
    /// <summary>
    /// Archivo de la versión 1 tal como lo escribe la app. NUNCA SE MODIFICA: garantiza que los casos que
    /// guardaron los abogados con la v0.1 se sigan abriendo en versiones futuras.
    /// </summary>
    private const string ArchivoDeReferenciaVersion1 = """
        {
          "formato": "PrevisionAR.Caso",
          "versionDelFormato": 1,
          "guardadoConLaVersion": "0.1.0",
          "referencia": "Caso de prueba",
          "persona": { "fechaDeNacimiento": "1958-04-15", "sexo": "hombre" },
          "fechaDeCalculo": "2026-01-01",
          "periodos": [
            { "empleador": "Empresa A (ficticia)", "desde": "1990-01-01", "hasta": "2009-12-31",
              "tipo": "relacionDeDependencia", "origen": "conAportes" },
            { "empleador": "Actividad autónoma (ficticia)", "desde": "2005-01-01", "hasta": null,
              "tipo": "autonomo", "origen": "sinAportes" }
          ]
        }
        """;

    private static CasoEnEdicion CasoDeEjemplo()
    {
        var caso = new CasoEnEdicion
        {
            Referencia = "Carpeta 123",
            FechaDeNacimiento = new(1960, 2, 29),
            Sexo = Sexo.Mujer,
            FechaDeCalculo = new(2026, 1, 1),
        };
        caso.Filas.Add(new FilaPeriodo { Empleador = "Empresa A (ficticia)", Desde = new(1990, 1, 1), Hasta = new(2009, 12, 31) });
        caso.Filas.Add(new FilaPeriodo { Empleador = "Autónomo (ficticio)", Desde = new(2010, 1, 1), Hasta = null, Tipo = TipoDeServicio.Autonomo, ConAportes = false });
        return caso;
    }

    private static void AssertMismoCaso(CasoEnEdicion esperado, CasoEnEdicion obtenido)
    {
        Assert.Equal(esperado.Referencia, obtenido.Referencia);
        Assert.Equal(esperado.FechaDeNacimiento, obtenido.FechaDeNacimiento);
        Assert.Equal(esperado.Sexo, obtenido.Sexo);
        Assert.Equal(esperado.FechaDeCalculo, obtenido.FechaDeCalculo);
        Assert.Equal(esperado.Filas.Count, obtenido.Filas.Count);
        for (var i = 0; i < esperado.Filas.Count; i++)
        {
            Assert.Equal(esperado.Filas[i].APeriodo(), obtenido.Filas[i].APeriodo());
            Assert.Equal(esperado.Filas[i].Empleador, obtenido.Filas[i].Empleador);
        }
    }

    // Criterios 1, 3 y 4
    [Fact]
    public void GuardarYAbrir_CasoCompleto_DevuelveElMismoCaso()
    {
        var caso = CasoDeEjemplo();

        var apertura = ArchivoDeCasos.Abrir(ArchivoDeCasos.Guardar(caso, "0.1.0"));

        Assert.Null(apertura.Error);
        AssertMismoCaso(caso, apertura.Caso!);
    }

    // Criterio 3: se guarda tal cual, aunque tenga errores que el motor rechazaría al calcular.
    [Fact]
    public void GuardarYAbrir_CasoConFechasInvertidasYSinNombre_LoConservaTalCual()
    {
        var caso = new CasoEnEdicion { FechaDeCalculo = new(2026, 1, 1) };
        caso.Filas.Add(new FilaPeriodo { Empleador = string.Empty, Desde = new(2010, 1, 1), Hasta = new(2009, 12, 31) });

        var apertura = ArchivoDeCasos.Abrir(ArchivoDeCasos.Guardar(caso, "0.1.0"));

        Assert.Null(apertura.Error);
        AssertMismoCaso(caso, apertura.Caso!);
    }

    [Fact]
    public void GuardarYAbrir_SinPeriodosNiReferencia_DevuelveCasoVacio()
    {
        var caso = new CasoEnEdicion { FechaDeCalculo = new(2026, 1, 1) };

        var apertura = ArchivoDeCasos.Abrir(ArchivoDeCasos.Guardar(caso, "0.1.0"));

        Assert.Null(apertura.Error);
        AssertMismoCaso(caso, apertura.Caso!);
    }

    // Criterio 8: fechas AAAA-MM-DD, valores con nombre y versión 1.
    [Fact]
    public void Guardar_Caso_UsaFormatoEstable()
    {
        var contenido = ArchivoDeCasos.Guardar(CasoDeEjemplo(), "0.1.0");

        Assert.Contains("\"formato\": \"PrevisionAR.Caso\"", contenido, StringComparison.Ordinal);
        Assert.Contains("\"versionDelFormato\": 1", contenido, StringComparison.Ordinal);
        Assert.Contains("\"fechaDeNacimiento\": \"1960-02-29\"", contenido, StringComparison.Ordinal);
        Assert.Contains("\"sexo\": \"mujer\"", contenido, StringComparison.Ordinal);
        Assert.Contains("\"tipo\": \"autonomo\"", contenido, StringComparison.Ordinal);
        Assert.Contains("\"origen\": \"sinAportes\"", contenido, StringComparison.Ordinal);
    }

    // Criterio 5: el archivo no contiene resultados.
    [Fact]
    public void Guardar_Caso_NoIncluyeResultados()
    {
        var contenido = ArchivoDeCasos.Guardar(CasoDeEjemplo(), "0.1.0");

        Assert.DoesNotContain("total", contenido, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("derecho", contenido, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Abrir_ArchivoDeReferenciaVersion1_SeSigueAbriendo()
    {
        var apertura = ArchivoDeCasos.Abrir(ArchivoDeReferenciaVersion1);

        Assert.Null(apertura.Error);
        var caso = apertura.Caso!;
        Assert.Equal("Caso de prueba", caso.Referencia);
        Assert.Equal(new DateOnly(1958, 4, 15), caso.FechaDeNacimiento);
        Assert.Equal(Sexo.Hombre, caso.Sexo);
        Assert.Equal(new DateOnly(2026, 1, 1), caso.FechaDeCalculo);
        Assert.Equal(
            [
                new PeriodoDeServicio("Empresa A (ficticia)", new(1990, 1, 1), new(2009, 12, 31), TipoDeServicio.RelacionDeDependencia, ConAportes: true),
                new PeriodoDeServicio("Actividad autónoma (ficticia)", new(2005, 1, 1), null, TipoDeServicio.Autonomo, ConAportes: false),
            ],
            caso.Filas.Select(f => f.APeriodo()));
    }

    // Criterio 7
    [Fact]
    public void Abrir_VersionPosterior_PideActualizarElPrograma()
    {
        var contenido = ArchivoDeReferenciaVersion1.Replace("\"versionDelFormato\": 1", "\"versionDelFormato\": 2", StringComparison.Ordinal);

        var apertura = ArchivoDeCasos.Abrir(contenido);

        Assert.Null(apertura.Caso);
        Assert.Equal(ArchivoDeCasos.MensajeVersionPosterior, apertura.Error);
    }

    // Criterio 7
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("esto no es JSON")]
    [InlineData("{\"periodos\": [")]
    [InlineData("[]")]
    [InlineData("{}")]
    [InlineData("{\"formato\": \"OtroPrograma\", \"versionDelFormato\": 1}")]
    [InlineData("{\"formato\": \"PrevisionAR.Caso\"}")]
    [InlineData("{\"formato\": \"PrevisionAR.Caso\", \"versionDelFormato\": 0}")]
    [InlineData("{\"formato\": \"PrevisionAR.Caso\", \"versionDelFormato\": -1}")]
    public void Abrir_ArchivoQueNoEsUnCaso_RechazaConMensaje(string contenido)
    {
        var apertura = ArchivoDeCasos.Abrir(contenido);

        Assert.Null(apertura.Caso);
        Assert.StartsWith(ArchivoDeCasos.MensajeArchivoInvalido, apertura.Error, StringComparison.Ordinal);
    }

    // Criterio 7: un valor desconocido nunca se reemplaza en silencio; el mensaje dice qué y dónde.
    [Theory]
    [InlineData("\"sexo\": \"hombre\"", "\"sexo\": \"otro\"", "sexo \"otro\"")]
    [InlineData("\"tipo\": \"autonomo\"", "\"tipo\": \"monotributo\"", "Período 2: el tipo \"monotributo\"")]
    [InlineData("\"origen\": \"conAportes\"", "\"origen\": \"moratoria\"", "Período 1: el origen \"moratoria\"")]
    [InlineData("\"desde\": \"2005-01-01\"", "\"desde\": null", "Período 2: falta la fecha desde")]
    [InlineData("\"fechaDeCalculo\": \"2026-01-01\"", "\"fechaDeCalculo\": null", "Falta la fecha de cálculo")]
    [InlineData("\"fechaDeNacimiento\": \"1958-04-15\"", "\"fechaDeNacimiento\": \"15/04/1958\"", "problema en $.persona.fechaDeNacimiento")]
    [InlineData("\"sexo\": \"hombre\"", "\"sexo\": 0", "problema en $.persona.sexo")]
    [InlineData("\"persona\": { \"fechaDeNacimiento\": \"1958-04-15\", \"sexo\": \"hombre\" }", "\"persona\": null", "Falta la fecha de nacimiento")]
    [InlineData("\"periodos\": [", "\"periodos\": [null, ", "Período 1: está vacío")]
    public void Abrir_ValorInvalido_RechazaIndicandoDonde(string original, string reemplazo, string mensajeEsperado)
    {
        var contenido = ArchivoDeReferenciaVersion1.Replace(original, reemplazo, StringComparison.Ordinal);

        var apertura = ArchivoDeCasos.Abrir(contenido);

        Assert.Null(apertura.Caso);
        Assert.Contains(mensajeEsperado, apertura.Error, StringComparison.Ordinal);
    }

    // Criterio 2: el nombre del archivo no lleva datos del cliente.
    [Fact]
    public void NombreSugerido_Fecha_UsaSoloLaFecha()
    {
        Assert.Equal("caso-2026-11-27.previsionar.json", ArchivoDeCasos.NombreSugerido(new DateOnly(2026, 11, 27)));
    }

    [Fact]
    public void Abrir_PeriodosNoEsUnaLista_RechazaConMensaje()
    {
        var contenido = ArchivoDeReferenciaVersion1[..ArchivoDeReferenciaVersion1.IndexOf("\"periodos\"", StringComparison.Ordinal)] + "\"periodos\": {} }";

        var apertura = ArchivoDeCasos.Abrir(contenido);

        Assert.Null(apertura.Caso);
        Assert.StartsWith(ArchivoDeCasos.MensajeArchivoInvalido, apertura.Error, StringComparison.Ordinal);
    }

    // Deja fijado un comportamiento del lector JSON: un número escrito como texto se acepta.
    [Fact]
    public void Abrir_VersionEscritaComoTexto_SeAcepta()
    {
        var contenido = ArchivoDeReferenciaVersion1.Replace("\"versionDelFormato\": 1", "\"versionDelFormato\": \"1\"", StringComparison.Ordinal);

        Assert.Null(ArchivoDeCasos.Abrir(contenido).Error);
    }

    [Fact]
    public void Abrir_ValorMuyLargo_RecortaElMensaje()
    {
        var largo = new string('x', 5000);
        var contenido = ArchivoDeReferenciaVersion1.Replace("\"sexo\": \"hombre\"", $"\"sexo\": \"{largo}\"", StringComparison.Ordinal);

        var apertura = ArchivoDeCasos.Abrir(contenido);

        Assert.True(apertura.Error!.Length < 200, apertura.Error);
    }
}
