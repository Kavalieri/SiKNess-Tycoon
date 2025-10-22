# Assets/Scenes: Scene Organization for SiKNess Tycoon

Game scenes are the entry points for gameplay states. This document guides scene setup specifically for an **idle tycoon game** with persistent UI navigation.

---

## Scene Naming Convention
- **Main gameplay**: `MainGame.unity` - The core idle management screen
- **Bootstrap**: `Bootstrap.unity` - Initial loading and initialization (optional)
- **Test/Dev**: `DevScene_*.unity` - Prefixed for easy filtering (not in build)

---

## MainGame Scene Structure

The game uses a **single persistent scene** with tab-based navigation (no scene loading during gameplay).

### Recommended Root GameObjects
```
MainGame
├── [Managers] (DontDestroyOnLoad)
│   ├── GameManager          # Master state controller
│   ├── ResourceManager      # Currency system (Efectivo, Fama, Estrellas)
│   ├── SaveSystem           # JSON persistence
│   └── EventBus             # Global event relay
│
├── [Systems] (Active during gameplay)
│   ├── EconomySystem        # Revenue calculation
│   ├── StaffSystem          # Employee management
│   ├── MenuSystem           # Recipe handling
│   ├── RnDSystem            # Tech tree
│   ├── EventSystem          # Weekly/micro events
│   ├── AFKSystem            # Offline progression
│   └── VenueSystem          # Multi-location management
│
├── [UI Canvas] (Screen Space - Overlay)
│   ├── HeaderBar            # Efectivo, Fama, indicators
│   ├── NavigationBar        # Bottom tabs (Inicio, Menú, Personal, I+D)
│   ├── ScreenContainer      # Parent for swappable screens
│   │   ├── InicioScreen     # Service overview
│   │   ├── MenuScreen       # Recipe management
│   │   ├── PersonalScreen   # Staff hiring/optimization
│   │   └── RnDScreen        # Tech tree upgrades
│   ├── OverlayContainer     # Popups, notifications, AFK claim
│   └── FloatingBadges       # AFK ready, event notifications
│
├── [Audio]
│   └── AudioManager         # Background music + SFX
│
└── [Background]
    └── ParallaxBackground   # Urban nocturnal terrace aesthetic
```

---

## Scene Loading Philosophy

**No mid-gameplay scene loading** - All UI screens are **instantiated or activated** within the MainGame scene.

### Navigation Pattern
```csharp
using UnityEngine;

namespace SiKNessTycoon.UI.Navigation
{
    public enum GameScreen { Inicio, Menu, Personal, RnD }
    
    public class ScreenNavigator : MonoBehaviour
    {
        [SerializeField] private GameObject[] screens; // 4 screens
        private int currentScreenIndex = 0;
        
        public void NavigateTo(GameScreen screen)
        {
            // Deactivate current
            screens[currentScreenIndex].SetActive(false);
            
            // Activate target
            currentScreenIndex = (int)screen;
            screens[currentScreenIndex].SetActive(true);
            
            // Slide transition (lateral)
            // ... animation logic
            
            GameEvents.OnScreenChanged?.Invoke(screen.ToString());
        }
    }
}
```

### Why Single Scene?
- **No loading times** - Instant tab switching
- **Persistent state** - Managers stay alive
- **Easier debugging** - All systems visible in hierarchy
- **Mobile-friendly** - Reduced memory churn

---

## UI Canvas Setup

### Canvas Configuration
- **Render Mode**: Screen Space - Overlay (for simplicity)
- **Canvas Scaler**: Scale With Screen Size
  - Reference Resolution: **1080x1920** (portrait mobile)
  - Match: Width/Height = **0.5** (balanced)
- **Graphic Raycaster**: Enabled for touch/click detection

### Navigation Bar (Persistent)
Always visible at the bottom, contains 4 tabs:

```
NavigationBar (Horizontal Layout Group)
├── TabButton_Inicio    (Icon + Label)
├── TabButton_Menu      (Icon + Label)
├── TabButton_Personal  (Icon + Label)
└── TabButton_RnD       (Icon + Label)
```

Each button triggers `ScreenNavigator.NavigateTo()` with appropriate enum.

### Header Bar (Persistent)
Always visible at the top:

```
HeaderBar
├── EfectivoDisplay     (Icon + Number + Animation)
├── FamaDisplay         (Badge style)
└── InfoButton          (Contextual inspector)
```

Subscribes to `ResourceManager` events to update in real-time.

---

## Screen-Specific Setup

### Inicio Screen (Service Overview)
Shows restaurant status and suggests next action.

```
InicioScreen
├── TurnStatusCard      # Clients/h, satisfaction, dominant bottleneck
├── SuggestedActionCard # "Mejorar ahora" CTA
├── AreasContainer      # Kitchen, Service, Cashier cards
└── DailyObjectivesList # 3 daily goals
```

### Menú Screen (Recipe Management)
Card grid with category tabs.

```
MenuScreen
├── CategoryTabs        # Entrantes, Principales, Postres, Bebidas
├── RecipeScrollView    # Grid of recipe cards
│   └── RecipeCard (prefab instances)
└── SpecialDaySlot      # Highlighted "Especial del día"
```

### Personal Screen (Staff Management)
Lists employees by area.

```
PersonalScreen
├── AreaTabs            # Cocina, Sala
├── StaffScrollView     # Vertical list of employee cards
│   └── EmployeeCard (prefab instances)
└── OptimizeTurnsButton # Global optimization action
```

### I+D Screen (Tech Tree)
Vertical scroll of upgrades grouped by category.

```
RnDScreen
├── CategoryColumns     # Cocina, Servicio, Marketing
├── UpgradeScrollView   # Vertical scroll
│   └── UpgradeNode (prefab instances)
└── HistorialButton     # View unlocked upgrades
```

---

## Overlay System

Overlays appear **above all other UI** for:
- AFK reward claim
- Event notifications
- Upgrade confirmations
- Achievement popups

```
OverlayContainer (Canvas Group for fade)
├── AFKRewardOverlay    # Floating pin, appears when ready
├── EventNoticeOverlay  # Weekly/micro event announcements
├── UpgradeToast        # Brief confirmation with flavor text
└── ObjectiveComplete   # Daily goal completion celebration
```

### Overlay Behavior
- **Non-blocking**: Player can dismiss by tapping outside (except AFK claim)
- **Auto-dismiss**: Most overlays fade after 2-3 seconds
- **Queueing**: Multiple overlays queue, showing one at a time

---

## Camera Setup

- **Camera Type**: Orthographic (2D game)
- **Size**: Adjust to fit reference resolution (typically 10-12 for portrait)
- **Clear Flags**: Solid Color (Medianoche Terraza background color)
- **Culling Mask**: Everything (no need for layers in single-scene setup)

---

## Background and Atmosphere

### Visual Layers
```
Background
├── SkyGradient         # Dark blue-to-black gradient
├── CityScape           # Silhouette of buildings (parallax optional)
├── TerraceLighting     # Warm glows (sprite or particle system)
└── TextureOverlay      # Subtle noise/grain (matching GDD aesthetic)
```

### Aesthetic Goals (from GDD)
- **Urban nocturnal terrace** feel
- **Warm lighting** (farolillos, terrace lights)
- **Vibrant but not loud** - supports UI without competing
- **Subtle texture** (wall/wood/stone) for depth

---

## Physics and Colliders (Minimal)

This game is **UI-driven**, not physics-based:
- No Rigidbody2D required in MainGame scene
- Colliders only if future mini-games need them
- Physics2D gravity can be disabled (Project Settings)

---

## Audio Setup

```
AudioManager
├── BackgroundMusicSource   # Looping ambient track
└── SFXSource               # One-shot sounds (button clicks, overlays)
```

### Audio Considerations
- **BGM**: Urban nocturnal ambience (subtle, not distracting)
- **SFX**: Cartoon-style pops, whooshes for UI interactions
- **Volume**: Controlled via Settings overlay (future)

---

## Manager Initialization Order

Use `[DefaultExecutionOrder]` attribute to control initialization:

```csharp
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour { }

[DefaultExecutionOrder(-90)]
public class ResourceManager : MonoBehaviour { }

[DefaultExecutionOrder(-80)]
public class SaveSystem : MonoBehaviour { }
```

This ensures:
1. GameManager initializes first
2. ResourceManager loads saved data
3. Systems query ResourceManager during their Start()

---

## Development and Testing Scenes

### DevScene_EconomyTest.unity
Minimal scene for testing economy calculations in isolation.

```
DevScene_EconomyTest
├── EconomySystem (isolated)
├── TestUI (manual triggers)
└── DebugDisplay (show calculation results)
```

### DevScene_UICards.unity
Test card layouts and animations without full game context.

```
DevScene_UICards
├── UICanvas
│   ├── RecipeCardSample
│   ├── EmployeeCardSample
│   └── UpgradeCardSample
└── TestController (cycle through states)
```

**Important**: DevScenes are **NOT** added to Build Settings.

---

## When Creating New Scenes

1. **From Template**: Use `Assets/Settings/MainGameSceneTemplate.scenetemplate` (to be created)
2. **Manual Setup**:
   - Create scene file in `Assets/Scenes/`
   - Add Main Camera (Orthographic, Clear Solid Color)
   - Add Canvas (Screen Space Overlay, Canvas Scaler configured)
   - Add [Managers] parent object with core systems
3. **Register in Build Settings** before testing (if applicable)

---

## Scene Testing Workflow

### Play Mode Testing
1. Always **start from MainGame scene** (not subscenes)
2. Use `[ContextMenu]` methods in managers for quick testing
3. Enable Gizmos to visualize event triggers (if any)

### Build Testing
1. **Only MainGame** in Build Settings (index 0)
2. Bootstrap scene optional (for splash/initialization)
3. Test on target platform early (mobile touch vs mouse)

---

## Performance Considerations

### Canvas Optimization
- **Separate Canvas** for static vs dynamic UI (future optimization)
- **Disable raycast** on non-interactive images
- **Use sprite atlases** for UI elements (reduces draw calls)

### Update Loops
- Systems use **coroutines** for periodic updates (not Update())
- UI subscribes to **events**, not polling managers every frame

Example:
```csharp
// ❌ BAD: Polling every frame
private void Update()
{
    efectivoText.text = ResourceManager.Instance.Efectivo.ToString();
}

// ✅ GOOD: Event-driven update
private void Start()
{
    ResourceManager.Instance.OnEfectivoChanged += UpdateEfectivoDisplay;
}

private void UpdateEfectivoDisplay(int newAmount)
{
    efectivoText.text = newAmount.ToString();
}
```

---

## Lighting (URP 2D)

- Use **Universal Renderer** configured in `Assets/Settings/Renderer2D.asset`
- **Global Light2D** for overall illumination
- **No real-time shadows** (performance + aesthetic choice)
- **Bloom** and **Color Adjustment** for nighttime glow (post-processing)

---

## Related Documentation
- See `Assets/Scripts/AGENTS.md` for system architecture and code patterns
- See `Assets/Prefabs/AGENTS.md` for UI card prefab composition
- See `docs/SiKNessTycoon_GDD/` for game design and visual identity
- See `.github/copilot-instructions.md` for project-wide conventions
