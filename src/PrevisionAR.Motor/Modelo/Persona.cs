namespace PrevisionAR.Motor.Modelo;

/// <summary>Datos personales que usan las reglas. Sin datos identificatorios (privacidad).</summary>
/// <param name="FechaDeNacimiento">Fecha de nacimiento.</param>
/// <param name="Sexo">Sexo según la Ley 24.241, art. 19.</param>
public sealed record Persona(DateOnly FechaDeNacimiento, Sexo Sexo);
