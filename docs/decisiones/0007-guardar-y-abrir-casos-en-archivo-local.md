# 0007 — Guardar y abrir casos en un archivo local

- **Estado:** Propuesta (requiere aprobación del responsable: toca privacidad y suma un proyecto de tests)
- **Fecha:** 2026-11-27
- **Tarea:** issue #15 (versión v0.1)

## Contexto

Hoy la pantalla (`src/PrevisionAR.Web/Components/Pages/Home.razor`) guarda todo en memoria: si el abogado cierra el navegador o la app, pierde la historia laboral cargada. El requisito del issue #15: **guardar el caso en un archivo en su PC y volver a abrirlo después**. El caso incluye fecha de nacimiento, sexo, fecha de cálculo y los períodos (empleador, desde, hasta, tipo, con o sin aportes).

Condiciones que ya están fijadas:

- **Sin base de datos.** SQLite está planificado para v0.4 (issue #36), con su propio ADR.
- **El motor no accede a disco** (regla 3). El archivo es un detalle de la Web.
- **La app es Blazor Server local:** el "servidor" y el navegador corren en la misma PC, pero el código C# corre en el servidor. Para que el navegador descargue o suba un archivo hay que usar los mecanismos de Blazor.
- **Privacidad:** el archivo va a tener datos reales de clientes. Queda en la PC del abogado, bajo su responsabilidad, igual que un Word o un PDF del expediente. **Nunca en el repositorio** (tampoco como ejemplo o en tests).

### ¿Se cumple algún disparador de `docs/arquitectura.md`?

| Disparador | ¿Se cumple? | Por qué |
|-----------|-------------|---------|
| Capa de datos (guardar casos con SQLite) | No | Un archivo que el usuario descarga y sube no es una base de datos ni necesita repositorios. |
| Capa de aplicación (casos de uso con varios pasos o segunda interfaz) | No, todavía | Guardar y abrir son un solo paso cada uno, desde la misma pantalla. Revisar cuando llegue el informe (#16) o SQLite (#36). |
| Value object de `PeriodoDeServicio` (segundo lugar de entrada de datos) | No | El archivo se carga **en el formulario**, no directo en el motor. Los datos pasan por el mismo camino que la carga a mano (formulario → `Calcular` → validación del motor). Si algún día un archivo se pasa directo al motor, sí se cumple. |

Conclusión: no hace falta ninguna capa ni proyecto de producción nuevo. Sí hay que decidir **cómo se testea** (hoy no hay tests de la Web).

## Decisión 1 — Cómo viaja el archivo entre la app y la PC

### Opción A: descarga con JS interop mínimo + subida con `InputFile` (recomendada)

- **Guardar:** el C# arma el JSON y se lo pasa al navegador con `DotNetStreamReference`; una función JavaScript de unas diez líneas crea el archivo y dispara la descarga. Es el ejemplo oficial de Microsoft para Blazor.
- **Abrir:** el componente `InputFile`, que viene con Blazor (sin JS propio). El usuario elige el archivo y el C# lo lee con un tamaño máximo.

| Pros | Contras |
|------|---------|
| Es lo que el abogado ya conoce: "Descargar" y "Elegir archivo". | El navegador decide dónde guarda (por lo general "Descargas", salvo que el usuario tenga "preguntar dónde guardar"). |
| Funciona en cualquier navegador y aunque algún día la app no corra en la misma PC. | Hay un archivo JS chico que mantener. |
| Sin paquetes. Nada queda escrito en disco por la app: el archivo existe solo donde el usuario lo guarda. | La app no "recuerda" los casos: no hay lista de casos recientes (eso llega con SQLite). |

**Costo:** bajo (dentro de las 4 h del issue).

### Opción B: la app escribe directo en una carpeta (por ejemplo `Documentos\PrevisionAR\Casos`)

| Pros | Contras |
|------|---------|
| Sin JavaScript. | Hay que hacer una pantalla para listar, nombrar, renombrar y borrar archivos: es un mini explorador de archivos. |
| Se puede mostrar una lista de casos guardados. | Los nombres de archivo terminan llevando el nombre del cliente (más exposición en la carpeta). |
| | Depende de que servidor y navegador estén en la misma PC; si cambia, escribe en la máquina equivocada. |
| | Se superpone con lo que va a hacer SQLite en v0.4: trabajo que después se tira. |

**Costo:** medio.

### Opción C: API de acceso a archivos del navegador (`showSaveFilePicker`)

Permite elegir carpeta y sobrescribir el mismo archivo, pero solo en Chrome y Edge, con más JavaScript y permisos del navegador. **Descartada:** más complejidad para una mejora chica.

**Recomendación: opción A.**

## Decisión 2 — Formato del archivo

**JSON con número de versión del formato**, escrito con `System.Text.Json` (viene con .NET, no es un paquete nuevo).

- **Por qué JSON:** se lee con cualquier editor (auditable), lo arma .NET sin paquetes y tolera agregar campos en versiones futuras.
- **Extensión sugerida:** `.previsionar.json` (por ejemplo `caso-2026-11-27.previsionar.json`). Sigue siendo JSON y se distingue de otros archivos.
- **Se guardan solo los datos de entrada, nunca los resultados.** El motor es determinístico: al abrir, se vuelve a calcular. Así un archivo viejo no muestra un resultado calculado con reglas que después cambiaron.
- **Se guarda el estado del formulario tal cual**, aunque esté incompleto o tenga errores (un período con fechas invertidas, por ejemplo). El abogado tiene que poder guardar un caso a medio cargar; los errores aparecen al calcular, como hoy.

Ejemplo (datos ficticios):

```json
{
  "formato": "PrevisionAR.Caso",
  "versionDelFormato": 1,
  "guardadoConLaVersion": "0.1.0",
  "referencia": "Caso de prueba",
  "persona": { "fechaDeNacimiento": "1958-04-15", "sexo": "hombre" },
  "fechaDeCalculo": "2026-01-01",
  "periodos": [
    { "empleador": "Empresa A (ficticia)", "desde": "1990-01-01", "hasta": "2009-12-31",
      "tipo": "relacionDeDependencia", "origen": "conAportes" }
  ]
}
```

Detalles que importan:

- **Fechas** en formato `AAAA-MM-DD`, sin depender de la configuración regional.
- **Valores con nombre** (`"hombre"`, `"autonomo"`), nunca números de enum: si el orden de un enum cambia, el archivo no se corrompe.
- **El archivo no usa los tipos del motor directamente.** Tiene sus propios registros (DTO) en la Web, y la conversión se hace con `switch` exhaustivos. Si el motor renombra algo, el formato no cambia sin que alguien lo decida.
- **Campo `origen` desde la versión 1** (en lugar de `conAportes: true/false`). Hoy solo admite `conAportes` y `sinAportes`, y se convierte al `bool ConAportes` del motor. Cuando llegue el issue #40 (enum de origen, ver ADR 0008), se suman valores nuevos (`moratoriaLey24476`, `ucap`) **sin cambiar de versión ni migrar archivos**.
- **`guardadoConLaVersion`** es informativo: permite avisar "este caso se guardó con la versión 0.1; los resultados pueden diferir si cambiaron reglas".

### Versiones futuras del formato

| Situación al abrir | Qué hace la app |
|--------------------|-----------------|
| `versionDelFormato` igual a la actual | Abre normal. |
| Versión anterior | La convierte paso a paso (`De1A2`, `De2A3`...) en la misma clase, y abre. Cada paso es una función chica con su test. |
| Versión posterior (archivo de una app más nueva) | No abre. Mensaje claro: "Este caso se guardó con una versión más nueva de Previsión AR. Actualizá el programa para abrirlo." |
| `formato` distinto o JSON inválido | No abre. Mensaje: "El archivo no es un caso de Previsión AR o está dañado." |
| Un valor desconocido (por ejemplo `"sexo": "otro"`) | No abre y dice qué campo y qué fila. **Nunca** se reemplaza en silencio por un valor por defecto. |

Al abrir, si hay cualquier error, **no se carga nada a medias**: el formulario queda como estaba.

**Regla de compatibilidad:** se sube la versión solo cuando un archivo viejo ya no se puede leer igual (se renombra o se quita un campo, o cambia su significado). Agregar un campo opcional no sube la versión.

## Decisión 3 — ¿Se guarda el nombre del cliente?

Esto es **privacidad y manejo de datos: lo decide el responsable**. El motor no recibe nunca este dato (`Persona` no tiene datos identificatorios, y así sigue).

| Opción | Qué se guarda | Pros | Contras |
|--------|---------------|------|---------|
| **1. Nada identificatorio** | Solo fechas, sexo y períodos | El archivo dice lo mínimo; si se pierde, cuesta saber de quién es. | El abogado identifica el caso solo por el nombre del archivo, que en la práctica igual va a llevar el nombre del cliente. Con varios casos, confunde. |
| **2. Campo libre opcional "Referencia del caso"** (recomendada) | Un texto que escribe el abogado, vacío por defecto (por ejemplo "Expte. 1234" o "García, J.") | El abogado decide cuánto identifica. Sirve para reconocer el caso al abrirlo y para el encabezado del informe (#16). Un solo campo, sin validar formato. | Si escribe el nombre completo, el archivo tiene un dato personal más (aunque el archivo ya es, de por sí, un dato personal: la historia laboral). |
| **3. Datos identificatorios completos** (nombre, DNI, CUIL) | Campos estructurados | Útil para un informe formal o para cruzar con documentos de ANSES. | Más datos sensibles sin un caso de uso concreto hoy. Si el informe los necesita, se decide en #16 (agregar un campo opcional no rompe el formato). |

**Recomendación: opción 2**, con una aclaración junto al campo: "Opcional. Se guarda en el archivo del caso, en tu PC. Nunca se envía a ningún lado."

**Otros cuidados de privacidad:**

- El archivo **no se cifra**. Es un archivo más del expediente y su resguardo es responsabilidad del abogado, como cualquier documento. Si los abogados piden cifrado, va a un ADR aparte.
- Para evitar que un desarrollador suba por error un caso real, en la implementación se agrega `*.previsionar.json` al `.gitignore`. Los ejemplos de los tests van como texto dentro del código, con datos ficticios.
- La app no deja copias: no hay archivos temporales ni registros (logs) con el contenido del caso.

## Decisión 4 — Dónde vive el código y cómo se prueba

### Dónde vive (Web, nunca Motor)

```
src/PrevisionAR.Web/
  Casos/
    CasoEnEdicion.cs        Estado del formulario (hoy son campos privados y la clase FilaPeriodo de Home.razor). Sin lógica de negocio.
    ArchivoDeCaso.cs        Registros del formato JSON (DTO), versión 1.
    ArchivoDeCasos.cs       Clase estática pura: CasoEnEdicion → JSON y JSON → CasoEnEdicion. Sin disco, sin Blazor, sin JS.
  wwwroot/js/descargas.js   Función de descarga (unas diez líneas).
  Components/Pages/Home.razor   Botones "Guardar caso" y "Abrir caso"; usa CasoEnEdicion.
```

Firma de ejemplo (orientativa, no es código definitivo):

```csharp
/// <summary>Convierte un caso en edición a archivo y viceversa. Formato: ADR 0007.</summary>
public static class ArchivoDeCasos
{
    public const int VersionActual = 1;

    public static string Guardar(CasoEnEdicion caso, string versionDeLaApp);

    /// <summary>Nunca lanza por datos del archivo: devuelve el caso o un mensaje para el usuario.</summary>
    public static AperturaDeCaso Abrir(string contenido);
}

public sealed record AperturaDeCaso(CasoEnEdicion? Caso, string? Error);
```

No hace falta una interfaz (puerto) `IRepositorioDeCasos`: hay una sola forma de guardar y nada que reemplazar en los tests (la clase es pura). La interfaz se evalúa con SQLite (#36), si las dos formas tienen que convivir.

### Cómo se prueba: opciones

| Opción | Pros | Contras |
|--------|------|---------|
| **T1. Proyecto nuevo `tests/PrevisionAR.Web.Tests`** (xUnit, mismos paquetes y versiones que `PrevisionAR.Motor.Tests`; recomendada) | Prueba el código real de la Web. Deja la casa lista para los tests de componentes (bUnit) del roadmap, cuando la Web tenga lógica propia. Respeta la regla "Tests → lo que prueban". | Es un **proyecto nuevo** (arquitectura central: necesita el "sí" del responsable). Suma unos segundos al CI. |
| T2. Que `PrevisionAR.Motor.Tests` referencie a la Web | Sin proyecto nuevo. | Rompe la regla 7 tal como está escrita y mezcla tests del dominio con tests de la interfaz. El test de arquitectura del motor quedaría en un proyecto que depende de ASP.NET. |
| T3. Poner la serialización en el Motor | Ya tiene tests. | El formato del archivo es un detalle externo, no una regla previsional. Ensucia el dominio. **Descartada.** |
| T4. Clase pura en la Web sin tests automáticos | Cero costo hoy. | Viola "nunca entregar código sin tests". Un error en el formato hace perder casos de clientes. **Descartada.** |

**Recomendación: T1.** No agrega tipos de paquete nuevos (usa xUnit, el SDK de tests y coverlet, ya aprobados). Si el responsable no aprueba el proyecto, la alternativa menos mala es T2, cambiando la regla 7 de `CLAUDE.md`, que también requiere su aprobación.

Con T1 las dependencias quedan: `Web → Motor`, `Motor.Tests → Motor`, `Web.Tests → Web`.

### Tests a escribir

- **Ida y vuelta:** guardar y abrir devuelve el mismo caso (con y sin `hasta`, con referencia vacía, con períodos inválidos).
- **Archivo de referencia de la versión 1:** un JSON fijo (ficticio) escrito en el test, que **nunca se modifica**. Cuando exista la versión 2, ese test sigue pasando: es la garantía de que los casos viejos de los abogados siguen abriendo.
- **Errores:** versión posterior, formato distinto, JSON roto, valor de enum desconocido, archivo vacío. Cada uno con su mensaje y sin carga parcial.
- **Formato estable:** las fechas salen como `AAAA-MM-DD` y los enums como texto.
- **Arquitectura** (en `PrevisionAR.Motor.Tests/Arquitectura/CodigoDelMotorTests.cs`): agregar `System.Text.Json` y `JsonSerializer` a los patrones prohibidos en el motor, para que la serialización no se cuele en el dominio.
- **Arquitectura de la Web** (en el proyecto nuevo): `ArchivoDeCasos` no usa `File.`, `Directory.` ni `IJSRuntime` (se mantiene pura y testeable).

## Criterios de aceptación (para implementar después)

1. En la pantalla hay un botón **"Guardar caso"** que descarga un archivo `.previsionar.json` con fecha de nacimiento, sexo, fecha de cálculo, referencia (si se aprueba la decisión 3) y todos los períodos.
2. El nombre sugerido es `caso-AAAA-MM-DD.previsionar.json` (fecha del día). No incluye la referencia ni datos del cliente.
3. Se puede guardar un caso incompleto o con errores; el archivo refleja exactamente lo que había en pantalla.
4. Hay un botón **"Abrir caso"** (con `InputFile`) que carga el archivo en el formulario. Después, "Calcular" da el mismo resultado que antes de guardar.
5. El archivo no contiene resultados del cálculo. Al abrir, los resultados en pantalla se limpian.
6. Archivos de más de 1 MB se rechazan con un mensaje.
7. Un archivo de una versión posterior, de otro formato, dañado o con un valor desconocido se rechaza con un mensaje claro en castellano, y el formulario queda como estaba.
8. El archivo usa fechas `AAAA-MM-DD`, valores con nombre y el campo `versionDelFormato` igual a 1.
9. El motor no cambia (ni referencias ni código) por esta funcionalidad.
10. `*.previsionar.json` está en `.gitignore`.
11. Tests de ida y vuelta, archivo de referencia v1 y errores en verde en el CI.
12. `docs/arquitectura.md` (mapa y regla de dependencias), `CLAUDE.md` (estructura y regla 7) y `docs/roadmap-tecnico.md` actualizados en el mismo cambio.

**Prueba manual sugerida:** cargar el ejemplo, guardar, recargar la página (se pierde todo), abrir el archivo, calcular y comparar con el resultado anterior. Abrir el archivo con el Bloc de notas y comprobar que se entiende.

## Consecuencias

- El abogado puede retomar un caso sin volver a cargarlo, sin instalar nada más y sin base de datos.
- Los datos reales quedan solo donde el abogado guarda el archivo. La app no guarda copias.
- El formato queda versionado desde el primer día: los casos guardados en v0.1 se van a poder abrir en versiones futuras (y migrar a SQLite en v0.4).
- `CasoEnEdicion` saca el estado del formulario de `Home.razor`: la pantalla queda más simple y lista para el informe (#16).
- Se suma un proyecto de tests (si se aprueba T1) y un archivo JS chico.

## Pendiente del responsable

1. **Aprobar o elegir** cómo viaja el archivo (recomendado: opción A).
2. **Privacidad:** ¿se guarda una "Referencia del caso" opcional (recomendado), nada identificatorio, o datos completos?
3. **Proyecto de tests:** ¿se crea `tests/PrevisionAR.Web.Tests` (recomendado) o se cambia la regla 7 para que `Motor.Tests` pruebe la Web?
4. Confirmar que **no se cifra** el archivo en v0.1 (queda bajo resguardo del abogado, como el resto del expediente).
