---
description: Cierra la sesión dejando todo verificado, subido y con los próximos pasos claros para el responsable.
---

1. `dotnet build` y `dotnet test`. Si algo falla, arreglalo o avisá; no cierres con el CI en rojo.
2. Si hubo cambios de código sin revisar, pedí revisión al agente `revisor-de-codigo` y aplicá lo que corresponda.
3. Commits chicos en español, con "Closes #N" en los que terminan una tarea (GitHub solo reconoce la palabra en inglés). `git push`. Si no hay `gh`, verificá con la API pública que el issue se haya cerrado.
4. `python herramientas/estado_del_proyecto.py` (usa `gh` si está; si no, la API pública), commit y push del estado.
5. Si apareció trabajo nuevo, creá el issue con versión, tamaño y tipo (o listalo para el responsable si no hay `gh`).
6. Cerrá con un resumen para el responsable, en este orden:
   - **Qué quedó hecho** (con links a issues o archivos).
   - **Qué tenés que hacer vos**: decisiones pendientes y preguntas para el abogado.
   - **Cómo probarlo a mano**: pasos concretos.
