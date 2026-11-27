# 0006 — Clean Architecture por disparadores y buenas prácticas verificadas

- **Estado:** Aceptada
- **Fecha:** 2026-11-26

## Contexto

El responsable pidió evaluar si conviene aplicar Clean Architecture, patrones de diseño y buenas prácticas para que el producto escale. Se hizo una auditoría completa del código con el agente `revisor-de-codigo`.

## Hallazgos de la auditoría

- La base ya cumple los principios centrales: regla de dependencias, dominio puro, interfaz sin lógica, resultados inmutables y trazables, cuatro capas de tests.
- Había arreglos baratos: superposiciones identificadas por nombre de empleador, enums inválidos tratados en silencio, constructores con muchos enteros posicionales, una constante de corte presentada como norma cuando es "a validar", colecciones sin copiar, bordes sin test y un control de arquitectura incompleto.

## Decisión

1. **Clean Architecture por disparadores:** se mantienen sus principios desde ya, pero las capas de Aplicación, Datos e Importación se agregan solo cuando una funcionalidad las necesita (tabla en `docs/arquitectura.md`).
2. **Arreglos aplicados ahora:** los nueve puntos de bajo costo de la auditoría (superposiciones con períodos y ordenadas, `switch` exhaustivos, argumentos con nombre, `CorteDeTramos` marcado "a validar", copia defensiva, constantes únicas, tests de bordes, controles de arquitectura ampliados).
3. **Analizadores de .NET** activos en toda la solución (sin paquetes externos).
4. **Agente `arquitecto-de-software`:** propone diseños y redacta ADRs antes de cambios grandes. El agente `revisor-de-codigo` controla las buenas prácticas en cada cambio. No se creó un tercer agente "de buenas prácticas" porque esa tarea ya la cubre el revisor.

## Consecuencias

- El producto crece sin reescrituras: cada pieza tiene su momento y su forma ya definidos.
- La IA tiene un mapa (`docs/arquitectura.md`) y controles automáticos que impiden romper las reglas.
- Se evita la sobreingeniería: no hay proyectos vacíos ni patrones sin uso.
