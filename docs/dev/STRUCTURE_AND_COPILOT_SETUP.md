# 📋 SiKNess-Tycoon: Estructura y Configuración de Copilot Chat

## 🎯 Recomendación de Estructura

### Estado Actual ❌
```
SiKNess-Tycoon/
└── SiKNess Tycoon/        ← Subdirectorio innecesario
    ├── Assets/
    ├── ProjectSettings/
    ├── Packages/
    └── ...
```

### Estructura Recomendada ✅
```
SiKNess-Tycoon/
├── .github/
│   ├── copilot-instructions.md
│   └── workflows/              (para CI/CD futuro)
├── .vscode/
│   ├── settings.json           (configuración Copilot Chat)
│   ├── extensions.json         (extensiones recomendadas)
│   └── launch.json             (debugging - opcional)
├── Assets/
│   ├── Scripts/
│   │   └── AGENTS.md           (guía para scripting)
│   ├── Scenes/
│   │   └── AGENTS.md           (guía para scenes)
│   ├── Prefabs/
│   │   └── AGENTS.md           (guía para prefabs)
│   ├── Settings/
│   ├── Resources/
│   └── AGENTS.md               (contexto raíz Assets)
├── ProjectSettings/
├── Packages/
├── .gitignore
├── README.md
└── LICENSE
```

## 🚀 Por qué Reorganizar?

| Aspecto | Actual | Recomendado |
|--------|--------|-------------|
| **Profundidad de carpetas** | 2 niveles | 1 nivel |
| **Paths en instrucciones** | `SiKNess Tycoon/Assets/...` | `Assets/...` |
| **Estándar industrial** | ❌ No | ✅ Sí |
| **CI/CD Scripts** | Complicados | Simples |
| **Experiencia Copilot** | Confusa | Directa |

## ✨ Configuración de Copilot Chat

### Archivos Creados

#### 1. `.vscode/settings.json` ✅
Optimizado para:
- ✅ GitHub Copilot Chat integrado
- ✅ C# IntelliSense con Omnisharp
- ✅ Formateo automático de código
- ✅ Exclusiones de artefactos Unity (Library, Temp, etc.)
- ✅ Detección correcta de archivos meta/asmdef

**Características principales:**
- Copilot Chat habilitado en C# y Markdown
- Formateo automático con C# Dev Kit
- Búsqueda optimizada (excluyendo Build artifacts)
- Ruler a 80 y 120 caracteres para legibilidad

#### 2. `.vscode/extensions.json` ✅
Recomendaciones de extensiones:
- `ms-dotnettools.csharp` - C# Dev Kit (esencial)
- `GitHub.copilot-chat` - Copilot Chat (esencial)
- `unity.unity-debug` - Debugging en Unity
- `SonarSource.sonarlint-vscode` - Análisis de código

Instalar con: `Ctrl+Shift+X` → Click en extensión recomendada

#### 3. Archivos `AGENTS.md` Anidados ✅
**Nested Agents** - Copilot utiliza archivos locales para contexto más específico

```
Assets/AGENTS.md
    └─ Contexto general de Assets (convenciones de nombre, 2D patterns)

Assets/Scripts/AGENTS.md
    └─ Patrones C#, MonoBehaviour structure, naming, Input System (1.14.2)

Assets/Scenes/AGENTS.md
    └─ Jerarquía de escenas, Canvas setup, Physics 2D, Loading patterns

Assets/Prefabs/AGENTS.md
    └─ Composición de prefabs, pooling, component references
```

### Cómo Usar Copilot Chat Efectivamente

#### 1. En `Assets/Scripts/`
Copilot automáticamente usa `Assets/Scripts/AGENTS.md`:
```
"Crea una clase MonoBehaviour para gestionar la economía del juego"
→ Copilot genera código siguiendo convenciones de Scripts/AGENTS.md
```

#### 2. En `Assets/Scenes/`
Para organización de escenas:
```
"Configura una jerarquía de Canvas para el HUD del juego"
→ Copilot crea estructura según Scenes/AGENTS.md
```

#### 3. En `Assets/Prefabs/`
Para crear prefabs reutilizables:
```
"Crea un prefab de botón estándar del UI"
→ Copilot utiliza patrones de Prefabs/AGENTS.md
```

## 📝 Pasos Siguientes

### Inmediato
1. **Revisar `Assets/Scripts/AGENTS.md`** - Define cómo escribir C#
2. **Revisar `.vscode/settings.json`** - Verifica configuración
3. **Instalar extensiones recomendadas** - Click en recomendaciones

### A Corto Plazo
- Mover contenido de `SiKNess Tycoon/` a raíz
- Confirmar patrones en código existente
- Adaptar AGENTS.md según necesidades específicas

### Documentación Adicional
- `.github/copilot-instructions.md` - Instrucciones globales de proyecto
- `Assets/Scripts/AGENTS.md` - Guía de C# (más detallada)
- `Assets/Scenes/AGENTS.md` - Guía de scenes
- `Assets/Prefabs/AGENTS.md` - Guía de prefabs

## 🔧 Troubleshooting Copilot Chat

**P: ¿Copilot no respeta las convenciones?**
- A: Verifica que los AGENTS.md estén en la carpeta correcta
- A: Reinicia VS Code después de crear AGENTS.md

**P: ¿Dónde busca Copilot contexto?**
- A: Primero en AGENTS.md local, luego en .github/copilot-instructions.md

**P: ¿Cómo actualizo instrucciones mientras desarrollo?**
- A: Edita AGENTS.md directamente - Copilot recarga en siguiente query

## 💡 Consejo Pro
Usa mensajes explícitos a Copilot:
```
"Siguiendo las convenciones en Assets/Scripts/AGENTS.md, crea..."
"Basándote en los patrones de prefabs documentados, diseña..."
```

Esto refuerza que use las instrucciones específicas de directorio.

---

**Creado**: Octubre 22, 2025
**Unity Version**: 6.2.8f1
**Copilot Chat Optimizado**: ✅ Sí