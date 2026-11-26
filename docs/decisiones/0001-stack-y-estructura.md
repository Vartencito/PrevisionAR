# 0001 — Stack y estructura inicial

- **Estado:** Aceptada
- **Fecha:** 2026-11-26

## Contexto

Hace falta una base para empezar el motor previsional. El responsable trabaja en .NET/Blazor y dispone de ~10 horas semanales.

## Decisión

- **Lenguaje y plataforma:** C# sobre .NET 10 (LTS).
- **Solución:** formato `.slnx`.
- **Proyectos iniciales:**
  - `PrevisionAR.Motor`: biblioteca de clases sin dependencias externas. Contiene todas las reglas y cálculos.
  - `PrevisionAR.Web`: Blazor Web App con interactividad Server, corre localmente. Solo presenta datos; no calcula.
  - `PrevisionAR.Motor.Tests`: xUnit.
- **Configuración común** en `Directory.Build.props`. El motor compila con advertencias como errores.
- **SQLite y la capa de IA** (`IAIProvider`) se agregan cuando haya un requisito que los necesite, no antes.

## Consecuencias

- El motor se puede probar y reutilizar sin la interfaz.
- La dependencia va siempre Web → Motor, nunca al revés.
- Menos proyectos al inicio = menos mantenimiento; se separa más cuando haga falta.
