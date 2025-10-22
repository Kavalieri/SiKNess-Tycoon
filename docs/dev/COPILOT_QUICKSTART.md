# ⚡ Quick Start: Copilot Chat en SiKNess-Tycoon

## 📦 Qué se ha Configurado

✅ **GitHub Copilot Chat** optimizado para Unity 6.2 + C#  
✅ **Nested AGENTS.md** en cada directorio principal  
✅ **VS Code settings** para máximo rendimiento  
✅ **Extensiones recomendadas** para desarrollo Unity  

## 🚀 Primeros Pasos (5 minutos)

### 1. Instalar Extensiones Recomendadas
```
Ctrl+Shift+X → Busca las extensiones en .vscode/extensions.json
O click en notificación de extensiones recomendadas
```

**Esenciales:**
- `ms-dotnettools.csharp` (C# Dev Kit)
- `GitHub.copilot`
- `GitHub.copilot-chat`

### 2. Reiniciar VS Code
```powershell
# Cierra y reabre VS Code para cargar todas las configuraciones
```

### 3. Verificar Copilot Chat Funciona
- Abre un archivo `.cs` en `Assets/Scripts/`
- Presiona `Ctrl+I` para abrir Copilot Chat
- Escribe: `"Hola, crea un MonoBehaviour simple"`

**✅ Éxito** si Copilot respeta el formato de `Assets/Scripts/AGENTS.md`

## 📁 Archivos de Configuración Creados

| Archivo | Propósito | Prioridad |
|---------|-----------|-----------|
| `.vscode/settings.json` | Config Copilot Chat + C# | 🔴 Esencial |
| `.vscode/extensions.json` | Extensiones recomendadas | 🟡 Importante |
| `.github/copilot-instructions.md` | Instrucciones globales | 🟡 Importante |
| `Assets/AGENTS.md` | Contexto de Assets | 🟢 Recomendado |
| `Assets/Scripts/AGENTS.md` | Guía de scripting C# | 🟢 Recomendado |
| `Assets/Scenes/AGENTS.md` | Guía de scenes | 🟢 Recomendado |
| `Assets/Prefabs/AGENTS.md` | Guía de prefabs | 🟢 Recomendado |

## 💬 Ejemplos de Uso en Copilot Chat

### En `Assets/Scripts/`
```
"Crea un GameManager que maneje el estado del juego"
```
✨ Copilot seguirá namespacing, convenciones y patrones de Scripts/AGENTS.md

### En `Assets/Scenes/`
```
"Configura una escena con HUD y canvas para el menú principal"
```
✨ Creará jerarquía correcta según Scenes/AGENTS.md

### En `Assets/Prefabs/`
```
"Crea un prefab reutilizable de botón con pool support"
```
✨ Implementará patrones de composición de Prefabs/AGENTS.md

## 🔧 Recomendación de Estructura

**Opcional pero RECOMENDADO:** Mover contenido de `SiKNess Tycoon/` a raíz

```powershell
# En terminal (PowerShell), desde SiKNess-Tycoon/
Copy-Item -Path "SiKNess Tycoon\*" -Destination "." -Recurse -Force
Remove-Item "SiKNess Tycoon" -Recurse -Force
```

**Beneficios:**
- Paths más simples en código y CI/CD
- Estándar industrial
- Mejor experiencia con Copilot

## ❓ Preguntas Frecuentes

**P: ¿Puedo usar Copilot sin las extensiones?**
- Sí, pero con limitada intellisense. Instálalas para mejor experiencia.

**P: ¿Cómo sé si AGENTS.md está funcionando?**
- Pregunta a Copilot específicamente sobre un patrón, ej: "¿Cuál es la convención de nombre para campos?"

**P: ¿Puedo editar AGENTS.md?**
- ¡Absolutamente! Ajusta según tus necesidades. Copilot recargar automáticamente.

**P: ¿Qué pasa si muevo/renombro AGENTS.md?**
- Copilot dejará de encontrarlo. Mantenlo con exacto nombre en cada carpeta.

## 📚 Documentación Completa

- Leer `STRUCTURE_AND_COPILOT_SETUP.md` para detalles técnicos
- Revisar cada `Assets/*/AGENTS.md` para guías específicas
- Ver `.github/copilot-instructions.md` para arquitectura global

## 🎯 Siguiente Paso Recomendado

1. **Instala extensiones** (5 min)
2. **Prueba Copilot Chat** en un archivo .cs (2 min)
3. **Lee Assets/Scripts/AGENTS.md** (5 min)
4. **Crea tu primer script** con ayuda de Copilot (10 min)

**Total: 22 minutos para estar 100% productivo** ✨

---

Para dudas o ajustes, edita los archivos AGENTS.md directamente.
Copilot Chat se actualizará automáticamente.