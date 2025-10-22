# Convenciones Unity (Proyecto en raíz)

## Serialización y archivos .meta

En Unity Editor → Project Settings → Editor:

- Asset Serialization: Force Text
- Version Control: Visible Meta Files

Por qué:
- YAML legible y mergeable
- Evita conflictos binarios
- Facilita revisión en PRs

## Estructura de carpetas

- Proyecto Unity en raíz: `Assets/`, `Packages/`, `ProjectSettings/`
- Documentación en `docs/`

## .gitignore (Unity)

No versionar artefactos:
- `[Ll]ibrary/`, `[Tt]emp/`, `[Oo]bj/`
- `[Bb]uild*/`, `[Bb]uilds*/`, `[Ll]ogs/`, `UserSettings/`, `MemoryCaptures/`
- Archivos de solución temporales: `*.csproj`, `*.sln`, `*.user`, `*.pidb`, `*.booproj`, `*.unityproj`
- Addressables: `ServerData/` y contenido temporal

Consulta `.gitignore` en la raíz para la lista completa.

## Smart Merge (UnityYAMLMerge)

- Configura Git para usar `UnityYAMLMerge` con `.unity`, `.prefab`, `.anim`, `.asset`
- Ver `docs/dev/UNITY_SMART_MERGE.md`

## Git LFS

- Rastrear binarios grandes (imágenes, audio, vídeo, 3D)
- Ver `docs/dev/GIT_LFS.md`
