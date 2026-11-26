# 0004 — Agentes de IA, comandos de flujo y estrategia de pruebas

- **Estado:** Aceptada
- **Fecha:** 2026-11-26

## Contexto

El proyecto lo desarrolla principalmente la IA (ADR 0003). El responsable no revisa cada línea de código, así que la calidad tiene que salir de procesos repetibles y de controles automáticos, no de la memoria de una sesión.

## Decisión

### Agentes (`.claude/agents/`)

| Agente | Rol | Puede | No puede |
|--------|-----|-------|----------|
| `investigador-previsional` | Investiga normativa y clasifica preguntas (✅/⚠️/❓) con fuentes | Buscar en la web, escribir en `docs/` | Decidir criterios, escribir código, validar fichas |
| `revisor-de-codigo` | Revisa un cambio contra `CLAUDE.md` y su ficha, sin haberlo escrito | Leer el repo y el historial de git | Modificar archivos |

El que escribe no se revisa a sí mismo: el revisor llega sin contexto, a propósito.

### Comandos de flujo (`.claude/commands/`)

- `/nueva-ficha`: investigación → ficha con criterios y casos → preguntas. Sin código.
- `/implementar-ficha`: tests primero → código → revisión → docs → commit → pasos de prueba manual.
- `/procesar-respuestas`: respuestas del abogado → preguntas, fichas, convenciones y tests.

Fijan el flujo de `CLAUDE.md` en pasos concretos, para que cualquier sesión lo siga igual.

### Estrategia de pruebas (cuatro capas)

1. **Casos de la ficha:** ejemplos concretos, verificados a mano (`// Caso N`).
2. **Invariantes:** reglas que valen para *cualquier* entrada (cada día cuenta una vez, los desgloses suman el total, el orden de los períodos no importa, el derecho no se pierde con el tiempo). Se prueban con cientos de casos generados al azar con semilla fija, así un fallo se puede repetir. Sin paquetes externos.
3. **Trazabilidad:** un test lee las fichas implementadas y falla si un caso o un criterio no tiene su test.
4. **Arquitectura:** el motor no depende de nada externo, no lee el reloj y no usa `double` ni `float`.

Además, el CI mide la **cobertura** (qué parte del motor ejecutan los tests) y la muestra en cada ejecución. Por ahora informa; no bloquea.

### Permisos de los asistentes

La configuración de permisos de Claude Code (`.claude/settings.json`) no la edita la IA: la decide el responsable.

## Consecuencias

- Una sesión nueva de IA tiene el contexto (`CLAUDE.md`), el flujo (comandos) y los controles (tests) sin depender de recuerdos.
- Agregar una regla sin tests, o romper una regla de arquitectura, hace fallar el CI.
- Los invariantes encuentran casos raros que los ejemplos no cubren; si uno falla, el caso se agrega a la ficha como ejemplo.
