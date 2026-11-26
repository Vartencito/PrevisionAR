# Verificación independiente de R001 y R002

- **Fecha de consulta de todas las fuentes:** 2026-11-26
- **Alcance:** fuentes citadas en "Norma y fuente", casos de prueba y normas que faltan citar.
- **Qué no es:** no valida las fichas ni decide criterios. Lo que sigue abierto lo tiene que contestar un previsionalista.
- **Referencias:** ✅ coincide con la fuente · ⚠️ depende de una convención no respaldada · ❌ error.

## R001 — Cómputo de servicios

### Fuentes

| Norma citada | Link | Estado | Observación |
|---|---|---|---|
| Ley 24.241, art. 19 inc. c) | [argentina.gob.ar](https://www.argentina.gob.ar/normativa/nacional/ley-24241-639/actualizacion) | ✅ | Abre. Pide 30 años de servicios con aportes en el sistema de reciprocidad. Hay una nota sobre el Decreto 1306/2000 (abrogado por la Ley 26.222) que no cambia el inciso. |
| Ley 24.241, art. 22 | Ídem | ⚠️ | El contenido que resume la ficha es correcto: se computan los servicios del sistema y también los anteriores, solo hasta que se pide la PBU. **Pero el artículo no remite a la Ley 18.037**, aunque la ficha lo dice en la fila de la Ley 18.037 (ver hallazgo 2). |
| Decreto 679/95, art. 2 | [ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) | ✅ | Está en el cuerpo del decreto (no en el anexo). En dependencia habla de aportes "devengado y retenido"; en autónomos, de cotizaciones "devengado e ingresado". |
| Decreto 679/95, reglamentación del art. 19 | Ídem | ✅ | Ap. 2: 360 meses (120 meses para tareas discontinuas). El texto vigente viene del Decreto 460/1999, que la ficha no menciona (hallazgo 6). |
| Decreto-Ley 9.316/46, art. 2 | [InfoLEG](https://servicios.infoleg.gob.ar/infolegInternet/anexos/230000-234999/233426/texact.htm) | ⚠️ | La norma es la correcta (2/4/1946) y dice "no se acumularán los tiempos de servicios". Habla de servicios simultáneos en distintas Secciones o Cajas, es decir, del sistema de reciprocidad. InfoLEG solo tiene el texto original con algunas notas, así que no se puede confirmar si sigue vigente. La ficha ya lo deja "a confirmar". |
| Ley 18.037 t.o. 1976, arts. 14 a 16 | [InfoLEG t.o.](https://servicios.infoleg.gob.ar/infolegInternet/anexos/150000-154999/154617/texact.htm) | ⚠️ | Art. 14: se computa desde los 16 años ✅. Art. 15: los trabajos continuos se cuentan desde que empiezan hasta que cesan ✅. Art. 16: un día por jornada legal aunque se trabaje para distintos empleadores, sin pasar del tiempo calendario ni de 12 meses por año ✅. **La frase "no se acumulan los tiempos" no está en el t.o.**: viene del texto original (hallazgo 4). La ley está derogada por la Ley 24.241, art. 168, y se aplica en forma supletoria por el art. 156. La ficha no menciona ninguna de las dos cosas (hallazgo 2). El t.o. lo aprobó la Resolución SSS 522/76. |
| Ley 18.037 texto original | [InfoLEG](https://servicios.infoleg.gob.ar/infolegInternet/anexos/25000-29999/28127/norma.htm) | ✅ | Ley del 30/12/1968. Su art. 15 dice que se computa desde los 18 años y que los servicios simultáneos no se acumulan. |
| Decreto 1645/78 | [BO CABA](https://boletinoficial.buenosaires.gob.ar/normativaba/norma/50213) | ❌ | **Está mal identificado.** No reglamenta la Ley 18.037: crea el régimen previsional de la Municipalidad de la Ciudad de Buenos Aires, a cargo del Instituto Municipal de Previsión Social. La regla de despreciar las fracciones menores de un mes está en el art. 25 inc. e). Ese artículo trata de cómo determinar la edad cuando hay servicios en distintos regímenes. No dice nada sobre ANSES (hallazgo 1). |
| Ley 24.241, arts. 23, 24 y 30; Decreto 679/95, art. 30 | Texto actualizado | ⚠️ | Art. 23 inc. b): servicios hasta la vigencia del Libro I ✅. Art. 24 inc. a): la fracción mayor de 6 meses cuenta como año ✅. El art. 30 se titula "Opción de los afiliados" (texto de la Ley 26.222) y la PAP está en su **inc. b)**. Del Decreto 679/95 corresponde citar la "reglamentación del art. 30", que toma los meses desde julio de 1994 inclusive. El Decreto 56/94 (Libro I vigente desde el 15/07/1994) se nombra, pero sin link: [ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000056_1994_01_19), vigente, art. 1 ✅. |

### Casos de prueba

Se recalcularon a mano contando las dos fechas (desde y hasta) y con meses calendario completos.

| # | Recalculado | Estado | Motivo |
|---|---|---|---|
| 1 | 01/01/2000–31/12/2009 = 10a 0m 0d = 120 m | ⚠️ | La cuenta da bien. Depende de contar las dos fechas, que es la pregunta 2. Si la fecha hasta no se contara, daría 119 m y 30 d. |
| 2 | 60 + 60 = 120 m | ⚠️ | La cuenta da bien. Misma convención de fechas inclusive. |
| 3 | 60 m; B no computa | ⚠️ | La cuenta da bien. Que B quede afuera sale del Dec. 679/95, art. 2 ✅. Las fechas inclusive siguen siendo convención. |
| 4 | 120 m; superposición 01/01/2003–31/12/2004 | ⚠️ | La cuenta da bien. No acumular tiempo entre dos dependencias tiene respaldo en el art. 16 del t.o. (aplicado en forma supletoria). Las fechas inclusive son convención. |
| 5 | 120 m; superposición 01/01/2004–31/12/2005 | ⚠️ | Igual que el caso 4. |
| 6 | 01/01/2016–31/12/2025 = 120 m | ⚠️ | La cuenta da bien. Además de las fechas inclusive, contar el propio día de cálculo (criterio 6) es convención: el art. 22 dice "hasta el momento de solicitar", sin aclarar si ese día cuenta. |
| 7 | Corte en 31/12/2025 = 120 m | ⚠️ | Igual que el caso 6. |
| 8 | Error | ✅ | Validación de datos. No necesita norma. |
| 9 | 01/03–15/03/2000 = 15 d, 0 m | ⚠️ | La cuenta da bien. Es la convención de días sueltos (pregunta 1) y la ficha ya la marca "a validar". |
| 10 | Tramo 1: 01/01/1990–30/06/1994 = 54 m. Tramo 2: 01/07/1994–31/12/1999 = 66 m. Total 120 | ⚠️ | La cuenta da bien. El corte el 01/07 sale del Dec. 679/95 (PAP), pero para la PC el corte de la ley es el 15/07/1994 (pregunta 8). Con ese corte, el tramo 1 sería 54 m y 14 d. |
| 11 | n = 60, p = 60, m = 60; total 180 | ⚠️ | La cuenta da bien. La clasificación n/m/p sale del curso, no de una norma (notas de dominio). No acumular tiempo entre dependencia y autónomo es la pregunta 4, que sigue abierta. |

## R002 — Requisitos de la PBU

### Fuentes

| Norma citada | Link | Estado | Observación |
|---|---|---|---|
| Ley 24.241, art. 19 incs. a), b), c) | [InfoLEG](https://servicios.infoleg.gob.ar/infolegInternet/anexos/0-4999/639/texact.htm) | ✅ | 65 años los hombres, 60 las mujeres y 30 años de servicios con aportes. |
| Art. 19, compensación | Ídem | ✅ | Dos años de edad de más compensan uno de servicios que falte, solo para llegar al mínimo de servicios. |
| Art. 19, opción de la mujer | Ídem | ✅ | La mujer puede seguir trabajando hasta los 65. El artículo remite a la escala del art. 128. |
| CCyC, art. 6 | [argentina.gob.ar](https://www.argentina.gob.ar/normativa/nacional/norma-235975/actualizacion) | ✅ | Ley 26.994. Los plazos de meses y años se cuentan de fecha a fecha. Si falta el día equivalente, el plazo vence el último día del mes. Vence a la hora 24 y una ley puede disponer otro modo. No tiene notas de modificación. Que se aplique a la edad jubilatoria sigue siendo la pregunta 15. |
| Decreto 679/95, reglamentación del art. 19 | [ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) | ✅ | Ap. 2: 360 meses. Ap. 5: la fracción de edad, aunque sea "menor de UN (1) año", también compensa. No dice cómo redondear. Texto del Decreto 460/1999. |
| Ley 24.241, art. 37 | InfoLEG (ídem) | ✅ | La escala termina en 65/60 desde 2001 (hasta 2011 en la tabla), así que coincide con el art. 19. |
| Vigencia (texto de la ficha) | — | ⚠️ | La ficha dice que la Ley 26.222 quedó "con vigencia suspendida en ese punto". Según InfoLEG, lo que se suspendió por orden judicial (Decreto 438/01) fue el **Decreto 1306/2000**, y la Ley 26.222 lo abrogó (hallazgo 5). La conclusión no cambia: el art. 19 sigue con 65/60 y 30 años. |

### Casos de prueba (fecha de cálculo 01/01/2026)

| # | Recalculado | Estado | Motivo |
|---|---|---|---|
| 1 | Nacido el 15/03/1960: 65a 9m. 1990–2019 = 360 m. Tiene derecho | ✅ | No toca casos límite de edad ni de días. |
| 2 | Nacida el 10/05/1965: 60a 7m. 360 m. Tiene derecho | ✅ | |
| 3 | Nacido el 01/06/1961: 64a 7m, le faltan 5 m. 1985–2019 = 420 m. No tiene derecho | ✅ | Que la compensación no funcione al revés sale del texto del art. 19, que solo compensa servicios faltantes. |
| 4 | Nacido el 01/01/1961: cumple 65 el 01/01/2026. Tiene derecho | ⚠️ | Que el día del cumpleaños ya cuente sale del CCyC, art. 6, pero aplicarlo a la edad jubilatoria es la pregunta 15. Detalle: el art. 6 hace vencer el plazo a la hora 24 de ese día. |
| 5 | Nacido el 01/01/1955: 71a 0m, 72 m de exceso. 1980–2006 = 324 m, faltan 36. 72 ÷ 2 = 36. Tiene derecho | ✅ | División exacta en meses. La fracción de edad tiene respaldo en el Dec. 679/95, ap. 5. |
| 6 | Nacido el 01/01/1958: 36 m de exceso, compensa 18, faltan 18. No tiene derecho | ✅ | División exacta. |
| 7 | Nacida el 01/08/1960: 65a 5m, 65 m de exceso. 1990–2016 = 324 m, faltan 36. 65 ÷ 2 = 32,5 | ⚠️ | "No tiene derecho" se mantiene con cualquier redondeo. Pero "faltan 4" depende de truncar: sin redondear quedan 3,5 y redondeando hacia arriba quedan 3. Es la pregunta 13. |
| 8 | Error | ✅ | Validación de datos. |

## Hallazgos (de más grave a menos grave)

1. **Grave. El Decreto 1645/78 está mal identificado (R001 y pregunta 1).** La ficha lo presenta como reglamentario de la Ley 18.037 y como una pista sobre cómo trabaja ANSES. En realidad crea el régimen previsional municipal de la Ciudad de Buenos Aires (Instituto Municipal de Previsión Social). Además, la regla de las fracciones menores de un mes (art. 25 inc. e) solo rige para determinar la edad cuando hay servicios en distintos regímenes. No sirve como antecedente de ANSES. Hay que corregir la fila de la tabla, la convención "Días sueltos" y la pregunta 1.
2. **Grave. Está mal citado de dónde viene la remisión a la Ley 18.037 (R001).** El art. 22 de la Ley 24.241 no menciona la Ley 18.037. Lo que hace aplicable la ley vieja es el **art. 156** (la aplica en forma supletoria si no se opone a la nueva), y el **art. 168** la deroga. Además, la reglamentación del art. 22 (Dec. 679/95) dice que los servicios anteriores se reconocen según la Ley 24.241 y su reglamentación. La sección "Vigencia" de R001 no dice que la Ley 18.037 está derogada. Esto afecta el respaldo de las reglas de fechas inclusive, tope calendario y simultaneidad: valen solo si no son incompatibles con la Ley 24.241, y eso tiene que confirmarlo un previsionalista (relacionado con las preguntas 3 y 4).
3. **Medio. Los casos de R001 dependen de la convención de fechas inclusive.** La ficha dice que los casos 1 a 8 no dependen de convenciones porque usan meses completos. Es cierto para los días sueltos, pero 1–7, 10 y 11 dependen de contar las dos fechas (pregunta 2) y los casos 6 y 7, además, de contar el día de cálculo. Lo que respalda las fechas inclusive es una frase de la Ley 18.037 aplicada en forma supletoria (ver hallazgo 2).
4. **Medio. Se le atribuye al t.o. 1976 una frase que no tiene.** "No se acumularán los tiempos" está en el art. 15 del texto original de 1968, no en el t.o. 1976. En el t.o., el efecto equivalente sale del art. 16. La ficha mezcla los dos textos en la misma fila.
5. **Bajo. La nota de vigencia de R002 está mal descripta.** Lo suspendido por orden judicial fue el Decreto 1306/2000, y la Ley 26.222 lo abrogó. La Ley 26.222 no tiene vigencia suspendida en ese punto.
6. **Bajo. Falta citar el Decreto 460/1999**, que modificó el Decreto 679/95. Los textos vigentes de la reglamentación del art. 19 (incluidos los 120 meses para tareas discontinuas) vienen de ahí. Afecta las dos fichas.
7. **Bajo. Faltan precisiones en las citas.** El art. 30 de la Ley 24.241 se titula "Opción de los afiliados" y la PAP está en su inc. b). Lo del 01/07/1994 es la "reglamentación del art. 30" del Dec. 679/95, no el art. 30 del decreto. El Decreto 56/94 se nombra sin link en la tabla.
8. **Bajo. Hay una posible ambigüedad entre R001 y R002.** R002 pide "meses completos" para los 360, mientras que R001 convierte 30 días sueltos en 1 mes. No queda claro si R002 usa el total de R001 con esos días ya convertidos. Va con las preguntas 1 y 14.
9. **Bajo. Faltan normas.** En R001: Ley 24.241 arts. 156 y 168, Decreto 56/94 (con link) y Decreto 460/1999. En R002: Decreto 460/1999. No se encontró ninguna resolución de ANSES o de la SSS que diga cómo pasar días a meses en el cómputo (búsqueda general en la web y en el Boletín Oficial). No se buscó jurisprudencia de la CSJN ni de la CFSS.
