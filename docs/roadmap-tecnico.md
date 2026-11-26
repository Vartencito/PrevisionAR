# Roadmap técnico

Qué piezas técnicas tiene el proyecto hoy y cuáles se agregan, y en qué momento.
Regla: **una pieza entra cuando una funcionalidad la necesita, no antes.**
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

## Próximas piezas (con su disparador)

| Pieza | Se agrega cuando... | Etapa |
|-------|---------------------|-------|
| Modelo de dominio (`HistoriaLaboral`, `PeriodoDeServicio`) | Se apruebe la primera ficha (R001) | MVP |
| Resultado trazable (cada número con su regla) | Primer cálculo del motor | MVP |
| Analizador que prohíba `DateTime.Now` y `double` en el motor | Exista código de dominio en el motor | MVP |
| Medición de cobertura de tests en CI | Haya varias reglas implementadas | MVP |
| Tests de componentes de la UI (bUnit) | La Web tenga la primera pantalla con lógica | MVP |
| Generación del informe (PDF o HTML imprimible) | Se defina el formato con los abogados | MVP |
| Persistencia local con SQLite | Haya que guardar casos entre sesiones | Post-MVP |
| Empaquetado e instalador para Windows | Primera versión para que la prueben abogados | Post-MVP |
| Versionado y releases en GitHub | Primera versión publicada | Post-MVP |
| Tests de regresión con casos reales anonimizados | Los abogados aporten casos | Post-MVP |
| OCR y capa de IA (`IAIProvider`) | Etapa 2: automatización documental | Etapa 2 |
