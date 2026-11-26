---
description: Implementa en el motor una ficha ya revisada, con tests primero, y deja todo verificado y documentado.
argument-hint: <archivo de la ficha, ej. R003-computo-por-hijos.md>
---

Implementar la ficha `docs/normativa/$ARGUMENTS`.

1. Leé `CLAUDE.md` y la ficha completa. Si la ficha no tiene criterios y casos, o el responsable no la revisó, pará y avisá.
2. Revisá `docs/roadmap-tecnico.md`: si el disparador de alguna pieza se cumplió, proponela al responsable antes de agregarla.
3. **Tests primero** en `tests/PrevisionAR.Motor.Tests/`: un test por caso (`// Caso N`) y por criterio (`// Criterio N`), en una clase cuyo resumen diga "ficha RXXX". El test de trazabilidad falla si falta alguno. Agregá invariantes en `Invariantes/` si la regla tiene propiedades generales.
4. Implementá lo mínimo en `src/PrevisionAR.Motor/` hasta que pasen. Convenciones "A VALIDAR" aisladas y comentadas con el número de pregunta. Resultado con la regla (`Reglas.cs`) y explicación con la norma de cada paso.
5. Si corresponde, mostralo en la pantalla de prueba (`src/PrevisionAR.Web`), sin lógica en la Web.
6. `dotnet build` y `dotnet test` en verde. Pedile una revisión al agente `revisor-de-codigo` y corregí los bloqueantes.
7. Marcá la ficha como "implementada" en su Estado (activa el test de trazabilidad) con las rutas de código y tests. Actualizá `docs/vision.md`, `docs/roadmap-tecnico.md`, `CLAUDE.md` (Estado actual) y el README si cambió algo visible.
8. Commit y push. Esperá el CI en verde.
9. Dale al responsable los pasos para probarlo a mano, con un ejemplo concreto y el resultado esperado.
