# Notas de dominio previsional

Lo que la IA aprendió de cursos y material de capacitación para abogados previsionalistas.
**No es normativa.** Sirve para entender el trabajo del abogado y anticipar requisitos. Toda regla que se implemente necesita su ficha con fuente oficial.

Fuentes consultadas (2026-11-26):
- Material de un curso de reajustes previsionales (4 clases en PDF, aportadas por el responsable; no se suben al repo).
- Programa del curso "Tramitaciones y cómputos previsionales", Facultad de Derecho UBA.
- Programa ejecutivo de jubilaciones y pensiones del CPCECABA (módulo de cómputo de servicios).
- Ley 18.037 (régimen anterior), arts. 15, 17 y 18.

## Cómo trabaja un previsionalista en un reajuste

Flujo típico enseñado en el curso:

1. **Análisis previo** de la liquidación que hizo ANSES (pantallas resumen de la prestación acordada).
2. **Cálculo manual** para detectar errores de ANSES.
3. **Error material:** si hay un error de cálculo, se pide corrección en sede administrativa.
4. **Cálculo judicial:** se recalcula aplicando los criterios de los fallos (actualización de remuneraciones, topes, movilidad, PBU).
5. **Honorarios**, según el proceso.
6. **Inicio del proceso judicial.**

**Implicancia para el producto:** el abogado necesita comparar *el cálculo de ANSES* contra *el cálculo correcto* y ver dónde difieren. El informe auditable del MVP apunta a eso.

## Datos que el abogado reúne de cada caso

- Datos personales y fechas importantes (nacimiento, solicitud, cese, adquisición del derecho, alta).
- **Todos los servicios y aportes**, y cuáles entran en cada fórmula.
- Promedios de remuneraciones (dependencia) y de rentas de referencia (autónomos).
- Datos de cobro y liquidación del retroactivo.

## Componentes del haber de la jubilación ordinaria (Ley 24.241)

| Componente | Qué es | Servicios que usa |
|------------|--------|-------------------|
| PBU (Prestación Básica Universal) | Suma fija, no depende de los aportes | Requisito de acceso: 30 años con aportes |
| PC (Prestación Compensatoria) | Según aportes | Servicios **hasta el 30/06/1994** |
| PAP (Prestación Adicional por Permanencia) | Según aportes | Servicios **desde el 01/07/1994** |

**Implicancia para el motor:** el cómputo de servicios tiene que poder separarse en dos tramos (antes y desde el 01/07/1994). Ya se contempla en R001.

## Cómo se usan los servicios en las fórmulas (según el curso, a verificar con norma)

- El curso clasifica los meses de servicio en tres grupos:
  - **n**: meses solo en relación de dependencia
  - **m**: meses solo como autónomo
  - **p**: meses simultáneos (dependencia y autónomo a la vez)
- Para servicios mixtos, la fórmula pondera el promedio de dependencia (W) y el de autónomo (R) según esos meses.
- La cantidad de años (N) que multiplica la fórmula redondea: en los ejemplos, 3 años y 11 meses se toman como 4, y 28 años y 2 meses como 28. Coincide con el Decreto 679/95 (la fracción mayor de 6 meses cuenta como 1 año, para el art. 20).

**Implicancia para el motor:** el cómputo tiene que devolver los meses separados en n, m y p, por tramo. Ya se contempla en R001.

## Reglas de cómputo del régimen anterior (Ley 18.037)

- Se computan servicios continuos o discontinuos, en principio desde los 18 años.
- Las interrupciones sin remuneración no se computan, salvo excepción.
- Los servicios simultáneos **no se acumulan** para la antigüedad.
- No se computa más tiempo que el calendario entre las fechas, **ni más de 12 meses en un año calendario**.
- Se cuenta un día por jornada legal.

La Ley 24.241 (art. 22) remite a esa normativa para los servicios anteriores. Queda por confirmar con un previsionalista cuánto de esto se aplica a servicios posteriores a 1994.

## Hallazgos de la investigación normativa del 2026-11-26

Contexto para las próximas fichas. Las fuentes y los links están en `preguntas-previsionalista.md` (preguntas 4 a 9 y 13 a 15) y en `docs/normativa/R003-computo-por-hijos.md`.

- **Numeración de la Ley 18.037 t.o. 1976 (copia de ARCA):** la edad de cómputo (16 años) está en el art. 14, la regla de jornada y simultaneidad (un día por jornada legal, tope calendario y 12 meses por año) en el art. 16, y las licencias pagas en el art. 17. En algunos documentos del repo figura "art. 15" para la simultaneidad.
- **Corte PC/PAP:** el Decreto 679/95 toma para la PAP los meses desde julio de 1994 inclusive, pero el Libro I de la Ley 24.241 entró en vigor el 15/07/1994 (Decreto 56/94), y la PC usa servicios hasta esa fecha. Hay que definir con un previsionalista qué pasa con el 01/07 al 14/07/1994.
- **Fracción de años:** la Ley 24.241, art. 24 inc. a), cuenta como año la fracción mayor de 6 meses para la PC. El art. 30 (PAP) remite a la metodología de la PC.
- **Edad y 29 de febrero:** el Código Civil y Comercial, art. 6, cuenta los plazos de años de fecha a fecha. Si no hay día equivalente, el plazo vence el último día del mes (28/02). Falta confirmar si se aplica a la edad jubilatoria.
- **Cómputo por hijos (art. 22 bis):** vigente al 26/11/2026, sin modificaciones encontradas. Sirve solo para el mínimo de servicios de la PBU, no para el haber. ANSES lo tramita como "Reconocimiento de aportes por tareas de cuidado".

## Temas que vendrán (no son del MVP)

- **Autónomos y monotributo:** reglas distintas según la época del aporte (1998-2000, 2000-2010, desde 2010). Algunos períodos de monotributo no suman a la PAP. Hay jurisprudencia que excluye el monotributo cuando perjudica el haber.
- **Actualización de remuneraciones:** se actualizan las últimas 120 remuneraciones con un coeficiente que surge de índices (variación salarial entre la fecha del aporte y la fecha de corte).
- **PBU:** monto fijo desde 03/2009 (Ley 26.417). Reajuste según el fallo "Quiroga" (CSJN 2014), con un test de incidencia sobre el haber total. Distintas cámaras usan criterios diferentes (Badaro, ISBIC).
- **Haber inicial vs. haber al alta:** la fórmula da el haber a la fecha de solicitud o adquisición; a eso se le suma la movilidad hasta el alta.
- **Otras prestaciones:** PEA, RTI, pensiones, PUAM, regímenes especiales; cada una con su propia forma de calcular el haber inicial.

## Panorama relevado el 2026-11-26 (detalle y fuentes en las fichas R004 a R006)

- **Moratorias vigentes hoy:** Ley 24.476 (aportes de autónomos hasta 09/1993, con evaluación socioeconómica) y Unidades de Cancelación de Aportes Previsionales de la Ley 27.705 (personas en actividad cerca de la edad jubilatoria, períodos hasta 03/2012). La moratoria para quien ya tiene la edad (Ley 27.705, Unidad de Pago) venció en 2025; la prórroga (Ley 27.792) fue vetada (Decreto 534/2025). Ver R004.
- **Haber inicial:** PBU suma fija (Ley 26.417). PC: 1,5% por año (fracción mayor de 6 meses = 1 año), tope 35 años, sobre el promedio de 120 remuneraciones actualizadas. PAP: 1,5% por año desde 07/1994, sin tope (0,85% para pedidos antes del 01/07/2007). Actualización por índice combinado trimestral (Ley 27.609); movilidad mensual por IPC (Decreto 274/2024). El art. 125 bis garantiza el 82% del salario mínimo con 30 años de aportes efectivos. Ver R005.
- **Sin 30 años:** PEA (70 años, 10 años de aportes con 5 en los últimos 8; 70% de PBU + PC + PAP) y PUAM (65 años, no contributiva, 80% del haber mínimo). Ver R006.
