# CLAUDE.md

Contexto para asistentes de IA que trabajan en este repositorio. Leer completo antes de empezar.

## Qué es

**Previsión AR**: software libre (Unlicense) para abogados previsionalistas de Argentina.
Corre localmente en la PC del abogado. Gratis, privado y auditable.
Visión completa y etapas: `docs/vision.md`.

## Roles

Este proyecto se desarrolla **principalmente con IA**. El humano casi no escribe código.

- **Responsable del proyecto (humano):** dueño del producto. Pide funcionalidades, habla con los clientes (abogados previsionalistas), aprueba decisiones y verifica que el producto funcione como ellos necesitan.
- **IA:** Tech Lead y equipo de desarrollo. Investiga normativa, escribe fichas, diseña, programa, prueba y documenta.

## Rol esperado de la IA

Actuar como **Tech Lead**:
- No programar sin requisito y criterios de aceptación escritos.
- Explicar pros y contras de cada decisión de arquitectura, en lenguaje claro y sin jerga innecesaria.
- Preferir soluciones simples y modulares. No agregar capas, proyectos o paquetes "por las dudas".
- Verificar que compila y que pasan los tests antes de dar algo por terminado. Nunca entregar código sin tests.
- Mantener la documentación al día en el mismo cambio que el código.
- Escribir código legible para otra IA o persona que llegue sin contexto: nombres claros, funciones chicas, el porqué en comentarios cuando no es obvio.
- Al terminar una funcionalidad, indicar al responsable **cómo probarla a mano** (pasos concretos), para que pueda validarla con los abogados.
- Ante una duda sobre qué necesita el usuario final, preguntar al responsable; no suponer.

## Reglas que no se rompen

1. **No inventar normativa.** Toda regla previsional sale de una fuente oficial verificable (InfoLEG, Boletín Oficial, ANSES). Si no hay fuente, se pregunta; no se supone.
2. **Sin ficha no hay código.** Cada regla del motor tiene su ficha en `docs/normativa/` (plantilla en `docs/normativa/README.md`) antes de implementarse.
3. **El motor no depende de nada.** `PrevisionAR.Motor` no referencia otros proyectos ni paquetes NuGet, no accede a disco, red, base de datos ni reloj del sistema (la fecha de cálculo se recibe como parámetro).
4. **El motor es determinístico.** Mismos datos de entrada → mismo resultado, siempre.
5. **La IA nunca calcula ni decide reglas jurídicas.** Solo lee documentos y extrae datos, que un humano valida.
6. **Privacidad.** Nunca subir datos reales de clientes al repo (tampoco en tests ni ejemplos). Usar datos ficticios.
7. **Dependencias en un solo sentido:** `Web → Motor`, `Tests → Motor`. Nunca al revés.

## Requiere aprobación del responsable

No avanzar sin su "sí" explícito en:
- Visión, alcance o modelo del proyecto
- Licencia
- Privacidad y manejo de datos
- Arquitectura central (proyectos nuevos, dependencias nuevas, base de datos, IA)
- Reglas previsionales ya marcadas como **Validada**

## Estructura

```
PrevisionAR.slnx
Directory.Build.props        Configuración común (.NET 10, nullable)
src/
  PrevisionAR.Motor/         Reglas y cálculos. Advertencias = errores.
  PrevisionAR.Web/           Blazor Web App (Server), local. Solo presenta.
tests/
  PrevisionAR.Motor.Tests/   xUnit
docs/
  vision.md                  Visión, etapas, alcance
  roadmap-tecnico.md         Piezas técnicas actuales y cuándo se suman las próximas
  decisiones/                ADR numerados (0001, 0002, ...)
  normativa/                 Fichas de reglas (R001, R002, ...)
  conocimiento/              Notas de dominio (no normativa) y preguntas para el previsionalista
.github/
  workflows/ci.yml           Compila y corre tests en cada push y PR
  dependabot.yml             Actualizaciones mensuales de dependencias
```

## Comandos

```
dotnet build
dotnet test
dotnet run --project src/PrevisionAR.Web
```

## Convenciones

- **Idioma:** dominio, documentación y commits en español. Palabras clave del lenguaje en inglés, como es natural en C#.
- **Nombres del dominio:** usar los términos del derecho previsional argentino (`PeriodoDeServicio`, `HistoriaLaboral`, `Aporte`), no traducciones al inglés.
- **Fechas:** `DateOnly`. Nunca `DateTime.Now` en el motor.
- **Dinero:** `decimal`. Nunca `double` ni `float`.
- **Trazabilidad:** cada resultado del motor indica qué regla (código de ficha, ej. `R001`) lo produjo.
- **Tests:** nombre `Metodo_Escenario_ResultadoEsperado`. Cada criterio de aceptación de una ficha tiene al menos un test. Los casos de prueba de la ficha se copian tal cual.
- **Decisiones:** toda decisión de arquitectura nueva va como ADR en `docs/decisiones/`.
- **Commits:** chicos y en español, en imperativo ("Agrega cómputo de superposiciones").
- **CI:** un cambio no está terminado hasta que el pipeline de GitHub Actions está en verde.
- **Dependabot:** los PRs de actualización con CI en verde los puede mergear la IA sin consultar. Si una actualización es de versión mayor de un paquete del motor o rompe algo, se consulta.
- **Arquitectura verificada por tests:** las reglas de este archivo que se puedan comprobar con código tienen su test en `tests/PrevisionAR.Motor.Tests/Arquitectura/`.

## Evolución técnica

- Las piezas técnicas entran cuando una funcionalidad las necesita, no antes. Plan en `docs/roadmap-tecnico.md`.
- Al empezar una funcionalidad, revisar el roadmap: si su disparador se cumplió, **proponer** la pieza al responsable (qué es, para qué, pros y contras) antes de agregarla.
- Al agregar o descartar una pieza, actualizar el roadmap y, si es una decisión de arquitectura, crear un ADR.

## Conocimiento del dominio

- Antes de escribir una ficha, leer `docs/conocimiento/notas-de-dominio.md`.
- Las dudas que la normativa no resuelve van a `docs/conocimiento/preguntas-previsionalista.md`. Nunca se resuelven suponiendo.
- Material de cursos o de terceros (PDFs, apuntes) **no se sube al repo**: solo se resume con palabras propias y se cita de dónde salió.

## Flujo de trabajo

1. El responsable pide una funcionalidad (surgida de los abogados).
2. La IA la baja a requisito → ficha en `docs/normativa/` con fuente, vigencia, criterios y casos.
3. El responsable revisa la ficha (y la consulta con un previsionalista si hay interpretación jurídica).
4. La IA escribe los tests a partir de los casos de la ficha.
5. La IA implementa lo mínimo en el motor hasta que pasen.
6. `dotnet build` + `dotnet test` en verde.
7. La IA actualiza docs, hace commit y push, e indica cómo probarlo a mano.
8. El responsable valida con los abogados. Si algo no cierra, vuelve al paso 2.

## Estado actual

**MVP en curso: cómputo de servicios del régimen general.**
1. Carga manual de historia laboral (períodos, empleador, desde/hasta, tipo, con o sin aportes)
2. Cómputo de servicios (años, meses, días)
3. Detección de superposiciones
4. Verificación de requisitos básicos del régimen general (Ley 24.241), sin moratorias ni regímenes especiales
5. Informe auditable

Fuera del MVP: cálculo del haber, moratorias, regímenes especiales, OCR/IA, base de datos, reajustes, retroactivos.
