# 0008 — Servicios acreditados y orquestador de evaluación de derecho

- **Estado:** Propuesta (requiere aprobación del responsable: cambia cómo se combinan las reglas del motor)
- **Fecha:** 2026-11-27
- **Tareas:** issue #39 (versión v0.2) y su relación con el issue #40 (origen del servicio)

## Contexto

Hoy hay dos reglas encadenadas: R002 (`RequisitosPbu.Verificar`) recibe la historia laboral y llama por dentro a R001 (`ComputoDeServicios.Calcular`). El total de servicios que usa R002 son los meses con aportes de R001.

En v0.2 ("¿A qué puede acceder?") se suman tres reglas que cambian ese esquema:

| Ficha | Qué aporta | Por qué no entra en el esquema actual |
|-------|-----------|----------------------------------------|
| R003 (años por hijo, Ley 24.241 art. 22 bis) | Meses que **solo** sirven para llegar al mínimo de 360 de la PBU; no suben el haber | No son períodos con fecha: salen de los hijos (cantidad, adopción, discapacidad, AUH), no del calendario. |
| R004 (moratorias) | Meses reconocidos por moratoria (Ley 24.476, UCAP de la Ley 27.705) | Hoy un período es "con aportes" o "sin aportes" (`bool ConAportes`); la moratoria es un tercer caso, que vale para algunas cosas y no para otras. |
| R006 (PEA y PUAM) | Alternativas cuando no se cumple la PBU | Es una regla que se evalúa **después** de R002 y según su resultado. Alguien tiene que decidir ese "después". |

Las tres fichas están en **Investigación**: no tienen criterios de aceptación y dependen de preguntas abiertas al previsionalista. Este ADR define **la forma** de combinarlas, no su contenido jurídico.

### ¿Se cumple el disparador?

Sí. `docs/arquitectura.md` lista dos disparadores que se cumplen con v0.2:

- **"Servicios acreditados y orquestador"**: disparador R003 o R004. Se cumple.
- **"Origen del servicio"** (issue #40): disparador R004. Se cumple.

**Pero el cambio entra con la primera de esas fichas que tenga criterios aprobados, no antes** (YAGNI). Mientras R003 y R004 sigan en investigación, el código actual alcanza.

No se cumplen (y no se proponen): capa de aplicación, capa de datos, ni tabla de vigencias. Esta última puede activarse con R004, si la evaluación socioeconómica entra al alcance (sus parámetros cambian según la fecha); se decidirá en esa ficha.

## Opciones

### Opción A: R002 crece y llama a todo

`RequisitosPbu.Verificar` recibe historia y hijos, y por dentro llama a R001, R003 y R004. La Web llama a R002 y, si da "no tiene derecho", llama a R006.

| Pros | Contras |
|------|---------|
| Pocos tipos nuevos. | R002 mezcla cuatro reglas: sus tests necesitan armar hijos y moratorias para probar algo tan simple como "le falta edad". |
| | La decisión "si no cumple PBU, evaluar PEA y PUAM" queda en la Web, que no debe tener lógica de negocio (y no tiene tests). |
| | El informe (#16) tendría que repetir esa decisión. |
| | Cuesta ver qué regla produjo cada mes. |

**Costo:** bajo al principio y alto después (cada regla nueva engorda R002).

### Opción B: `ServiciosAcreditados` + orquestador `EvaluacionDeDerecho` (recomendada)

Cada regla sigue siendo una clase estática chica que hace una sola cosa. Aparecen dos piezas:

1. **`ServiciosAcreditados`**: un registro que junta los meses que cuentan para el mínimo de la PBU, **separados por origen**, y cada parte dice qué regla la produjo (R001, R004, R003).
2. **`EvaluacionDeDerecho`**: una clase estática con un método que llama a las reglas **en un orden fijo, escrito en C# directo**, y devuelve un resultado con todo lo evaluado.

R002 deja de recibir la historia laboral: recibe `ServiciosAcreditados` ya armado. La Web llama solo a `EvaluacionDeDerecho.Evaluar`.

| Pros | Contras |
|------|---------|
| Cada regla se prueba sola, con entradas chicas. Los casos de R002 se siguen escribiendo con números de meses. | Un tipo y una clase nuevos, y un cambio de firma en R002 (hay que adaptar sus tests). |
| El orden de las reglas está en un solo lugar del motor, con tests. | Al principio parece más que lo que hace falta para R001 + R002 solos (por eso entra recién con R003 o R004). |
| La Web y el informe no deciden nada: muestran lo que devuelve el orquestador. | |
| La trazabilidad queda en los datos: cada parte de los servicios y cada resultado dicen su regla. | |

**Costo:** medio (unas 4 h de refactor, sin cambiar resultados, antes de la primera regla nueva).

### Opción C: la Web orquesta

La Web llama a R001, R003, R004, R002 y R006 por separado y combina los resultados.

| Pros | Contras |
|------|---------|
| No cambia nada en el motor. | Pone reglas de negocio (orden, topes, cuándo evaluar R006) en la interfaz, sin tests y en contra de `docs/arquitectura.md`. |
| | El informe y una futura importación tendrían que repetir la misma combinación. |

**Descartada.**

También queda descartado, sin evaluar, un **motor de reglas genérico** (interfaz `IRegla`, lista de reglas configurable, reglas en JSON): está en la lista de lo que no se usa en `docs/arquitectura.md`.

**Recomendación: opción B.**

## Diseño de la opción B

### 1. Origen del servicio (issue #40)

`PeriodoDeServicio.ConAportes` (`bool`) pasa a `Origen` (`OrigenDelServicio`). El issue #40 hace **solo el reemplazo, sin cambiar resultados**:

```csharp
/// <summary>De dónde sale el reconocimiento de un período.</summary>
public enum OrigenDelServicio
{
    /// <summary>Servicios con aportes (Decreto 679/95, art. 2). Lo computa R001.</summary>
    ConAportes,

    /// <summary>Servicios sin aportes: no suman para la PBU (Ley 24.241, art. 19 inc. c).</summary>
    SinAportes,
}
```

R004 suma después los valores que su ficha defina (por ejemplo `MoratoriaLey24476`, `Ucap`), cada uno con su norma en el comentario. Reglas:

- **Cada regla filtra los orígenes que su ficha permite**, con `switch` exhaustivo que lanza error ante un valor desconocido. R001 cuenta solo `ConAportes`; los períodos de moratoria aparecen en R001 como "no computados" con un motivo nuevo (`PorMoratoria`, "lo computa R004"), así el informe de R001 sigue explicando cada fila.
- **Los años por hijo no van en este enum** (diferencia con lo que hoy dice `docs/arquitectura.md`): no tienen fechas desde/hasta, y meterlos como período inventaría fechas. Van como una parte separada de `ServiciosAcreditados`. Si se aprueba este ADR, hay que corregir esa fila de `docs/arquitectura.md`.
- **Orden recomendado:** #40 se puede hacer en cualquier momento (es un cambio interno, sin efecto en los resultados), pero conviene hacerlo **después** del #15 (ADR 0007), cuyo formato de archivo ya guarda `origen` como texto. Así el archivo no se migra.

### 2. Contrato `ServiciosAcreditados`

Vive en `src/PrevisionAR.Motor/Computo/`. Firma de ejemplo (orientativa):

```csharp
/// <summary>
/// Meses que cuentan para el mínimo de servicios de la PBU, separados por origen.
/// Cada parte es el resultado de su propia regla y dice cuál es (trazabilidad).
/// </summary>
public sealed record ServiciosAcreditados(
    ResultadoDelComputo ConAportes,       // R001
    ResultadoMoratorias PorMoratoria,     // R004 (vacío si no hay períodos de moratoria)
    ResultadoComputoPorHijos PorHijos)    // R003 (vacío si no aplica)
{
    public int MesesConAportes => ConAportes.Total.TotalMeses;
    public int MesesPorMoratoria => PorMoratoria.Meses;

    /// <summary>Meses por hijos tal como los da R003, antes del tope. El tope lo aplica R002.</summary>
    public int MesesPorHijosSinTope => PorHijos.Meses;

    /// <summary>Para el MVP y los tests de R002: solo servicios con aportes.</summary>
    public static ServiciosAcreditados SoloConAportes(ResultadoDelComputo computo);
}
```

Puntos de diseño:

- **No suma ni recorta nada por su cuenta.** Es un contenedor: los topes y el orden son reglas jurídicas y viven en R002 (ver punto 4).
- **Las moratorias no cuentan dos veces el mismo mes.** R004 calcula sus meses como "meses con aportes o moratoria" menos "meses con aportes", sobre el mismo calendario de días que usa R001. Para eso, la clase privada `CalendarioDeCobertura` de R001 pasa a ser `internal` en `Computo/` y la usan las dos reglas. Así un mes mitad con aportes y mitad con moratoria cuenta una sola vez, con la misma convención de R001.
- **R003 recibe los hijos, no la historia.** Hace falta un dato nuevo en el modelo (`Hijo`, con los campos que defina la ficha: nacimiento con vida o adopción, discapacidad, AUH). `Persona` sigue sin datos identificatorios.

### 3. El orquestador `EvaluacionDeDerecho`

Vive en `src/PrevisionAR.Motor/Evaluacion/`. Firma de ejemplo:

```csharp
/// <summary>Datos de entrada de una evaluación completa.</summary>
public sealed record DatosDelCaso(
    Persona Persona,
    HistoriaLaboral Historia,
    IReadOnlyList<Hijo> Hijos,
    DateOnly FechaDeCalculo);

/// <summary>
/// Evalúa a qué prestación puede acceder la persona, aplicando las reglas en un orden fijo.
/// Código directo a propósito: el orden es una decisión jurídica y tiene que leerse de corrido.
/// </summary>
public static class EvaluacionDeDerecho
{
    public static ResultadoEvaluacionDeDerecho Evaluar(DatosDelCaso datos);
}

public sealed record ResultadoEvaluacionDeDerecho(
    ServiciosAcreditados Servicios,
    ResultadoRequisitosPbu Pbu,
    ResultadoRequisitosPea? Pea,     // null = no se evaluó (cumple la PBU)
    ResultadoRequisitosPuam? Puam,   // ídem
    IReadOnlyList<string> ReglasAplicadas); // en el orden en que se aplicaron, por ejemplo R001, R004, R003, R002, R006
```

**Orden de ejecución:**

1. **R001** computa los meses con aportes (igual que hoy).
2. **R004** computa los meses por moratoria que su ficha admita (sin duplicar meses de R001).
3. **R003** computa los meses por hijos (sin tope).
4. Se arma `ServiciosAcreditados`.
5. **R002** verifica edad y servicios con `ServiciosAcreditados` (ver punto 4).
6. **R006** se evalúa **solo si R002 dice que no tiene derecho**, como indica su ficha. La PEA usa los servicios que su ficha permita (en principio, solo con aportes de R001); la PUAM no usa servicios.

Los pasos 1 a 3 no dependen entre sí: el orden importa para la lectura y para `ReglasAplicadas`, no para el resultado. El orden que sí es jurídico está en el paso 5 y queda pendiente (punto 4).

El orquestador **no calcula nada propio**: solo llama, arma y decide si sigue con R006. Si algún día necesita lógica (por ejemplo "elegir la mejor prestación"), eso es una regla y lleva ficha.

### 4. Cómo combina R002 (y qué queda pendiente)

`RequisitosPbu.Verificar(Persona persona, ServiciosAcreditados servicios, DateOnly fechaDeCalculo)` reemplaza a la firma actual (sin dejar las dos, para que no haya dos caminos). Adentro, **un único método privado** completa el mínimo de 360 meses:

```
faltantes = 360 − (con aportes + moratoria)     ← ¿la moratoria suma acá? depende de R004
hijos usados = mín(meses por hijos, faltantes)   ← tope: pregunta 16
compensación por edad (2 a 1) sobre lo que falta ← orden respecto de hijos: pregunta 17
```

Ese método lleva el comentario `A VALIDAR` con los números de pregunta, como pide `docs/arquitectura.md` para las convenciones aisladas. Hasta que el previsionalista responda, **R003 no se puede implementar**: el orden cambia el resultado cuando la persona tiene edad de más.

`ResultadoRequisitosPbu` cambia `ServiciosAcreditadosMeses` y `ComputoDeServicios` por `Servicios` (el `ServiciosAcreditados` usado) y agrega `MesesPorHijosUtilizados` y `MesesPorMoratoria`. Cada línea de la explicación cita su regla y su norma ("12 meses por moratoria de la Ley 24.476 (R004)"). Si el informe necesita la norma en una columna, se activa el disparador de "Explicación estructurada" (aparte de este ADR).

### 5. Trazabilidad

- Cada parte de `ServiciosAcreditados` es un `Resultado...` con su propiedad `Regla` (R001, R004, R003).
- `ResultadoRequisitosPbu` sigue con `Regla = R002`; los de PEA y PUAM, con `R006`.
- `ResultadoEvaluacionDeDerecho` no es una regla: en lugar de `Regla` tiene `ReglasAplicadas`, en orden. Hay que adaptar el test `Motor_TiposResultado_IndicanLaReglaQueLosProdujo` para que acepte `Regla` o `ReglasAplicadas`.
- Hay que agregar el código `R003`, `R004` y `R006` en `Reglas.cs` cuando se implemente cada una.

### 6. Dónde vive cada cosa

```
src/PrevisionAR.Motor/
  Modelo/OrigenDelServicio.cs        #40
  Modelo/Hijo.cs                     R003
  Computo/CalendarioDeCobertura.cs   sale de ComputoDeServicios (internal)
  Computo/ServiciosAcreditados.cs
  Computo/ComputoPorHijos.cs         R003
  Moratorias/ComputoDeMoratorias.cs  R004 (carpeta propia: R004 va a tener más piezas, como la detección de huecos)
  Requisitos/RequisitosPbu.cs        R002, nueva firma
  Requisitos/RequisitosPea.cs        R006
  Requisitos/RequisitosPuam.cs       R006
  Evaluacion/EvaluacionDeDerecho.cs  orquestador y su resultado
src/PrevisionAR.Web/
  Components/Pages/Home.razor        llama solo a EvaluacionDeDerecho.Evaluar
```

No hay interfaces (puertos) nuevas: todo es dominio puro, sin nada externo que reemplazar.

### 7. Cómo se prueba

- **Casos de cada ficha** en su propio archivo de tests (R003 con hijos, R004 con períodos de moratoria, R002 con `ServiciosAcreditados` armados a mano). Los tests actuales de R002 pasan a usar `ServiciosAcreditados.SoloConAportes(...)` y **tienen que dar los mismos resultados**: es la prueba de que el refactor no cambió nada.
- **Tests del orquestador** (`EvaluacionDeDerechoTests`): el orden de `ReglasAplicadas`, que R006 solo corre si no cumple PBU, y que el resultado coincide con llamar a cada regla a mano. Si un archivo de tests termina mezclando casos de dos fichas, se activa el disparador "Trazabilidad más estricta" (`[Trait("Ficha", "R003")]`).
- **Invariantes nuevos** (con semilla fija):
  - Agregar hijos o meses de moratoria **nunca quita** el derecho a la PBU.
  - Con aportes + moratoria = meses del calendario conjunto (ningún mes cuenta dos veces).
  - Los meses por hijos usados nunca superan lo que faltaba para 360 (si se confirma la pregunta 16).
  - Si `Pbu.TieneDerecho`, entonces `Pea` y `Puam` son `null`.
  - Mismos datos, mismo resultado y mismo `ReglasAplicadas`.
- **Tests de arquitectura** (en `tests/PrevisionAR.Motor.Tests/Arquitectura/`):
  - `RequisitosPbu.Verificar` no recibe `HistoriaLaboral` (reflexión sobre los parámetros): R002 solo ve servicios ya acreditados.
  - El código de la Web (`.razor` y `.cs` de `src/PrevisionAR.Web`) no llama directamente a `RequisitosPbu.`, `ComputoDeServicios.` ni a las otras reglas: solo a `EvaluacionDeDerecho`. Se revisa el texto, como hace `CodigoDelMotorTests`.
  - Todo tipo `Resultado...` tiene `Regla` o `ReglasAplicadas`.
  - Cada valor de `OrigenDelServicio` está contemplado por R001 (un test recorre el enum y verifica que ninguno lanza "valor no válido").

## Plan de implementación sugerido (cada paso con el CI en verde)

1. **#40:** `bool ConAportes` → `OrigenDelServicio` con dos valores. Sin cambios de resultados.
2. **Refactor:** `ServiciosAcreditados` + `EvaluacionDeDerecho` con solo R001 y R002. La Web pasa a llamar al orquestador. Sin cambios de resultados. Se hace junto con la primera de R003 o R004 que tenga criterios aprobados.
3. **R004**, **R003** y **R006**, cada una cuando su ficha tenga criterios y respuestas.

## Consecuencias

- Sumar una regla es agregar una clase chica, una línea en el orquestador y sus tests; R002 no crece con cada ficha.
- El orden jurídico queda en dos lugares conocidos: el orquestador (qué se evalúa y cuándo) y un método de R002 (cómo se completan los 360 meses), ambos marcados `A VALIDAR` hasta que el previsionalista responda.
- La Web y el informe solo muestran: no deciden nada.
- Hay un cambio de firma en R002 y hay que adaptar sus tests (con los mismos resultados).
- `docs/arquitectura.md` tiene que corregir la fila "Origen del servicio" (los años por hijo no son un origen de período) y sumar `Evaluacion/` al mapa del motor.
- El formato del archivo de casos (ADR 0007) va a sumar los hijos como campo opcional, sin cambiar de versión.

## Dependencias jurídicas (no se deciden acá)

| Tema | Pregunta | Qué bloquea |
|------|----------|-------------|
| ¿Los años por hijo solo completan hasta 360 y el resto se pierde? | 16 (`preguntas-previsionalista.md`) | El tope en R002 |
| ¿Años por hijo y compensación por edad se suman? ¿En qué orden? | 17 | El método que completa los 360 meses; R003 entera |
| ¿Los años por hijo sirven para la PEA? | 25 | Qué servicios recibe la PEA |
| ¿Los meses de moratoria cuentan para la compensación por edad y para la PEA? (la Ley 24.476, Cap. I, habla de "servicios, no de aportes") | preguntas-previsionalista.md (R004) | Si la moratoria entra en "faltantes" y en R006 |
| ¿Qué moratorias carga el programa: solo las ya otorgadas, o también propone candidatas para los huecos? | Alcance de R004 (responsable) | Qué valores se suman al enum y si hace falta la tabla de vigencias |
| ¿Desde cuándo se cuentan los 8 años de la PEA: desde el cese o desde la solicitud? | preguntas-previsionalista.md (R006) (caso 4 de R006) | `RequisitosPea` |
| PUAM: nacionalidad, residencia, otras prestaciones y evaluación socioeconómica | preguntas-previsionalista.md (R006) | Qué datos nuevos pide el caso (o si el motor solo los lista como "a verificar por el abogado") |

## Pendiente del responsable

1. **Aprobar** la opción B (servicios acreditados + orquestador en el motor).
2. **Aprobar** que los años por hijo no sean un valor de `OrigenDelServicio` (corrige lo que hoy dice `docs/arquitectura.md`).
3. **Confirmar** que R006 se evalúa solo cuando no se cumple la PBU (lo dice la ficha) y no siempre, para comparar.
4. **Definir el alcance de R004** en v0.2 (moratorias ya otorgadas, candidatas o ambas).
5. Llevar las preguntas 16, 17 y 25, y las de R004 y R006 de la tabla de arriba, a la próxima reunión con el abogado.
