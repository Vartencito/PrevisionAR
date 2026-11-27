# Análisis de dos videos sobre el cálculo del haber (2026-11-27)

**Qué son:** dos videos que trajo el responsable, transcritos con su herramienta local (faster-whisper). Son **fuentes secundarias** (divulgación y una clase práctica), no normativa: sirven para generar preguntas, hipótesis e ideas de producto, nunca para fijar una regla (regla 1 de `CLAUDE.md`).

- **Video A:** "Cómo calcular tu jubilación ANSES paso a paso (Actualizado agosto 2025)". Unos 8 minutos, de una abogada previsionalista en YouTube, para el público general.
- **Video B:** "Cálculo del Haber Jubilatorio: ¿Sabes cómo ANSES determina tu haber de jubilación?". Unos 29 minutos: una clase práctica de una abogada para colegas de un consejo profesional, de alrededor de septiembre de 2025.

Las transcripciones completas **no se suben al repositorio** (son material de terceros). Quedan en la PC del responsable: `Desktop\UCEMA\herramientas\transcripciones_previsionar\`.

## Qué dicen, contrastado con lo que ya sabemos

| Tema | Qué dicen los videos (resumido) | Qué sabemos nosotros | Conclusión |
|------|----------------------------------|----------------------|------------|
| Porcentaje por año | A: 30 años = 45 % y **1 % por cada año extra** (35 años = 50 %). B: **1,5 % por año** de servicio | Ley 24.241 (fichas R005) y los casos CD-001, CD-002 y CD-004: 1,5 % por año, al centavo | **El video A está mal o simplifica de más.** No usar. El B coincide con ANSES |
| Fracción de año | B: cuenta la "fracción mayor a 6 meses y un día" | CD-002: más de 6 meses suma un año. CD-001 y CD-004: hasta 6 no suma. CD-003 (BlueCorp): 6 exactos no suman | Coincide. Refuerza la lectura de P-9, pero **sigue siendo doctrina**, no norma |
| Qué remuneraciones se promedian | B: las **últimas 120 de relación de dependencia**, aunque sean de hace décadas o con cortes | CD-002 promedia remuneraciones de 1994 a 2008 con huecos | Coincide. Dato útil para R005 |
| Qué no computa para el haber | B: monotributo (casi siempre), casas particulares, muchas moratorias, UCAP, la moratoria que venció en marzo de 2025, tareas de cuidado y desempleo **solo sirven para llegar a 30 años**, no para el haber | CD-004: la moratoria completa los 30 años pero no entra en la PC ni la PAP. R003: los años por hijo solo sirven para el mínimo | Coincide. **Es el dato más útil para el diseño** (ver abajo) |
| Servicios mixtos | B: la relación de dependencia "computa doble" en los períodos simultáneos | CD-002: promedio ponderado por meses de cada tipo (n, m y p); en los simultáneos se suman los dos promedios | B lo dice de forma imprecisa. La fórmula verificada es la de CD-002 |
| Índice de actualización | A y B: el **RIPTE**. B: coeficiente = índice del mes de actualización ÷ índice del mes de la remuneración | Notas de dominio y R005: índice combinado de la Ley 27.609. BlueCorp cita resoluciones y circulares de ANSES | **Contradicción.** Puede ser una simplificación. Hay que investigarlo en la norma (R005) |
| Hasta cuándo se actualiza | B: hasta la **fecha más tardía** entre la solicitud y el cierre del cómputo, según una resolución de 2021 (número a verificar) | CD-001 (BlueCorp): actualizado a la fecha de solicitud | Pendiente: buscar la resolución (R005) |
| Tope de años | B: la PC tiene un tope de 35 años y **la PAP también**, que recién se alcanzaría en 2029 (35 años desde julio de 1994). Menciona que la ley tiene "más de 9 topes" | Notas de dominio: PC con tope de 35 y **PAP sin tope**. CD-001 tiene 31 años de PAP, así que no lo prueba | **Contradicción.** Investigar (R005) |
| PBU y años de más | B: la PBU sumaba un 1 % por año por encima de 30 y hoy es igual para todos | BlueCorp, en CD-003, informa "años de aporte para la PBU: 36" | **Contradicción** o cambio de norma. Investigar (R005) |
| Haber mínimo | A: en agosto de 2025, 314.305 más un bono de 70.000. CD-002 tiene un "complemento haber mínimo" | Sin ficha todavía | Para la v0.3, con fuente oficial y vigencia por mes (#33) |
| Movilidad | B: mensual por IPC desde abril de 2024 (antes, otras fórmulas). Los valores mensuales (mínimo, máximo, PBU, bases imponibles, cuota UCAP) los saca de un medio privado | Notas: Decreto 274/2024 | Coincide. Los valores tienen que salir de las resoluciones oficiales, no de terceros |
| Prestaciones de ANSES | B repasa: jubilación ordinaria, **PAD** (anticipada, 80 %, para quien tenía 30 años de aportes a junio de 2023; dice que vencía en septiembre de 2025), RTI (retiro por invalidez, 70 % o 50 %), PEA, pensiones, PUAM, PNC y regímenes diferenciales | R006 cubre PEA y PUAM. La PAD **no tiene ficha** (hay una normativa ANSES PREV-11-68/23 citada en preguntas) | Candidata para el "¿a qué puede acceder?" de la v0.2. Verificar si sigue vigente |
| De dónde salen los datos | B: con la clave de ARCA (ex AFIP): SICAM (autónomos, monotributo, casas particulares y planes de pago). Con la de Mi ANSES: historia laboral y padrón histórico de autónomos. Para jubilados, el **detalle del beneficio** | Los casos CD tienen justamente el detalle del beneficio | Responde en parte P-10 y P-28 |
| Errores típicos | B: el SICAM suele estar mal liquidado (categorías mal cargadas, períodos de autónomo pagados que figuran como deuda o prescriptos). Revisarlo permite "rescatar" años de autónomo | — | Idea de producto (ver abajo) |

## Qué se puede hacer con esta información (sin implementar nada todavía)

### 1. Preguntas para el abogado (agregar a la próxima hoja)

1. ¿La PAP tiene un tope de 35 años como la PC, o no tiene tope?
2. ¿La PBU sigue sumando un 1 % por año por encima de 30, o es igual para todos? ¿Desde cuándo?
3. ¿Qué índice usa ANSES hoy para actualizar las remuneraciones: el RIPTE solo, o el combinado de la Ley 27.609? ¿Hasta qué fecha actualiza: la solicitud o el cierre del cómputo, la que sea más tardía?
4. ¿Sigue vigente la jubilación anticipada (PAD)? ¿La usan sus clientes?
5. ¿Qué tipos de servicio cuentan para el haber y cuáles solo para llegar a 30 años? (Confirmar la lista del video B.)

### 2. Investigación (para cuando se libere el límite de dos fichas en investigación)

- **R005 (haber inicial):** índice de actualización y su fecha; la resolución de 2021 que menciona el video B; los topes de la PC, la PAP, las remuneraciones y el haber máximo; la PBU con más de 30 años.
- **Prestación anticipada (PAD):** norma, requisitos, porcentaje y vigencia. Sería una ficha nueva, posiblemente R007.

### 3. Diseño (aporte para el ADR 0008 y el issue #40)

El video B y CD-004 coinciden en algo que el diseño tiene que contemplar: **cada tipo de servicio dice si cuenta para el acceso (los 30 años), para el haber, o para las dos cosas**. Por ejemplo:

| Origen del servicio | ¿Cuenta para los 30 años? | ¿Cuenta para el haber? |
|---------------------|---------------------------|------------------------|
| Relación de dependencia con aportes | Sí | Sí |
| Autónomo con aportes | Sí | Sí |
| Monotributo | Sí | En general no (salvo algunos períodos) |
| Moratorias, UCAP, años por hijo | Sí | No |

Es una hipótesis que hay que **validar con el abogado y con la norma** antes de llevarla a una ficha. Pero conviene que el diseño del ADR 0008 deje lugar para ese dato.

### 4. Ideas de producto (para priorizar con los abogados)

- **Control del detalle del beneficio:** el abogado carga lo que figura en el detalle de ANSES (tiempos tomados para el cálculo, promedios de dependencia y de autónomos, fechas) y el programa recalcula la PC y la PAP y marca las diferencias. **No hace falta el índice de actualización**, porque los promedios vienen en el detalle. Usaría solo reglas que ya verificamos en cuatro casos reales: 1,5 % por año, fracciones y truncamiento de centavos. El video B muestra que los abogados hoy hacen este control a mano o con sistemas pagos.
- **Revisión del SICAM:** una lista de controles para encontrar años de autónomo mal cargados y "rescatarlos". Sería más una guía que un cálculo.
- **Etapa 2 (importar documentos):** las fuentes concretas son la historia laboral de Mi ANSES, el SICAM y el detalle del beneficio.

## Cuidados

- Ninguno de los dos videos es una fuente oficial. Los datos que contradicen lo que ya sabemos (índice, tope de la PAP, PBU) se **investigan en la norma**, no se toman del video.
- Los importes de agosto y septiembre de 2025 (haber mínimo, PBU, bases imponibles) cambian todos los meses: en el programa tienen que salir de las resoluciones oficiales con su vigencia (#33).
