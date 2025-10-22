# Assets/Prefabs: Prefab Composition for SiKNess Tycoon

Prefabs are the backbone of reusable UI elements in this idle tycoon game. This guide focuses on **card-based UI**, **overlays**, and **cartoon-style interactions**.

---

## Prefab Naming Convention
- **UI Cards**: `UICard_Recipe.prefab`, `UICard_Employee.prefab`, `UICard_Upgrade.prefab`
- **Buttons**: `UIButton_Primary.prefab`, `UIButton_Secondary.prefab`, `UIButton_Ghost.prefab`
- **Overlays**: `Overlay_AFK.prefab`, `Overlay_Event.prefab`, `Overlay_Upgrade.prefab`
- **Badges**: `Badge_Notification.prefab`, `Badge_Counter.prefab`
- **Screens**: `Screen_Inicio.prefab`, `Screen_Menu.prefab`, `Screen_Personal.prefab`, `Screen_RnD.prefab`

Use descriptive names that indicate **purpose and context**.

---

## Card Prefab Structure (Core UI Element)

Cards are the **primary visual element** of SiKNess Tycoon, inspired by collectible card games but with restaurant theming.

### Standard Card Hierarchy
```
UICard_Recipe (Rect Transform)
├── Background (Image - Luz de Faro color, rounded corners)
│   └── Glow (Image - subtle halo effect, disabled by default)
├── HeroImage (Image - 60% of card area)
├── ContentArea (Vertical Layout Group - 40% of card)
│   ├── TitleText (TextMeshPro - recipeName)
│   ├── DescriptionText (TextMeshPro - stats/traits)
│   └── ActionButton (Button - "Mejorar", "Comprar", etc.)
├── Badge (Image - top corner, for notifications/counters)
└── RarityIndicator (Image - border color/glow for special items)
```

### Card Proportions (from GDD)
| Type | Aspect Ratio | Use Case |
|------|--------------|----------|
| **Employees/Staff** | 2:3 (vertical) | Greater presence, character focus |
| **Recipes/Items** | 1:1 (square) | Grid layouts, consistent spacing |
| **Upgrades/I+D** | 3:2 (horizontal) | Panel-like feel, technical info |

### Card Visual Style
- **Border Radius**: 8-12px (medium roundness)
- **Background**: Luz de Faro (warm gray, translucent)
- **Outline**: Thin on card itself, **thicker on hero image** (separates art from UI)
- **Elevation**: Subtle glow/halo (not drop shadow)

---

## Card Prefab Components

### Standard Card Script
Every card prefab should have a `UICard` component:

```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SiKNessTycoon.UI.Cards
{
    public class UICard : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private Image heroImage;
        [SerializeField] private Image background;
        [SerializeField] private Image glowEffect;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        [Header("Interaction")]
        [SerializeField] private Button actionButton;
        [SerializeField] private Image badge;
        
        [Header("Animation Settings")]
        [SerializeField] private float tiltAmount = 5f;
        [SerializeField] private float tiltDuration = 0.2f;
        
        private RectTransform rectTransform;
        private Vector3 originalRotation;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            originalRotation = rectTransform.eulerAngles;
        }
        
        // Polymorphic initialization
        public void Initialize(RecipeData data) { /* ... */ }
        public void Initialize(EmployeeData data) { /* ... */ }
        public void Initialize(UpgradeData data) { /* ... */ }
        
        // Cartoon-style micro-tilt on hover/touch
        public void OnPointerEnter()
        {
            // Use DOTween if available, else Coroutine
            rectTransform.DORotate(originalRotation + new Vector3(0, 0, tiltAmount), tiltDuration);
            glowEffect.gameObject.SetActive(true);
        }
        
        public void OnPointerExit()
        {
            rectTransform.DORotate(originalRotation, tiltDuration);
            glowEffect.gameObject.SetActive(false);
        }
    }
}
```

### Card States
| State | Visual Change | Trigger |
|-------|---------------|---------|
| **Normal** | Base appearance | Default |
| **Hover/Focus** | Micro-tilt + glow | Pointer enter |
| **Pressed** | Slight scale down + darkened | Button press |
| **Locked** | Desaturated + lock icon | Not unlocked yet |
| **Maxed** | Gold border + checkmark | Fully upgraded |

---

## Button Prefabs

### UIButton_Primary (Main Actions)
```
UIButton_Primary
├── Background (Image - Salsa Brava color)
│   └── Highlight (Image - lighter shade, hidden by default)
├── IconSprite (Image - optional icon)
└── ButtonText (TextMeshPro - "Mejorar ahora", "Comprar", etc.)
```

**Visual Style:**
- Color: **Salsa Brava** (red-orange primary)
- Shape: Rounded rectangle (8-12px radius)
- Effect: Slight cartoon relief (gradient or inner shadow)
- Animation: Squash/stretch on press

**Script:**
```csharp
public class PrimaryButton : Button
{
    [SerializeField] private Image highlight;
    
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        
        switch (state)
        {
            case SelectionState.Highlighted:
                // Show highlight, slight scale up
                transform.DOScale(1.05f, 0.1f);
                break;
            case SelectionState.Pressed:
                // Darken briefly, then highlight
                // Squash/stretch animation
                transform.DOScale(0.95f, 0.05f).OnComplete(() => 
                    transform.DOScale(1.05f, 0.1f));
                break;
            case SelectionState.Normal:
                transform.DOScale(1f, 0.1f);
                break;
        }
    }
}
```

### UIButton_Secondary (Complementary Actions)
- Color: **Albahaca Fresca** or **Luz de Faro**
- Use: "Ver detalles", "Cancelar", "Más info"
- Less prominent than primary

### UIButton_Ghost (Passive Actions)
- Style: Outline only (Aceituna Negra border)
- Use: "Volver luego", "Saltar", "Ignorar"
- Minimal visual weight

---

## Overlay Prefabs

Overlays appear **above all other UI** for important notifications and interactions.

### Overlay_AFK (Claim Offline Rewards)
```
Overlay_AFK
├── DimBackground (Image - dark translucent, full screen)
├── ContentPanel (Vertical Layout Group)
│   ├── IconImage (Image - money bag or clock icon)
│   ├── MessageText (TextMeshPro - "Tu gente ha tirado del carro 💪")
│   ├── RewardAmount (TextMeshPro - "7h12m de curro fresquito")
│   └── ClaimButton (UIButton_Primary - "Recaudar")
└── CloseButton (UIButton_Ghost - small X in corner)
```

**Behavior:**
- **Auto-appears** when AFK reward is ready
- **Blocks interaction** until claimed (dim background intercepts)
- **Flavor text** uses "gamberro ligero" tone

### Overlay_Event (Weekly/Micro Events)
```
Overlay_Event
├── Badge (Image - gota/pin shape, Neón Habanero color)
├── EventPanel
│   ├── EventTitle (TextMeshPro - "Semana del Picante 🌶️")
│   ├── EventDescription (TextMeshPro - "+20% demanda entrantes")
│   └── DismissButton (UIButton_Secondary - "Entendido")
└── AnimatedElements (particles, glow effects)
```

**Behavior:**
- **Auto-dismiss after 3s** or manual dismiss
- **Queue system** if multiple events trigger
- **Visual style**: Vibrant and celebratory

### Overlay_Upgrade (Confirmation/Success)
```
Overlay_Upgrade
├── UpgradeIcon (Image - matching upgrade category)
├── SuccessText (TextMeshPro - "Bien ahí 👌 Cocina fina")
├── FlavorText (TextMeshPro - "La cosa fluye mejor aquí")
└── EffectValue (TextMeshPro - "+8% velocidad preparación")
```

**Behavior:**
- **Brief appearance** (2-3 seconds)
- **Non-blocking** (player can tap to dismiss)
- **Particle effect** (confetti, sparkles) optional

---

## Badge Prefabs

Badges are **small indicators** that draw attention.

### Badge_Notification (Event/AFK/Alert)
- **Shape**: Gota/Pin (teardrop)
- **Color**: **Neón Habanero** (bright pink-red)
- **Use**: "Something new is ready!"
- **Animation**: Gentle pulse/bounce

### Badge_Counter (Quantity Indicator)
- **Shape**: Circle
- **Color**: Context-dependent (Efectivo = Aceite de Oliva, Alert = Guindilla)
- **Use**: Number of available actions, unread notifications
- **Text**: 1-2 digits max, "9+" for overflow

---

## Screen Prefabs

Each main screen is a **prefab instance** activated/deactivated during navigation.

### Screen_Inicio (Service Overview)
```
Screen_Inicio
├── ScrollView (Vertical Scroll Rect)
│   ├── TurnStatusCard (prefab instance)
│   ├── SuggestedActionCard (prefab instance)
│   ├── AreasContainer (Horizontal Layout Group)
│   │   ├── KitchenAreaCard
│   │   ├── ServiceAreaCard
│   │   └── CashierAreaCard
│   └── DailyObjectivesPanel
│       ├── Objective1 (checkbox + text)
│       ├── Objective2
│       └── Objective3
```

### Screen_Menu (Recipe Management)
```
Screen_Menu
├── CategoryTabBar (Horizontal Layout Group)
│   ├── Tab_Entrantes
│   ├── Tab_Principales
│   ├── Tab_Postres
│   └── Tab_Bebidas
├── RecipeScrollView (Grid Layout Group)
│   └── [UICard_Recipe instances spawned here]
└── SpecialDaySlot (highlighted card)
```

### Screen_Personal (Staff Management)
```
Screen_Personal
├── AreaTabBar (Cocina / Sala)
├── StaffScrollView (Vertical Scroll Rect)
│   └── [UICard_Employee instances]
└── OptimizeTurnsButton (global action)
```

### Screen_RnD (Tech Tree)
```
Screen_RnD
├── CategoryColumns (Cocina, Servicio, Marketing)
├── UpgradeScrollView (Vertical Scroll Rect)
│   └── [UpgradeNode prefabs with connection lines]
└── HistorialButton (view unlocked upgrades)
```

---

## Component Organization Best Practices

### Standard Prefab Structure
```
[Root - Descriptive Name]
├── [Components on root]
│   ├── RectTransform (for UI) or Transform
│   ├── Canvas Group (for fading)
│   └── UICard / UIButton / UIOverlay script
│
├── Visual (child for visual separation)
│   ├── Background
│   ├── Icon/Image
│   └── (optional) Effects
│
└── Content (child for text/interactive elements)
    ├── TextMeshPro components
    └── Button children
```

### Best Practices
- **One primary script per prefab** - on the root object
- **Logical grouping** - visual elements under "Visual" child
- **No empty GameObjects** - every object should have purpose
- **Clear naming** - avoid generic names like "Object", "Child", "Panel"

---

## Script References in Prefabs

✅ **Recommended:**
```csharp
[SerializeField] private Image heroImage;  // Assign in prefab
[SerializeField] private TextMeshProUGUI titleText; // Assign in prefab

private void Awake()
{
    // Use assigned references immediately
    heroImage.sprite = defaultSprite;
}
```

❌ **Avoid:**
```csharp
private Image heroImage; // Found at runtime - fragile
private void Start()
{
    heroImage = GetComponentInChildren<Image>(); // Slow and brittle
}
```

---

## Prefab Variants

Use **Prefab Variants** for:
- **Recipe cards** by category (Entrantes, Principales, Postres, Bebidas)
- **Employee cards** by area (Cocina, Sala)
- **Buttons** with different sizes (Small, Medium, Large)
- **Badges** with different colors (per currency type)

**Don't use variants** if hierarchy differs significantly - create new prefabs instead.

---

## Animation and Transitions

### Card Interactions
- **Hover**: Micro-tilt (5°) + glow activation
- **Press**: Brief scale down (0.95x) → spring back (1.05x)
- **Appear**: Slide-in from bottom + fade-in
- **Disappear**: Fade-out + slight scale down

### Screen Transitions
- **Tab switching**: Lateral slide (like mobile app)
- **Overlay opening**: Fade-in background + scale-up panel (from 0.8 to 1.0)
- **Overlay closing**: Scale-down + fade-out

### Use DOTween (if available)
```csharp
using DG.Tweening;

// Card appear animation
rectTransform.localScale = Vector3.zero;
rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

// Overlay fade in
canvasGroup.alpha = 0;
canvasGroup.DOFade(1f, 0.2f);
```

---

## Visual Style Reference (from GDD)

### Color Palette (Applied to Prefabs)
| Role | Color Name | Use in Prefabs |
|------|-----------|----------------|
| **Primary** | Salsa Brava | Primary buttons, important actions |
| **Secondary 1** | Albahaca Fresca | Secondary buttons, success states |
| **Secondary 2** | Aceituna Negra | Borders, disabled states, ghost buttons |
| **Background** | Medianoche Terraza | Dim overlays, scene background |
| **Elevated** | Luz de Faro | Card backgrounds, panel surfaces |
| **Success** | Aceite de Oliva | Level-ups, positive feedback |
| **Alert** | Guindilla Suave/Picante | Warnings, bottleneck indicators |
| **Event** | Neón Habanero | Event notifications, special badges |

### Typography (Applied to TextMeshPro)
- **Titles (H1)**: Urbana font, 35-48px, bold
- **Body (H2)**: Redondeada font, 18-24px, regular
- **Microcopy**: Redondeada font, 12-16px, light

### Spacing (Applied to Layout Groups)
- **Card padding**: 16-24px
- **Button padding**: 12-16px
- **Grid spacing**: 12-16px between cards
- **Section margins**: 24-32px between major sections

---

## Prefab Testing and Validation

### Context Menu Testing
```csharp
#if UNITY_EDITOR
[ContextMenu("Test: Populate with Sample Data")]
private void TestPopulate()
{
    var sampleRecipe = ScriptableObject.CreateInstance<RecipeData>();
    sampleRecipe.recipeName = "Bravas Test";
    Initialize(sampleRecipe);
}
#endif
```

### Prefab Isolation Testing
Create a `DevScene_PrefabTest.unity` with:
- Single Canvas
- Instance of prefab to test
- Test controller to cycle through states

---

## Performance Considerations

### UI Optimization
- **Canvas separation**: Static UI (header) vs dynamic UI (card lists) in separate Canvases
- **Disable raycast**: On non-interactive images (hero images, backgrounds)
- **Sprite atlases**: Group all UI sprites into atlas (reduces draw calls)
- **Text mesh pools**: Reuse TextMeshPro components where possible

### Card Pooling
For screens with many cards (Menu, Personal):
```csharp
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
```

---

## Common Patterns

### Recipe Card Pattern
```
UICard_Recipe
├── Background (Luz de Faro, rounded)
├── HeroImage (food illustration, 60%)
├── RecipeName (TextMeshPro, bold)
├── Stats (price, time, popularity icons)
└── UpgradeButton (UIButton_Primary)
```

### Employee Card Pattern
```
UICard_Employee
├── Background (Luz de Faro, rounded)
├── Portrait (character art, 60%)
├── EmployeeName (TextMeshPro, bold)
├── TraitDescription (TextMeshPro, regular)
├── FatigueIndicator (progress bar)
└── AssignButton (UIButton_Primary)
```

### Upgrade Node Pattern
```
UpgradeNode
├── NodeBackground (locked/available/unlocked states)
├── UpgradeIcon (gastro-themed icon)
├── UpgradeName (TextMeshPro, hybrid name)
├── Description (effect description)
├── ConnectionLine (to parent/child nodes)
└── UnlockButton (cost + CTA)
```

---

## Related Documentation
- See `Assets/Scripts/AGENTS.md` for UI component scripting patterns
- See `Assets/Scenes/AGENTS.md` for how prefabs are used in MainGame scene
- See `docs/SiKNessTycoon_GDD/SiKNess_Vol1.md` for UI Kit and visual identity
- See `.github/copilot-instructions.md` for project-wide conventions
