# Previsión AR

[![CI](https://github.com/Vartencito/PrevisionAR/actions/workflows/ci.yml/badge.svg)](https://github.com/Vartencito/PrevisionAR/actions/workflows/ci.yml)

Software libre para abogados previsionalistas de Argentina.  
Alternativa gratuita, privada y transparente a las plataformas comerciales de cálculo previsional.

## Principios

- **Gratis e ilimitado.** Todo el núcleo es open source (dominio público, ver `LICENSE`).
- **Local.** Corre en la PC del abogado. Expedientes y documentos quedan en su máquina.
- **Privado.** No se guardan datos de clientes en servidores propios ni se usan para entrenar modelos.
- **Auditable.** Los cálculos los hace un motor determinístico: mismos datos, mismo resultado, con la normativa que respalda cada paso.
- **IA opcional.** Solo para leer documentos y extraer datos. Nunca decide reglas jurídicas ni calcula.

## Estructura

```
PrevisionAR.slnx
src/
  PrevisionAR.Motor/        Motor previsional (sin dependencias externas)
  PrevisionAR.Web/          Interfaz Blazor (corre localmente)
tests/
  PrevisionAR.Motor.Tests/  Pruebas del motor (xUnit)
docs/
  vision.md                 Visión, alcance y etapas
  decisiones/               Registro de decisiones (ADR)
  normativa/                Fichas de reglas previsionales con su fuente
```

## Para abogados

Descargar la versión de prueba desde [Releases](https://github.com/Vartencito/PrevisionAR/releases) y seguir la [guía de uso](docs/guia-de-uso.md). No hace falta instalar nada.

## Requisitos (para desarrollar)

- .NET SDK 10
- Visual Studio 2026 (o `dotnet` CLI)

## Comandos

```
dotnet build
dotnet test
dotnet run --project src/PrevisionAR.Web
```

## Estado

Avance y próximos pasos: [`docs/estado-del-proyecto.md`](docs/estado-del-proyecto.md) · Plan: [`docs/plan-del-proyecto.md`](docs/plan-del-proyecto.md).

MVP en curso. Ya funcionan el cómputo de servicios con aportes (R001), los requisitos de la PBU (R002) y el informe imprimible, pendientes de validación por un previsionalista. Ver `docs/vision.md`.

## Aviso

Herramienta de apoyo. No reemplaza el criterio profesional del abogado.
