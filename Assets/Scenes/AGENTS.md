# Assets/Scenes: Scene Organization Guidelines

Game scenes are the entry points for gameplay states. This document guides scene setup and structure.

## Scene Naming Convention
- **Main gameplay**: `MainScene.unity` or `GameplayScene.unity`
- **Menus**: `MenuScene.unity`, `SettingsScene.unity`, `PauseScene.unity`
- **Loading**: `LoadingScene.unity`
- **Test/Dev**: `DevScene_*.unity` (prefixed with DevScene for easy filtering)

## Scene Hierarchy Structure

### Recommended Root GameObjects
```
[Root]
├── Managers (non-visual game systems)
│   ├── GameManager
│   ├── PlayerManager
│   └── EventManager
├── Environment (visual level setup)
│   ├── Tilemap
│   ├── Decorations
│   └── Buildings
├── Player (player-controlled entity)
│   └── Player (with Rigidbody2D, InputHandler, etc.)
├── UI Canvas (ALL UI must be in Canvas)
│   ├── HUD
│   ├── MainMenu
│   └── Popups
└── Temporary (runtime-spawned objects go here)
    └── (cleared on scene exit)
```

### Best Practices
- **One Canvas per scene** - place all UI elements inside it
- **Managers on separate root objects** - not children of visible objects
- **Don't nest too deep** - max 3-4 levels usually
- **Consistent naming** - `[System]` prefix for manager objects

## Scene Loading

### With SceneManager
```csharp
// Load additively (for menus over gameplay)
SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);

// Load exclusively (replace current scene)
SceneManager.LoadScene("GameplayScene");
```

### Build Settings
- Register all scenes in Edit > Project Settings > Scenes in Build
- Scene 0 should be your main/first scene
- Keep organization in same order as in project

## Camera Setup

- **One active camera per scene** - typically on a GameObject with `Camera` + `AudioListener`
- **2D games**: Set camera projection to Orthographic
- **Clear color**: Use Settings > Renderer2D asset or solid color

## Physics Setup (2D-specific)

- Scene should have **one Physics2D Gravity** setting (Project Settings > Physics2D)
- Use `Rigidbody2D` + `Collider2D` for interactive objects
- Static colliders on environment (Tilemap colliders OK)

## Lighting (URP 2D)

- Use **Universal Renderer** configured in `Assets/Settings/Renderer2D.asset`
- Add global lights via Light2D components
- Sprites rendered with appropriate sorting order

## When Creating New Scenes

1. **From Template**: Use `Assets/Settings/Lit2DSceneTemplate.scenetemplate`
2. **Manual Setup**:
   - Create scene file in `Assets/Scenes/`
   - Add Main Camera (Orthographic, Clear Solid Color)
   - Add Canvas for UI
   - Add GameManager or SystemsHolder
3. **Add to Build Settings** before testing

## Development Scenes

- Prefix with `DevScene_` for quick identification
- Use for feature testing without full game setup
- Don't commit to main unless specifically for testing

## Related Files
- See `Assets/Scripts/AGENTS.md` for how to structure scene managers
- See `Assets/Prefabs/AGENTS.md` for reusable prefab references
- See `.github/copilot-instructions.md` for project architecture
