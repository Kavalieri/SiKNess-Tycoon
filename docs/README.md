# Documentación del repositorio

Este directorio centraliza la documentación para mantener la raíz del repo limpia y ordenada.

## Índice rápido

- Estructura del repo: [REPO_STRUCTURE.md](./REPO_STRUCTURE.md)
- Núcleo/convenciones Unity: [core/](./core/)
- Guías prácticas (how-to): [guides/](./guides/)
	- Setup: [guides/setup/README_SETUP.md](./guides/setup/README_SETUP.md)
	- Resumen setup: [guides/setup/SETUP_SUMMARY.md](./guides/setup/SETUP_SUMMARY.md)
	- Guía de migración: [guides/MIGRATION_GUIDE.md](./guides/MIGRATION_GUIDE.md)
- Desarrollo/Tooling: [dev/](./dev/)
	- Quickstart Copilot: [dev/COPILOT_QUICKSTART.md](./dev/COPILOT_QUICKSTART.md)
	- Estructura y Copilot: [dev/STRUCTURE_AND_COPILOT_SETUP.md](./dev/STRUCTURE_AND_COPILOT_SETUP.md)
	- Unity Smart Merge: [dev/UNITY_SMART_MERGE.md](./dev/UNITY_SMART_MERGE.md)
	- Git LFS: [dev/GIT_LFS.md](./dev/GIT_LFS.md)
- Decisiones de arquitectura (ADRs): [decisions/](./decisions/) (crea si no existe)
- Diseño y notas técnicas: `design/` (crea si no existe)
- Plantillas: [templates/](./templates/)

## Convenciones

- No crear `.md` sueltos en la raíz del repo. Ubicar la documentación aquí.
- Nombres de archivo descriptivos, en minúsculas y con guiones: `mi-guia-de-ui.md`.
- Para decisiones formales, usa ADRs (una por decisión) con este formato: `aaaa-mm-dd-titulo-corto.md`.

## Empezar un ADR

1) Copia la plantilla: [templates/ADR.md](./templates/ADR.md)  
2) Guarda el archivo en `docs/decisions/` con el nombre `aaaa-mm-dd-titulo-corto.md`  
3) Enlázalo desde `docs/README.md` si es relevante.

## Relación con el proyecto Unity

- Toda la lógica y assets del juego viven en la carpeta del proyecto Unity (p.ej. `UnityProject/`).
- Los archivos `AGENTS.md` van dentro de las carpetas del proyecto Unity (`Assets/…`) para que la guía esté junto al código/asset.
- Esta carpeta `docs/` es para documentación del repositorio y del desarrollo en general.
