# Arquitectura

Cómo está armado Previsión AR, qué principios se respetan y **cuándo** se agrega cada pieza.
Regla general: la arquitectura más simple que alcanza hoy, con el camino de crecimiento ya pensado.

## Mapa actual

```
┌────────────────────────┐        ┌────────────────────────────┐
│ PrevisionAR.Web        │ ─────► │ PrevisionAR.Motor          │
│ Blazor, local          │        │ Dominio: reglas y cálculos │
│ Carga, muestra y       │        │ Sin dependencias           │
│ guarda casos (Casos/)  │        └────────────────────────────┘
└────────────────────────┘                    ▲
            ▲                                 │
┌────────────────────────┐    ┌────────────────────────┐
│ PrevisionAR.Web.Tests  │    │ PrevisionAR.Motor.Tests│
└────────────────────────┘    └────────────────────────┘
```

## Principios de Clean Architecture que ya se cumplen

| Principio | Cómo se cumple | Quién lo controla |
|-----------|----------------|-------------------|
| Regla de dependencias: el dominio no depende de nada | El Motor no referencia proyectos ni paquetes | `ReglasDeArquitecturaTests` |
| El dominio no conoce detalles externos (disco, red, reloj, azar, formato de archivos) | Todo entra como parámetro, incluida la fecha de cálculo. El archivo de caso vive en la Web (ADR 0007) | `CodigoDelMotorTests` |
| La conversión del archivo de caso es pura | `PrevisionAR.Web/Casos` no usa disco, JavaScript ni Blazor | `ArchivoDeCasosPuroTests` |
| La interfaz no tiene lógica de negocio | La Web arma los datos, llama al Motor y muestra el resultado | Revisión (agente `revisor-de-codigo`) |
| Resultados trazables e inmutables | Records con la propiedad `Regla` | `Motor_TiposResultado_IndicanLaReglaQueLosProdujo` |

**No se separó en proyectos Domain / Application / Infrastructure** porque todavía no hay nada para poner en las dos últimas. Se agregan cuando llegue su disparador (abajo).

## Buenas prácticas del código

- **Funciones del dominio sin estado** (clases estáticas) mientras no haya nada que reemplazar. Son lo más fácil de probar y de leer.
- **Records inmutables** para datos y resultados. Las colecciones se copian al construir (`HistoriaLaboral`).
- **Argumentos con nombre** al construir resultados con muchos números del mismo tipo.
- **`switch` exhaustivo con error** en enums: un valor inválido nunca se trata en silencio como otro.
- **Convenciones "A VALIDAR" aisladas** en un solo lugar y comentadas con el número de pregunta.
- **Constantes con nombre y norma** (`EdadHombresMeses`, `CorteDeTramos`), nada de números mágicos.
- **Fechas en texto con formato fijo** (sin depender de la configuración regional).
- **Analizadores de .NET** activos en toda la solución (`AnalysisLevel latest-recommended`, estilo del `.editorconfig` verificado al compilar). En el Motor, toda advertencia es error.

## Evolución por disparadores

| Cambio | Disparador | Qué se hace |
|--------|-----------|-------------|
| Servicios acreditados y orquestador | R003 o R004 (sumar meses que solo valen para el mínimo de la PBU) | `RequisitosPbu` recibe `ServiciosAcreditados` (R001 + R003 + R004, cada parte con su regla). Un orquestador simple (`EvaluacionDeDerecho`) ejecuta R002 y, si no alcanza, R006. Código C# directo, no un motor de reglas genérico |
| Origen del servicio | R004 (moratorias) | `bool ConAportes` pasa a un enum `OrigenDelServicio` (con aportes, sin aportes, moratoria Ley 24.476, UCAP, años por hijo). Cada fórmula filtra lo que su ficha permite |
| Constantes con vigencia | Primera regla con dos valores según la fecha (por ejemplo, PAP 0,85% o 1,5%) | Tabla `Vigencias<T>` dentro del Motor: filas (desde, hasta, valor, norma) y `ValorAl(fecha)`, con un test por cada borde |
| Series oficiales que se publican seguido | R005 (índices de actualización, haber mínimo y máximo) | La Web o una capa de datos las carga y se las pasa al Motor como objeto (`SerieDeIndices`). El resultado dice qué serie y qué versión usó. El Motor nunca lee archivos |
| Explicación estructurada | Informe que necesite citar la norma en una columna | `IReadOnlyList<string>` pasa a `PasoDeExplicacion(Texto, Norma, Valores)` |
| Validación que junte todos los errores | Formulario con muchas filas o importación de documentos | `ValidarHistoria` devuelve todas las observaciones; las excepciones quedan como red de seguridad. Sin tipo `Result<T>` genérico |
| Value object de `PeriodoDeServicio` | Segundo lugar de entrada de datos (SQLite o importación) | La validación pasa al constructor o a una fábrica `Crear` |
| Formato de números con cultura fija | Primer importe (`decimal`) que aparezca en un texto | Formatear con `CultureInfo` fija y testearlo |
| Capa de aplicación | Casos de uso con varios pasos (cargar, calcular, guardar, generar informe) o una segunda interfaz | Proyecto `PrevisionAR.Aplicacion` con los casos de uso y las interfaces (puertos) hacia afuera |
| Capa de datos | Guardar casos en la PC (SQLite) | Proyecto `PrevisionAR.Datos`, referenciado solo desde afuera del Motor, con sus propios modelos que se convierten a los del Motor. Requiere ADR y aprobación |
| Importación con IA | Etapa 2 | Proyecto aparte que produce "períodos propuestos". Un humano los valida antes de convertirlos en `PeriodoDeServicio` (regla 5) |
| App local segura | Empaquetado para Windows | `Program.cs`: solo http en `localhost`, sin HTTPS de plantilla, sin escuchar en la red |
| Trazabilidad más estricta en tests | Dos fichas que compartan archivo de tests | Pasar a `// R003 Caso 2` o `[Trait("Ficha", "R003")]` |

Para cualquier cambio de esta tabla se usa el agente `arquitecto-de-software`, que propone opciones y redacta el ADR.

## Lo que no se va a usar (por ahora o nunca)

- Motores de reglas genéricos o configurables (interfaz `IRegla` con reflexión, reglas en JSON), MediatR, CQRS, event sourcing, repositorios genéricos, DDD con agregados. Es demasiado para esta escala y dificulta el trabajo de una IA sin memoria.
- Inyección de dependencias o abstracciones del reloj dentro del Motor: la fecha como parámetro ya lo resuelve.
- Paquetes de validación o de "Result" dentro del Motor (rompen la regla 3).
- Clases con estado modificable para resultados o tiempos.
