# 0003 — Desarrollo principalmente con IA

- **Estado:** Aceptada
- **Fecha:** 2026-11-26

## Contexto

El responsable del proyecto dispone de ~10 horas semanales y quiere que el desarrollo lo haga la IA, como ya ocurre en muchas empresas.

## Decisión

- La IA diseña, programa, prueba y documenta.
- El responsable actúa como dueño del producto: pide funcionalidades, habla con los abogados, aprueba decisiones y valida el resultado.
- El contexto y las reglas para la IA están en `CLAUDE.md`.

## Consecuencias

- **Los tests son la red de seguridad principal.** Como el humano no revisa cada línea, todo cambio del motor tiene tests derivados de una ficha normativa.
- **La documentación es la memoria del proyecto.** Cada sesión de IA arranca sin recuerdos: lo que no está en `docs/` o en `CLAUDE.md` se pierde.
- **Las fichas normativas son el punto de control humano.** El responsable (y un previsionalista si hace falta) revisa qué se va a calcular antes de que se programe.
- **Verificación automática recomendada:** un pipeline de integración continua que compile y corra los tests en cada push.
