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

## Hallazgos del 2026-11-27 (R003)

- **Texto del art. 22 bis:** los 2 años adicionales por AUH valen "en la medida en que por este se haya computado el tiempo previsto **en el presente apartado**" (no "párrafo anterior", como decía antes la ficha). Verificado en InfoLEG y en el Boletín Oficial del 19/07/2021.
- **Normativa ANSES PREV-11-67 (tareas de cuidado):** sigue sin texto oficial publicado. Solo circula en sitios de estudios jurídicos, así que no se usa como fuente. Se le pide al abogado en la hoja de reunión v0.2.

## Lo que contó el abogado el 2026-11-27 (resumido; detalle en `reuniones/2026-11-27.md`)

Son afirmaciones de un abogado, sin norma citada: sirven para preguntar e investigar, y entran a una ficha solo con su fuente.

- **Dos cuentas distintas:** para **llegar a los 30 años** se cuenta todo, días incluidos (30 días = 1 mes). Para la **PC y la PAP** se cuentan años enteros, y más de 6 meses suma un año.
- **Promedio en dependencia:** las últimas 120 remuneraciones aunque no sean consecutivas; si hay meses incompletos, se suman días hasta completar 120 meses; con menos de 10 años, se divide por los meses que haya (⚠️ el Decreto 679/95 dice dividir por 120: P-45). **El aguinaldo no entra** (✅ Ley 24.241, art. 25). Los conceptos sin aportes tampoco, salvo que se ganen en un juicio.
- **Promedio de autónomos:** todas las categorías aportadas en la vida laboral, sin el límite de 10 años.
- **Topes:** (1) la remuneración máxima sujeta a aportes de cada mes; (2) la remuneración actualizada frente a la máxima actual; (3) el tope de la PC; (4) el haber máximo, también cuando se cobran jubilación y pensión a la vez. Las sentencias suelen liberar un tope si la diferencia supera el 15 % (se considera confiscatorio).
- **Sentencias de reajuste:** cada fallo trae su criterio (con o sin tope, el 15 %). Mencionó tres fallos nuevos para el período 2021 a junio de 2024.
- **Regímenes diferenciales y especiales:** muchas actividades (aeronavegantes, subte, docentes, investigadores, radiólogos, choferes, construcción, ballet) piden menos edad o menos años, o computan el tiempo distinto. La actividad se acredita con la certificación de servicios, no con la palabra de la persona.
- **Régimen anterior (Ley 18.037):** el haber era un porcentaje (70 a 82 %) del promedio de los últimos 10 años, según los años de más. Se usa poco hoy.
- **Otras prestaciones:** pensión derivada (de un jubilado), pensión directa (fallece en actividad; pide regularidad de aportes), retiro por invalidez, PEA. La anticipada por desempleo ya no está vigente.
- **Cómo trabaja:** usa BlueCorp y consigue PRPA de colegas. Su interés principal es la **liquidación de sentencias** ("subir la sentencia o la PRPA y que el programa calcule").

## Material leído el 2026-11-27 (segunda tanda)

**Cartilla oficial de ANSES "Jubilación ordinaria" (SIPA, actualizada a octubre de 2025).** Es fuente oficial.
- Requisitos: 60 años las mujeres y 65 los hombres, y 30 años de servicios con aportes en el sistema de reciprocidad. Se compensa **un año de servicios por cada dos años de edad excedentes**, como dice el art. 19 y al revés de lo que dijo el abogado.
- Documentación que pide ANSES. Sirve para la etapa 2 (importar documentos) y para una guía de trámite:
  - dependencia: formulario PS. 6.2 "Certificación de Servicios y Remuneraciones";
  - autónomos y monotributo: formularios 558/A, 558/B y 558/C del SICAM (ARCA) y, si hay moratoria de la Ley 24.476, el plan y la aceptación del descuento de cuotas (PS. 6.278);
  - el formulario PS. 6.284, para declarar prestaciones en provincias no adheridas o en fuerzas armadas y de seguridad.

**Presentación de un curso de la SCBA sobre el régimen de ANSES** (material de terceros, de alrededor de 2016: tiene importes viejos). Sirve como guía para investigar, no como fuente:
- **Caja otorgante:** si hubo aportes en varias cajas, la que tiene más años fija la edad y los servicios exigidos, y paga la prestación (reciprocidad, Decreto-Ley 9.316/46). El programa hoy supone siempre ANSES: conviene avisarlo cuando haya servicios en otras cajas.
- **Servicios por declaración jurada** (Ley 24.241, art. 38): un máximo de años según el año de cese (7 años en 1994-95, bajando hasta 1 año en 2006-07). Ya figura como excepción en R001 y R002.
- **Pensión por fallecimiento y retiro por invalidez:** piden ser aportante "regular" o "irregular con derecho": por ejemplo, 30 de los últimos 36 meses, o 18 de 36 con los años mínimos, o 12 de 60 con la mitad de los años (Decretos 1120/94, 136/97 y 460/99). Para una ficha futura.
- **Autónomos:** prescripción de deudas a los 10 años (Ley 14.236, art. 16); renuncia a meses de autónomo que sobran o son simultáneos (Ley 25.321, art. 1); padrón histórico de autónomos (PUC) y SICAM. Para R004 y para la idea de revisar el SICAM.
- **Moratoria y PEA:** cita la Res. ANSES 540/2014 (ver R006-6, a verificar).
