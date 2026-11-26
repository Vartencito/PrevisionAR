namespace PrevisionAR.Motor.Computo;

/// <summary>
/// Tiempo computado dentro de un tramo, separado por tipo de servicio.
/// Lo usarán las fichas de PC y PAP (fórmula de servicios mixtos).
/// </summary>
/// <param name="Total">Todo el tiempo del tramo.</param>
/// <param name="SoloDependencia">"n": días cubiertos solo por relación de dependencia.</param>
/// <param name="SoloAutonomo">"m": días cubiertos solo por servicios autónomos.</param>
/// <param name="Simultaneo">"p": días cubiertos a la vez por dependencia y autónomo.</param>
public sealed record DesgloseDeTramo(
    TiempoDeServicio Total,
    TiempoDeServicio SoloDependencia,
    TiempoDeServicio SoloAutonomo,
    TiempoDeServicio Simultaneo);
