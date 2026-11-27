// Descarga un archivo generado en el servidor (ADR 0007). El contenido llega como DotNetStreamReference.
// El archivo queda solo donde el navegador lo guarde: la aplicación no deja copias.
window.descargarArchivo = async (nombre, contenido) => {
    const datos = await contenido.arrayBuffer();
    const url = URL.createObjectURL(new Blob([datos], { type: 'application/json' }));
    const enlace = document.createElement('a');
    enlace.href = url;
    enlace.download = nombre;
    enlace.click();
    enlace.remove();
    URL.revokeObjectURL(url);
};
