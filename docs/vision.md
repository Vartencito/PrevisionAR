# Visión

## Problema

Los abogados previsionalistas de Argentina dependen de plataformas comerciales pagas para sus cálculos (cómputo de servicios, haber, reajustes, retroactivos). Son cajas negras: no muestran cómo calculan ni qué normativa aplican.

## Propuesta

Una herramienta gratuita, local y transparente, donde cada cálculo se pueda auditar paso a paso con la norma que lo respalda.

## Referentes a estudiar

BlueCorp, JubiLex, Universo Prev, PreviProrrata, Sistema Jauregui, Sistema Moreno, Júbilo.

## Etapas

### Etapa 1: Motor previsional
- Cómputo de servicios y aportes
- Derecho jubilatorio
- Edad y prorrateos
- Superposiciones
- Régimen general; luego regímenes especiales
- Informes auditables

### Etapa 2: Automatización documental
- Historias laborales de ANSES
- Recibos de sueldo
- OCR / IA para extracción de datos
- Validación humana obligatoria

### Etapa 3: Plataforma integral
- Reajustes, retroactivos e intereses
- Ejecución de sentencias
- Cajas provinciales
- Gestión de clientes y expedientes

## Reglas de trabajo

- No se desarrolla sin requisitos y criterios de aceptación.
- No se inventa normativa: cada regla tiene fuente verificable (ver `normativa/`).
- La interpretación jurídica relevante la validan previsionalistas.
- Pruebas unitarias, de integración y de regresión.
- Cambios en visión, licencia, privacidad, arquitectura central o reglas validadas requieren aprobación del responsable del proyecto.

## MVP (aprobado 2026-11-26)

Cómputo de servicios del régimen general:
1. Carga manual de historia laboral
2. Cómputo de servicios (años, meses, días)
3. Detección de superposiciones
4. Verificación de requisitos básicos (Ley 24.241), sin moratorias ni regímenes especiales
5. Informe auditable

Fuera del MVP: cálculo del haber, moratorias, regímenes especiales, OCR/IA, base de datos, reajustes, retroactivos.

## Pendiente

- [x] Definir alcance del MVP
- [ ] R001 — Cómputo de servicios con aportes (implementada; falta validar la convención de días con previsionalista)
- [ ] R002 — Requisitos de edad y servicios para la PBU (implementada; falta validar redondeos con previsionalista)
- [ ] R003 — Cómputo de servicios por hijos (en investigación: norma y vigencia relevadas, faltan criterios y casos)
- [ ] R004 — Moratorias (en investigación)
- [ ] R005 — Haber inicial: PBU, PC y PAP (en investigación)
- [ ] R006 — PUAM y prestación por edad avanzada (en investigación)

Conocimiento del dominio y dudas abiertas: `docs/conocimiento/`.
