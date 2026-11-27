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
| CD-001 | Jubilación PBU/PC/PAP otorgada por ANSES en 2026. Varón, todos los servicios en relación de dependencia | **Parcial:** tiene el cómputo de ANSES pero no los períodos. Falta la historia laboral anonimizada para cargarlo en el programa y comparar |

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

**Qué hace falta para completarlo:** la historia laboral de esta persona (empleadores reemplazados por letras, fechas exactas, con o sin aportes) para cargarla en el programa y ver si da 15a 4m 19d y 31a 5m 0d.
