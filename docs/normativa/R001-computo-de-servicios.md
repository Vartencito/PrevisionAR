# R001 — Cómputo de servicios con aportes (régimen general)

- **Estado:** Borrador — **implementada** con la convención propuesta (pendiente de validar con previsionalista)
- **Código:** `src/PrevisionAR.Motor/Computo/ComputoDeServicios.cs` · **Tests:** `tests/PrevisionAR.Motor.Tests/Computo/`
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
| Ley 18.037 (t.o. 1976), cómputo de tiempo | Régimen anterior, al que remite el art. 22 de la Ley 24.241 para servicios previos. Trabajos continuos: la antigüedad corre desde la fecha de inicio hasta la de cese. Simultaneidad: no se acumulan los tiempos. Se computa un día por jornada legal. **No se computa más tiempo que el calendario entre las fechas ni más de 12 meses dentro de un año calendario.** El t.o. 1976 fija los 16 años como edad desde la que se computan servicios (el texto original de 1968 decía 18) | [InfoLEG t.o. 1976](https://servicios.infoleg.gob.ar/infolegInternet/anexos/150000-154999/154617/texact.htm) · [Texto original](https://servicios.infoleg.gob.ar/infolegInternet/anexos/25000-29999/28127/norma.htm) |
| Decreto 1645/78 (reglamentario de la Ley 18.037) | Para el prorrateo de edades con servicios mixtos dispone que en los cómputos finales se desprecian las fracciones menores de un mes. Es otro contexto, pero es una pista sobre cómo trata ANSES las fracciones | [Copia en el Boletín Oficial CABA](https://boletinoficial.buenosaires.gob.ar/normativaba/norma/50213) |
| Ley 24.241, arts. 17, 24 y 30 | La PC usa servicios hasta el 30/06/1994 y la PAP desde el 01/07/1994; por eso el cómputo se separa en esos dos tramos | Texto actualizado (ver arriba). Detalle en ficha futura de PC/PAP |

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
7. **Desglose para fórmulas futuras.** El total en meses se separa:
   - por tramo: **hasta el 30/06/1994** y **desde el 01/07/1994** (los usarán la PC y la PAP);
   - por tipo, dentro de cada tramo: **n** = solo relación de dependencia, **m** = solo autónomo, **p** = simultáneo dependencia y autónomo.
   - En **días calendario** se cumple siempre: n + m + p = total del tramo, y tramo 1 + tramo 2 = total. Expresado en años/meses/días puede haber una diferencia de días sueltos si un mismo mes calendario queda repartido entre dos tipos.

   Este punto no decide ningún derecho: solo ordena el tiempo ya computado para que las fichas de PC y PAP lo usen después.

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

## Convención de cómputo

| Punto | Decisión | Respaldo |
|-------|----------|----------|
| Fechas inclusive | Desde y hasta se cuentan las dos | Ley 18.037: la antigüedad corre "desde la fecha de iniciación… hasta la de cesación". **Confirmar** con previsionalista |
| Tope calendario | Nunca más tiempo que el calendario ni más de 12 meses por año calendario | Ley 18.037. El algoritmo lo cumple por construcción: cuenta cada día del calendario una sola vez |
| Meses completos | Se cuentan meses calendario cubiertos enteros | Propuesta técnica |
| Días sueltos | **30 días sueltos = 1 mes** | **A VALIDAR.** No hay norma nacional explícita encontrada. El IPS (provincia de Bs. As.) usa mes de 30 días; el Decreto 1645/78 desprecia fracciones menores de un mes en otro contexto |
| Años | 12 meses = 1 año | — |

La conversión de días sueltos está aislada en el código (`ConvencionDeComputo`) para poder cambiarla sin tocar el resto.

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
11. **Desglose por tramo.** Dado un período que cruza el 01/07/1994, entonces se parte en dos: hasta el 30/06/1994 y desde el 01/07/1994, y la suma de ambos da el total.
12. **Desglose por tipo.** Dados un período en dependencia y otro autónomo que se superponen, entonces el tramo común se informa como **p**, y el resto como **n** o **m** según corresponda.

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
| 10 | A: 01/01/1990–31/12/1999 c/a, dependencia | Total 120 meses. Hasta 30/06/1994: 54 meses (n=54). Desde 01/07/1994: 66 meses (n=66) | Construido (criterio 11) |
| 11 | A: 01/01/2000–31/12/2009 c/a, dependencia; B: 01/01/2005–31/12/2014 c/a, autónomo | Total 180 meses, todo desde 01/07/1994: n=60 (2000–2004), p=60 (2005–2009), m=60 (2010–2014) | Construido (criterio 12) |

Los casos 1 a 8 usan meses completos a propósito, para no depender de la convención de días que falta validar. Cuando un previsionalista la confirme, se agregan casos con días sueltos y con un caso real anonimizado.

## Preguntas abiertas para el previsionalista

Están en `docs/conocimiento/preguntas-previsionalista.md` (preguntas 1 a 9). Cuando se respondan, se actualiza esta ficha y se agregan casos con días sueltos y un caso real anonimizado.
