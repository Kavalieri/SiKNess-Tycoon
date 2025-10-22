# SiKNess-Tycoon: AI Coding Agent Instructions

## Project Overview
**SiKNess-Tycoon** is a **2D idle management/tycoon game** built with **Unity 6.2 LTS** using **C#**. 

### Core Concept
Players create and manage a bar/restaurant that evolves into a franchise. The game emphasizes:
- **Idle gameplay**: Progress continues offline (AFK system)
- **Management optimization**: Staff, recipes, schedules, and upgrades
- **Progression**: Efectivo (cash), Fama (fame), Estrellas (stars), and AFK time
- **Expansion**: Multiple venues with unique modifiers
- **Tone**: Casual cartoon style with "gamberro ligero" (light cheeky) humor

### Visual Identity
- **Art Style**: Cartoon 2D with outlines, vibrant "gastronomy palette" (Salsa Brava red-orange, Albahaca green, Medianoche Terraza dark background)
- **UI**: Card-based design (collectible feel), rounded borders, hero images at 60%
- **Aesthetic**: Urban nocturnal terrace atmosphere with warm lighting
- **Typography**: Urban titles + rounded body text, responsive fluid sizing

### Technical Stack
- **Engine**: Unity 6.2.8f1 (6000.2.8f1)
- **Language**: C#
- **Rendering**: Universal Render Pipeline (URP) 17.2.0 with 2D Renderer
- **Input**: Modern InputSystem 1.14.2 (not legacy Input Manager)
- **Graphics**: Sprites with 2D Animation, Aseprite support
- **Physics**: 2D Rigidbodies and Colliders
- **Scripting**: Visual Scripting 1.9.7 available (optional)
- **Testing**: Unity Test Framework 1.6.0

### Main Code Structure
- `Assets/Scripts/` - Game logic organized by system
  - `Core/` - GameManager, ResourceManager, SaveSystem, EventBus
  - `Data/` - ScriptableObjects for recipes, employees, upgrades, venues
  - `Systems/` - Economy, Staff, Menu, RnD, Events, AFK, Venues
  - `UI/` - Screen controllers, card components, overlays, transitions
- `Assets/Scenes/` - Game scenes (MainGame with 4-tab navigation)
- `Assets/Prefabs/` - Reusable UI cards, overlays, badges, buttons
- `Assets/Resources/` - Runtime-loaded data (recipes, employees, events)
- `ProjectSettings/` - Unity configuration (not in version control)
- `Packages/manifest.json` - Dependencies configuration

## Git & Repository Structure
Only source files are tracked; Unity build artifacts are in `.gitignore`:
- **Ignored**: Library/, Temp/, Obj/, Build/, Logs/, UserSettings/
- **Tracked**: C# source files (`Assets/Scripts/`), scenes, prefabs (as YAML), configuration
- **Note**: This repository has a nested structure. See `.vscode/settings.json` for editor configuration
- **Convention**: Use conventional commits (e.g., `feat: add economy system`, `fix: player collision issue`)

## C# Code Patterns & Conventions

### MonoBehaviour Structure
```csharp
using UnityEngine;

public class MySystem : MonoBehaviour
{
    [SerializeField] private int value; // Inspector-exposed fields
    private State currentState;
    
    private void Start() { } // Initialization
    private void Update() { } // Game loop
}
```

### Expected Patterns
1. **Scene Management**: Single MainGame scene with UI tab navigation (Inicio/Menú/Personal/I+D)
2. **Event System**: EventBus pattern for decoupled communication between systems
3. **UI Integration**: Card-based UI with hero layout (60% illustration, 40% text/stats)
4. **Resource Management**: ResourceManager handling Efectivo, Fama, Estrellas currencies
5. **Data Architecture**: ScriptableObjects for all game data (recipes, employees, upgrades)
6. **AFK System**: Offline progress calculation based on venue production + I+D multipliers
7. **Save System**: JSON-based persistent storage for player progress

### Important Conventions
- **Access Modifiers**: Prefer private with [SerializeField] over public
- **Dependency Injection**: Consider constructor or SceneContext injection for testability
- **Magic Numbers**: Extract to constants or [SerializeField] values

### Naming Conventions

#### C# Code
| Elemento | Formato | Ejemplo |
|----------|---------|---------|
| **Clases** | `PascalCase` | `GameManager`, `ResourceManager` |
| **Interfaces** | `IPascalCase` | `IUpgradeable`, `ISaveable` |
| **Campos privados** | `camelCase` | `private int currentFame;` |
| **Campos serializados** | `camelCase` + `[SerializeField]` | `[SerializeField] private float speed;` |
| **Propiedades** | `PascalCase` | `public int CurrentFame { get; }` |
| **Métodos** | `PascalCase` | `CalculateRevenue()`, `UnlockUpgrade()` |
| **Constantes** | `UPPER_SNAKE_CASE` | `const float MAX_AFK_HOURS = 24f;` |
| **Namespaces** | `SiKNessTycoon.Feature` | `SiKNessTycoon.UI.Cards` |
| **Enums** | `PascalCase` | `RecipeCategory`, `EmployeeTrait` |

#### Unity Assets
| Tipo | Formato | Ejemplo |
|------|---------|---------|
| **Prefabs** | `Type_Name.prefab` | `UICard_Recipe.prefab`, `Overlay_AFK.prefab` |
| **Scenes** | `PascalCase.unity` | `MainGame.unity`, `DevScene_Balance.unity` |
| **Sprites** | `category_name.png` | `hero_bravas.png`, `icon_kitchen.png` |
| **Materials** | `MAT_Name.mat` | `MAT_GlowEffect.mat`, `MAT_CardOutline.mat` |
| **ScriptableObjects** | `Name.asset` | `Bravas.asset`, `Lola.asset` |
| **Audio** | `SFX_action.wav` / `MUS_theme.mp3` | `SFX_click.wav`, `MUS_ambient.mp3` |

**Detalles específicos:**
- Prefabs usan `Type_` prefix para indicar categoría (UICard_, Overlay_, Button_)
- Sprites usan `category_` prefix (hero_, icon_, bg_, effect_)
- ScriptableObjects usan el nombre del concepto sin prefijo (más limpio en Inspector)
- Materiales y Audio usan prefijos `MAT_` / `SFX_` / `MUS_` para filtrado rápido

## Development Workflow

### Initial Setup
```powershell
# Clone and open in Unity
git clone https://github.com/Kavalieri/SiKNess-Tycoon.git
# Open with Unity Editor (version must match ProjectSettings/ProjectVersion.txt)
```

### Creating New Features
1. **Scripts**: Add C# files to `Assets/Scripts/` organized by feature (e.g., `Assets/Scripts/UI/`, `Assets/Scripts/Economy/`)
2. **Scenes**: Create new scenes for different game states
3. **Prefabs**: Encapsulate reusable gameplay elements as prefabs with references to scripts

### Testing & Building
- **Play Mode Tests**: Use Unity's Test Framework in `Assets/Tests/`
- **Build**: Unity Editor menu: File > Build Settings > Build
- **No CI/CD pipeline evident yet** - consider adding GitHub Actions for automated builds

## Key Integration Points
- **Input System**: Check if using old Input Manager or new InputSystem package
- **Audio**: AudioSources on GameObjects or centralized audio manager
- **Physics**: Rigidbodies and Colliders for gameplay interactions
- **UI Canvas**: All UI within Canvas hierarchy with EventSystem

## Known Gaps to Verify
When contributing, confirm these with existing code:
- ✓ Project initialization status (are Assets/ and ProjectSettings/ present locally?)
- ✓ Input handling approach (InputManager vs InputSystem) → **Using InputSystem 1.14.2**
- ✓ UI framework used (built-in Unity UI, UI Toolkit, etc.) → **Using built-in Canvas UI**
- ✓ Game architecture: Idle tycoon with 4-screen navigation
- ✓ Core systems: Economy (3 currencies), AFK, Events, I+D (tech tree)
- ✓ Data layer: ScriptableObject-based configuration
- ✓ Visual style: Cartoon 2D with outlines, card-based UI
- ⚠️ Save system implementation (JSON planned)
- ⚠️ Scene structure (MainGame scene to be created)
- ⚠️ Resource management implementation
- ⚠️ UI navigation system

## Nested Agent Instructions
Each major directory has its own `AGENTS.md` file for focused guidance:
- `Assets/AGENTS.md` - Asset organization overview
- `Assets/Scripts/AGENTS.md` - C# coding patterns and conventions
- `Assets/Scenes/AGENTS.md` - Scene hierarchy and setup patterns
- `Assets/Prefabs/AGENTS.md` - Prefab composition and reusability patterns

When working in Copilot Chat, these files provide contextual guidance specific to each subdirectory.

## VS Code Configuration
- `.vscode/settings.json` - Optimized for Copilot Chat, C# development, and Unity
- `.vscode/extensions.json` - Recommended extensions (GitHub Copilot, C# Dev Kit, etc.)
- Configure these settings before starting intensive Copilot Chat sessions for best results
