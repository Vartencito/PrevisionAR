namespace PrevisionAR.Web.Casos;

// Formato del archivo de caso, versión 1 (ADR 0007). Son tipos propios del archivo, separados de los del motor:
// si el motor renombra algo, el formato no cambia sin que alguien lo decida.
// Los valores de enums van como texto con nombre ("hombre", "autonomo"), nunca como números.
// Si se cambia un campo existente, sube la versión y se agrega la conversión desde la anterior.

/// <summary>Raíz del archivo <c>.previsionar.json</c>.</summary>
public sealed record ArchivoDeCaso(
    string? Formato,
    int VersionDelFormato,
    string? GuardadoConLaVersion,
    string? Referencia,
    PersonaEnArchivo? Persona,
    DateOnly? FechaDeCalculo,
    IReadOnlyList<PeriodoEnArchivo>? Periodos);

/// <summary>Datos personales que usan las reglas. <c>Sexo</c>: "hombre" o "mujer".</summary>
public sealed record PersonaEnArchivo(DateOnly? FechaDeNacimiento, string? Sexo);

/// <summary>
/// Un período tal como estaba en pantalla. <c>Tipo</c>: "relacionDeDependencia" o "autonomo".
/// <c>Origen</c>: "conAportes" o "sinAportes" (se suman valores con el issue #40, sin cambiar de versión).
/// </summary>
public sealed record PeriodoEnArchivo(string? Empleador, DateOnly? Desde, DateOnly? Hasta, string? Tipo, string? Origen);
