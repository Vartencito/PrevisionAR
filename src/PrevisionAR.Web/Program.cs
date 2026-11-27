using System.Diagnostics;
using System.Text;
using PrevisionAR.Web.Components;

// Para que los mensajes en español de la ventana del programa muestren bien los acentos.
Console.OutputEncoding = Encoding.UTF8;

// La carpeta de contenido es la del programa, no la carpeta desde donde se lo lanza: así encuentra
// wwwroot aunque se abra desde un acceso directo con otra carpeta de inicio.
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
});

// Versión para el abogado (ADR 0009): fuera de desarrollo, escucha SOLO en esta PC (127.0.0.1),
// así nadie de la red local puede ver los datos de un caso. Si se pasa --urls, se respeta.
var esVersionDelAbogado = !builder.Environment.IsDevelopment();
const string DireccionLocal = "http://127.0.0.1:5064";
if (esVersionDelAbogado)
{
    if (string.IsNullOrEmpty(builder.Configuration["urls"]))
    {
        builder.WebHost.UseUrls(DireccionLocal);
    }

    // Solo acepta pedidos dirigidos a esta PC por nombre: evita que una página web maliciosa
    // haga apuntar su dominio a 127.0.0.1 y lea la aplicación ("DNS rebinding").
    builder.Configuration["AllowedHosts"] = "127.0.0.1;localhost";
}

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (esVersionDelAbogado)
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // Abre el navegador al arrancar, para que el abogado solo tenga que hacer doble clic en el programa.
    // Se puede desactivar con --AbrirNavegador=false (por ejemplo, para pruebas automáticas).
    if (app.Configuration.GetValue("AbrirNavegador", defaultValue: true))
    {
        app.Lifetime.ApplicationStarted.Register(() => AbrirNavegador(app.Urls.FirstOrDefault() ?? DireccionLocal));
    }
}

// Sin HTTPS ni HSTS a propósito: el programa corre y se usa solo en la misma PC (127.0.0.1),
// los datos no viajan por la red y un certificado local solo agregaría avisos de seguridad del navegador.
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

try
{
    app.Run();
}
catch (IOException ex) when (esVersionDelAbogado)
{
    // El caso típico: el programa ya está abierto (doble clic dos veces) y el puerto está ocupado.
    Console.WriteLine("No se pudo iniciar Previsión AR: el programa ya está abierto o el puerto 5064 está en uso.");
    Console.WriteLine($"Si ya está abierto, use la ventana del navegador en {DireccionLocal}.");
    Console.WriteLine($"Detalle técnico: {ex.Message}");
    Console.WriteLine("Presione una tecla para cerrar.");
    Console.ReadKey();
}

static void AbrirNavegador(string url)
{
    try
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }
    catch (System.ComponentModel.Win32Exception)
    {
        // Sin navegador predeterminado: el abogado puede abrir la dirección a mano (la guía de uso lo explica).
        Console.WriteLine($"Abrí el navegador en {url}");
    }
}
