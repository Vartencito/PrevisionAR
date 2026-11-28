"""
Estado del proyecto: lee issues y milestones de GitHub y escribe docs/estado-del-proyecto.md
con el avance, el pronóstico y qué hacer ahora.

Uso:  python herramientas/estado_del_proyecto.py
Usa la CLI `gh` si está instalada; si no, lee la API pública de GitHub (el repositorio es público).
Lo corre la IA al cerrar cada sesión de trabajo (ver docs/plan-del-proyecto.md).
"""
import datetime as dt
import json
import math
import pathlib
import re
import subprocess
import urllib.request

REPO = "Vartencito/PrevisionAR"
HORAS_POR_TAMANIO = {"tamaño: S": 2, "tamaño: M": 4, "tamaño: L": 8}
CAPACIDAD_SEMANAL_HORAS = 10  # disponibilidad del responsable; ver plan


def gh(ruta):
    """Lee la API de GitHub con la CLI `gh`; si no está instalada o autenticada, usa la API pública."""
    try:
        # encoding explícito: en Windows, sin esto Python lee la salida de gh como cp1252 y falla con los acentos.
        salida = subprocess.run(["gh", "api", "--paginate", f"repos/{REPO}/{ruta}"],
                                capture_output=True, text=True, encoding="utf-8", check=True).stdout
        # --paginate concatena arrays JSON: los unimos.
        return json.loads(salida.replace("][", ","))
    except (FileNotFoundError, subprocess.CalledProcessError):
        return api_publica(ruta)


def api_publica(ruta):
    """Plan B sin `gh`: el repositorio es público, así que se puede leer sin credenciales (60 consultas por hora)."""
    resultado = []
    url = f"https://api.github.com/repos/{REPO}/{ruta}"
    while url:
        pedido = urllib.request.Request(url, headers={"Accept": "application/vnd.github+json",
                                                      "User-Agent": "estado-del-proyecto"})
        with urllib.request.urlopen(pedido, timeout=30) as respuesta:
            resultado += json.load(respuesta)
            siguiente = re.search(r'<([^>]+)>;\s*rel="next"', respuesta.headers.get("Link", ""))
        url = siguiente.group(1) if siguiente else None
    return resultado


def horas(issue):
    etiquetas = {e["name"] for e in issue["labels"]}
    return next((h for nombre, h in HORAS_POR_TAMANIO.items() if nombre in etiquetas), 4)


def etiquetas(issue):
    return {e["name"] for e in issue["labels"]}


def main():
    hoy = dt.date.today()
    milestones = sorted(gh("milestones?state=all&per_page=100"), key=lambda m: m["title"])
    issues = [i for i in gh("issues?state=all&per_page=100") if "pull_request" not in i and i.get("milestone")]

    lineas = [
        "# Estado del proyecto",
        "",
        f"Generado el {hoy:%d/%m/%Y} con `herramientas/estado_del_proyecto.py` a partir de GitHub. No editar a mano.",
        "",
        "## Avance por versión",
        "",
        "| Versión | Tareas hechas | Horas hechas / total | Avance | Fecha objetivo | Pronóstico |",
        "| --- | --- | --- | --- | --- | --- |",
    ]

    acumulado_semanas = 0.0
    total_h = hecho_h = 0
    for m in milestones:
        propias = [i for i in issues if i["milestone"]["number"] == m["number"]]
        if not propias:
            continue
        h_total = sum(horas(i) for i in propias)
        h_hecho = sum(horas(i) for i in propias if i["state"] == "closed")
        hechas = sum(1 for i in propias if i["state"] == "closed")
        total_h += h_total
        hecho_h += h_hecho
        pendiente = h_total - h_hecho
        acumulado_semanas += pendiente / CAPACIDAD_SEMANAL_HORAS
        objetivo = m.get("due_on")
        objetivo_txt = dt.date.fromisoformat(objetivo[:10]).strftime("%d/%m/%Y") if objetivo else "—"
        if pendiente == 0:
            pronostico = "Terminada"
        else:
            fin = hoy + dt.timedelta(weeks=math.ceil(acumulado_semanas))
            pronostico = f"~{fin:%d/%m/%Y}"
        porcentaje = round(100 * h_hecho / h_total) if h_total else 0
        lineas.append(f"| {m['title']} | {hechas}/{len(propias)} | {h_hecho} / {h_total} h | {porcentaje}% | {objetivo_txt} | {pronostico} |")

    lineas += [
        "",
        f"**Total:** {hecho_h} de {total_h} horas estimadas ({round(100 * hecho_h / total_h) if total_h else 0}%). "
        f"Quedan ~{total_h - hecho_h} horas, unas **{math.ceil((total_h - hecho_h) / CAPACIDAD_SEMANAL_HORAS)} semanas** "
        f"a {CAPACIDAD_SEMANAL_HORAS} h por semana, sin contar las esperas del abogado.",
        "",
    ]

    abiertas = sorted((i for i in issues if i["state"] == "open"),
                      key=lambda i: (i["milestone"]["title"], i["number"]))
    version_actual = abiertas[0]["milestone"]["title"] if abiertas else None
    de_la_version = [i for i in abiertas if i["milestone"]["title"] == version_actual]

    def item(i):
        return f"- [#{i['number']}](https://github.com/{REPO}/issues/{i['number']}) {i['title']} ({horas(i)} h)"

    responsable = [i for i in de_la_version if "responsable" in etiquetas(i)]
    bloqueadas = [i for i in de_la_version if "bloqueado: abogado" in etiquetas(i)]
    ia = [i for i in de_la_version if not ({"responsable", "bloqueado: abogado"} & etiquetas(i))]

    lineas += [f"## Qué hacer ahora ({version_actual})", ""]
    lineas += ["**Le toca a la IA** (en este orden):", ""] + ([item(i) for i in ia[:5]] or ["- Nada sin bloquear."]) + [""]
    lineas += ["**Le toca al responsable:**", ""] + ([item(i) for i in responsable] or ["- Nada."]) + [""]
    lineas += ["**Esperando al abogado:**", ""] + ([item(i) for i in bloqueadas] or ["- Nada."]) + [""]
    lineas += [
        "## Cómo se calcula",
        "",
        f"- Cada tarea tiene un tamaño: S = 2 h, M = 4 h, L = 8 h. El avance se mide en horas, no en cantidad de tareas.",
        f"- El pronóstico reparte las horas pendientes a {CAPACIDAD_SEMANAL_HORAS} h por semana, versión por versión, en orden.",
        "- Es una estimación: se corrige cada semana con lo que realmente se avanzó (ver docs/plan-del-proyecto.md).",
        "",
    ]

    destino = pathlib.Path(__file__).resolve().parent.parent / "docs" / "estado-del-proyecto.md"
    destino.write_text("\n".join(lineas), encoding="utf-8")
    print(destino)


if __name__ == "__main__":
    main()
