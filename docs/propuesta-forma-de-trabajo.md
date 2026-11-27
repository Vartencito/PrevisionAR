# Propuesta: estructura del proyecto, rol del responsable y comunicación con la IA

- **Estado:** Propuesta (requiere aprobación del responsable: cambia el modelo de trabajo)
- **Fecha:** 2026-11-27
- **Base:** revisión de `CLAUDE.md`, agentes y comandos (`.claude/`), código y tests, `docs/` (plan, estado, arquitectura, ADR, fichas, conocimiento), issues de GitHub y el historial de commits.

## 1. Qué tipo de proyecto es

Previsión AR es un **motor de reglas reguladas**, parecido a un software de liquidación de impuestos: el valor no está en la pantalla sino en que el resultado **coincida con lo que hace ANSES** y que se pueda explicar norma por norma. Tiene tres características que definen la mejor estructura:

1. **La verdad está afuera del equipo.** La IA puede leer normas, pero los criterios de aplicación (días sueltos, redondeos, cortes de 1994) los conoce la práctica de ANSES y de los abogados. El **cuello de botella es la validación, no la programación.**
2. **El error es caro y silencioso.** Un mes mal contado no rompe nada, pero cambia si alguien se jubila. Por eso importan la trazabilidad y los casos de prueba con resultado conocido.
3. **La IA no tiene memoria entre sesiones.** Todo lo que no está escrito en el repositorio o en los issues se pierde.

## 2. Diagnóstico: qué está bien y qué conviene cambiar

### Lo que ya está muy bien (mantener)

| Pieza | Por qué funciona |
|-------|------------------|
| Fichas normativas antes del código, con fuente y "A VALIDAR" | Es el control humano justo donde está el riesgo |
| Motor puro y determinístico, con tests de arquitectura | Garantiza que el resultado sea auditable y repetible |
| Cuatro capas de tests (casos, invariantes, trazabilidad, arquitectura) | Un cambio de criterio no puede pasar sin que se note |
| Agentes separados (investigador, arquitecto, revisor) | Cada uno mira con ojos nuevos y con un límite claro de lo que puede hacer |
| Issues + milestones + script de estado | Cualquier sesión nueva sabe qué sigue |

### Problemas encontrados (con evidencia)

| # | Problema | Evidencia | Riesgo |
|---|----------|-----------|--------|
| P1 | **Material clave fuera del repositorio** | La "guía con 5 ejemplos" y la "hoja de preguntas" (bloques A, B, C) que mencionan los issues #11 y #14 no están en el repo | La IA no puede usarlos; el issue #14 se resolvió con los casos de las fichas en su lugar |
| P2 | **Preguntas al abogado en muchos archivos** | `preguntas-previsionalista.md`, `preguntas-R004/5/6.md`, la hoja externa, las repreguntas A2-bis y A5-bis | Se pierde el hilo de qué se preguntó, qué se respondió y qué falta |
| P3 | **Copias del repo desincronizadas** | Al empezar esta sesión la copia local estaba 4 commits atrás (justo los de las respuestas del abogado) | La IA trabaja sobre información vieja |
| P4 | **Herramientas faltantes en esta PC** | `gh` no está instalado: no se pueden crear issues ni generar el estado | El tablero deja de ser la memoria compartida |
| P5 | **Investigación muy adelantada respecto de la validación** | R003 a R006 investigadas a fondo; R001 y R002 todavía sin validar | Trabajo que puede quedar viejo o rehacerse cuando lleguen las respuestas |
| P6 | **Una sola fuente de validación, y es un programa** | Las respuestas del abogado describen cómo calcula BlueCorp, no cómo resuelve ANSES | Copiar el criterio de un programa comercial, con sus errores |
| P7 | **Estado duplicado** | "Estado actual" en `CLAUDE.md` y `docs/estado-del-proyecto.md` | Se contradicen con el tiempo |
| P8 | **La Web no tiene tests** | La pantalla ya tiene ejemplos, informe y pronto guardar casos | Errores de presentación que nadie detecta (ADR 0007 propone el proyecto de tests) |

## 3. Estructura recomendada

La estructura actual es buena. **No hace falta reorganizarla, sino completarla con cuatro piezas** que atacan el cuello de botella real (validar contra ANSES):

1. **Banco de casos reales anonimizados ("casos dorados").** Historias laborales reales de clientes, con los datos personales tapados, **junto con el cómputo que hizo ANSES** (resolución, PRPA/PRPB o liquidación). Es la prueba definitiva: si el programa da lo mismo que ANSES en 20 casos reales, el criterio está validado, digan lo que digan los programas comerciales. Van a `tests/.../CasosDorados/` **solo después de anonimizar** y con tu aprobación caso por caso (regla de privacidad). Es lo que más valor agrega por hora del abogado.
2. **Un solo canal de preguntas** (resuelve P1 y P2). Cada pregunta tiene un código fijo (`Q-001`...), un estado (abierta / respondida / validada) y la ficha a la que afecta. La hoja de cada reunión se **genera** desde ahí, no se escribe aparte. Las notas de cada reunión van a `docs/conocimiento/reuniones/AAAA-MM-DD.md` (con palabras propias, sin copiar material de terceros).
3. **Límite de trabajo en curso para la investigación** (resuelve P5). No más de **dos fichas en "investigación"** esperando al abogado. Mientras tanto, la IA invierte en lo que no depende de él: producto (informe, guardar casos, distribución), calidad y preparación de casos concretos para la reunión.
4. **Dos validadores por regla** (resuelve P6). "Validada por" pide dos abogados, o un abogado más un caso dorado que coincida.

Además, ajustes chicos: sacar el "Estado actual" de `CLAUDE.md` y dejar solo el enlace a `docs/estado-del-proyecto.md` (P7), y aprobar el proyecto de tests de la Web (P8, ADR 0007).

## 4. Tu rol

Tu hipótesis, **analista funcional entre los abogados y la IA**, es correcta pero se queda corta. Hoy la IA ya hace la mayor parte de la traducción (normativa → ficha → código). Donde nadie te puede reemplazar es en lo que pasa **fuera** del repositorio.

### Recomendación: Product Owner con foco en validación

| Responsabilidad | Qué significa en la práctica | Tiempo sugerido |
|-----------------|------------------------------|-----------------|
| **Conseguir la verdad del dominio** | Reuniones con abogados, conseguir casos dorados anonimizados, traer respuestas tal cual | ~40 % |
| **Decidir** | Aprobar o rechazar ADR, fichas y prioridades. Un "sí" o "no" claro destraba más que cualquier código | ~20 % |
| **Probar como usuario** | Seguir los pasos de prueba manual, y mirar el programa junto a un abogado | ~25 % |
| **Planificar** | Repaso semanal, reordenar la cola, ajustar fechas | ~15 % |

### Otras opciones, por si preferís otra forma de participar

| Rol | Cuándo conviene | Qué cambia |
|-----|-----------------|------------|
| **Analista funcional puro** | Si los abogados prefieren hablar con vos y no leer documentos | Vos escribís el requisito en lenguaje del abogado y la IA lo pasa a ficha. Te quita tiempo de validación |
| **QA / tester de aceptación** | Si conseguís muchos casos reales | Te enfocás en comparar el programa contra resoluciones de ANSES. Es la mitad más valiosa del rol recomendado |
| **Coordinador de comunidad** (más adelante, v0.4 en adelante) | Cuando haya varios abogados probando | Juntar opiniones, priorizar pedidos, sumar validadores. Es el camino para que el proyecto sobreviva sin depender de una persona |

Lo que **no** conviene que hagas: revisar código línea por línea (para eso están los tests y el revisor) o investigar normativa (el investigador lo hace con fuentes). Tu tiempo vale más del lado de los abogados.

## 5. Cómo comunicarte con Claude para que el proyecto tenga continuidad

### Al empezar y al terminar cada sesión

- Empezá con **`/inicio-de-sesion`** (comando nuevo). La IA trae los cambios del remoto, lee el estado, te muestra en pocas líneas qué está bloqueado y qué va a hacer, y arranca.
- Cerrá con **`/cierre-de-sesion`**. La IA corre los tests, commitea y sube, regenera el estado y te deja **qué tenés que hacer vos** y **cómo probar lo nuevo**.

### Cuando traés respuestas de un abogado

- Pegalas **tal cual**, aunque sean desordenadas o una transcripción de audio, con **`/procesar-respuestas`**. Indicá quién respondió y la fecha.
- Si podés, referí cada respuesta al código de la pregunta (A2-bis, Q-014). Si no, la IA las ubica.
- Si el abogado mostró algo (un PDF de ANSES, una captura de BlueCorp), **tapá los datos personales** antes de pasarlo.

### Cuando tenés que decidir

- Respondé con la decisión **explícita**: "Apruebo ADR 0007 con la opción 2 de nombre" o "No: no quiero guardar el nombre del cliente". Un "dale" suelto puede ser ambiguo dentro de varias propuestas.
- Si no estás seguro, pedí "explicame la diferencia con un ejemplo": la IA tiene que poder explicarlo sin jerga.

### Lo que conviene evitar

- Dejar material importante fuera del repo (hojas, guías, ejemplos): si la IA lo necesita, subilo a `docs/conocimiento/` (sin datos reales ni material con derechos de autor de terceros) o pegalo en el chat.
- Mezclar varios temas grandes en un mismo pedido sin orden: decí cuál va primero.
- Trabajar desde dos PCs sin `git pull`: `/inicio-de-sesion` lo hace solo, pero si editás a mano, sincronizá antes.

### Instalar en cada PC donde trabajes con la IA

- GitHub CLI (`winget install GitHub.cli` y después `gh auth login`): sin esto la IA no puede crear issues ni actualizar el estado.
- Python 3 (para `herramientas/estado_del_proyecto.py`).

## 6. Qué necesito que apruebes

1. Las cuatro piezas de la sección 3 (casos dorados, canal único de preguntas, límite de investigación, dos validadores).
2. El rol recomendado (sección 4) o el que prefieras.
3. Los comandos `/inicio-de-sesion` y `/cierre-de-sesion` (ya creados en `.claude/commands/`; si no los querés, se borran).

Con tu "sí", la IA actualiza `CLAUDE.md`, `docs/plan-del-proyecto.md` y unifica las preguntas en un solo archivo.
