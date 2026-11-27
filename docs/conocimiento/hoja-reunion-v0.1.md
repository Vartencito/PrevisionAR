# Hoja para la próxima reunión con el abogado: lo que destraba la v0.1

Para imprimir. Unos 30 minutos. Armada el 2026-11-27 desde `preguntas-previsionalista.md`, con los códigos de cada pregunta.
Objetivo: cerrar los criterios que faltan para la primera versión de prueba (cómputo de servicios y requisitos de la PBU).

**Qué llevar:** esta hoja, la [comparación de métodos de días](comparacion-metodos-de-dias-R001.md) impresa y, si se puede, un informe del programa impreso con un ejemplo.

---

## 1. Días sueltos y meses (A2-bis, P-1, P-14) — lo más importante

En un acuerdo real de ANSES (caso CD-001, anonimizado) vimos esto:

| | Antes del 7/94 | Después del 7/94 |
|--|----------------|------------------|
| Tiempo informado | 15 años, 4 meses, 19 días | 31 años, 5 meses, 0 días |
| Tiempo tomado para el cálculo | 15 años, 4 meses | 31 años, 5 meses |

O sea: **para el cálculo, ANSES descarta los días sueltos.**

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 1.1 | ¿Siempre es así? ¿Los días sueltos nunca suman para llegar a los 360 meses? (Ejemplo: 359 meses y 20 días, ¿no alcanza?) | |
| 1.2 | ¿Cómo llega ANSES a los "19 días": cuenta meses de 30 días (como BlueCorp) o meses del calendario? Mirar la tabla de la hoja de comparación: ¿qué columna coincide con ANSES? | |
| 1.3 | Febrero completo (del 01/02 al 28/02), ¿cuenta como un mes entero? | |
| 1.4 | Si una persona tiene días sueltos en **varios** trabajos (20 días en uno, 15 en otro), ¿ANSES los suma entre sí antes de descartar los que sobran? | |

## 2. Fracciones de año para la PC y la PAP (P-9)

En CD-001, la PC se calculó con 15 años (descartó 4 meses) y la PAP con 31 (descartó 5 meses).

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 2.1 | Con **más** de 6 meses (por ejemplo, 15 años y 7 meses), ¿se toma como 16 años? ¿En la PC y en la PAP? | |
| 2.2 | ¿Y con 6 meses exactos? | |

## 3. Corte de julio de 1994 (P-8)

ANSES separa "antes del 7/94" y "después del 7/94".

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 3.1 | Un trabajo del 01/07/1994 al 14/07/1994, ¿va a la PC (antes) o a la PAP (después)? | |

## 4. Compensación de edad por servicios (P-13)

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 4.1 | Si a alguien le sobran 5 meses de edad, ¿compensan 2 meses de servicios, 2 meses y medio o 3 meses? ¿Tiene algún caso real de ANSES con compensación? | |

## 5. Edad (P-15)

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 5.1 | ¿Se puede pedir el **mismo día** del cumpleaños 65 (o 60)? En CD-001 se pidió al día siguiente | |
| 5.2 | Alguien nacido un 29 de febrero, ¿cumple el 28 de febrero o el 1 de marzo en los años no bisiestos? | |

## 6. Sobre el caso CD-001 (P-29, P-30, P-31)

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 6.1 | El total "tomado para el cálculo" dice 46 años y 5 meses, pero 15a 4m + 31a 5m = 46a 9m. ¿Error de impresión, o hay un tope o descuento que no se ve? | |
| 6.2 | La resolución cita las leyes 24.476, 25.865, 25.994 y 27.705 (moratorias) aunque no usó moratoria. ¿Es un texto que ANSES pone siempre? | |
| 6.3 | ¿Tiene otros casos parecidos (resolución de ANSES + historia laboral) que nos pueda pasar con los datos personales tapados? Sirven más los que están cerca de los 30 años, tienen días sueltos o trabajos superpuestos | |

## 7. El informe del programa (C4, issue #17)

Mostrarle un informe impreso.

| # | Pregunta | Respuesta |
|---|----------|-----------|
| 7.1 | ¿Lo usaría tal como está (para el cliente, para el expediente, para él)? | |
| 7.2 | ¿Qué le falta o qué le sobra? | |
| 7.3 | ¿Qué datos de la persona debería tener (nombre, CUIL, expediente) o conviene que no los tenga? | |

---

**Después de la reunión:** pasarle las respuestas a la IA tal cual, con `/procesar-respuestas`, indicando la fecha y quién respondió.
