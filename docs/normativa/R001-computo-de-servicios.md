# R001 — Cómputo de servicios con aportes (régimen general)

- **Estado:** Borrador
- **Validada por:** (pendiente: responsable del proyecto y previsionalista)
- **Alcance MVP:** puntos 1, 2 y 3 (carga de historia laboral, cómputo de servicios, superposiciones)

## Norma y fuente

| Norma | Qué aporta | Fuente |
|-------|-----------|--------|
| Ley 24.241, art. 19 inc. c) | Para la PBU se exigen 30 años de servicios con aportes computables en uno o más regímenes del sistema de reciprocidad | [Texto actualizado](https://www.argentina.gob.ar/normativa/nacional/ley-24241-639/actualizacion) |
| Ley 24.241, art. 22 | Son computables los servicios del SIJP y los prestados antes; el cómputo comprende solo las actividades desarrolladas hasta el momento de solicitar la PBU | Ídem |
| Decreto 679/95, art. 2 | Define "servicios con aportes": en relación de dependencia, los períodos en que se devengaron y retuvieron aportes y contribuciones; en autónomos, los períodos en que se devengaron e ingresaron las cotizaciones | [Biblioteca AFIP/ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) |
| Decreto 679/95, reglamentación art. 19 | Los 30 años equivalen a **360 meses** de servicios con aportes | Ídem |
| Decreto-Ley 9.316/46, art. 2 | En servicios simultáneos **no se acumulan los tiempos de servicio** (sí las remuneraciones, a otros fines) | [InfoLEG](https://servicios.infoleg.gob.ar/infolegInternet/anexos/230000-234999/233426/texact.htm) |

## Vigencia

- Ley 24.241: desde 15/07/1994 (Libro I), vigente con modificaciones.
- Decreto 679/95: vigente.
- Decreto-Ley 9.316/46, art. 2: a confirmar vigencia y aplicación actual por previsionalista (ver preguntas abiertas).

## Modificaciones relevantes

- Decreto 475/2021 incorporó el art. 22 bis (cómputo por hijos). **Fuera de esta ficha**: irá en una ficha propia.

## Excepciones (fuera de esta ficha)

- Servicios con tareas discontinuas (Decreto 679/95 admite mínimos distintos).
- Servicios acreditados por declaración jurada (art. 38, con topes por año de cese).
- Moratorias y regímenes especiales.
- Cómputo por hijos (art. 22 bis).
- Compensación de edad por servicios faltantes (va en R002, requisitos).

## Enunciado

Dada la historia laboral de una persona (lista de períodos de trabajo), el motor calcula el **tiempo total de servicios con aportes** hasta una fecha de cálculo:

1. Solo cuentan los períodos **con aportes**. Los períodos sin aportes se informan aparte y no suman.
2. Si dos o más períodos se **superponen** en el tiempo (trabajos simultáneos), ese tramo se cuenta **una sola vez**.
3. Un período sin fecha de fin (trabajo actual) se computa hasta la **fecha de cálculo**.
4. Nada posterior a la fecha de cálculo se computa.
5. El resultado se expresa en **años, meses y días**, y también en **meses totales** (unidad que usa el Decreto 679/95).
6. Cada superposición detectada se informa (qué períodos, qué tramo, cuántos días se descontaron).

## Datos de entrada

**Período de servicio:**
- Empleador o actividad (texto)
- Fecha desde (obligatoria)
- Fecha hasta (opcional: vacía = sigue trabajando)
- Tipo: relación de dependencia | autónomo
- Con aportes: sí | no

**Parámetros del cálculo:**
- Fecha de cálculo (obligatoria; en la práctica, la de solicitud de la PBU)

**Validaciones:**
- Fecha hasta anterior a fecha desde → error.
- Fecha desde posterior a la fecha de cálculo → el período no computa y se avisa.

## Convención de cómputo propuesta (A VALIDAR)

No encontré en la normativa revisada una regla explícita sobre cómo convertir días a meses y años. Propuesta técnica, a confirmar por un previsionalista:

- Ambas fechas del período son **inclusive** (del 01/03/2000 al 31/03/2000 = 1 mes completo).
- Se cuentan **meses calendario completos**; los días sueltos que no completan un mes se acumulan como días.
- **30 días sueltos = 1 mes**; **12 meses = 1 año**.

## Criterios de aceptación

1. **Período único.** Dado un período con aportes del 01/01/2000 al 31/12/2009, cuando calculo al 01/01/2026, entonces el total es 10 años, 0 meses, 0 días (120 meses).
2. **Períodos sucesivos.** Dados dos períodos con aportes sin superposición, entonces el total es la suma de ambos.
3. **Período sin aportes.** Dado un período sin aportes, entonces no suma al total y figura en el detalle como "no computable: sin aportes".
4. **Superposición total.** Dado un período contenido completamente dentro de otro, entonces el total es igual al del período más largo y se informa la superposición.
5. **Superposición parcial.** Dados dos períodos que se pisan en parte, entonces el tramo común se cuenta una sola vez y se informa.
6. **Trabajo actual.** Dado un período sin fecha de fin, entonces se computa hasta la fecha de cálculo inclusive.
7. **Fecha de cálculo en medio de un período.** Dado un período que termina después de la fecha de cálculo, entonces solo se computa hasta la fecha de cálculo.
8. **Fechas inválidas.** Dado un período con fecha hasta anterior a fecha desde, entonces el cálculo informa un error que identifica el período.
9. **Trazabilidad.** Todo resultado indica que fue calculado con la regla R001.
10. **Determinismo.** Con los mismos datos y la misma fecha de cálculo, el resultado es idéntico siempre.

## Casos de prueba

Fecha de cálculo para todos: **01/01/2026** salvo que se indique otra.

| # | Entrada | Resultado esperado | Fuente del caso |
|---|---------|--------------------|-----------------|
| 1 | A: 01/01/2000–31/12/2009, con aportes | 10a 0m 0d — 120 meses | Construido (criterio 1) |
| 2 | A: 01/01/2000–31/12/2004 c/a; B: 01/01/2005–31/12/2009 c/a | 10a 0m 0d — 120 meses | Construido (criterio 2) |
| 3 | A: 01/01/2000–31/12/2004 c/a; B: 01/01/2005–31/12/2009 **sin** aportes | 5a 0m 0d — 60 meses; B informado como no computable | Construido (criterio 3) |
| 4 | A: 01/01/2000–31/12/2009 c/a; B: 01/01/2003–31/12/2004 c/a | 10a 0m 0d — 120 meses; superposición 01/01/2003–31/12/2004 | Construido (criterio 4) |
| 5 | A: 01/01/2000–31/12/2005 c/a; B: 01/01/2004–31/12/2009 c/a | 10a 0m 0d — 120 meses; superposición 01/01/2004–31/12/2005 | Construido (criterio 5) |
| 6 | A: 01/01/2016–(sin fin) c/a; fecha de cálculo 31/12/2025 | 10a 0m 0d — 120 meses | Construido (criterio 6) |
| 7 | A: 01/01/2016–31/12/2030 c/a; fecha de cálculo 31/12/2025 | 10a 0m 0d — 120 meses | Construido (criterio 7) |
| 8 | A: 01/01/2010–31/12/2009 | Error: período A con fechas invertidas | Construido (criterio 8) |
| 9 | A: 01/03/2000–15/03/2000 c/a | 0a 0m 15d — 0 meses (días sueltos) | Construido (convención, **a validar**) |

Los casos 1 a 8 usan meses completos a propósito, para no depender de la convención de días que falta validar. Cuando un previsionalista la confirme, se agregan casos con días sueltos y con un caso real anonimizado.

## Preguntas abiertas para el previsionalista

1. ¿Cómo convierte ANSES días sueltos a meses y años? ¿30 días = 1 mes? ¿Se suman días de distintos períodos para formar meses?
2. ¿Las fechas desde y hasta se cuentan ambas inclusive?
3. ¿La regla de no acumular servicios simultáneos (Decreto-Ley 9.316/46, art. 2) es la que se aplica hoy, también entre dependencia y autónomo? ¿Hay otra norma más específica?
4. ¿Hay edad mínima desde la que se computan servicios en el régimen general actual?
5. Cuando hay superposición entre un período con aportes y otro sin aportes, ¿el tramo cuenta como con aportes? (La propuesta asume que sí, porque basta un período con aportes en ese tramo.)
