# R005 — Haber inicial de la jubilación ordinaria (PBU, PC y PAP)

- **Estado:** Investigación (borrador). Sin criterios de aceptación. No implementar.
- **Validada por:** (pendiente: responsable del proyecto y previsionalista)
- **Alcance:** fuera del MVP actual (cálculo del haber). Ficha de relevamiento para una etapa futura.
- **Depende de:** R001 (cómputo de servicios, con desglose por tramo y por tipo n/m/p) y R002 (requisitos de la PBU).
- **Fecha de consulta de todas las fuentes:** 2026-11-26.
- **Dudas:** `docs/conocimiento/preguntas-previsionalista.md`, sección R005 (códigos R005-n).

## Norma y fuente

| Norma | Qué aporta | Cita breve | Fuente |
|-------|-----------|------------|--------|
| Ley 24.241, art. 20 (texto según Ley 26.417, art. 4) | Haber de la PBU: suma fija en pesos. El texto vigente **no trae** el adicional por años de servicios por encima de 30 | "se establece en la suma de PESOS TRESCIENTOS VEINTISEIS ($ 326)" | [InfoLEG, texto actualizado](https://servicios.infoleg.gob.ar/infolegInternet/anexos/0-4999/639/texact.htm) |
| Ley 24.241, art. 20 (texto original de 1993) | La PBU era 2,5 AMPO con 30 años, más 1% por año adicional hasta 45 años. Sirve para entender el Decreto 679/95, que reglamenta ese texto viejo | "se incrementará en un uno por ciento (1 %) por año adicional" | [InfoLEG, texto original](https://servicios.infoleg.gob.ar/infolegInternet/anexos/0-4999/639/norma.htm) |
| Ley 24.241, art. 23 | Requisitos de la PC: los de la PBU, servicios con aportes en el sistema de reciprocidad (hasta la vigencia del Libro I) y no cobrar retiro por invalidez | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 24 inc. a) (según Ley 26.417, art. 12) | PC, todo en dependencia: 1,5% por año con aportes o fracción mayor de 6 meses, máximo 35 años, sobre el promedio de remuneraciones actualizadas de los 10 años anteriores al cese. No se computan períodos de inactividad sin remuneración | "o fracción mayor de SEIS (6) meses" | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 24 inc. b) | PC, todo autónomo: mismo 1,5% y tope de 35 años, sobre el promedio mensual de los montos actualizados de las categorías; se computa todo el tiempo con aportes en cada categoría | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 24 inc. c) | PC, servicios mixtos (sucesivos o simultáneos): se suman el haber de dependencia y el de autónomo en proporción al tiempo de cada clase | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 25 | El promedio no incluye el aguinaldo (SAC) ni lo que supere el tope del art. 9 | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 26 | Haber máximo de la PC: 1 AMPO por cada año de servicios con aportes computados | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 30 inc. b) (según Ley 26.222, art. 2) y inc. d) | PAP: 1,5% por año de servicios con aportes al régimen público, misma forma y metodología que la PC; requisitos de los incs. a) y c) del art. 23. En movilidad y otros aspectos se asimila a la PC | "en igual forma y metodología que la establecida para la Prestación Compensatoria" | [argentina.gob.ar, texto actualizado](https://www.argentina.gob.ar/normativa/nacional/ley-24241-639/actualizacion) |
| Ley 24.241, art. 32 (según Decreto 274/2024) | Movilidad mensual de todas las prestaciones del art. 17 (incluye PBU, PC y PAP) según el IPC del INDEC; nunca puede bajar el haber | "En ningún caso la aplicación de dicho índice podrá producir la disminución del haber" | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 125 | Haber mínimo garantizado, ajustado por la movilidad del art. 32 | — | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 125 bis (incorporado por Ley 27.426, art. 5) | Suplemento hasta el 82% del Salario Mínimo Vital y Móvil para quien cobra PBU con 30 años o más de aportes efectivos (excluye a quienes accedieron por moratorias) | "acrediten treinta (30) años o más de servicios con aportes efectivos" | InfoLEG, texto actualizado (ídem) |
| Ley 24.241, art. 161 (según Ley 26.222) | Se aplica la ley vigente al cese o a la solicitud, lo que ocurra primero | — | InfoLEG, texto actualizado (ídem) |
| Decreto 679/95 (texto según Decreto 460/99), reglamentación art. 20 | La fracción mayor de 6 meses cuenta como 1 año (reglamenta el incremento del texto original del art. 20) | "la fracción mayor de SEIS (6) meses será computada como UN (1) año" | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) |
| Decreto 679/95, reglamentación art. 24, ap. 1 a 5 y Anexo I | Cesación = fin de la relación o solicitud, lo que ocurra primero. Los 10 años son 120 meses con remuneraciones. Simultaneidad en dependencia: se suman remuneraciones mes a mes con tope del art. 9 (salvo lo devengado antes del 01/02/1994). Autónomos: rentas de referencia de las categorías, valores a la fecha de solicitud. Mixtos: promedios separados, máximo 420 meses computables (se descartan los menos favorables); fórmula en el Anexo I (no leído: PDF aparte) | — | Ídem |
| Decreto 679/95, reglamentación art. 30 | PAP: se toma el total de meses con aportes desde julio de 1994 inclusive, de cualquier régimen de reciprocidad. Los considerandos aclaran que no hay límite de tiempo | — | Ídem |
| Ley 26.417, art. 2 (según Ley 27.609, art. 4) | Actualización **trimestral** de las remuneraciones del art. 24 inc. a) con un índice combinado: índice de la Ley 27.260 (art. 5) + RIPTE | "actualización trimestral de las remuneraciones" | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/LEY_C_026417_2008_10_01) |
| Ley 26.417, arts. 3, 13, 14 y 15 | Rentas de referencia de autónomos ajustadas por el índice del art. 32. Las referencias al MOPRE pasan a ser proporción del haber mínimo. Transición del suplemento por movilidad. Primer ajuste: 01/03/2009 | — | Ídem |
| Ley 26.222, art. 2 | Sustituye el art. 30: la PAP pasa a 1,5% por año (antes 0,85%) | — | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/LEY_C_026222_2007_02_27) |
| Decreto 313/2007, arts. 1 inc. c) y 2 | El nuevo cálculo de la PAP rige para beneficios solicitados desde el 01/07/2007; quienes la tenían al 0,85% pueden pedir recálculo desde esa fecha | "a los beneficios solicitados a partir del 1º de julio de 2007" | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000313_2007_03_29) |
| Ley 26.425, arts. 1 y 3 | Unifica todo en el SIPA (régimen público). Los servicios prestados mientras se estaba en capitalización se toman como prestados al régimen público | "como si hubiesen sido prestados al régimen previsional público" | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/LEY_C_026425_2008_11_20) |
| Ley 27.426, arts. 3 y 5 | Modificó el índice de actualización (2017) e incorporó el art. 125 bis (82% SMVM) | — | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/LEY_C_027426_2017_12_26) |
| Disposición SSSS 10/2026 (B.O. 12/08/2026) | Índice combinado para quienes cesen desde el 31/08/2026 o soliciten desde el 01/09/2026. Valores en el Anexo IF-2026-73017425-APN-DNPSS#MCH (solo en la edición web del B.O.). Base: Decreto 104/2021, art. 6 (publicación trimestral) y Res. SSS 3/2021, Anexo I (metodología) | "cesen desde el 31 de agosto de 2026" | [argentina.gob.ar](https://www.argentina.gob.ar/normativa/nacional/norma-428831/texto) |
| Disposiciones SSSS anteriores (misma serie trimestral) | 5/2026 (B.O. 11/05/2026, ceses desde 31/05/2026); 2/2025 (B.O. 05/02/2025); 25/2024 (B.O. 11/11/2024) | — | [5/2026](https://www.argentina.gob.ar/normativa/nacional/norma-425643/texto) · [2/2025](https://www.boletinoficial.gob.ar/detalleAviso/primera/320670/1) · [25/2024](https://www.boletinoficial.gob.ar/detalleAviso/primera/316856/1) |
| Resolución ANSES 304/2026 (B.O. 28/10/2026), arts. 1 a 4 | Movilidad de noviembre 2026: 1,93%. Haber mínimo $ 444.158,46; haber máximo $ 2.988.765,82; base imponible mínima $ 149.592,45 y máxima $ 4.861.685,38; **PBU $ 203.182,22** | — | [argentina.gob.ar](https://www.argentina.gob.ar/normativa/nacional/norma-431696/texto) |

**Pendiente de verificar en fuente oficial:** un medio privado informa una **Disposición SSSS 15/2026** (B.O. 03/11/2026) con el índice para ceses desde el 30/11/2026 o solicitudes desde el 01/12/2026 ([Comercio y Justicia](https://comercioyjusticia.info/leyes-y-comentarios/oficializan-nuevo-indice-combinado-de-actualizacion-para-jubilaciones-y-haberes-que-rige-desde-diciembre-de-2026/)). No se encontró en argentina.gob.ar ni en el Boletín Oficial al 26/11/2026; no se usa hasta confirmarla.

## Vigencia

- Ley 24.241, arts. 20, 24 y 30: vigentes con los textos indicados (InfoLEG, consulta 2026-11-26).
- Ley aplicable al caso: la vigente al cese o a la solicitud, lo que ocurra primero (art. 161).
- Valores en pesos (PBU, mínimo, máximo, topes): cambian **todos los meses** por movilidad (art. 32). El último publicado al 26/11/2026 es el de **noviembre 2026** (Res. ANSES 304/2026). La de diciembre 2026 todavía no estaba publicada a esa fecha.
- Índice de actualización de remuneraciones: se publica **trimestralmente** y se elige según la fecha de cese o solicitud. Último verificado en fuente oficial: Disposición SSSS 10/2026.
- PAP al 1,5%: para beneficios solicitados desde el 01/07/2007 (Decreto 313/2007).

## Modificaciones

| Fecha (B.O.) | Norma | Qué cambió |
|--------------|-------|-----------|
| 28/06/1994 | Ley 24.347 | Sustituye el art. 24 |
| 30/03/1995 | Ley 24.463 | Incorpora incisos al art. 17 y topes mínimos/máximos |
| 1995 / 1999 | Decreto 679/95, modificado por Decreto 460/99 | Reglamentación de arts. 19, 20, 22, 24, 30, 34 bis, 35 y 38 |
| 08/03/2007 | Ley 26.222 (art. 2) | Art. 30: PAP de 0,85% a 1,5% |
| 03/04/2007 | Decreto 313/2007 | PAP al 1,5% para beneficios solicitados desde 01/07/2007 y recálculo |
| 16/10/2008 | Ley 26.417 (arts. 2, 4, 5, 12, 13) | PBU suma fija (art. 20); deroga el art. 21 (AMPO); nuevo art. 24 inc. a); índice de actualización; MOPRE reemplazado |
| 09/12/2008 | Ley 26.425 | Unificación en el SIPA |
| 28/12/2017 | Ley 27.426 (arts. 3 y 5) | Índice combinado (Ley 27.260 + RIPTE estables); art. 125 bis (82% SMVM) |
| 2020-2021 | Ley 27.609 (art. 4), Decreto 104/2021 (art. 6), Res. SSS 3/2021 | Índice combinado con RIPTE, actualización trimestral, metodología |
| 25/03/2024 | Decreto 274/2024 (art. 1) | Art. 32: movilidad mensual por IPC, desde julio 2024 |

## Excepciones (fuera de esta ficha)

- Moratorias (afectan PBU y el art. 125 bis).
- Regímenes especiales y diferenciales.
- PEA, retiro por invalidez, pensiones, PUAM.
- Beneficios anteriores a la Ley 26.417 o a 01/07/2007 (PAP al 0,85%).
- Reajustes judiciales (Quiroga, Badaro, Elliff y otros): ver notas de dominio. Esta ficha solo describe lo que dice la norma.

## Enunciado preliminar (sin criterios de aceptación)

Lenguaje claro, solo lo que dicen las normas. Todo lo marcado "duda" está en `preguntas-previsionalista.md` (códigos R005-n).

**PBU.** Monto fijo, igual para todos los que cumplen los requisitos de R002. Hoy lo publica ANSES cada mes; noviembre 2026: $ 203.182,22. El texto vigente del art. 20 no da un plus por años por encima de 30 (duda P1).

**PC.** Para los servicios con aportes **anteriores** al corte del Libro I (corte exacto: duda abierta, pregunta 8 de `preguntas-previsionalista.md`):

- Años = años de servicios con aportes del tramo; si sobra una fracción **mayor** de 6 meses, suma 1 año. Máximo 35.
- Solo dependencia: PC = 1,5% × años × promedio W. W = promedio de las remuneraciones actualizadas de los 120 meses con remuneración dentro de los 10 años antes del cese, sin aguinaldo y con tope del art. 9.
- Solo autónomo: PC = 1,5% × años × promedio R. R = promedio de las rentas de referencia de las categorías en que estuvo, a valores de la fecha de solicitud, tomando todo el tiempo aportado.
- Mixtos: se suman la parte de dependencia y la de autónomo en proporción al tiempo de cada clase, con tope de 420 meses (se descartan los menos favorables). Fórmula exacta en el Anexo I del Decreto 679/95, aún no leído (duda P5).
- Tope de la PC: 1 AMPO por año (art. 26). El AMPO ya no existe; qué valor lo reemplaza es duda (P7).

**PAP.** Para los meses con aportes **desde julio de 1994 inclusive**: PAP = 1,5% × años × promedio, con la misma metodología que la PC. No tiene tope de años según los considerandos del Decreto 679/95. Si la fracción de 6 meses y el tope del art. 26 se aplican también a la PAP: dudas P3 y P7.

**Actualización de remuneraciones.** Cada trimestre la Subsecretaría de Seguridad Social publica en el Boletín Oficial un índice combinado (índice Ley 27.260 + RIPTE). Se usa el que corresponde a la fecha de cese o solicitud. Los coeficientes están en un anexo que solo sale en la edición web del B.O.

**Después del cálculo.** El haber no puede ser menor que el mínimo ni mayor que el máximo vigentes (noviembre 2026: $ 444.158,46 y $ 2.988.765,82). Con 30 años o más de aportes efectivos (sin moratoria) se garantiza el 82% del SMVM (art. 125 bis). En qué orden se aplican: duda P8.

## Casos tomados de fuentes

Datos ficticios. Promedios inventados para que la cuenta sea simple; **no** incluyen tope del art. 26 ni haber mínimo/máximo, salvo que se diga. Cada caso solo aplica un artículo.

| # | Datos de entrada | Resultado esperado | Sale de | Estado |
|---|------------------|--------------------|---------|--------|
| 1 | PC, todo en dependencia. Servicios con aportes antes del corte: 31 años y 7 meses. Promedio W = $ 1.000.000 | Años computables = 32 (7 meses es fracción mayor de 6). PC = 1,5% × 32 × 1.000.000 = **$ 480.000** | Ley 24.241, art. 24 inc. a): "o fracción mayor de SEIS (6) meses" | Directo de la ley |
| 2 | PC, todo en dependencia. Servicios con aportes antes del corte: 38 años y 2 meses. W = $ 1.000.000 | Años computables = 35 (tope). PC = 1,5% × 35 × 1.000.000 = **$ 525.000** | Ley 24.241, art. 24 inc. a): "hasta un máximo de TREINTA Y CINCO (35) años" | Directo de la ley |
| 3 | PC, servicios mixtos sucesivos antes del corte: 20 años en dependencia (W = $ 1.000.000) y 10 años como autónomo (R = $ 600.000) | Lectura literal "proporcional al tiempo": 1,5% × 20 × 1.000.000 + 1,5% × 10 × 600.000 = 300.000 + 90.000 = **$ 390.000** | Ley 24.241, art. 24 inc. c): "en forma proporcional al tiempo computado para cada clase de servicios" | **A VALIDAR** (Anexo I del Decreto 679/95 no leído; P5) |
| 4 | PAP. Meses con aportes desde 07/1994: 180 (15 años justos). Promedio = $ 1.000.000 | PAP = 1,5% × 15 × 1.000.000 = **$ 225.000** | Ley 24.241, art. 30 inc. b): 1,5% por año, "en igual forma y metodología" que la PC | Directo de la ley |
| 5 | PBU, solicitud en noviembre 2026. Cumple requisitos de R002 con 40 años de servicios con aportes | PBU = **$ 203.182,22**, sin plus por los 10 años por encima de 30 | Ley 24.241, art. 20 (Ley 26.417): suma fija; Res. ANSES 304/2026, art. 4 | **A VALIDAR** (el Decreto 679/95 reglamenta un plus que el texto vigente ya no menciona; P1) |

Casos que **no** se armaron porque dependen de una duda: 6 meses exactos de fracción (P2), PAP con fracción de meses (P3), tope del art. 26 (P7), corte 01/07–14/07/1994 (pregunta 8 general).
