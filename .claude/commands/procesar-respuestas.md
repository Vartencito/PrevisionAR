---
description: Incorpora las respuestas del previsionalista a las preguntas, fichas, convenciones y tests.
argument-hint: <respuestas tal como las trajo el responsable>
---

Respuestas del previsionalista:

$ARGUMENTS

1. Anotá cada respuesta en `docs/conocimiento/preguntas-previsionalista.md` (columnas Respuesta y Fecha). Si citó una norma, pedile al agente `investigador-previsional` que la busque y la cite con link.
2. Para cada respuesta que cambie una convención "A VALIDAR":
   - Actualizá la ficha (convención y casos nuevos con días sueltos o redondeos) y marcá ese punto como validado.
   - Ajustá primero los tests y después el código (las convenciones están aisladas, por ejemplo en `ConvencionDeComputo`).
3. Si algún ejemplo de la guía no coincidió con el abogado, agregalo como caso de prueba con el resultado que dio él y corregí hasta que pase.
4. Las respuestas sobre su trabajo diario (qué necesita, qué programas usa, formato del informe) resumilas en `docs/conocimiento/notas-de-dominio.md` y proponé al responsable cómo cambian las prioridades.
5. `dotnet build` y `dotnet test` en verde, docs al día, commit y push, CI en verde.
6. Resumile al responsable qué cambió y qué preguntas siguen abiertas.
