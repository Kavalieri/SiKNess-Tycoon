# Assets/Prefabs: Prefab Composition Patterns

Prefabs are the backbone of reusable gameplay elements. This guide ensures consistency and modularity.

## Prefab Naming Convention
- **Gameplay objects**: `Player.prefab`, `Enemy_Goblin.prefab`, `Building_Factory.prefab`
- **UI elements**: `UIButton_Standard.prefab`, `PanelSettings.prefab`
- **Systems**: `AudioManager.prefab`, `SpawnerPool.prefab`
- **Effects**: `FX_Explosion.prefab`, `FX_LevelUp.prefab`

Use descriptive names that indicate purpose and context.

## Component Organization

### Standard Prefab Structure
```
[Root - Descriptive Name]
├── [Components on root]
│   ├── Transform (always present)
│   ├── Rigidbody2D (if physics needed)
│   ├── Collider2D (for interaction)
│   └── MainScript (e.g., PlayerController)
│
├── Graphics (child for visual separation)
│   ├── Sprite Renderer
│   ├── Animator (if animated)
│   └── (optional) Particle System
│
└── (optional) UI Child
    └── Canvas + UI elements if part of prefab
```

### Best Practices
- **One primary script per prefab** - on the root object
- **Logical grouping** - visual elements under "Graphics" child
- **No empty GameObjects** - every object should have purpose
- **Clear naming** - avoid generic names like "Object", "Child", "Manager"

## Script References in Prefabs

✅ **Recommended:**
```csharp
[SerializeField] private Animator animator;  // Assign in prefab
[SerializeField] private ParticleSystem vfx; // Assign in prefab

private void Start()
{
    // Use assigned references
    animator.SetTrigger("Idle");
}
```

❌ **Avoid:**
```csharp
private Animator animator; // Found at runtime - fragile
private void Start()
{
    animator = GetComponentInChildren<Animator>();
}
```

## Prefab Variants

Use **Prefab Variants** for:
- Enemy types inheriting from `Enemy_Base.prefab`
- UI themes (Light/Dark variants)
- Difficulty settings (Easy/Hard variants)

Don't use variants if hierarchy differs significantly - create new prefabs instead.

## Common Patterns

### Enemy Prefab Pattern
```
[Enemy_Type]
├── Body (SpriteRenderer, Animator, Collider2D)
├── EnemyController (script)
├── AI (Brain script reference)
└── VFX
    └── DeathEffect (disabled, played on death)
```

### Building/Structure Prefab Pattern
```
[Building_Type]
├── Visual (SpriteRenderer with sorting order)
├── Foundation (physics collider)
├── BuildingScript
└── Indicator (child showing range/interaction)
```

### UI Prefab Pattern
```
[UIPanel_Type]
├── Background (Image)
├── Content (VerticalLayoutGroup)
│   ├── Title (Text)
│   └── Buttons (ButtonGroup)
└── UIManager (script handling panel)
```

## Pooling Pattern

For frequently spawned/destroyed prefabs (bullets, enemies):

```csharp
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefabTemplate;
    [SerializeField] private int initialPoolSize = 20;
    
    private Queue<GameObject> pool = new();
    
    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            pool.Enqueue(Instantiate(prefabTemplate));
        }
    }
    
    public GameObject Get()
    {
        if (pool.Count > 0) return pool.Dequeue();
        return Instantiate(prefabTemplate);
    }
    
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
```

## When Creating New Prefabs

1. **Design in scene** - build and test in scene first
2. **Validate in Inspector** - all references assigned
3. **Drag to Prefabs folder** - right-click > Create Prefab
4. **Delete scene instance** - keep only the prefab
5. **Document if complex** - add comments to scripts

## Testing Prefabs

- Instantiate in `DevScene_PrefabTest.unity` before committing
- Verify all references resolve
- Test pooling/spawning patterns work correctly

## Related Files
- See `Assets/Scripts/AGENTS.md` for component script patterns
- See `Assets/Scenes/AGENTS.md` for scene integration
- See `.github/copilot-instructions.md` for general architecture
