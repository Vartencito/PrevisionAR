# R007 — Control del detalle del beneficio: PC y PAP

- **Estado:** Borrador con criterios y casos (A VALIDAR, no implementar hasta que el responsable lo revise)
- **Validada por:** (pendiente: responsable del proyecto y previsionalista)
- **Issue:** [#41](https://github.com/Vartencito/PrevisionAR/issues/41) (candidata, v0.3)
- **Depende de:** R005 (haber inicial), de donde salen las normas. Esta ficha usa solo la parte de R005 que **no necesita índices de actualización**.
- **Fecha de consulta de las fuentes:** 2026-11-26 y 2026-11-27 (ver R005 y `docs/conocimiento/reuniones/2026-11-27.md`).

## Para qué sirve

El abogado tiene el **detalle del beneficio** que emite ANSES al otorgar una jubilación. Ahí figuran el tiempo tomado para el cálculo, los promedios de remuneraciones y rentas, y la PC y la PAP liquidadas. Con esta regla el programa **recalcula la PC y la PAP desde esos mismos datos y marca las diferencias**. Así el abogado controla si ANSES aplicó bien la fórmula sin rehacer todo a mano.

No controla la actualización de las remuneraciones ni los promedios: los toma tal como vienen en el detalle. Ese control es de R005.

## Norma y fuente

Las fuentes con link están en la tabla "Norma y fuente" de `R005-haber-inicial.md`. Las que usa esta ficha:

| Norma | Qué aporta |
|-------|-----------|
| Ley 24.241, art. 24 inc. a) (según Ley 26.417, art. 12) | PC en dependencia: 1,5 % por año con aportes o **fracción mayor de 6 meses**, **máximo 35 años**, sobre el promedio de remuneraciones |
| Ley 24.241, art. 24 inc. b) | PC de autónomos: 1,5 % por año sobre el promedio de las rentas de las categorías aportadas |
| Ley 24.241, art. 24 inc. c) | Servicios mixtos (sucesivos o simultáneos): se combinan "en forma proporcional al tiempo computado para cada clase de servicios" |
| Ley 24.241, art. 30 inc. b) (según Ley 26.222, art. 2) | PAP: 1,5 % por año con aportes desde julio de 1994, "en igual forma y metodología" que la PC |
| Decreto 313/2007, arts. 1 inc. c) y 2 | El 1,5 % de la PAP vale para beneficios **solicitados desde el 01/07/2007**; antes era 0,85 % |
| Decreto 679/95, reglamentación del art. 30 | La PAP toma el total de meses con aportes desde 07/1994; los considerandos dicen que no hay límite de tiempo |
| Res. SSS 6/2009, art. 14 (reglamentación del art. 24, ap. 6) | Tope del haber de la PC: **0,208 haberes mínimos por cada año** computado, desde el 01/03/2009 ([texto](https://www.argentina.gob.ar/normativa/nacional/norma-150950/texto)) |

**Evidencia de ANSES** (`docs/conocimiento/casos-dorados.md`): CD-002 y CD-004 cierran al centavo con esta fórmula. CD-001 también cierra, pero sus importes no se registran.

## Vigencia

- Para beneficios **solicitados desde el 01/07/2007** (PAP al 1,5 %). Los anteriores quedan fuera: el programa avisa y no calcula.
- Tope de la PC en haberes mínimos: desde el 01/03/2009. Para beneficios anteriores, el tope no se controla.

## Excepciones (fuera de esta ficha)

- PBU, haber mínimo, haber máximo, complementos y bonificaciones (por ejemplo, zona austral).
- Actualización de remuneraciones y cálculo de los promedios (R005).
- Topes de las remuneraciones (art. 9 y Res. SSS 6/2009): ya vienen aplicados en los promedios del detalle.
- Reajustes y criterios de sentencias (liberar topes con más del 15 %, Badaro, Elliff y otros). Es lo que pidió el abogado, pero va en una etapa posterior.
- Regímenes especiales y diferenciales. Moratorias: sus meses no entran en el "tiempo tomado" del detalle (CD-004), así que no hace falta tratarlas acá.

## Enunciado

Dado lo que figura en el detalle del beneficio, el programa calcula para cada prestación (PC con el tramo anterior a 7/94, PAP con el posterior):

1. **Meses del tramo** = meses solo en dependencia (n) + solo autónomo (m) + simultáneos (p). Los simultáneos cuentan una sola vez en el tiempo.
2. **Años computables** = meses ÷ 12; si sobra una fracción **mayor** de 6 meses, suma un año. En la PC, máximo 35 años.
3. **Promedio del tramo** = (n × promedio de dependencia + m × promedio de autónomos + p × (promedio de dependencia + promedio de autónomos)) ÷ (n + m + p). Sin servicios mixtos, es el promedio de la única clase.
4. **Prestación** = 1,5 % × años computables × promedio del tramo, **truncada a centavos**.
5. **PC con tope:** si se informa el haber mínimo vigente, la PC no puede superar 0,208 × haber mínimo × años computables.
6. **Diferencia** = lo que calcula el programa − lo que liquidó ANSES, con una explicación paso a paso y la norma de cada paso.

## Datos de entrada (los del detalle del beneficio)

- Fecha de solicitud.
- Tiempo tomado para el cálculo en años y meses, antes y después del 7/94, separado en dependencia, autónomos y simultáneos.
- "Haber Rel. Dep." (promedio de dependencia) y "Haber Autónomos" (promedio de autónomos).
- PC y PAP liquidadas por ANSES (opcional: sin ellas, el programa solo calcula).
- Haber mínimo vigente a la solicitud (opcional: sin él, no se controla el tope de la PC).

**Validación:** meses negativos o un mes mayor que 11 → error. Hay meses de una clase sin su promedio → error. Solicitud anterior al 01/07/2007 → fuera de alcance.

## Convenciones (A VALIDAR)

| Punto | Decisión propuesta | Respaldo |
|-------|-------------------|----------|
| 6 meses exactos | **No** suman un año | Ley: "fracción **mayor** de seis meses". BlueCorp (CD-003) hace lo mismo. El abogado se contradijo: **A VALIDAR (P-42)** |
| Centavos | Se **truncan** | Cinco cuentas en CD-001, CD-002 y CD-004 dan truncado; ninguna da redondeado. Sin norma expresa: **A VALIDAR (P-35)** |
| Simultáneos en el promedio | Pesan con la suma de los dos promedios | CD-002 cierra al centavo así. El Anexo I del Decreto 679/95 no fue leído: **A VALIDAR (R005, P5)** |
| Simultáneos en los años | Cuentan una vez como tiempo | Ley 18.037, art. 16, en forma supletoria (no se cuenta más tiempo que el calendario; ver R001). En CD-002 no cambia el resultado: **A VALIDAR** |
| PAP sin tope de años | No se aplica el tope de 35 | Decreto 679/95 (considerandos) y art. 30, que no fija tope propio. El abogado habló de un tope de 35: **A VALIDAR (P-36)**. En la práctica nadie llega a 35 años de PAP antes de 2029 |
| Tope de la PC | Solo si se informa el haber mínimo; sin liberar por sentencia | Res. SSS 6/2009, art. 14. La liberación por confiscatoriedad queda fuera |

## Criterios de aceptación

1. **Años con fracción mayor de 6 meses.** Dado un tramo con más de 6 meses sueltos, entonces los años computables son los años enteros más 1.
2. **Años con fracción de 6 meses o menos.** Dado un tramo con 6 meses sueltos o menos, entonces se toman solo los años enteros (6 exactos: A VALIDAR).
3. **Tope de 35 años en la PC.** Dado un tramo anterior a 7/94 con más de 35 años computables, entonces la PC usa 35.
4. **Sin tope de años en la PAP.** La PAP usa todos los años computables (A VALIDAR).
5. **Una sola clase de servicios.** Dado un tramo solo en dependencia o solo autónomo, entonces el promedio es el de esa clase.
6. **Servicios mixtos.** Dado un tramo con meses de más de una clase, entonces el promedio se pondera por los meses de cada clase, y los simultáneos pesan con la suma de los dos promedios.
7. **Truncamiento.** La PC y la PAP se truncan a centavos (A VALIDAR).
8. **Tope monetario de la PC.** Dado el haber mínimo, si la PC calculada supera 0,208 × haber mínimo × años, entonces la PC es ese tope y se informa que se aplicó.
9. **Diferencia.** Dado lo liquidado por ANSES, entonces se informa la diferencia (calculado − liquidado) de la PC y de la PAP; cero si coinciden.
10. **Fuera de alcance.** Solicitud anterior al 01/07/2007 → no calcula y avisa por qué.
11. **Datos inválidos.** Meses negativos, un mes mayor que 11 o meses sin su promedio → error con un mensaje claro.
12. **Trazabilidad.** El resultado indica la regla R007 y explica cada paso con su norma.

## Casos de prueba

Los casos 1 a 4 son de **detalles reales de ANSES** (anonimizados, `casos-dorados.md`); el resultado esperado es lo que liquidó ANSES. Los demás son construidos, con promedios inventados.

| # | Datos de entrada | Resultado esperado | Fuente del caso |
|---|------------------|--------------------|-----------------|
| 1 | Solicitud 2025. Antes de 7/94: autónomos 1a 1m. Promedio de autónomos 135.415,69 | PC: 1 año; 1,5 % × 1 × 135.415,69 = 2.031,2353 → **2.031,23** | CD-004 (ANSES) |
| 2 | Solicitud 2025. Después de 7/94: dependencia 28a 0m. Promedio de dependencia 2.733.128,11 | PAP: 28 años; = 1.147.913,8062 → **1.147.913,80** | CD-004 (ANSES) |
| 3 | Solicitud 2009. Antes de 7/94: dependencia 16a 10m. Promedio de dependencia 676,68 | PC: 17 años (10 meses es fracción mayor de 6); = 172,5534 → **172,55** | CD-002 (ANSES) |
| 4 | Solicitud 2009. Después de 7/94: dependencia 9a 3m y simultáneos 0a 2m. Promedios: dependencia 676,68, autónomos 324,23 | PAP: 113 meses = 9a 5m → 9 años. Promedio = (111 × 676,68 + 2 × 1.000,91) ÷ 113 = 682,4186. PAP = 92,1265 → **92,12** | CD-002 (ANSES) |
| 5 | Antes de 7/94: dependencia 31a 7m. Promedio 1.000.000 | PC: 32 años → **480.000,00** | Construido (criterio 1; igual que el caso 1 de R005) |
| 6 | Antes de 7/94: dependencia 20a 6m. Promedio 1.000.000 | PC: 20 años → **300.000,00** (con 21 años daría 315.000,00) | Construido (criterio 2, **A VALIDAR P-42**) |
| 7 | Antes de 7/94: dependencia 38a 2m. Promedio 1.000.000 | PC: 35 años (tope) → **525.000,00** | Construido (criterio 3; igual que el caso 2 de R005) |
| 8 | Antes de 7/94: dependencia 20a 0m y autónomos 10a 0m (sucesivos). Promedios: dependencia 1.000.000, autónomos 600.000 | Promedio = (240 × 1.000.000 + 120 × 600.000) ÷ 360 = 866.666,67. PC: 30 años → **390.000,00** | Construido (criterio 6; coincide con el caso 3 de R005) |
| 9 | Igual que el caso 3, con PC liquidada 170,00 | Calculada 172,55; diferencia **+2,55** a favor del beneficiario | Construido (criterio 9) |
| 10 | Antes de 7/94: dependencia 20a 0m. Promedio 10.000.000. Haber mínimo 444.158,46 | Sin tope: 3.000.000,00. Tope: 0,208 × 444.158,46 × 20 = 1.847.699,1936 → **PC 1.847.699,19**, con aviso de tope aplicado | Construido (criterio 8; Res. SSS 6/2009, art. 14) |
| 11 | Solicitud 15/03/2006 | **Fuera de alcance:** la PAP de ese beneficio se calculaba al 0,85 % | Construido (criterio 10; Decreto 313/2007) |
| 12 | Antes de 7/94: dependencia 10a 13m | **Error:** un tramo no puede tener 13 meses (deberían ser 11a 1m) | Construido (criterio 11) |

## Preguntas abiertas

En `docs/conocimiento/preguntas-previsionalista.md`: P-35 (truncamiento), P-36 (tope de la PAP), P-42 (6 meses exactos), R005-P5 (Anexo I, servicios mixtos). Para el abogado: si el control le sirve tal como está planteado (pregunta 8.6 de `hoja-reunion-v0.1.md`).
