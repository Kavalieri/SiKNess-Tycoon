# Unity Smart Merge (YAMLMerge)

Configura Git para resolver mejor merges de escenas (.unity), prefabs (.prefab) y assets YAML.

## 1) Requisitos
- Unity 6.2.x instalado (Unity Hub)
- Git instalado

La herramienta se llama `UnityYAMLMerge.exe` y suele estar en:

```
C:\Program Files\Unity\Hub\Editor\<version>\Editor\Data\Tools\UnityYAMLMerge.exe
```

Añade esa carpeta al PATH o referencia la ruta absoluta en la configuración.

## 2) Configuración del merge driver

En el repositorio, ejecuta en PowerShell (pwsh):

```powershell
# Establece el mergetool por defecto
git config merge.tool unityyamlmerge

# Define el comando del mergetool (ajusta la ruta si no está en PATH)
$yamlMerge = 'UnityYAMLMerge'
# Si no está en PATH, usa la ruta completa, por ejemplo:
# $yamlMerge = 'C:\\Program Files\\Unity\\Hub\\Editor\\6000.2.8f1\\Editor\\Data\\Tools\\UnityYAMLMerge.exe'

git config mergetool.unityyamlmerge.cmd "'$yamlMerge' merge -p \"$BASE\" \"$REMOTE\" \"$LOCAL\" \"$MERGED\""

git config mergetool.unityyamlmerge.trustExitCode false
```

Esto escribe en `.git/config` del repo.

## 3) Asociar extensiones a UnityYAMLMerge

Ya hemos añadido en `.gitattributes` (en este repo):

```
*.unity    merge=unityyamlmerge
*.prefab   merge=unityyamlmerge
*.anim     merge=unityyamlmerge
*.asset    merge=unityyamlmerge
```

Con esto, Git usará UnityYAMLMerge para esos archivos durante conflictos.

## 4) Uso

Cuando Git detecte un conflicto en una escena/prefab:
- Ejecuta `git mergetool` para abrir el merge con UnityYAMLMerge
- Revisa el resultado y haz commit

Consejos:
- Evita cambios simultáneos grandes en la misma escena
- Divide escenas en subescenas (addressables o escenas aditivas) cuando sea posible

## 5) Troubleshooting

- Si `git mergetool` dice que no encuentra `UnityYAMLMerge`, configura la ruta absoluta en `mergetool.unityyamlmerge.cmd`.
- Si los merges fallan, revisa que los archivos estén en formato YAML y que ProjectSettings esté en `Force Text`.
