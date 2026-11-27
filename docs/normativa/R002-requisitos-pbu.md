# R002 — Requisitos de edad y servicios para la PBU (régimen general)

- **Estado:** Borrador — implementada con los puntos marcados "A VALIDAR" (pendiente de revisión del responsable y de un previsionalista)
- **Validada por:** (pendiente)
- **Alcance MVP:** punto 4 (verificación de requisitos básicos, sin moratorias ni regímenes especiales)
- **Depende de:** R001 (cómputo de servicios con aportes)

## Norma y fuente

| Norma | Qué aporta | Fuente |
|-------|-----------|--------|
| Ley 24.241, art. 19 inc. a) | Hombres: 65 años de edad cumplidos | [InfoLEG, texto actualizado](https://servicios.infoleg.gob.ar/infolegInternet/anexos/0-4999/639/texact.htm) |
| Ley 24.241, art. 19 inc. b) | Mujeres: 60 años de edad cumplidos | Ídem |
| Ley 24.241, art. 19 inc. c) | 30 años de servicios con aportes computables en uno o más regímenes del sistema de reciprocidad | Ídem |
| Ley 24.241, art. 19, párrafo de compensación | Solo para acreditar el mínimo de servicios de la PBU, el exceso de edad compensa la falta de servicios: **2 años de edad excedentes por 1 año de servicios faltantes** | Ídem |
| Ley 24.241, art. 19, opción de la mujer | Las mujeres pueden optar por seguir trabajando hasta los 65 años | Ídem |
| Código Civil y Comercial, art. 6 | Los plazos de años y meses se cuentan de fecha a fecha; sin día equivalente, vencen el último día del mes | [argentina.gob.ar](https://www.argentina.gob.ar/normativa/nacional/norma-235975/actualizacion) |
| Decreto 679/95, reglamentación art. 19 | Los 30 años equivalen a **360 meses** de servicios con aportes. Cualquier fracción de edad excedente, aunque sea menor a un año, compensa servicios faltantes en la misma proporción | [Biblioteca AFIP/ARCA](https://biblioteca.afip.gob.ar/dcp/DEC_C_000679_1995_05_11) |
| Ley 24.241, art. 37 | Escala transitoria de edades por año (gradualismo). Sus escalones terminaron: hoy coinciden con los 65/60 del art. 19 | InfoLEG (ídem) |

## Vigencia

- Ley 24.241, art. 19: vigente. El Decreto 1306/2000 intentó modificarlo; su aplicación fue suspendida judicialmente y luego la Ley 26.222 lo abrogó. Las edades y los 30 años no cambiaron. El Decreto 460/1999 modificó el Decreto 679/95. **Confirmar** con previsionalista.
- Gradualismo (arts. 37 y 128): agotado para solicitudes actuales. Fuera de esta ficha.

## Excepciones (fuera de esta ficha)

- Moratorias (Leyes 24.476, 25.994, 26.970, 27.705).
- Cómputo por hijos (art. 22 bis, Decreto 475/2021).
- Regímenes especiales y diferenciales (edades y servicios distintos).
- Prestación por edad avanzada, retiro por invalidez, pensiones.
- Servicios por declaración jurada (art. 38).
- Opción de la mujer de seguir hasta los 65: no cambia si tiene derecho; afecta otras prestaciones. Se informa como dato, no se calcula.

## Enunciado

Dada una persona (fecha de nacimiento y sexo según el art. 19) y su historia laboral, el motor responde si **cumple los requisitos de la PBU** a la fecha de cálculo:

1. **Edad:** cumple si a la fecha de cálculo tiene cumplida la edad requerida (65 hombres, 60 mujeres). El día del cumpleaños ya la cumple.
2. **Servicios:** cumple si tiene al menos **360 meses** de servicios con aportes (resultado de R001, total en meses completos).
3. **Compensación:** si cumple la edad pero le faltan servicios, cada **2 meses de edad excedente** compensan **1 mes de servicios faltante**. Cumple si la compensación alcanza para cubrir lo que falta.
4. La compensación va en un solo sentido: los servicios de más **no** compensan edad faltante.
5. **Tiene derecho** si cumple la edad y cumple los servicios (con o sin compensación).
6. El resultado explica cada paso con su número (edad cumplida, edad requerida, meses acreditados, meses faltantes, meses de edad excedente, meses compensados) y la norma que lo respalda.

## Datos de entrada

- Fecha de nacimiento (obligatoria)
- Sexo según la Ley 24.241, art. 19: hombre | mujer (obligatorio)
- Historia laboral y fecha de cálculo (las mismas de R001)

**Validación:** fecha de nacimiento posterior a la fecha de cálculo → error.

## Convenciones (A VALIDAR)

| Punto | Decisión propuesta | Respaldo |
|-------|-------------------|----------|
| Edad en meses | Meses cumplidos "de fecha a fecha" entre el nacimiento y la fecha de cálculo. Si el mes no tiene ese día (29/02, 31), se cumple el último día del mes. El día del cumpleaños ya cuenta como cumplido | Código Civil y Comercial, art. 6 ([texto](https://www.argentina.gob.ar/normativa/nacional/norma-235975/actualizacion)). **Confirmar** que ANSES lo aplica así a la edad jubilatoria (pregunta 15) |
| Compensación en meses | Meses compensados = meses de edad excedente ÷ 2, **sin redondear hacia arriba** (5 meses de exceso compensan 2 meses) | La ley fija la proporción 2 a 1 y el Decreto 679/95 admite fracciones de edad. El redondeo de la fracción no está explícito: **A VALIDAR** |
| Días sueltos de servicios | Se usan los meses que da R001 (que ya convierte cada 30 días sueltos en un mes); los días que quedan sin formar un mes no suman (359 meses y 20 días no alcanza) | **A VALIDAR** (ligado a la pregunta 1 de R001) |

## Criterios de aceptación

1. **Hombre con edad y servicios.** Dado un hombre con 65 años o más y 360 meses o más, entonces tiene derecho, sin compensación.
2. **Mujer con edad y servicios.** Dada una mujer con 60 años o más y 360 meses o más, entonces tiene derecho.
3. **Falta edad.** Dada una persona por debajo de la edad requerida, entonces no tiene derecho, aunque tenga servicios de más.
4. **Cumple el día del cumpleaños.** Dada una fecha de cálculo igual al día en que cumple la edad requerida, entonces cumple la edad.
5. **Compensación suficiente.** Dada una persona con edad excedente y servicios faltantes, si (meses excedentes ÷ 2) ≥ meses faltantes, entonces tiene derecho e informa los meses compensados.
6. **Compensación insuficiente.** Si (meses excedentes ÷ 2) < meses faltantes, entonces no tiene derecho e informa cuántos meses faltan después de compensar.
7. **Sin compensación al revés.** Servicios de más no compensan edad faltante (cubierto por el criterio 3).
8. **Fecha de nacimiento inválida.** Posterior a la fecha de cálculo → error.
9. **Trazabilidad.** El resultado indica la regla R002 y que el cómputo de servicios usó R001.

## Casos de prueba

Fecha de cálculo para todos: **01/01/2026**. "Servicios" = un período con aportes en relación de dependencia.

| # | Persona | Servicios | Resultado esperado | Fuente del caso |
|---|---------|-----------|--------------------|-----------------|
| 1 | Hombre, nacido 15/03/1960 (65a 9m) | 01/01/1990–31/12/2019 (360 meses) | Tiene derecho. Sin compensación | Construido (criterio 1) |
| 2 | Mujer, nacida 10/05/1965 (60a 7m) | 01/01/1990–31/12/2019 (360 meses) | Tiene derecho | Construido (criterio 2) |
| 3 | Hombre, nacido 01/06/1961 (64a 7m) | 01/01/1985–31/12/2019 (420 meses) | No tiene derecho: le faltan 5 meses de edad | Construido (criterio 3) |
| 4 | Hombre, nacido 01/01/1961 (cumple 65 ese día) | 01/01/1990–31/12/2019 (360 meses) | Tiene derecho | Construido (criterio 4) |
| 5 | Hombre, nacido 01/01/1955 (71a 0m: 72 meses de exceso) | 01/01/1980–31/12/2006 (324 meses: faltan 36) | Tiene derecho con compensación: 72 ÷ 2 = 36 meses compensados | Construido (criterio 5) |
| 6 | Hombre, nacido 01/01/1958 (68a 0m: 36 meses de exceso) | 01/01/1980–31/12/2006 (324 meses: faltan 36) | No tiene derecho: compensa 18, siguen faltando 18 meses | Construido (criterio 6) |
| 7 | Mujer, nacida 01/08/1960 (65a 5m: 65 meses de exceso) | 01/01/1990–31/12/2016 (324 meses: faltan 36) | No tiene derecho: compensa 32 (65 ÷ 2 sin redondear), faltan 4 | Construido (convención **a validar**) |
| 8 | Hombre, nacido 01/02/2026 | Cualquiera | Error: fecha de nacimiento posterior a la de cálculo | Construido (criterio 8) |
| 9 | Hombre, nacido 01/01/1961 (cumple 65 ese día, sin excedente) | 01/01/1990–30/11/2019 y 01/12/2019–20/12/2019 (359 meses y 20 días) | No tiene derecho: 359 meses no alcanzan | Construido (convención de días sueltos, **a validar**) |

## Preguntas abiertas para el previsionalista

En `docs/conocimiento/preguntas-previsionalista.md` (preguntas 13 a 15).
