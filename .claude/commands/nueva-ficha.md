---
description: Crea la ficha normativa de una regla nueva (investigación, criterios, casos y preguntas), sin programar.
argument-hint: <código y tema, ej. "R004 moratoria Ley 27.705">
---

Crear la ficha normativa para: $ARGUMENTS

1. Leé `CLAUDE.md`, `docs/conocimiento/notas-de-dominio.md`, la plantilla `docs/normativa/README.md` y las fichas existentes para seguir su estilo.
2. Delegá la investigación al agente `investigador-previsional`: norma, fuente con link, vigencia, modificaciones, excepciones y preguntas clasificadas (✅/⚠️/❓).
3. Con lo investigado, escribí la ficha en `docs/normativa/RXXX-tema.md`:
   - Enunciado en lenguaje claro y datos de entrada.
   - Convenciones que la norma no resuelve, marcadas **A VALIDAR**, con la propuesta y su respaldo.
   - Criterios de aceptación numerados ("Dado…, cuando…, entonces…").
   - Casos de prueba numerados con datos ficticios y el resultado esperado, verificados a mano.
4. Agregá las dudas nuevas a `docs/conocimiento/preguntas-previsionalista.md` con la numeración siguiente.
5. Actualizá la lista de fichas en `docs/vision.md`.
6. Estado de la ficha: "Borrador". **No programes.** Commit y push de la documentación.
7. Avisale al responsable qué revisar (en lenguaje claro) y qué preguntas llevar al abogado.
