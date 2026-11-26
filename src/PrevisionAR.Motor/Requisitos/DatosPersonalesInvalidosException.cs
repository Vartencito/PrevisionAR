namespace PrevisionAR.Motor.Requisitos;

/// <summary>Los datos de la persona no permiten verificar requisitos (por ejemplo, nacimiento posterior a la fecha de cálculo).</summary>
public sealed class DatosPersonalesInvalidosException(string mensaje) : Exception(mensaje);
