# SiKNess-Tycoon: AI Coding Agent Instructions

## Project Overview
**SiKNess-Tycoon** is a **2D management/tycoon game** built with **Unity 6.2 LTS** using **C#**. The game logic is written in C# MonoBehaviours organized in the Assets folder, with a focus on 2D gameplay using sprites, tilemaps, and physics.

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
- `Assets/Scripts/` - Game logic and core systems
- `Assets/Scenes/` - Game scenes (one per game state)
- `Assets/Prefabs/` - Reusable game objects
- `Assets/Scenes/` - Tilemaps and level layouts
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
1. **Scene Management**: Use `SceneManager.LoadScene()` or scene loading systems
2. **Event System**: Consider implementing an event bus (Observer pattern) for decoupled communication
3. **UI Integration**: UI scripts typically inherit from `MonoBehaviour` and reference UI components via `GetComponent<>`
4. **Resource Loading**: Use `Resources.Load<>()` for dynamic asset loading at runtime
5. **Data Persistence**: Game save/load systems - verify if using Unity's PlayerPrefs or a custom save system

### Important Conventions
- **Naming**: PascalCase for classes, camelCase for fields/properties
- **Access Modifiers**: Prefer private with [SerializeField] over public
- **Dependency Injection**: Consider constructor or SceneContext injection for testability
- **Magic Numbers**: Extract to constants or [SerializeField] values

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
- ✓ Game save/load mechanism
- ✓ Input handling approach (InputManager vs InputSystem) → **Using InputSystem 1.14.2**
- ✓ UI framework used (built-in Unity UI, UI Toolkit, etc.) → **Using built-in Canvas UI**
- ✓ Testing strategy
- ✓ Economy/Tycoon systems architecture
- ✓ NPC/Employee management systems

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
