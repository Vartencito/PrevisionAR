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
