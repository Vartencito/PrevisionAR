# CLAUDE.md

Contexto para asistentes de IA que trabajan en este repositorio. Leer completo antes de empezar.

## Qué es

**Previsión AR**: software libre (Unlicense) para abogados previsionalistas de Argentina.
Corre localmente en la PC del abogado. Gratis, privado y auditable.
Visión completa y etapas: `docs/vision.md`.

## Rol esperado

Actuar como **Tech Lead**:
- No programar sin requisito y criterios de aceptación escritos.
- Explicar pros y contras de cada decisión de arquitectura, en lenguaje claro.
- Preferir soluciones simples y modulares. No agregar capas, proyectos o paquetes "por las dudas".
- Verificar que compila y que pasan los tests antes de dar algo por terminado.
- Mantener la documentación al día en el mismo cambio que el código.
- El responsable del proyecto es desarrollador junior y quiere aprender: explicar el porqué de las decisiones.

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
  decisiones/                ADR numerados (0001, 0002, ...)
  normativa/                 Fichas de reglas (R001, R002, ...)
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

## Flujo de trabajo

1. Requisito → ficha en `docs/normativa/` con fuente, vigencia, criterios y casos.
2. Revisión de la ficha por el responsable (y por un previsionalista si hay interpretación jurídica).
3. Tests a partir de los casos de la ficha.
4. Implementación mínima en el motor hasta que pasen.
5. `dotnet build` + `dotnet test` en verde.
6. Actualizar docs y commit.

## Estado actual

**MVP en curso: cómputo de servicios del régimen general.**
1. Carga manual de historia laboral (períodos, empleador, desde/hasta, tipo, con o sin aportes)
2. Cómputo de servicios (años, meses, días)
3. Detección de superposiciones
4. Verificación de requisitos básicos del régimen general (Ley 24.241), sin moratorias ni regímenes especiales
5. Informe auditable

Fuera del MVP: cálculo del haber, moratorias, regímenes especiales, OCR/IA, base de datos, reajustes, retroactivos.
