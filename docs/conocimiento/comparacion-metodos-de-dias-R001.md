# Comparación de métodos para contar días (R001)

Para la repregunta **A2-bis** (y las preguntas 1, 2 y 14 de `preguntas-previsionalista.md`).
Objetivo: que el abogado confirme el método mirando resultados concretos, no definiciones.
Fecha: 2026-11-27. Los números del método actual salen del motor real; los otros, de la fórmula descripta abajo.

## Los tres métodos

| Método | Cómo cuenta | De dónde sale |
|--------|-------------|---------------|
| **Actual** (lo que hace hoy el programa) | Cuenta los meses calendario completos. Los días de meses incompletos se suman (aunque sean de períodos distintos) y cada 30 forman un mes | Propuesta técnica (R001, convención "a validar") |
| **30/360** (lo que contó el abogado sobre BlueCorp) | Todos los meses valen 30 días y el año 360. El día 31 no se cuenta, pero un mes de 31 días completo vale un mes entero. Desde y hasta inclusive | Respuesta del abogado del 27/11/2026 (pregunta 2) |
| **30/360 con febrero completo** | Igual que el anterior, pero si el período termina el último día del mes (28 o 29 de febrero), cuenta como día 30 | Variante posible: no sabemos si BlueCorp o ANSES la usan |

Ninguno de los tres tiene una norma oficial que lo fije (ver pregunta 1). Por eso se pregunta.

## Casos

Todos con aportes, en relación de dependencia. "a m d" = años, meses, días.

| # | Período(s) | Actual | 30/360 | 30/360 con febrero completo | ¿Difieren? |
|---|-----------|--------|--------|------------------------------|------------|
| A | 01/01/2000–31/12/2009 | 10a 0m 0d | 10a 0m 0d | 10a 0m 0d | No |
| B | 15/01/2020–14/02/2020 | 0a 1m 1d | 0a 1m 0d | 0a 1m 0d | **Sí**: el actual suma el 31 de enero |
| C | 01/02/2021–28/02/2021 (febrero completo) | 0a 1m 0d | 0a 0m 28d | 0a 1m 0d | **Sí**: el 30/360 puro no da un mes por febrero |
| D | 01/03/2000–15/03/2000 | 0a 0m 15d | 0a 0m 15d | 0a 0m 15d | No |
| E | 10/05/2019–31/05/2019 | 0a 0m 22d | 0a 0m 21d | 0a 0m 21d | **Sí**: el 30/360 no cuenta el 31 |
| F | 31/01/2020–29/02/2020 | 0a 1m 1d | 0a 1m 0d | 0a 1m 1d | **Sí** |
| G | 01/01/2020–20/01/2020 y 01/03/2020–20/03/2020 | 0a 1m 10d | 0a 1m 10d | 0a 1m 10d | No, **si** el 30/360 también suma días de períodos distintos |
| H | 01/01/1990–30/11/2019 y 01/12/2019–20/12/2019 | 29a 11m 20d | 29a 11m 20d | 29a 11m 20d | No (los 20 días sueltos son la pregunta 14) |
| I | 01/02/2001–15/06/2021 | 20a 4m 15d | 20a 4m 15d | 20a 4m 15d | No |

**Conclusión:** los métodos solo se separan en los días sueltos de los bordes (el 31 y febrero). En historias largas la diferencia es de unos pocos días. Igual importa justo en el límite de los 360 meses (caso H y pregunta 14).

## Qué hay que preguntarle al abogado

1. ¿Cuál de las tres columnas coincide con lo que hace **ANSES** (no BlueCorp)? Si no lo sabe, ¿cuál usa él en la práctica y por qué?
2. Febrero completo (caso C): ¿cuenta como un mes entero?
3. Período que empieza un 31 (caso F): ¿cómo se cuenta?
4. Días sueltos de períodos distintos (caso G): ¿se suman entre sí para formar meses o cada período se redondea por separado?
5. ¿Nos puede pasar una historia laboral de ANSES con datos tapados donde figure el total de años, meses y días que calculó ANSES? Con eso comparamos los tres métodos contra el dato oficial (pregunta 10).

## Qué cambia en el programa según la respuesta

- La conversión está aislada en `src/PrevisionAR.Motor/Computo/ConvencionDeComputo.cs` y el cómputo de días en `ComputoDeServicios.cs`.
- Si la respuesta es "actual": no cambia nada; R001 pasa los puntos a validados.
- Si es "30/360" (con o sin febrero): se actualiza la convención de R001, se agregan los casos B, C, E y F de esta tabla a la ficha con el resultado elegido y se cambia el cálculo (unas 4 h, issue #12).
- Tratamiento de las superposiciones con 30/360: hay que definir cómo se descuentan los tramos que se pisan. Lo más simple es seguir contando cada día calendario una sola vez y aplicar el 30/360 solo al pasar los días a meses. Esto se confirma cuando llegue la respuesta.
