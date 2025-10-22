# Assets: Root Context for AI Agents

This directory contains all game assets organized by type. When working in this directory, follow these patterns:

## Directory Structure
- **Scripts/** - All C# MonoBehaviours and game logic
- **Scenes/** - Game scenes (typically one per game state)
- **Prefabs/** - Reusable game objects with pre-configured components
- **Resources/** - Runtime-loaded assets via `Resources.Load<T>()`
- **Settings/** - Render pipeline and scene templates
- **InputSystem_Actions.inputactions** - Modern InputSystem action bindings

## Key Conventions

### Asset Naming
- **Prefabs**: `PrefabName.prefab` (PascalCase)
- **Scenes**: `SceneName.unity` (PascalCase)
- **Materials**: `MAT_MaterialName.mat`
- **Sprites**: `SPR_SpriteName.png`

### When Adding New Assets
1. Create in appropriate subdirectory
2. Use meaningful names following convention above
3. Configure all components in prefabs (don't rely on runtime setup)
4. Document non-obvious setup in adjacent AGENTS.md

### 2D-Specific Patterns (Project uses 2D)
- Sprites use Renderer2D with URP
- Tilemaps in dedicated prefabs
- Animation clips in Scenes or Prefabs using 2D Animation
- Particle effects use 2D particle systems

## Related Files
- See `Assets/Scripts/AGENTS.md` for scripting conventions
- See `Assets/Scenes/AGENTS.md` for scene organization
- See `Assets/Prefabs/AGENTS.md` for prefab patterns
