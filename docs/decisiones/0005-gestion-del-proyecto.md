# 0005 — Gestión del proyecto con Kanban en GitHub

- **Estado:** Aceptada
- **Fecha:** 2026-11-26

## Contexto

El responsable dispone de ~10 h por semana, la IA ejecuta la mayor parte del trabajo en sesiones sin memoria, y parte de las tareas dependen de un abogado con tiempos inciertos. Hace falta saber qué está hecho, qué sigue, cuánto falta y cuándo se llega a cada versión.

## Decisión

- **Metodología:** Kanban con repaso semanal. De PMI se toman hitos con fecha objetivo, estimación y seguimiento.
- **Herramienta:** GitHub Issues (tareas), Milestones (versiones) y Projects (tablero). Los commits cierran tareas con "Closes #N" (corrección del 2026-11-27: GitHub no reconoce "Cierra #N").
- **Estimación:** tamaños S/M/L = 2/4/8 h. El avance se mide en horas.
- **Pronóstico:** `herramientas/estado_del_proyecto.py` genera `docs/estado-del-proyecto.md` con el avance, el pronóstico a 10 h por semana y qué hacer ahora.
- Detalle en `docs/plan-del-proyecto.md`.

## Alternativas descartadas

- **Scrum:** sprints, roles y ceremonias no tienen sentido con una persona y una IA.
- **PMI completo** (cronograma detallado, Gantt): el alcance se descubre con los abogados; un cronograma fijo quedaría viejo enseguida.
- **Herramienta externa** (Trello, Jira, una planilla): duplicaría información y la IA no la actualizaría junto con el código.

## Consecuencias

- Cualquier sesión de IA sabe qué hacer leyendo un archivo.
- Las fechas son pronósticos que se corrigen cada semana, no compromisos.
- Hay un script en Python fuera del código del producto; no afecta al motor ni a la Web.
