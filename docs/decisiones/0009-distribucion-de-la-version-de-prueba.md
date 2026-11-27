# 0009 — Distribución de la versión de prueba: ejecutable autocontenido local

- **Estado:** Aceptada el 2026-11-27 por el responsable. Programada, todavía sin publicar: la primera versión se publica cuando el responsable crea la etiqueta, con la licencia ya decidida (Unlicense, ADR 0002, issue #20)
- **Fecha:** 2026-11-27
- **Issues:** #19 (publicar v0.1) · relacionado con #37 (instalador, v0.4)

## Contexto

Para la v0.1 un abogado tiene que poder probar el programa en su PC sin instalar .NET ni Visual Studio, y sin que los datos de un caso salgan de su computadora. El instalador formal está planificado para la v0.4.

## Opciones

| Opción | Pros | Contras |
|--------|------|---------|
| **A. Un .zip con un .exe autocontenido** (incluye .NET adentro) | Sin instalación ni permisos de administrador. Se arma solo en GitHub Actions. Nada nuevo en el código | Pesa ~100 MB. Windows SmartScreen avisa "editor desconocido" la primera vez (no está firmado) |
| B. Pedir que instalen el runtime de .NET y pasar un .zip chico | Descarga liviana | Un paso técnico más para el abogado; más soporte |
| C. Instalador (MSI/MSIX) ya en v0.1 | Experiencia "normal" de Windows | Más trabajo y firma de código; es lo previsto para la v0.4 |

## Decisión propuesta

**Opción A.** Además, en el código:

1. Fuera de desarrollo, el programa escucha **solo en 127.0.0.1** (la propia PC). Nadie de la red local (por ejemplo, el wifi del estudio) puede abrirlo ni ver los datos.
2. Al arrancar abre el navegador solo, para que el abogado solo haga doble clic. Se desactiva con `--AbrirNavegador=false`.
3. Sin HTTPS ni HSTS: los datos no viajan por la red, y un certificado local solo agregaría avisos del navegador.
4. El workflow `.github/workflows/release.yml` arma el .zip y crea la versión en GitHub Releases (marcada como "versión preliminar") **solo cuando el responsable sube una etiqueta** `v*`. Antes corre los tests.

## Consecuencias

- Publicar una versión = crear la etiqueta (`git tag v0.1.0` y `git push origin v0.1.0`). Lo decide el responsable.
- El aviso de SmartScreen se explica en la guía de uso (`docs/guia-de-uso.md`). Firmar el ejecutable se evalúa con el instalador (#37).
- El puerto fijo 5064: si está ocupado (por ejemplo, el programa ya está abierto), muestra un mensaje en español y espera una tecla. Si pasa seguido, se agrega un puerto libre automático.
- Solo acepta pedidos dirigidos a `127.0.0.1` o `localhost` (`AllowedHosts`), para que una página web no pueda llegar a la aplicación haciendo apuntar su dominio a la PC.

## Pendiente del responsable

- Aprobar esta propuesta.
- ~~Decidir la licencia (#20)~~ Decidido: Unlicense (2026-11-27).
- ~~Crear la etiqueta para publicar la primera versión~~ Publicada la v0.1.0 el 2026-11-27 (versión preliminar): https://github.com/Vartencito/PrevisionAR/releases/tag/v0.1.0
