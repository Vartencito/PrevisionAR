# R006 — Prestaciones para quienes no llegan a la PBU: PUAM y Prestación por Edad Avanzada (PEA)

- **Estado:** Borrador con criterios y casos (A VALIDAR, no implementar)
- **Validada por:** (pendiente: responsable y previsionalista; se revisa en la reunión del issue #23, bloque E)
- **Alcance:** fuera del MVP actual (el MVP solo verifica la PBU, R002). Sirve para informar al abogado qué alternativa evaluar cuando R002 da "no tiene derecho".
- **Depende de:** R001 (cómputo de servicios con aportes) y R002 (requisitos de la PBU).
- **Fecha de consulta de todas las fuentes:** 26/11/2026. Criterios y casos armados el 27/11/2026 con las mismas fuentes (no se agregaron normas nuevas).

## Norma y fuente

### A. PUAM — Pensión Universal para el Adulto Mayor

Una aclaración: en el texto actualizado de argentina.gob.ar la PUAM está en el **Libro I, Título III** (arts. 13 a 17) de la Ley 27.260, no en el Título II.

| Norma | Qué aporta | Cita breve | Fuente |
|-------|-----------|------------|--------|
| Ley 27.260, art. 13 (primer párrafo e inc. 1 a 5) | Pensión vitalicia y no contributiva. Requisitos: 65 años o más (sin distinción de sexo); argentino nativo o por opción; naturalizado con 10 años de residencia legal antes de la solicitud; extranjero con 20 años de residencia legal, de los cuales 10 inmediatamente anteriores a la solicitud; no cobrar jubilación, pensión o retiro (contributivo o no); no cobrar la prestación por desempleo (Ley 24.013); mantener la residencia en el país. Quien cobra una única prestación puede optar por la PUAM | "para todas las personas de sesenta y cinco (65) años de edad o más" | [argentina.gob.ar, texto actualizado](https://www.argentina.gob.ar/normativa/nacional/263691/actualizacion) · [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/LEY_C_027260_2016_06_29) |
| Ley 27.260, art. 13, último párrafo (agregado por Ley 27.467, art. 37, B.O. 04/12/2018) | ANSES hace una evaluación socioeconómica y patrimonial previa, con criterios objetivos de la reglamentación, para priorizar a los más vulnerables | — | Ídem |
| Ley 27.260, art. 14 | Monto: 80 % del haber mínimo garantizado (Ley 24.241, art. 125). Se actualiza con la movilidad del art. 32 de la Ley 24.241 | "ochenta por ciento (80%) del haber mínimo garantizado" | Ídem |
| Ley 27.260, art. 15 | Personalísima, vitalicia, **no genera pensión** a derechohabientes, inembargable salvo alimentos (hasta 20 %) | — | Ídem |
| Ley 27.260, art. 16 (sustituido por Ley 27.467, art. 38) | Incompatible con trabajo en relación de dependencia o por cuenta propia, salvo monotributistas sociales inscriptos en el Registro de Efectores. El texto original de 2016 decía lo contrario: era compatible y los aportes contaban como servicios | "es incompatible con el desempeño de cualquier actividad en relación de dependencia" | Ídem |
| Ley 27.260, art. 17 | Cobertura de PAMI (INSSJP) | — | Ídem |
| Circular ANSES DP 27/16 (12/07/2016; copia aportada por el responsable) | Instrucciones internas al lanzar la PUAM, con el texto original de 2016: compatible con trabajo en dependencia o autónomo, y esos aportes cuentan como servicios. Confirma el cambio posterior de la Ley 27.467. Monto de entonces: $ 3.967,18 (80 % del mínimo) | Copia de la circular (fuente secundaria; histórica) |
| Decreto 894/2016, arts. 13 a 15 | ANSES otorga la PUAM; los organismos públicos le pasan datos para controlar edad y residencia | — | [Boletín Oficial 28/07/2016](https://www.boletinoficial.gob.ar/detalleAviso/primera/148620/20160728) |
| Resolución ANSES 56/2023 (B.O. 16/05/2023) | Aplica a la PUAM la evaluación patrimonial y socioeconómica del art. 2 de la RESGC 5345/2023 (AFIP-ANSES) y deja sin efecto los criterios propios de 2019 (Res. ANSES DGDNYP 17/2019) | — | [Boletín Oficial](https://www.boletinoficial.gob.ar/detalleAviso/primera/286477/20230510) |
| Resolución ANSES 918/2024 (B.O. 07/10/2024) | Para prestaciones no contributivas que exigen residencia sin plazo fijo: ausencia del país de más de 90 días corridos = no cumple la residencia; se suspende el pago | "NOVENTA DÍAS (90) días corridos" | [Boletín Oficial](https://www.boletinoficial.gob.ar/detalleAviso/primera/315235/20241007) |
| Resolución ANSES 211/2025, art. 5 (B.O. 28/04/2025) | Ejemplo numérico: mayo 2025, haber mínimo $296.481,74 y PUAM $237.185,39 (= 80 % exacto) | — | [Boletín Oficial](https://www.boletinoficial.gob.ar/pdf/aviso/primera/324510/20250527) |
| Derecho Fácil (Ministerio de Justicia), guía PUAM, actualizada 26/01/2023 | Resumen oficial en lenguaje simple. Agrega asignaciones familiares. Dice "ni tener derecho" a otra prestación, algo que el art. 13 no dice con esas palabras (ver pregunta) | "No cobrar ni tener derecho a jubilación, retiro o cualquier otra pensión." | [argentina.gob.ar](https://www.argentina.gob.ar/node/159819) |

### B. PEA — Prestación por Edad Avanzada (Ley 24.241, art. 34 bis)

| Norma | Qué aporta | Cita breve | Fuente |
|-------|-----------|------------|--------|
| Ley 24.241, art. 34 bis, apartado 1 (incorporado por Ley 24.347, B.O. 28/06/1994) | Crea la prestación para trabajadores en relación de dependencia y autónomos | — | [InfoLEG, texto actualizado](https://servicios.infoleg.gob.ar/infolegInternet/anexos/0-4999/639/texact.htm) |
| Ley 24.241, art. 34 bis, apartado 2, inc. a) | 70 años, sin distinción de sexo | "Hubieran cumplido setenta (70) años, cualquiera fuera su sexo" | Ídem |
| Ley 24.241, art. 34 bis, apartado 2, inc. b) | 10 años de servicios con aportes computables en el sistema de reciprocidad, con al menos 5 años de servicios en los 8 años inmediatamente anteriores al **cese de la actividad** | "Acrediten diez (10) años de servicios con aportes computables" | Ídem |
| Ley 24.241, art. 34 bis, apartado 2, inc. c) | Autónomos: además, antigüedad en la afiliación de al menos 5 años | — | Ídem |
| Ley 24.241, art. 34 bis, apartado 3 | Haber: 70 % de la PBU (art. 17 inc. a), más PC y PAP (o jubilación ordinaria, según corresponda). La pensión derivada se rige por arts. 28 y 98 | "equivalente al setenta por ciento (70 %) de la prestación" | Ídem |
| Ley 24.241, art. 34 bis, apartado 4 | Incompatible con toda jubilación, pensión o retiro civil o militar (nacional, provincial o municipal); se puede optar | "es incompatible con la percepción de toda jubilación, pensión o retiro" | Ídem |
| Ley 24.241, art. 34 bis, apartado 5 | Retiro por invalidez y pensión por fallecimiento del afiliado en actividad hasta los 65 años. Si un afiliado mayor de 65 se incapacita, va a la PEA; si fallece, la pensión es el 70 % de lo que correspondía al causante | — | Ídem |
| Ley 24.241, art. 17 inc. f) (incorporado por Ley 24.463) | Incluye la PEA en la lista de prestaciones del sistema | — | Ídem |
| Decreto 679/95, reglamentación art. 34 bis, apartado 1 | Los 5 años del inc. b) también tienen que ser **servicios con aportes** (la ley sola dice "prestación de servicios") | — | [Biblioteca ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) |
| Decreto 679/95, reglamentación art. 34 bis, apartado 2 | Autónomos: la antigüedad en la afiliación (no menor a 5 años) se cuenta desde la afiliación formal y expresa; períodos anteriores no cuentan aunque se hayan pagado. Solo cuentan años con al menos la mitad de los aportes del año pagos, sin contar pagos por intimación ni planes de regularización | "no inferior a CINCO (5) años" | Ídem |
| Guía de trámite "Obtener la prestación por edad avanzada", actualizada 04/05/2023 | Resumen oficial. Agrega como requisito **no acreditar tareas como personal de casas particulares** (no figura en el art. 34 bis) | — | [argentina.gob.ar](https://www.argentina.gob.ar/servicio/obtener-la-prestacion-por-edad-avanzada) |

## Vigencia

- **PUAM:** vigente al 26/11/2026 con el texto de la Ley 27.467 (arts. 13 y 16). Sin cambios en edad, residencia ni monto desde 2016.
  - En 2025 el Congreso sancionó el proyecto 27.792 (entre otras cosas: 60 años para mujeres, compatibilidad con otras prestaciones y con trabajo, y fin de la evaluación socioeconómica). Fue **vetado completo** por el Decreto 534/2025 ([Boletín Oficial](https://www.boletinoficial.gob.ar/pdf/aviso/primera/329236/20250819)). El listado oficial de leyes insistidas de Diputados, actualizado al 29/10/2025, no la incluye ([HCDN](https://www3.hcdn.gob.ar/dependencias/secparl/dgral_info_parlamentaria/dip/leyes-insistidas/leyes_insistidas_final.pdf)), y la crónica parlamentaria de la sesión del 21/08/2025 indica que ese veto no llegó a votarse ([Parlamentario](https://www.parlamentario.com/tag/moratoria-previsional/), fuente no oficial). **Confirmar** que no hubo insistencia después de octubre de 2025.
  - Evaluación socioeconómica: criterios de la RESGC 5345/2023, aplicados por Res. ANSES 56/2023. **Confirmar** si siguen vigentes después de que terminó la moratoria de la Ley 27.705 (marzo de 2025).
- **PEA:** vigente al 26/11/2026. El art. 34 bis no tiene modificaciones desde 1994 según InfoLEG. La nota de InfoLEG menciona el Decreto 1306/2000 (abrogado) y la Ley 26.222 (con una parte de su vigencia suspendida); igual que en R002, **confirmar** que no cambian este artículo.

## Modificaciones

| Fecha | Norma | Qué cambió |
|-------|-------|-----------|
| 28/06/1994 | Ley 24.347, art. 3 | Incorpora el art. 34 bis (PEA) a la Ley 24.241 |
| 11/05/1995 | Decreto 679/95 | Reglamenta el art. 34 bis (servicios con aportes, afiliación de autónomos) |
| 22/07/2016 | Ley 27.260 | Crea la PUAM |
| 04/12/2018 | Ley 27.467, arts. 37 y 38 | PUAM: agrega la evaluación socioeconómica y vuelve incompatible el trabajo |
| 16/05/2023 | Res. ANSES 56/2023 | PUAM: nuevos criterios patrimoniales (remite a RESGC 5345/2023) |
| 07/10/2024 | Res. ANSES 918/2024 | Residencia: más de 90 días fuera del país suspende la prestación |
| 04/08/2025 | Decreto 534/2025 | Veta el proyecto 27.792 (no modifica la PUAM) |

No se encontraron cambios en la PEA en 2024-2026.

## Excepciones (fuera de esta ficha)

- PEA de trabajadores rurales (trámite propio en argentina.gob.ar) y regímenes especiales o diferenciales.
- Personal de casas particulares: la guía oficial lo excluye de la PEA general. Falta encontrar la norma (ver preguntas).
- Moratorias: si sus servicios sirven para la PEA no está resuelto acá.
- Cálculo del haber (70 % de PBU + PC + PAP, y el monto de la PUAM): fuera del MVP.
- Evaluación socioeconómica y patrimonial de la PUAM: la hace ANSES con sus bases; el motor no puede verificarla.
- PEA por invalidez después de los 65 (apartado 5): necesita dictamen de comisión médica.

## Enunciado preliminar (lenguaje claro, A VALIDAR)

Cuando R002 dice que una persona **no tiene derecho a la PBU** (no llega a los 30 años de aportes, ni con la compensación por edad), el motor podría informar si cumple los requisitos de alguna de estas dos alternativas:

**PEA (art. 34 bis):**
1. Tiene 70 años o más (hombres y mujeres por igual).
2. Tiene al menos 10 años de servicios con aportes (resultado de R001).
3. De esos, al menos 5 años con aportes están dentro de los 8 años inmediatamente anteriores al cese.
4. Si fue autónomo: además, al menos 5 años de antigüedad en la afiliación, contada como dice el Decreto 679/95.
5. No cobra otra jubilación, pensión o retiro (o elige la PEA).

**PUAM (Ley 27.260, art. 13):**
1. Tiene 65 años o más (hombres y mujeres por igual).
2. Es argentino nativo o por opción; o naturalizado con 10 años de residencia legal antes de pedirla; o extranjero con 20 años de residencia legal, de los cuales los últimos 10 son inmediatamente anteriores al pedido.
3. No cobra jubilación, pensión o retiro, ni prestación por desempleo (si cobra una sola prestación, puede elegir la PUAM).
4. Vive en el país y no trabaja (salvo monotributo social).
5. Pasa la evaluación socioeconómica de ANSES (el motor no la verifica: solo la informa como pendiente).

Los servicios no son requisito de la PUAM: es no contributiva.

**Relación con R002:** la PBU pide 65/60 años y 30 años de aportes, con compensación de edad. La PEA pide más edad (70) y menos servicios (10). La PUAM no pide servicios. Quien no llega a R002 puede caer en la PEA (si tiene 70 y servicios recientes), en la PUAM (si tiene 65 y cumple residencia) o en ninguna. Elegir entre ellas es decisión del abogado.

## Criterios de aceptación

Los criterios marcados **A VALIDAR** dependen de la pregunta indicada. "R006-n" es la pregunta n de `docs/conocimiento/preguntas-previsionalista.md (sección R006)`; "P-n" es la pregunta n de `docs/conocimiento/preguntas-previsionalista.md`. Fecha de cálculo = fecha de solicitud.

**PEA (Ley 24.241, art. 34 bis)**

1. **Edad.** Dada una persona con 70 años o más a la fecha de cálculo, sea varón o mujer, entonces cumple la edad de la PEA. El día del cumpleaños ya la cumple (misma convención que R002; **A VALIDAR P-15**). *(Art. 34 bis, ap. 2 inc. a)*
2. **Diez años con aportes.** Dados 120 meses o más con aportes según R001, entonces cumple los servicios. Con 119 o menos, no. Por ahora no se suman meses de moratoria ni años por hijos (**A VALIDAR R006-5 y R006-6**). *(Art. 34 bis, ap. 2 inc. b)*
3. **Cinco de los últimos ocho.** Dados 60 meses o más con aportes dentro de los 96 meses inmediatamente anteriores al cese, entonces cumple. Propuesta: cese = último día del último período con aportes, y la cuenta se hace en meses. **A VALIDAR R006-1, R006-2, R006-3 y R006-17.** *(Art. 34 bis, ap. 2 inc. b; Decreto 679/95, regl. art. 34 bis, ap. 1)*
4. **Autónomos.** Dada una persona con servicios autónomos, entonces además tiene que tener 5 años o más de antigüedad en la afiliación. El motor no la calcula: la recibe como dato cargado por el abogado (años contados como dice el Decreto 679/95). **A VALIDAR R006-7.** *(Art. 34 bis, ap. 2 inc. c; Decreto 679/95, regl. art. 34 bis, ap. 2)*
5. **Incompatibilidad.** Dada una persona que cumple 1 a 4 y cobra una jubilación, pensión o retiro, entonces el motor informa "cumple los requisitos, pero es incompatible: puede optar". *(Art. 34 bis, ap. 4)*
6. **Sin compensación por edad.** Dada una persona con más de 70 años y menos de 120 meses, entonces el exceso de edad no compensa servicios. **A VALIDAR R006-4.** *(El art. 19 prevé la compensación solo para la PBU)*
7. **Casas particulares.** Dados servicios como personal de casas particulares, entonces el motor muestra una advertencia (la guía oficial los excluye) y no da un resultado firme. **A VALIDAR R006-8.**
8. **Relación con R002.** El motor evalúa la PEA aunque la PBU se cumpla, y muestra los dos resultados sin recomendar. **A VALIDAR R006-10.**

**PUAM (Ley 27.260, arts. 13 a 17)**

9. **Edad.** Dada una persona con 65 años o más, sea varón o mujer, entonces cumple la edad de la PUAM. *(Art. 13, primer párrafo)*
10. **Nacionalidad y residencia.** Argentino nativo o por opción: cumple. Naturalizado: cumple con 10 años o más de residencia legal antes de la solicitud. Extranjero: cumple con 20 años o más de residencia legal, de los cuales los últimos 10 son inmediatamente anteriores a la solicitud. Los años se cuentan de fecha a fecha (**A VALIDAR P-15**). *(Art. 13, inc. 1)*
11. **Otras prestaciones.** Dada una persona que cobra jubilación, pensión, retiro o prestación por desempleo, entonces no cumple. Si cobra una sola prestación, el motor informa que puede optar por la PUAM. *(Art. 13)*
12. **Trabajo.** Dada una persona que trabaja en relación de dependencia o por cuenta propia, entonces no cumple, salvo que sea monotributista social inscripta en el Registro de Efectores. *(Art. 16, texto de la Ley 27.467)*
13. **Evaluación socioeconómica.** Siempre se informa como "pendiente: la hace ANSES". El motor no la verifica. *(Art. 13, último párrafo; Res. ANSES 56/2023)*
14. **Derecho a otra prestación no pedida.** Dada una persona que cumple la PEA o la PBU pero no la cobra, entonces el motor informa los dos resultados y advierte que la guía oficial pide "no tener derecho" a otra prestación. **A VALIDAR R006-11.**
15. **Ausencias del país.** Dada una ausencia de más de 90 días corridos, entonces el motor advierte (Res. ANSES 918/2024) y no decide. **A VALIDAR R006-14.**
16. **Trazabilidad.** Cada requisito aparece con su resultado (cumple, no cumple, pendiente o a revisar), la norma y la regla R006.

## Casos de prueba

Fecha de cálculo para todos: **01/01/2026** (igual que R002). "Servicios" = períodos con aportes en relación de dependencia, salvo que se diga otra cosa. El cese es la fecha de fin del último período. Todos los datos son ficticios.

| # | Persona | Datos | Resultado esperado | Fuente del caso |
|---|---------|-------|--------------------|-----------------|
| 1 | Hombre, nacido 01/01/1955 (71 años) | Servicios 01/01/2016–31/12/2025 (120 meses). Cese 31/12/2025. En los 8 años anteriores (2018–2025): 96 meses. No cobra otra prestación | **Cumple PEA**: 70 años, 10 años con aportes, 5 de los últimos 8. Para R002 **no** cumple PBU: le faltan 240 meses; los 72 meses de edad excedente compensan 36 y siguen faltando 204 | Ley 24.241, art. 34 bis, ap. 2 incs. a) y b); R002 |
| 2 | Mujer, nacida 01/06/1956 (69 años y 7 meses) | Mismos servicios que el caso 1 | **No cumple PEA**: le faltan 5 meses para los 70 (la edad es igual para ambos sexos). Por edad podría evaluarse la PUAM (65 o más) | Ley 24.241, art. 34 bis, ap. 2 inc. a) ("cualquiera fuera su sexo"); Ley 27.260, art. 13 |
| 3 | Hombre, nacido 01/01/1950 (76 años) | Servicios 01/01/1990–31/12/1995 (72 meses) y 01/01/2014–31/12/2017 (48 meses). Total 120 meses. Cese 31/12/2017 | **No cumple PEA**: en los 8 años antes del cese (2010–2017) tiene 48 meses, menos de 5 años. Da igual si los 8 años se cuentan desde el cese o desde la solicitud | Ley 24.241, art. 34 bis, ap. 2 inc. b); Decreto 679/95, regl. art. 34 bis, ap. 1 |
| 4 | Hombre, nacido 01/01/1950 (76 años) | Servicios 01/01/2006–31/12/2015 (120 meses). Cese 31/12/2015. No trabajó después | **A VALIDAR.** Si los 8 años se cuentan hacia atrás desde el cese de 2015 (2008–2015): 96 meses, **cumple**. Si se cuentan desde la solicitud (2018–2025): 0 meses, **no cumple**. La ley dice "cese de la actividad" pero no lo define para la PEA | Ley 24.241, art. 34 bis, ap. 2 inc. b); Decreto 679/95 (define "cese" solo para el art. 24) |
| 5 | Mujer argentina nativa, nacida 01/03/1963 (62 años y 10 meses) | Sin prestación, sin trabajo, vive en el país | **No cumple PUAM**: le faltan 2 años y 2 meses para los 65 (la edad es igual para ambos sexos; el proyecto que la bajaba a 60 fue vetado) | Ley 27.260, art. 13, primer párrafo; Decreto 534/2025 |
| 6 | Hombre extranjero, nacido 01/01/1959 (67 años) | Residencia legal en el país 01/01/2000–31/12/2015 y desde 01/01/2018 hasta hoy (24 años en total; 8 años inmediatamente anteriores). Sin prestación, sin trabajo | **No cumple PUAM**: tiene más de 20 años de residencia, pero solo 8 inmediatamente anteriores a la solicitud (pide 10) | Ley 27.260, art. 13 inc. 1 |
| 7 | Hombre, nacido 01/01/1956 (70 años exactos) | Mismos servicios que el caso 1 | **Cumple PEA** (borde: cumple los 70 ese mismo día) | Art. 34 bis, ap. 2 inc. a); convención de R002 (**A VALIDAR P-15**) |
| 8 | Hombre, nacido 01/01/1955 (71 años) | Servicios 01/02/2016–31/12/2025 (119 meses). Cese 31/12/2025 | **No cumple PEA**: le falta 1 mes para los 120 | Art. 34 bis, ap. 2 inc. b) |
| 9 | Hombre, nacido 01/01/1954 (72 años) | Servicios 01/01/2008–31/12/2012 (60 meses) y 01/01/2021–31/12/2025 (60 meses). Total 120. Cese 31/12/2025 | **Cumple PEA** (borde): en los 8 años anteriores al cese (2018–2025) tiene 60 meses justos | Art. 34 bis, ap. 2 inc. b); Decreto 679/95, regl. art. 34 bis, ap. 1 |
| 10 | Hombre, nacido 01/01/1954 (72 años) | Servicios 01/01/2008–31/01/2013 (61 meses) y 01/02/2021–31/12/2025 (59 meses). Total 120. Cese 31/12/2025 | **No cumple PEA**: 59 meses en los últimos 8 años (falta 1) | Ídem |
| 11 | Hombre autónomo, nacido 01/01/1955 (71 años) | Aportes autónomos 01/01/2016–31/12/2025 (120 meses). Antigüedad en la afiliación computable cargada por el abogado: 3 años | **No cumple PEA**: le faltan 2 años de antigüedad en la afiliación. Cómo se obtiene ese dato: **A VALIDAR R006-7** | Art. 34 bis, ap. 2 inc. c); Decreto 679/95, regl. art. 34 bis, ap. 2 |
| 12 | Hombre, nacido 01/01/1955 (71 años) | Mismos servicios que el caso 1. Cobra una pensión por fallecimiento | **Cumple los requisitos de la PEA, pero es incompatible** con la pensión: el abogado evalúa la opción | Art. 34 bis, ap. 4 |
| 13 | Hombre, nacido 01/01/1946 (80 años: 120 meses por encima de los 70) | Servicios 01/05/2018–31/12/2025 (92 meses; faltan 28). Cese 31/12/2025 | Propuesta: **no cumple PEA** (sin compensación). Si se aplicara el 2 a 1, 120 ÷ 2 = 60 meses compensarían los 28 y cumpliría. **A VALIDAR R006-4** | Art. 34 bis (no prevé compensación); Ley 24.241, art. 19 |
| 14 | Mujer, nacida 01/01/1955 (71 años) | 120 meses con aportes como personal de casas particulares, 2016–2025 | El motor **advierte** que la guía oficial excluye a este personal de la PEA; no da resultado firme. **A VALIDAR R006-8** | Guía "Obtener la prestación por edad avanzada" (04/05/2023) |
| 15 | Mujer argentina nativa, nacida 01/01/1961 (65 años exactos) | Sin prestación, sin trabajo, vive en el país | **Cumple los requisitos que el motor verifica** para la PUAM (borde: cumple los 65 ese día). Evaluación socioeconómica: **pendiente (ANSES)** | Ley 27.260, art. 13 y último párrafo |
| 16 | Hombre naturalizado, nacido 01/01/1959 (67 años) | Residencia legal desde 01/01/2016 (10 años justos al 01/01/2026). Sin prestación, sin trabajo | **Cumple** residencia (borde, contando de fecha a fecha: **A VALIDAR P-15**). Evaluación socioeconómica pendiente | Ley 27.260, art. 13 inc. 1 |
| 17 | Igual que el 16 | Residencia legal desde 02/01/2016 | **No cumple** residencia: le falta 1 día para los 10 años | Ídem |
| 18 | Hombre extranjero, nacido 01/01/1959 (67 años) | Residencia legal continua desde 01/01/2001 (25 años). Sin prestación, sin trabajo | **Cumple** residencia: más de 20 años y los últimos 10 continuos | Ídem |
| 19 | Mujer argentina nativa, nacida 01/01/1960 (66 años) | Trabaja en relación de dependencia | **No cumple PUAM** (incompatible). Si en cambio fuera monotributista social inscripta en el Registro de Efectores, **sería compatible** | Ley 27.260, art. 16 (texto de la Ley 27.467) |
| 20 | Hombre argentino nativo, nacido 01/01/1958 (68 años) | Cobra solo una pensión no contributiva por invalidez | El motor informa que **puede optar** por la PUAM | Ley 27.260, art. 13 (opción de quien cobra una única prestación) |
| 21 | Hombre argentino nativo, nacido 01/01/1955 (71 años) | Mismos servicios que el caso 1 (cumple PEA), sin ninguna prestación ni trabajo | El motor informa **cumple PEA** y **cumple los requisitos verificables de la PUAM**, con una advertencia: la guía oficial pide no tener derecho a otra prestación. **A VALIDAR R006-11** | Ley 27.260, art. 13 ("no ser beneficiario"); guía Derecho Fácil |
| 22 | Mujer argentina nativa, nacida 01/01/1956 (70 años) | En 2025 estuvo 120 días corridos fuera del país. Sin prestación, sin trabajo | El motor **advierte** sobre la ausencia de más de 90 días; no decide. **A VALIDAR R006-14** | Res. ANSES 918/2024 |

Casos que faltan y dependen de respuestas: persona con servicios por moratoria (R006-6), mujer con años por hijos que pide la PEA (R006-5 y P-25) y paso de la PUAM a una jubilación (R006-15).

## Preguntas abiertas para el previsionalista

En `docs/conocimiento/preguntas-previsionalista.md`, sección R006 (códigos R006-n).
