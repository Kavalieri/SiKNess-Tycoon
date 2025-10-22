# Assets/Scripts: C# Development Guidelines

This directory contains all game logic written in C#. Copilot Chat should use these patterns when generating or refactoring code.

## Core Patterns

### MonoBehaviour Structure
```csharp
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    // [SerializeField] for Inspector-exposed fields
    [SerializeField] private int configValue = 10;
    
    // Private fields for internal state
    private GameState currentState;
    
    // Properties for clean API
    public bool IsActive { get; private set; }
    
    private void Start() { }      // Initialization
    private void Update() { }     // Per-frame logic
    private void OnDestroy() { }  // Cleanup
}
```

### Naming Conventions
- **Classes**: `PascalCase` (e.g., `PlayerController`, `EconomyManager`)
- **Fields**: `camelCase` with `private` + `[SerializeField]` (e.g., `[SerializeField] private int maxHealth`)
- **Methods**: `PascalCase` (e.g., `CalculateRevenue()`, `SpawnEnemy()`)
- **Constants**: `UPPER_SNAKE_CASE` (e.g., `const float GRAVITY_SCALE = 9.81f`)

### Access Modifiers Strategy
- Default to `private` + `[SerializeField]` for fields
- Use `public` properties sparingly (prefer `private set`)
- Use `internal` for same-assembly sharing
- Avoid `public` fields except for serialized data

## Common Patterns

### Event System (Observer Pattern)
```csharp
public class GameEvents
{
    public static event System.Action<int> OnScoreChanged;
    public static void RaiseScoreChanged(int newScore) => OnScoreChanged?.Invoke(newScore);
}
```

### Dependency Injection in MonoBehaviours
```csharp
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameModel gameModel; // Inject via Inspector
    
    private void Start()
    {
        if (gameModel == null) gameModel = FindObjectOfType<GameModel>();
    }
}
```

### Resource Loading (for runtime assets)
```csharp
var prefab = Resources.Load<GameObject>("Prefabs/MyPrefab");
var instance = Instantiate(prefab);
```

## Code Quality

### Magic Numbers → Constants
❌ Bad:
```csharp
if (health < 50) TakeDamage(25);
```

✅ Good:
```csharp
private const int LOW_HEALTH_THRESHOLD = 50;
private const int DAMAGE_AMOUNT = 25;
if (health < LOW_HEALTH_THRESHOLD) TakeDamage(DAMAGE_AMOUNT);
```

### Methods Should Be Single-Responsibility
❌ Bad:
```csharp
public void UpdateGame() { ProcessInput(); UpdatePhysics(); RenderUI(); }
```

✅ Good:
```csharp
private void Update() 
{
    ProcessInput();
    UpdateLogic();
}
private void LateUpdate() { RenderUI(); }
```

## Testing
- Place tests in `Assets/Tests/` using Unity Test Framework
- Name test classes `{ClassUnderTest}Tests.cs`
- Test only public API

## Input System
This project uses **new InputSystem** (com.unity.inputsystem v1.14.2):
- Bindings defined in `Assets/InputSystem_Actions.inputactions`
- Generate C# code from asset in Inspector
- Use `new GameInput().ActionMap.action.ReadValue<Vector2>()`

## When to Create New Scripts
1. One primary responsibility per class
2. Split large systems into focused managers
3. Use `namespace GameName.Feature` (e.g., `namespace SiKNessTycoon.Economy`)
4. Keep file name = class name

## Related Files
- See `.github/copilot-instructions.md` for project-wide patterns
- See `Assets/Scenes/AGENTS.md` for scene setup conventions
- See `Assets/Prefabs/AGENTS.md` for prefab composition
