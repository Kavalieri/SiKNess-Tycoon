# Git LFS para assets grandes

Usamos Git LFS para almacenar binarios pesados (arte, audio, video) fuera del repo normal, manteniendo clones ligeros.

## 1) Instalar LFS

```powershell
# Instala LFS (si no lo tienes)
# Windows: https://git-lfs.com/ (installer) o winget
# winget install GitHub.GitLFS

# Inicializa LFS en tu máquina
git lfs install
```

## 2) Rastrear tipos de archivo

Este repo ya incluye reglas en `.gitattributes` con:

```
*.psd  filter=lfs diff=lfs merge=lfs -text
*.tga  filter=lfs diff=lfs merge=lfs -text
*.png  filter=lfs diff=lfs merge=lfs -text
*.jpg  filter=lfs diff=lfs merge=lfs -text
*.jpeg filter=lfs diff=lfs merge=lfs -text
*.tiff filter=lfs diff=lfs merge=lfs -text
*.fbx  filter=lfs diff=lfs merge=lfs -text
*.blend filter=lfs diff=lfs merge=lfs -text
*.wav  filter=lfs diff=lfs merge=lfs -text
*.mp3  filter=lfs diff=lfs merge=lfs -text
*.ogg  filter=lfs diff=lfs merge=lfs -text
*.aac  filter=lfs diff=lfs merge=lfs -text
*.mp4  filter=lfs diff=lfs merge=lfs -text
*.mov  filter=lfs diff=lfs merge=lfs -text
*.avi  filter=lfs diff=lfs merge=lfs -text
*.zip  filter=lfs diff=lfs merge=lfs -text
*.dll  filter=lfs diff=lfs merge=lfs -text
```

Si necesitas otro tipo, añádelo a `.gitattributes` o ejecuta:

```powershell
git lfs track "*.exr"
```

## 3) Flujo de trabajo

- Añade/Modifica assets como siempre (`git add`, `git commit`)
- LFS subirá los punteros y almacenará binarios en el almacenamiento LFS del remoto
- En clones, `git lfs pull` descargará los binarios cuando se necesiten

## 4) Buenas prácticas

- No subas `Library/` nunca (usa caché en CI si quieres acelerar)
- Mantén `Build/` fuera de Git; publica builds como Releases
- Revisa tamaño de commits; evita añadir binarios innecesarios
