---
description: Cierra la sesión dejando todo verificado, subido y con los próximos pasos claros para el responsable.
---

1. `dotnet build` y `dotnet test`. Si algo falla, arreglalo o avisá; no cierres con el CI en rojo.
2. Si hubo cambios de código sin revisar, pedí revisión al agente `revisor-de-codigo` y aplicá lo que corresponda.
3. Commits chicos en español, con "Cierra #N" en los que terminan una tarea. `git push`.
4. Si `gh` está disponible: `python herramientas/estado_del_proyecto.py`, commit y push del estado. Si no, avisá que el estado quedó sin regenerar.
5. Si apareció trabajo nuevo, creá el issue con versión, tamaño y tipo (o listalo para el responsable si no hay `gh`).
6. Cerrá con un resumen para el responsable, en este orden:
   - **Qué quedó hecho** (con links a issues o archivos).
   - **Qué tenés que hacer vos**: decisiones pendientes y preguntas para el abogado.
   - **Cómo probarlo a mano**: pasos concretos.
