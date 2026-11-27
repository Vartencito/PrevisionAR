# Previsión AR — Guía de uso

Versión de prueba (v0.1). Para abogados previsionalistas. Lleva unos 5 minutos.

## Qué hace (y qué no)

- **Hace:** calcula los años de servicios con aportes de una historia laboral, detecta superposiciones, verifica si la persona cumple la edad y los 30 años para la PBU (con compensación de edad) y arma un informe con el paso a paso y las normas que respaldan cada regla.
- **No hace todavía:** moratorias, cómputo por hijos, regímenes especiales, cálculo del haber. Tampoco lee la historia laboral de ANSES: los períodos se cargan a mano.
- Es una **herramienta de apoyo**: no reemplaza su criterio profesional ni la resolución de ANSES.

## Privacidad

- El programa corre **solo en su computadora**. No usa internet ni manda datos a ningún servidor.
- Solo se puede abrir desde la misma PC: otra computadora de la red no tiene acceso.
- Esta versión **no guarda nada**: al cerrar, los datos cargados se pierden. Si imprime o guarda el informe como PDF, ese archivo queda donde usted lo guarde.

## Instalar

1. Descargue el archivo `PrevisionAR-v0.1.x-windows.zip` desde la página de versiones del proyecto: <https://github.com/Vartencito/PrevisionAR/releases>.
2. Haga clic derecho en el .zip → **Extraer todo** → elija una carpeta (por ejemplo, Documentos).
3. No hace falta instalar nada más.

## Abrir

1. Entre a la carpeta `PrevisionAR` y haga doble clic en **PrevisionAR.Web.exe**.
2. La primera vez, Windows puede mostrar "Windows protegió su PC" porque el programa no tiene firma comercial. Haga clic en **Más información** → **Ejecutar de todas formas**. Es software libre: el código está publicado en GitHub para quien quiera revisarlo.
3. Se abre una ventana negra (es el programa funcionando: **no la cierre** mientras lo usa) y el navegador con la pantalla de Previsión AR.
4. Si el navegador no se abre solo, escriba esta dirección en el navegador: `http://127.0.0.1:5064`.
5. Para salir, cierre la ventana negra.

## Usar

1. **Probar con un ejemplo:** en "Ejemplos", elija un caso de la lista y toque **Cargar ejemplo**. Debajo aparece qué resultado debería dar. Toque **Calcular** y compare.
2. **Cargar un caso propio:**
   - Referencia del caso (opcional, por ejemplo un número de carpeta). Sale en el informe.
   - Fecha de nacimiento y sexo.
   - Un renglón por cada período de la historia laboral: empleador, desde, hasta (vacío si sigue trabajando), tipo y si tiene aportes. **Agregar período** suma un renglón.
   - Fecha de cálculo: normalmente, la fecha de la solicitud.
3. Toque **Calcular**. Abajo aparece el informe: si cumple los requisitos, el paso a paso, el cómputo separado antes y después de julio de 1994, las superposiciones y los períodos que no se computaron.
4. **Imprimir o guardar como PDF:** toque **Imprimir informe o guardar como PDF**. En la ventana de impresión elija su impresora o "Guardar como PDF". En el papel sale solo el informe, sin los campos de carga.

## Cómo leer el resultado

- **Años, meses y días:** "27a 0m 0d" son 27 años, 0 meses y 0 días.
- **Superposiciones:** cuando dos trabajos se pisan en el tiempo, ese tramo se cuenta una sola vez.
- **Criterios no confirmados:** al final del informe hay una lista de criterios que todavía no confirmó un previsionalista, como el tratamiento de los días sueltos. Si alguno no coincide con lo que hace ANSES, el resultado puede cambiar. **Su opinión sobre esos puntos es lo más útil para nosotros.**

## Si algo no anda o no coincide

- **"No se pudo iniciar: el programa ya está abierto"**: ya hay una ventana negra abierta. Use esa, o ciérrela y vuelva a abrir el programa.
- **La pantalla se ve sin formato**: extraiga el .zip completo antes de abrir el programa (no lo abra desde adentro del .zip).

Anote el caso (con datos ficticios o tapados), qué resultado esperaba y cuál le dio el programa, y páselo al responsable del proyecto. **No envíe datos reales de clientes.**
