using PrevisionAR.Motor.Modelo;

namespace PrevisionAR.Web.Components.Pages;

/// <summary>Un caso ficticio listo para cargar en la pantalla con un clic.</summary>
/// <param name="Nombre">Lo que ve el abogado en la lista.</param>
/// <param name="QueMuestra">Qué resultado debería ver y de qué ficha sale el caso.</param>
internal sealed record EjemploDeCarga(
    string Nombre,
    string QueMuestra,
    DateOnly FechaDeNacimiento,
    Sexo Sexo,
    DateOnly FechaDeCalculo,
    IReadOnlyList<PeriodoDeServicio> Periodos);

/// <summary>
/// Ejemplos para probar la pantalla (issue #14). Todos menos el último ("Mixto") son copia de los casos de prueba
/// de las fichas R001 y R002, que ya tienen sus tests en el motor; acá solo se cargan. Todos los datos son ficticios.
/// </summary>
/// <remarks>
/// Si se cambia un caso en la ficha, se cambia también acá. Cuando aparezca la guía de ejemplos
/// que se usa con el abogado, se agregan sus casos a esta lista.
/// </remarks>
internal static class EjemplosDeCarga
{
    private static readonly DateOnly FechaDeLasFichas = new(2026, 1, 1);

    public static IReadOnlyList<EjemploDeCarga> Todos { get; } =
    [
        new(
            "R002 caso 1 — Hombre con edad y 30 años de aportes",
            "Cumple los requisitos, sin compensación.",
            new(1960, 3, 15), Sexo.Hombre, FechaDeLasFichas,
            [Dependencia("Empresa A (ficticia)", new(1990, 1, 1), new(2019, 12, 31))]),
        new(
            "R002 caso 3 — Le falta edad aunque le sobran servicios",
            "No cumple: le faltan 5 meses de edad. Los servicios de más no compensan edad.",
            new(1961, 6, 1), Sexo.Hombre, FechaDeLasFichas,
            [Dependencia("Empresa A (ficticia)", new(1985, 1, 1), new(2019, 12, 31))]),
        new(
            "R002 caso 5 — Compensación de edad suficiente",
            "Cumple: 72 meses de edad excedente compensan los 36 meses de servicios que faltan.",
            new(1955, 1, 1), Sexo.Hombre, FechaDeLasFichas,
            [Dependencia("Empresa A (ficticia)", new(1980, 1, 1), new(2006, 12, 31))]),
        new(
            "R002 caso 7 — Compensación insuficiente (redondeo a validar)",
            "No cumple: 65 meses de exceso compensan 32 (sin redondear hacia arriba) y faltan 4.",
            new(1960, 8, 1), Sexo.Mujer, FechaDeLasFichas,
            [Dependencia("Empresa A (ficticia)", new(1990, 1, 1), new(2016, 12, 31))]),
        new(
            "R002 caso 9 — 359 meses y 20 días (a validar)",
            "No cumple: los 20 días sueltos no completan el mes 360 (convención a validar con el abogado).",
            new(1961, 1, 1), Sexo.Hombre, FechaDeLasFichas,
            [
                Dependencia("Empresa A (ficticia)", new(1990, 1, 1), new(2019, 11, 30)),
                Dependencia("Empresa B (ficticia)", new(2019, 12, 1), new(2019, 12, 20)),
            ]),
        new(
            "R001 caso 11 — Dependencia y autónomo al mismo tiempo",
            "Caso solo de cómputo (R001): 180 meses en total; el tramo simultáneo 2005–2009 se cuenta una sola vez (n = 60, p = 60, m = 60). El veredicto de la PBU no es parte del caso.",
            new(1958, 4, 15), Sexo.Hombre, FechaDeLasFichas,
            [
                Dependencia("Empresa A (ficticia)", new(2000, 1, 1), new(2009, 12, 31)),
                new("Actividad autónoma (ficticia)", new(2005, 1, 1), new(2014, 12, 31), TipoDeServicio.Autonomo, ConAportes: true),
            ]),
        new(
            "Mixto — Antes y después de 1994, autónomo y período sin aportes",
            "Ejemplo construido para la pantalla (no es un caso de ficha). Muestra los dos tramos (PC y PAP), una superposición y un período no computado.",
            new(1958, 4, 15), Sexo.Hombre, FechaDeLasFichas,
            [
                Dependencia("Empresa A (ficticia)", new(1990, 1, 1), new(2009, 12, 31)),
                new("Actividad autónoma (ficticia)", new(2005, 1, 1), new(2014, 12, 31), TipoDeServicio.Autonomo, ConAportes: true),
                new("Empresa B sin aportes (ficticia)", new(2015, 1, 1), new(2016, 6, 30), TipoDeServicio.RelacionDeDependencia, ConAportes: false),
            ]),
    ];

    private static PeriodoDeServicio Dependencia(string empleador, DateOnly desde, DateOnly hasta) =>
        new(empleador, desde, hasta, TipoDeServicio.RelacionDeDependencia, ConAportes: true);
}
