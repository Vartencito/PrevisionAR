---
description: Arranca una sesión de trabajo con contexto al día: sincroniza, lee el estado y propone qué hacer.
argument-hint: <opcional: lo que querés que se haga hoy>
---

Pedido del responsable para hoy (puede estar vacío): $ARGUMENTS

1. `git status` y `git pull --ff-only`. Si hay cambios locales sin commitear o no se puede avanzar sin conflicto, pará y avisá.
2. Comprobá que `gh` esté disponible y autenticado (`gh auth status`). Si no, avisá en una línea cómo instalarlo y seguí sin él.
3. Leé `CLAUDE.md`, `docs/estado-del-proyecto.md` y, si hay, la última nota de `docs/conocimiento/reuniones/`.
4. Mostrá al responsable, en no más de 10 líneas:
   - Qué cambió desde la última sesión (commits nuevos, respuestas del abogado).
   - Qué está esperando una decisión suya (ADR o fichas en "Propuesta").
   - Qué vas a hacer hoy: su pedido o, si no pidió nada, la primera tarea de "Le toca a la IA".
5. Empezá a trabajar sin esperar, salvo que la tarea necesite una aprobación pendiente.
