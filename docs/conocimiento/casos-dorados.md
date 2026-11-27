# Casos dorados

Un **caso dorado** es una historia laboral real, **anonimizada**, junto con el cómputo que hizo **ANSES** para esa misma persona. Sirve para comprobar que el programa calcula igual que ANSES, que es la validación más fuerte que tenemos (forma de trabajo aprobada el 2026-11-27).

## Qué pedirle al abogado

Para cada caso, lo que tenga de esto (cuanto más, mejor):

1. La **historia laboral** con los períodos: empleador (se reemplaza por "Empresa A", "Empresa B"…), fechas desde y hasta, tipo (dependencia o autónomo) y si tiene aportes.
2. **Lo que calculó ANSES:** total de años, meses y días de servicios, y si otorgó o denegó el beneficio. Sale de la resolución, del cómputo de servicios o de la liquidación.
3. Fecha de nacimiento, sexo según la Ley 24.241, fecha de la solicitud y prestación pedida.
4. Si hubo moratoria, años por hijo u otra particularidad, cuál.

Sirven más los casos **en el límite**: cerca de los 360 meses, con días sueltos, con febrero o un día 31 en los extremos, con trabajos superpuestos o períodos que cruzan julio de 1994.

## Cómo anonimizar (lo hace el responsable, antes de pasárselo a la IA)

| Dato | Qué hacer |
|------|-----------|
| Nombre, DNI, CUIL, domicilio, número de expediente o de beneficio | **Borrar.** No se guardan |
| Empleadores | Reemplazar por "Empresa A", "Empresa B"… (si importa el rubro, "Empresa A (comercio)") |
| Fecha de nacimiento | Mantener **mes y año** y poner día 01, salvo que el día importe para el caso (cumpleaños el mismo día de la solicitud). En ese caso, avisarlo |
| Fechas de los períodos | **Se mantienen exactas**: son las que prueban el cálculo |
| Montos | No hacen falta para el cómputo de servicios. No pasarlos |
| Capturas o PDF de ANSES | Tapar los datos personales **antes** de pasarlos. Mejor todavía: transcribir los números en una tabla |

Si un caso no se puede anonimizar sin perder lo que lo hace interesante, **no se usa**.

## Cómo entra al proyecto

1. El responsable pega en el chat el caso ya anonimizado y dice de qué abogado viene (solo para la trazabilidad interna, no se publica su nombre sin su permiso).
2. La IA lo carga como `CD-001`, `CD-002`… en una tabla de casos dorados de la ficha que corresponda, con su origen ("resolución ANSES 2025, anonimizada").
3. **El responsable aprueba cada caso antes de que entre al repositorio** (regla de privacidad de `CLAUDE.md`).
4. La IA agrega el test. Si el programa no da lo mismo que ANSES, **no se ajusta el código para que coincida**: se abre una pregunta, porque la diferencia puede venir de un criterio de ANSES que no conocemos o de un error de carga.

Un caso dorado que coincide, más la confirmación de un abogado, alcanza para marcar una regla como **Validada**.

## Registro

| Código | Qué es | Estado |
|--------|--------|--------|
| CD-001 | Jubilación PBU/PC/PAP otorgada por ANSES en 2026. Varón, todos los servicios en relación de dependencia | **Parcial:** tiene el cómputo de ANSES y la fecha de cese, pero no los períodos. La historia laboral ya fue pedida |
| CD-002 | Prestación PBU/PC/PAP otorgada por ANSES en 2009. Varón, relación de dependencia con dos meses simultáneos de autónomo después de 7/94 | **Parcial:** tiene el cómputo de ANSES pero no los períodos. Probablemente no es del régimen general común (ver P-32) |

### CD-001 (parcial) — aprobado por el responsable el 2026-11-27

Origen: notificación de acuerdo de beneficio y "Detalle del beneficio" de ANSES, con fecha de solicitud en enero de 2026, traídos por el responsable. Se registran solo los datos del cómputo. **No se guardan** el nombre, el documento, el CUIL, el domicilio, la región, los números de beneficio y de expediente ni los importes.

**Datos de la persona y fechas**

- Varón. La solicitud se presentó **el día siguiente** a cumplir 65 años. "Cierre del cómputo", adquisición del derecho y solicitud son la misma fecha.
- Las 120 remuneraciones tomadas son las de los 120 meses anteriores al mes de la solicitud (el mes de la solicitud no entra).

**Cómputo de ANSES**

| | Antes del 7/94 | Después del 7/94 | Total |
|--|----------------|------------------|-------|
| Tiempo informado (relación de dependencia) | 15a 4m 19d | 31a 5m 0d | 46a 9m 19d |
| Tiempo tomado para el cálculo | 15a 4m | 31a 5m | **46a 5m** (ver P-29) |

Autónomos y simultáneos: vacíos.

**Lo que se pudo comprobar con las cuentas del propio documento**

1. **Los días sueltos se descartan** para el cálculo: los 19 días del tramo anterior a 7/94 no pasan a "tiempo tomado" ni se redondean a un mes (P-1, P-14).
2. **Las fracciones de 6 meses o menos no cuentan como año, ni en la PC ni en la PAP.** La PC y la PAP son exactamente 1,5 % × 15 años y 1,5 % × 31 años del mismo promedio (la relación PAP/PC es 31/15 al centavo). Los 4 y 5 meses sueltos no suman (P-9). El caso no muestra qué pasa con una fracción **mayor** de 6 meses.
3. **El tiempo se separa en antes y después de "7/94"** (P-8). El documento no aclara si julio de 1994 entra en uno u otro tramo.
4. **El promedio** es la suma de las 120 remuneraciones actualizadas dividida por 120 (R005).
5. **Centavos:** la PAP del documento es 1 centavo menor que la cuenta exacta. Sugiere que ANSES **trunca** en lugar de redondear (R005-n, a confirmar con más casos).
6. **Edad:** se otorgó con la solicitud presentada al día siguiente de cumplir 65. Es compatible con el criterio de R002 de fecha a fecha (P-15), pero no prueba qué pasa el mismo día del cumpleaños.

**Ampliación del 2026-11-27 (cálculo de BlueCorp del mismo caso, aprobado por el responsable)**

- **Cese de actividades: 30/11/2025** (la solicitud fue en enero de 2026). ANSES computa los servicios hasta el cese, no hasta la solicitud.
- **Tramo desde 7/94, comparado con el motor:** un período del 01/07/1994 al 30/11/2025 da **31a 5m 0d** en el motor, igual que ANSES. Es la primera coincidencia del motor con un cómputo real. Confirma que se cuentan las dos fechas y que el tramo empieza el 01/07/1994. Supone trabajo continuo, sin cortes; se confirma con la historia laboral.
- **El tramo antes de 7/94 va a definir el método de días.** Con trabajo continuo hasta el 30/06/1994, los 19 días salen si el ingreso fue el **10/02/1979** con el método actual del motor (meses calendario), y si fue el **12/02/1979** con meses de 30 días. La fecha real de ingreso alcanza para saber qué método usa ANSES (P-1, A2-bis).
- **BlueCorp no es evidencia independiente del cómputo de tiempo:** copió los tiempos que había cargado ANSES ("informados por el operador"); no los calculó desde los períodos.
- **Para el haber (R005, a verificar en la fuente oficial antes de usarlo):** BlueCorp limita cada remuneración actualizada a un tope mensual (cita la Res. SSS 6/2009, art. 14) y muestra un tope para la PC. Sus importes difieren de los de ANSES en algunos centavos.

**Qué hace falta para completarlo:** la historia laboral de esta persona (empleadores reemplazados por letras, fechas exactas, con o sin aportes) para cargarla en el programa y ver si da 15a 4m 19d y 31a 5m 0d.

### CD-002 (parcial) — aprobado por el responsable el 2026-11-27

Origen: "Detalle del beneficio" y notificación de acuerdo de ANSES de 2009, traídos por el responsable. **No se guardan** el nombre, el documento, el CUIL, el domicilio, la región, el cónyuge ni los números de beneficio, expediente o cuenta. De los importes se registran solo los que hacen falta para verificar la fórmula (son de 2009 y no identifican a nadie).

**Datos de la persona y fechas**

- Varón. Edad a la solicitud: **55 años y 10 meses**. Con esa edad no podía acceder a la jubilación ordinaria del régimen general: es probable un régimen diferencial o especial (P-32).
- Cierre del cómputo: 31/12/2008. Solicitud y adquisición del derecho: octubre de 2009.

**Cómputo de ANSES**

| | Antes del 7/94 | Después del 7/94 |
|--|----------------|------------------|
| Informado, relación de dependencia | 16a 10m 29d | 9a 3m 14d |
| Informado, simultáneos (dependencia y autónomo) | — | 0a 2m 0d |
| Tomado para el cálculo, relación de dependencia | 16a 10m | 9a 3m |

- Tiempo total informado: **34a 6m 23d**, que no coincide con la suma de los tramos (unos 26 años): ver P-33.
- Tiempo total tomado: impreso **"25a 13m"**. ANSES sumó años y meses sin pasar los 13 meses a 1 año.

**Lo que se pudo comprobar con las cuentas del propio documento**

1. **Una fracción de más de 6 meses suma un año en la PC (P-9).** La PC se calculó con **17 años** a partir de 16a 10m: 1,5 % × 17 × promedio de relación de dependencia (676,68) = 172,55, igual que ANSES.
2. **Los días sueltos se descartan en cada tramo sin sumarse entre tramos (P-1, P-14).** Hay 29 días en un tramo y 14 en el otro: juntos pasarían de un mes, pero ANSES descarta los dos.
3. **PAP con servicios mixtos.** Con 9 años y el promedio ponderado de la fórmula de servicios simultáneos (111 meses solo en dependencia y 2 meses simultáneos, con promedios de 676,68 en dependencia y 324,23 en autónomos): 1,5 % × 9 × 682,4186 = 92,1265, y ANSES pagó **92,12**. Coincide con la fórmula de n, m y p que el motor ya separa (R001, criterio 12) y es **el segundo indicio de que ANSES trunca** los centavos (el primero es CD-001).
4. La resolución cita casi las mismas leyes que la de CD-001 (24.241, 24.476, 25.865 y 25.994 art. 6), 16 años antes: refuerza que es un texto fijo (P-30).

**Qué hace falta para completarlo:** saber el régimen (P-32) y la historia laboral con los períodos.

## Plantilla para pasar una historia laboral

Lo más fácil es pegar en el chat el PDF o la captura **con los datos personales tapados**. Si se transcribe, sirve este formato (una fila por período, tal como figura en ANSES, sin juntar ni corregir nada):

| # | Empleador | Desde (dd/mm/aaaa) | Hasta (dd/mm/aaaa) | Tipo (dependencia / autónomo) | ¿Con aportes? | Observaciones de ANSES (si las hay) |
|---|-----------|--------------------|--------------------|-------------------------------|---------------|-------------------------------------|
| 1 | Empresa A | | | | | |
| 2 | Empresa B | | | | | |

Agregar debajo: el **total de años, meses y días** que informó ANSES (antes y después del 7/94), la fecha de solicitud y, para la edad, el mes y el año de nacimiento más cuántos días antes o después del cumpleaños se pidió.

**Cómo se compara:** la IA carga los períodos en el programa con la misma fecha de cálculo y compara tramo por tramo los años, meses y días contra ANSES. Si coinciden, el caso pasa a ser un test permanente. Si no coinciden, se anota la diferencia y se pregunta: no se cambia el código para forzar que coincida.
