# Plan del proyecto

**Estado actualizado (avance, pronóstico y qué hacer ahora):** `docs/estado-del-proyecto.md`.
**Tareas:** [Issues de GitHub](https://github.com/Vartencito/PrevisionAR/issues) · **Versiones:** [Milestones](https://github.com/Vartencito/PrevisionAR/milestones).

## Metodología: Kanban con revisión semanal

Se eligió **Kanban** (flujo continuo de tareas) con un **repaso semanal** de 15 minutos. Es la forma liviana de trabajo ágil que mejor encaja con este proyecto:

| Condición del proyecto | Por qué Kanban y no Scrum o PMI completo |
|------------------------|------------------------------------------|
| Una sola persona, ~10 h por semana | Scrum necesita equipo, roles y ceremonias; acá serían burocracia |
| La mayor parte la ejecuta la IA, en sesiones sin memoria | El tablero y los issues son la memoria compartida: cualquier sesión sabe qué sigue |
| Dependencia externa (el abogado) con tiempos inciertos | Kanban muestra lo bloqueado sin romper un sprint; se sigue con otra tarea |
| Alcance que se descubre con los abogados | Se reordena la cola en cualquier momento, sin replanificar todo |

De PMI se toma solo lo útil: **hitos** (versiones con fecha objetivo), **estimación** y **seguimiento contra el plan**.

## Herramienta: GitHub (Issues, Milestones y Projects)

- **Issue = tarea.** Título claro, versión (milestone) y etiquetas.
- **Milestone = versión.** GitHub muestra el porcentaje de avance de cada una.
- **Project = tablero Kanban** con columnas *Pendiente → En curso → Hecho* (se crea una vez a mano, ver abajo).
- **Commits que cierran tareas:** un commit con "Closes #14" cierra el issue 14 al llegar a `main`. Tiene que ser en inglés: GitHub no reconoce "Cierra #14".

### Etiquetas

| Etiqueta | Significado |
|----------|-------------|
| `tamaño: S` / `M` / `L` | Hasta 2 h / 2 a 5 h / 5 a 10 h de trabajo (se cuenta como 2, 4 y 8 h) |
| `responsable` | La hace el responsable del proyecto, no la IA |
| `bloqueado: abogado` | Espera respuestas del previsionalista |
| `tipo: ficha` · `motor` · `pantalla` · `investigación` · `decisión` · `distribución` · `proceso` | Qué clase de trabajo es |

## Versiones (hitos)

| Versión | Qué entrega | Fecha objetivo |
|---------|-------------|----------------|
| v0.0 Base | Estructura, CI, IA, R001 y R002, investigación inicial | Terminada |
| v0.1 MVP | Cómputo, requisitos de la PBU e informe imprimible, validados con un abogado, y una versión descargable | 28/12/2026 |
| v0.2 ¿A qué puede acceder? | Cómputo por hijos, moratorias vigentes, PEA y PUAM | 25/01/2027 |
| v0.3 Haber inicial | PBU, PC y PAP con índices oficiales | 22/02/2027 |
| v0.4 Distribución y beta | Instalador, casos guardados y beta con abogados | 08/03/2027 |
| Etapa 2 (sin fecha) | Importar la historia laboral de ANSES (OCR / IA) | — |

Las fechas suponen 10 h por semana y **no incluyen esperas del abogado**: si una respuesta tarda, la versión se corre. Se recalculan cada semana.

## Cómo saber cuánto avanzamos y cuánto falta

`docs/estado-del-proyecto.md` se genera con `python herramientas/estado_del_proyecto.py` (necesita `gh` autenticado). Muestra:

- Avance de cada versión en tareas y en **horas** (S = 2, M = 4, L = 8).
- Horas y semanas que faltan a 10 h por semana.
- Fecha pronosticada de cada versión.
- **Qué hacer ahora:** lo que le toca a la IA (en orden), lo que te toca a vos y lo que espera al abogado.

## Rutinas

**Cada sesión de trabajo con la IA:**
1. El responsable empieza con `/inicio-de-sesion`: la IA sincroniza, lee `docs/estado-del-proyecto.md`, muestra qué cambió y qué espera una decisión, y toma la primera tarea de "Le toca a la IA" (salvo que el responsable pida otra).
2. Al terminar, `/cierre-de-sesion`: commits con "Closes #N", CI en verde, estado regenerado y resumen de lo que le toca al responsable.
3. Tareas nuevas que aparezcan → issue nuevo con versión y tamaño.

**Repaso semanal (15 minutos, el responsable con la IA):**
1. Leer el estado: ¿qué se cerró?, ¿qué está bloqueado?
2. Reordenar prioridades si cambió algo (por ejemplo, después de hablar con abogados).
3. Ajustar la capacidad si la semana real no fue de 10 h (`CAPACIDAD_SEMANAL_HORAS` en el script).

## Definición de "listo para empezar" y de "hecho"

**Listo para empezar:** la tarea tiene versión, tamaño y, si es del motor, su ficha con criterios y casos revisada.

**Hecho:** cumple el flujo de `CLAUDE.md`: tests (las cuatro capas), CI en verde, revisión del agente revisor, documentación al día, issue cerrado y, si corresponde, pasos para probarlo a mano.

## Tablero Kanban (una sola vez, 2 minutos)

La API que usa la IA no permite crear tableros, así que este paso lo hace el responsable:

1. En GitHub, en el repositorio: pestaña **Projects** → **New project** → plantilla **Board** → nombre "Previsión AR".
2. En el tablero: **⋯** → **Workflows** → **Auto-add to project** → activarlo con el filtro `is:issue repo:Vartencito/PrevisionAR`, y desde la lista de issues agregar los ya creados (seleccionar todos → **Projects**).
3. Activar los workflows **Item closed → Done** y **Item reopened → Todo**.

Desde ahí el tablero se mantiene solo con lo que la IA hace en los issues.

## Validación con abogados (aprobado el 2026-11-27)

Forma de trabajo completa en `docs/propuesta-forma-de-trabajo.md`. En resumen:

- **Un solo archivo de preguntas:** `docs/conocimiento/preguntas-previsionalista.md`. Cada pregunta tiene un código fijo y se marca abierta, respondida o validada. La hoja de cada reunión sale de ahí.
- **Notas de reunión:** `docs/conocimiento/reuniones/AAAA-MM-DD.md`.
- **Casos dorados:** historias laborales reales anonimizadas junto con el cómputo de ANSES. Procedimiento en `docs/conocimiento/casos-dorados.md`.
- **Límite de investigación:** como máximo dos fichas en "Investigación" esperando al abogado.
- **Validación de una regla:** dos abogados, o un abogado más un caso dorado que coincida.
