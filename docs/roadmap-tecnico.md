# Roadmap técnico

Qué piezas técnicas tiene el proyecto hoy y cuáles se agregan, y en qué momento.
Regla: **una pieza entra cuando una funcionalidad la necesita, no antes.** Los cambios de arquitectura y sus disparadores están en `docs/arquitectura.md`.
La IA actualiza este archivo cada vez que agrega una pieza o cambia el plan.

## Ya incorporado

| Pieza | Para qué | Desde |
|-------|----------|-------|
| Solución .NET 10 (`.slnx`) con Motor, Web y Tests | Base del proyecto | 2026-11-26 |
| `Directory.Build.props`, `.editorconfig`, `.gitattributes` | Configuración y estilo comunes | 2026-11-26 |
| `CLAUDE.md` | Contexto y reglas para la IA | 2026-11-26 |
| ADR en `docs/decisiones/` | Registro de decisiones | 2026-11-26 |
| Fichas en `docs/normativa/` | Punto de control humano de cada regla | 2026-11-26 |
| CI con GitHub Actions | Compila y corre tests en cada push y PR | 2026-11-26 |
| Dependabot (mensual) | Avisa versiones nuevas de paquetes y acciones | 2026-11-26 |
| Test de arquitectura | Verifica que el motor no dependa de nada externo | 2026-11-26 |
| Modelo de dominio (`HistoriaLaboral`, `PeriodoDeServicio`) y resultado trazable | Base del motor; cada resultado indica su regla | 2026-11-26 |
| Test que revisa el código del motor | Prohíbe `DateTime.Now`, `double` y `float` en el motor (alternativa liviana a un analizador, sin paquetes) | 2026-11-26 |
| Pantalla de prueba en la Web | Cargar una historia laboral y ver el cómputo y los requisitos (sin lógica propia) | 2026-11-26 |
| Agente `investigador-previsional` | Investiga normativa y clasifica preguntas con fuentes | 2026-11-26 |
| Agente `revisor-de-codigo` y comandos `/nueva-ficha`, `/implementar-ficha`, `/procesar-respuestas` | Revisión con ojos nuevos y flujo repetible (ADR 0004) | 2026-11-26 |
| Tests de invariantes | Cientos de historias generadas con semilla fija (ADR 0004) | 2026-11-26 |
| Test de trazabilidad ficha ↔ tests | Falla si un caso o criterio de una ficha implementada no tiene test | 2026-11-26 |
| Medición de cobertura en CI | Informe en cada ejecución; por ahora no bloquea | 2026-11-26 |
| Pantalla separada en componentes (`Components/Resultados`) | Base reutilizable para el informe | 2026-11-26 |
| Gestión con Kanban en GitHub y script de estado | Avance, pronóstico y qué hacer ahora (ADR 0005) | 2026-11-26 |
| Analizadores de .NET y arreglos de la auditoría | Calidad verificada al compilar (ADR 0006) | 2026-11-26 |
| Agente `arquitecto-de-software` y `docs/arquitectura.md` | Diseño antes de cambios grandes; evolución por disparadores | 2026-11-26 |

## Próximas piezas (con su disparador)

| Pieza | Se agrega cuando... | Etapa |
|-------|---------------------|-------|
| Umbral mínimo de cobertura que bloquee el CI | La cobertura del motor se estabilice (varias fichas) | MVP |
| Tests de componentes de la UI (bUnit) | La Web tenga lógica propia (hoy solo muestra lo que da el motor) | MVP |
| Generación del informe (PDF o HTML imprimible) | Se defina el formato con los abogados | MVP |
| Persistencia local con SQLite | Haya que guardar casos entre sesiones | Post-MVP |
| Empaquetado e instalador para Windows | Primera versión para que la prueben abogados | Post-MVP |
| Versionado y releases en GitHub | Primera versión publicada | Post-MVP |
| Tests de regresión con casos reales anonimizados | Los abogados aporten casos | Post-MVP |
| OCR y capa de IA (`IAIProvider`) | Etapa 2: automatización documental | Etapa 2 |
