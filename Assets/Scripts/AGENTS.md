# Assets/Scripts: C# Development Guidelines for SiKNess Tycoon

This directory contains all game logic written in C#. These patterns are specifically designed for an **idle tycoon management game** with card-based UI, offline progression, and multiple interconnected systems.

---

## Directory Structure

```
Scripts/
├── Core/               # Central managers and foundational systems
│   ├── GameManager.cs          # Master game state controller
│   ├── ResourceManager.cs      # Handles Efectivo, Fama, Estrellas
│   ├── SaveSystem.cs           # JSON-based persistence
│   └── EventBus.cs             # Decoupled event communication
│
├── Data/               # ScriptableObject definitions (game data)
│   ├── RecipeData.cs           # Recipe configurations
│   ├── EmployeeData.cs         # Staff member data
│   ├── UpgradeData.cs          # I+D upgrade nodes
│   └── VenueData.cs            # Venue/location definitions
│
├── Systems/            # Feature-specific gameplay systems
│   ├── Economy/                # Currency and transactions
│   ├── Staff/                  # Employee management
│   ├── Menu/                   # Recipe and menu system
│   ├── RnD/                    # Tech tree (I+D)
│   ├── Events/                 # Weekly/micro events
│   ├── AFK/                    # Offline progression
│   └── Venues/                 # Multi-location management
│
└── UI/                 # Screen controllers and UI logic
    ├── Screens/                # Main screen controllers (Inicio, Menú, Personal, I+D)
    ├── Cards/                  # Card component behaviors
    ├── Overlays/               # Popup/overlay controllers
    └── Navigation/             # Tab switching and transitions
```

---

## Idle Tycoon-Specific Patterns

### 1. ScriptableObject Data Architecture
All game configuration data (recipes, employees, upgrades) uses **ScriptableObjects** for:
- **Designer-friendly**: Editable in Inspector without code changes
- **Runtime efficiency**: Pre-loaded, shared across systems
- **Modularity**: Easy to add new content

```csharp
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "SiKNess/Recipe")]
public class RecipeData : ScriptableObject
{
    [Header("Identity")]
    public string recipeName;
    public Sprite heroImage;          // 60% of card visual
    
    [Header("Economy")]
    public int basePrice;
    public float preparationTime;
    
    [Header("Categorization")]
    public RecipeCategory category;   // Entrantes, Principales, Postres, Bebidas
    public TimeOfDay popularity;      // ☀️ Día / 🌙 Noche
    
    [Header("Progression")]
    public int unlockFamaThreshold;
    public string[] synergyTags;      // For combo detection
}

public enum RecipeCategory { Entrantes, Principales, Postres, Bebidas }
public enum TimeOfDay { Day, Night, Both }
```

### 2. Resource Management (3-Currency System)
The game uses three currencies with distinct roles:

```csharp
using UnityEngine;
using System;

namespace SiKNessTycoon.Core
{
    public class ResourceManager : MonoBehaviour
    {
        // Singleton pattern for global access
        public static ResourceManager Instance { get; private set; }
        
        // Currency properties with events
        public int Efectivo { get; private set; }    // Immediate upgrades
        public int Fama { get; private set; }        // Venue unlocks
        public int Estrellas { get; private set; }   // Meta-progression
        
        public event Action<int> OnEfectivoChanged;
        public event Action<int> OnFamaChanged;
        public event Action<int> OnEstrellasChanged;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public bool TrySpendEfectivo(int amount)
        {
            if (Efectivo >= amount)
            {
                Efectivo -= amount;
                OnEfectivoChanged?.Invoke(Efectivo);
                return true;
            }
            return false;
        }
        
        public void AddEfectivo(int amount)
        {
            Efectivo += amount;
            OnEfectivoChanged?.Invoke(Efectivo);
        }
        
        // Similar methods for Fama and Estrellas...
    }
}
```

### 3. EventBus Pattern (Decoupled Communication)
Systems communicate without tight coupling:

```csharp
using System;

namespace SiKNessTycoon.Core
{
    public static class GameEvents
    {
        // Economy events
        public static event Action<int> OnRevenueGenerated;
        public static event Action<string> OnBottleneckDetected;
        
        // Staff events
        public static event Action<EmployeeData> OnEmployeeHired;
        public static event Action<EmployeeData> OnEmployeeFatigued;
        
        // UI events
        public static event Action<string> OnScreenChanged;
        public static event Action<string, float> OnOverlayShow;
        
        // Progression events
        public static event Action<UpgradeData> OnUpgradeUnlocked;
        public static event Action<VenueData> OnVenueUnlocked;
        
        // Invoke pattern
        public static void RaiseRevenueGenerated(int amount) 
            => OnRevenueGenerated?.Invoke(amount);
            
        public static void RaiseBottleneckDetected(string area)
            => OnBottleneckDetected?.Invoke(area);
            
        public static void RaiseOverlayShow(string message, float duration)
            => OnOverlayShow?.Invoke(message, duration);
    }
}
```

### 4. AFK System Architecture
Offline progress calculates production based on venue state + I+D multipliers:

```csharp
using UnityEngine;
using System;

namespace SiKNessTycoon.Systems.AFK
{
    public struct AFKReward
    {
        public int efectivo;
        public float duration;
        public string flavorText;
    }
    
    public class AFKSystem : MonoBehaviour
    {
        [SerializeField] private float baseProductionRate = 100f; // Per hour
        [SerializeField] private float maxAFKHours = 24f;
        
        public AFKReward CalculateOfflineReward(DateTime lastPlayTime)
        {
            TimeSpan elapsed = DateTime.Now - lastPlayTime;
            float hours = Mathf.Min((float)elapsed.TotalHours, maxAFKHours);
            
            float production = baseProductionRate * hours;
            production *= GetRnDMultiplier();        // I+D boosts
            production *= GetVenueMultiplier();      // Multiple venues
            production *= GetEventMultiplier();       // Active events
            
            return new AFKReward
            {
                efectivo = Mathf.RoundToInt(production),
                duration = hours,
                flavorText = MicrotextLibrary.GetAFKMessage(hours)
            };
        }
        
        private float GetRnDMultiplier()
        {
            // Query RnD system for current multiplier
            return 1.0f; // Placeholder
        }
        
        private float GetVenueMultiplier()
        {
            // Query venue system for passive production
            return 1.0f; // Placeholder
        }
        
        private float GetEventMultiplier()
        {
            // Check if any active event boosts AFK
            return 1.0f; // Placeholder
        }
    }
}
```

### 5. Card-Based UI Component Pattern
Cards are the primary UI element (recipes, employees, upgrades):

```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; // DOTween for animations (if available)

namespace SiKNessTycoon.UI
{
    public class UICard : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private Image heroImage;        // 60% of card
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        [Header("Interaction")]
        [SerializeField] private Button actionButton;
        [SerializeField] private float tiltAmount = 5f;  // Micro-tilt on touch
        
        private RectTransform rectTransform;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        
        public void Initialize(RecipeData recipe)
        {
            heroImage.sprite = recipe.heroImage;
            titleText.text = recipe.recipeName;
            descriptionText.text = $"{recipe.basePrice}€ · {recipe.preparationTime}s";
        }
        
        public void Initialize(EmployeeData employee)
        {
            heroImage.sprite = employee.portrait;
            titleText.text = employee.employeeName;
            descriptionText.text = employee.mainTrait;
        }
        
        public void OnPointerEnter()
        {
            // Micro-tilt animation (cartoon feel)
            rectTransform.DORotate(new Vector3(0, 0, tiltAmount), 0.2f);
        }
        
        public void OnPointerExit()
        {
            rectTransform.DORotate(Vector3.zero, 0.2f);
        }
    }
}
```

---

## Core MonoBehaviour Patterns

### Standard Structure
```csharp
using UnityEngine;

namespace SiKNessTycoon.Systems.Economy
{
    public class GameSystem : MonoBehaviour
    {
        // [SerializeField] for Inspector-exposed fields
        [SerializeField] private int configValue = 10;
        
        // Private fields for internal state
        private GameState currentState;
        
        // Properties for clean API
        public bool IsActive { get; private set; }
        
        private void Start() 
        { 
            InitializeSystem();
        }
        
        private void Update() 
        { 
            // Per-frame logic (avoid heavy operations)
        }
        
        private void OnDestroy() 
        { 
            // Cleanup subscriptions
        }
        
        private void InitializeSystem()
        {
            // Subscribe to events
            GameEvents.OnRevenueGenerated += HandleRevenue;
        }
        
        private void HandleRevenue(int amount)
        {
            // Event response logic
        }
    }
}
```

### Naming Conventions
- **Classes**: `PascalCase` (e.g., `ResourceManager`, `AFKSystem`)
- **Fields**: `camelCase` with `private` + `[SerializeField]` (e.g., `[SerializeField] private int maxHealth`)
- **Methods**: `PascalCase` (e.g., `CalculateRevenue()`, `UnlockUpgrade()`)
- **Constants**: `UPPER_SNAKE_CASE` (e.g., `const float MAX_AFK_HOURS = 24f`)
- **Namespaces**: `SiKNessTycoon.Feature` (e.g., `SiKNessTycoon.UI.Cards`)

---

## Code Quality for Idle Tycoon

### Balance Tuning → Constants
All balance values should be extractable for easy tuning:

```csharp
namespace SiKNessTycoon.Core
{
    public static class GameBalance
    {
        // Economy
        public const float BASE_TICKET_PRICE = 15f;
        public const float SATISFACTION_MULTIPLIER = 1.2f;
        
        // AFK
        public const float BASE_AFK_HOURS = 8f;
        public const float MAX_AFK_HOURS = 24f;
        
        // Staff
        public const float FATIGUE_RATE = 0.1f; // Per hour
        public const int MAX_STAFF_PER_AREA = 5;
        
        // UI
        public const float CARD_TILT_AMOUNT = 5f;
        public const float OVERLAY_DURATION = 2f;
    }
}
```

### Bottleneck Detection Pattern
Idle games thrive on showing the player where to optimize:

```csharp
namespace SiKNessTycoon.Systems.Economy
{
    public enum BottleneckType { Kitchen, Service, Cashier, None }
    
    public class BottleneckDetector : MonoBehaviour
    {
        public BottleneckType DetectCurrentBottleneck()
        {
            float kitchenUtilization = CalculateKitchenLoad();
            float serviceUtilization = CalculateServiceLoad();
            float cashierUtilization = CalculateCashierLoad();
            
            // Find highest utilization
            float maxUtil = Mathf.Max(kitchenUtilization, serviceUtilization, cashierUtilization);
            
            if (maxUtil < 0.7f) return BottleneckType.None; // No bottleneck
            
            if (kitchenUtilization == maxUtil)
                return BottleneckType.Kitchen;
            else if (serviceUtilization == maxUtil)
                return BottleneckType.Service;
            else
                return BottleneckType.Cashier;
        }
        
        private float CalculateKitchenLoad()
        {
            // Calculate kitchen utilization (0.0 - 1.0)
            return 0.5f; // Placeholder
        }
        
        // Similar methods for Service and Cashier...
    }
}
```

### "Gamberro Ligero" Microtexts
UI text should have personality (light cheeky tone):

```csharp
namespace SiKNessTycoon.Core
{
    public static class MicrotextLibrary
    {
        public static string GetBottleneckMessage(BottleneckType type)
        {
            return type switch
            {
                BottleneckType.Kitchen => "La cocina va a pedales, mete chispa 🔥",
                BottleneckType.Service => "La sala se arrastra... ¿les damos marcha? 😅",
                BottleneckType.Cashier => "La caja petardea, toca afinar ahí 💰",
                _ => "Todo controlado, jefe 😎"
            };
        }
        
        public static string GetAFKMessage(float hours)
        {
            int h = Mathf.FloorToInt(hours);
            int m = Mathf.FloorToInt((hours - h) * 60);
            return $"Tu gente ha tirado del carro 💪 Has rascado {h}h{m:00}m de curro fresquito.";
        }
        
        public static string GetUpgradeMessage(string upgradeName)
        {
            return $"Bien ahí 👌 {upgradeName} — Esto va fino ya";
        }
    }
}
```

---

## System Integration Examples

### Example: Recipe unlocking through progression
```csharp
using UnityEngine;
using System.Collections.Generic;

namespace SiKNessTycoon.Systems.Menu
{
    public class MenuSystem : MonoBehaviour
    {
        [SerializeField] private List<RecipeData> allRecipes;
        private List<RecipeData> unlockedRecipes = new List<RecipeData>();
        
        private void Start()
        {
            // Subscribe to progression events
            GameEvents.OnFamaChanged += CheckRecipeUnlocks;
        }
        
        private void OnDestroy()
        {
            GameEvents.OnFamaChanged -= CheckRecipeUnlocks;
        }
        
        private void CheckRecipeUnlocks(int currentFama)
        {
            foreach (var recipe in allRecipes)
            {
                if (!unlockedRecipes.Contains(recipe) && 
                    currentFama >= recipe.unlockFamaThreshold)
                {
                    UnlockRecipe(recipe);
                }
            }
        }
        
        private void UnlockRecipe(RecipeData recipe)
        {
            unlockedRecipes.Add(recipe);
            string message = $"¡Nueva receta desbloqueada! {recipe.recipeName} 🍽️";
            GameEvents.RaiseOverlayShow(message, 2f);
        }
    }
}
```

### Example: I+D upgrade affecting AFK multiplier
```csharp
using UnityEngine;
using System.Collections.Generic;

namespace SiKNessTycoon.Systems.RnD
{
    public class RnDSystem : MonoBehaviour
    {
        private Dictionary<string, bool> unlockedUpgrades = new Dictionary<string, bool>();
        
        public float GetAFKMultiplier()
        {
            float multiplier = 1f;
            
            if (unlockedUpgrades.ContainsKey("escuela_oficio"))
                multiplier *= 1.15f;
            if (unlockedUpgrades.ContainsKey("marca_barrio"))
                multiplier *= 1.10f;
                
            return multiplier;
        }
        
        public void UnlockUpgrade(UpgradeData upgrade)
        {
            unlockedUpgrades[upgrade.upgradeId] = true;
            GameEvents.RaiseUpgradeUnlocked(upgrade);
            
            // Show overlay with "gamberro" tone
            string message = MicrotextLibrary.GetUpgradeMessage(upgrade.displayName);
            GameEvents.RaiseOverlayShow(message, 2.5f);
        }
    }
}
```

---

## Performance Considerations for Idle Games

### Efficient Update Loops
```csharp
using UnityEngine;
using System.Collections;

// ❌ BAD: Updating every frame unnecessarily
private void Update()
{
    UpdateRevenue(); // Called 60+ times per second
}

// ✅ GOOD: Use coroutines for periodic updates
private IEnumerator Start()
{
    while (true)
    {
        UpdateRevenue();
        yield return new WaitForSeconds(1f); // Update once per second
    }
}
```

### Object Pooling for UI Cards
```csharp
using UnityEngine;
using System.Collections.Generic;

namespace SiKNessTycoon.UI
{
    public class CardPool : MonoBehaviour
    {
        [SerializeField] private UICard cardPrefab;
        private Queue<UICard> pool = new Queue<UICard>();
        
        public UICard GetCard()
        {
            if (pool.Count > 0)
            {
                var card = pool.Dequeue();
                card.gameObject.SetActive(true);
                return card;
            }
            
            return Instantiate(cardPrefab);
        }
        
        public void ReturnCard(UICard card)
        {
            card.gameObject.SetActive(false);
            pool.Enqueue(card);
        }
    }
}
```

---

## Testing and Debug Tools

### Simulating Time Progression
```csharp
#if UNITY_EDITOR
[ContextMenu("Debug: Add 1 Hour AFK")]
private void DebugAddAFKTime()
{
    System.DateTime fakeLastPlay = System.DateTime.Now.AddHours(-1);
    PlayerPrefs.SetString("LastPlayTime", fakeLastPlay.ToString());
    UnityEditor.EditorApplication.isPlaying = false;
    UnityEditor.EditorApplication.isPlaying = true;
}
#endif
```

### Balance Testing Tools
```csharp
using UnityEngine;

#if UNITY_EDITOR
public class BalanceTester : MonoBehaviour
{
    [ContextMenu("Test: Max Out Efectivo")]
    private void TestMaxEfectivo()
    {
        ResourceManager.Instance.AddEfectivo(999999);
    }
    
    [ContextMenu("Test: Unlock All Recipes")]
    private void TestUnlockAllRecipes()
    {
        var menuSystem = FindObjectOfType<MenuSystem>();
        // Unlock logic...
    }
}
#endif
```

---

## Input System Integration

This project uses **new InputSystem** (com.unity.inputsystem v1.14.2):
- Bindings defined in `Assets/InputSystem_Actions.inputactions`
- Generate C# code from asset in Inspector
- Use for UI navigation and interactions

```csharp
using UnityEngine.InputSystem;

public class UINavigator : MonoBehaviour
{
    private GameInput gameInput;
    
    private void Awake()
    {
        gameInput = new GameInput();
    }
    
    private void OnEnable()
    {
        gameInput.UI.Navigate.performed += OnNavigate;
        gameInput.Enable();
    }
    
    private void OnDisable()
    {
        gameInput.UI.Navigate.performed -= OnNavigate;
        gameInput.Disable();
    }
    
    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        // Handle navigation
    }
}
```

---

## Magic Numbers → Constants

❌ **Bad:**
```csharp
if (health < 50) TakeDamage(25);
```

✅ **Good:**
```csharp
private const int LOW_HEALTH_THRESHOLD = 50;
private const int DAMAGE_AMOUNT = 25;
if (health < LOW_HEALTH_THRESHOLD) TakeDamage(DAMAGE_AMOUNT);
```

---

## Methods Should Be Single-Responsibility

❌ **Bad:**
```csharp
public void UpdateGame() 
{ 
    ProcessInput(); 
    UpdatePhysics(); 
    RenderUI(); 
}
```

✅ **Good:**
```csharp
private void Update() 
{
    ProcessInput();
    UpdateLogic();
}

private void LateUpdate() 
{ 
    RenderUI(); 
}
```

---

## When to Create New Scripts
1. **One primary responsibility per class** - don't create "God objects"
2. **Split large systems** into focused managers (e.g., `StaffManager`, `StaffHiringSystem`, `StaffFatigueSystem`)
3. **Use namespaces** - `SiKNessTycoon.Feature` (e.g., `SiKNessTycoon.UI.Cards`)
4. **Keep file name = class name** - `ResourceManager.cs` contains `class ResourceManager`

---

## Related Documentation
- See `Assets/Scenes/AGENTS.md` for MainGame scene structure
- See `Assets/Prefabs/AGENTS.md` for UI card prefab patterns
- See `docs/SiKNessTycoon_GDD/` for complete game design reference
- See `.github/copilot-instructions.md` for project-wide conventions
