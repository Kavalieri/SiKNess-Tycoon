# SiKNess Tycoon — Technical Implementation Guide

> Puente entre el Game Design Document (GDD) y la arquitectura Unity

---

## 📋 Resumen Ejecutivo

Este documento traduce las mecánicas y visión del GDD (Volúmenes 1 y 2) en una arquitectura técnica implementable en Unity 6.2 LTS. Sirve como guía para desarrolladores, diseñadores y agentes de IA trabajando en el proyecto.

**Propósito:** Asegurar que cada sistema diseñado en el GDD tiene una representación clara en código, prefabs y escenas.

---

## 🎮 Visión General del Juego

### Concepto Core
**Idle tycoon management game** donde el jugador gestiona un bar/restaurante que evoluciona en franquicia.

### Pilares de Diseño
1. **Progreso AFK honesto** - El juego avanza offline de forma ética
2. **Optimización clara** - Siempre hay 1 cuello de botella → 1 acción sugerida
3. **Identidad visual vibrante** - Cartoon 2D con paleta gastronómica
4. **Tono gamberro ligero** - Microtextos con personalidad, sin vulgaridad

---

## 🏗️ Arquitectura General

### Escena Única Persistente
El juego usa **una escena principal (MainGame)** con navegación por pestañas:
- No hay cambios de escena durante gameplay
- UI modular: 4 screens activables (Inicio, Menú, Personal, I+D)
- Managers persistentes (DontDestroyOnLoad)

### Patrones Clave
| Patrón | Uso | Beneficio |
|--------|-----|-----------|
| **Singleton** | GameManager, ResourceManager | Acceso global sin acoplamiento |
| **Event Bus** | GameEvents estático | Comunicación desacoplada entre sistemas |
| **ScriptableObjects** | RecipeData, EmployeeData, etc. | Data-driven design, editable en Inspector |
| **Object Pooling** | UI Cards | Performance en listas dinámicas |
| **Coroutines** | Updates periódicos | Evita Update() pesado cada frame |

---

## 💰 Sistema de Economía (3 Monedas)

### Implementación
**Clase:** `ResourceManager.cs` (Core/)

**Monedas:**
1. **Efectivo** - Mejoras inmediatas, operaciones diarias
2. **Fama** - Desbloqueos de sedes y recetas
3. **Estrellas** - Meta-progresión (futuro)

**Eventos:**
```csharp
ResourceManager.Instance.OnEfectivoChanged += UpdateUI;
GameEvents.OnResourceChanged += LogTransaction;
```

### Flujo de Efectivo
```
[Servicio] → [Clientes] → [Venta de platos] → +Efectivo
                ↓
[Mejoras] ← -Efectivo ← [Botón "Mejorar ahora"]
```

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "Efectivo = mejoras inmediatas" | `ResourceManager.TrySpendEfectivo()` |
| "Fama = desbloqueos por sede" | `RecipeData.unlockFamaThreshold` |
| "AFK genera Efectivo" | `AFKSystem.CalculateOfflineReward()` |

---

## 🍽️ Sistema de Menú (Recetas)

### Implementación
**ScriptableObject:** `RecipeData.cs` (Data/)
**Sistema:** `MenuSystem.cs` (Systems/Menu/)

### Propiedades Clave
```csharp
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite heroImage;              // 60% de la card
    public RecipeCategory category;       // Entrantes, Principales, Postres, Bebidas
    public TimeOfDay popularity;          // ☀️/🌙
    public int basePrice;
    public float preparationTime;
    public int unlockFamaThreshold;
}
```

### Flujo de Desbloqueo
```
[Fama alcanza threshold] → GameEvents.OnFamaChanged
                          ↓
          [MenuSystem.CheckRecipeUnlocks()]
                          ↓
          [RecipeData.isUnlocked = true]
                          ↓
          [Overlay: "¡Nueva receta! 🍽️"]
```

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "4 categorías de recetas" | `RecipeCategory enum` |
| "Popularidad día/noche" | `TimeOfDay enum` + logic en EconomySystem |
| "Especial del día" | Slot destacado en MenuScreen + `GameEvents.OnSpecialDaySet` |
| "Sinergias entre platos" | `RecipeData.synergyTags[]` |

---

## 👥 Sistema de Personal

### Implementación
**ScriptableObject:** `EmployeeData.cs` (Data/)
**Sistema:** `StaffSystem.cs` (Systems/Staff/)

### Rasgos de Empleado
```csharp
public enum EmployeeTrait
{
    Fast,        // Rápido/a - ⚡
    Precise,     // Preciso/a - 🎯
    Organized,   // Ordenado/a - 📋
    Charismatic, // Simpático/a - 😊
    QueueMaster  // Gestiona colas - 👥
}
```

### Sistema de Fatiga
```csharp
employee.AddFatigue(hoursWorked);
if (employee.IsTooFatigued())
{
    GameEvents.RaiseEmployeeFatigued(employee.employeeName);
    // Overlay: "Lola está fundida 😴"
}
```

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "Rasgos únicos por empleado" | `EmployeeTrait enum` + métodos `GetEffectiveSpeed()` |
| "Fatiga ligera" | `EmployeeData.currentFatigue` + penalty multiplicativo |
| "Optimizar turnos" | Botón global → `GameEvents.RaiseShiftsOptimized()` |

---

## 🔬 Sistema I+D (Tech Tree)

### Implementación
**ScriptableObject:** `UpgradeData.cs` (Data/)
**Sistema:** `RnDSystem.cs` (Systems/RnD/)

### Estructura de Nodo
```csharp
public class UpgradeData : ScriptableObject
{
    public string upgradeId;              // Único
    public string displayName;            // "Marcha de fogones"
    public string technicalDescriptor;    // "(+ vel. cocina)"
    public UpgradeCategory category;      // Cocina, Servicio, Marketing
    public UpgradeTier tier;              // Tier1, Tier2, Tier3+
    
    // Efectos
    public float speedBonus;
    public float afkMultiplier;
    
    // Prerequisitos
    public UpgradeData[] prerequisites;
}
```

### Árbol Visual
- **Vertical scroll** (crece hacia abajo)
- **3 columnas** (Cocina, Servicio, Marketing)
- **Estados:** Bloqueado (opaco) → Disponible (iluminado) → Mejorado (glow + insignia)

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "Nombres híbridos (temático + técnico)" | `displayName + technicalDescriptor` |
| "Iconografía gastronómica" | `UpgradeData.icon` (sartén, cuchillo, campana) |
| "Mejoras permanentes + histórico" | `isUnlocked` persistente + UI de historial |
| "I+D afecta AFK" | `RnDSystem.GetAFKMultiplier()` usado por AFKSystem |

---

## 💤 Sistema AFK (Progreso Offline)

### Implementación
**Sistema:** `AFKSystem.cs` (Systems/AFK/)

### Cálculo de Recompensa
```csharp
public AFKReward CalculateOfflineReward(DateTime lastPlayTime)
{
    TimeSpan elapsed = DateTime.Now - lastPlayTime;
    float hours = Mathf.Min(elapsed.TotalHours, maxAFKHours);
    
    float production = baseProductionRate * hours;
    production *= GetRnDMultiplier();      // I+D boosts
    production *= GetVenueMultiplier();    // Sedes pasivas
    production *= GetEventMultiplier();    // Eventos activos
    
    return new AFKReward { efectivo = production, duration = hours };
}
```

### Presentación
- **Icono flotante** (pin) solo aparece cuando hay recompensa
- **Overlay con flavor text**: "Tu gente ha tirado del carro 💪"
- **Micro-animación** (rebote suave)

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "AFK ético (no castiga desconexión)" | Acumula hasta `maxAFKHours`, sin penalizaciones |
| "Escala con I+D y sedes" | Multiplicadores dinámicos |
| "Presentación emocional" | `MicrotextLibrary.GetAFKMessage()` |

---

## 🏢 Sistema de Sedes (Expansión)

### Implementación
**ScriptableObject:** `VenueData.cs` (Data/)
**Sistema:** `VenueSystem.cs` (Systems/Venues/)

### Tipos de Sedes
```csharp
public enum VenueType
{
    BarDelBarrio,   // Inicial
    BistroUrbano,   // Segunda
    FoodCourt,      // Tercera
    Estacion        // Cuarta
}
```

### Modificadores por Sede
```csharp
public class VenueData : ScriptableObject
{
    public float demandMultiplier;      // Escala de clientes
    public float speedMultiplier;       // Ritmo operacional
    public float pressureLevel;         // Frecuencia de cuellos
    public float afkProductionRate;     // AFK base
}
```

### Progresión
1. **Sede activa**: Donde juegas actualmente
2. **Sedes previas**: Generan AFK pasivo (30% de su producción)
3. **Desbloqueo**: Requiere Fama + Efectivo

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "Sedes previas = sucursales pasivas" | `GetPassiveAFKContribution()` |
| "Nueva sede ≠ reset" | I+D y Personal se conservan |
| "Cada sede tiene modificadores" | `VenueData` properties aplicados en EconomySystem |

---

## 🎉 Sistema de Eventos

### Implementación
**Sistema:** `EventSystem.cs` (Systems/Events/)

### Tipos de Eventos
```csharp
public enum EventType
{
    Weekly,     // Modificador prolongado (1 semana)
    Micro,      // Boost breve (30min - 12h)
    Special     // Anclados a hitos
}
```

### Ejemplos
```csharp
// Semanal: Semana del Picante 🌶️
demandMultiplier["Entrantes"] *= 1.20f;

// Micro: Racha Buena 💫
afkMultiplier *= 1.30f; // 30 minutos

// Especial: Cliente VIP 🎩
if (satisfactionHigh) AddFama(bonus);
```

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "1 semanal activo siempre" | EventSystem mantiene evento activo |
| "Microeventos sorpresa" | Triggers aleatorios con cooldowns |
| "Eventos cambian meta temporal" | Multiplicadores aplicados globalmente |

---

## 🎨 Sistema de UI (Card-Based)

### Implementación
**Prefabs:** UICard_Recipe, UICard_Employee, UICard_Upgrade
**Scripts:** `UICard.cs` (UI/Cards/)

### Proporciones de Cards (GDD)
| Tipo | Ratio | Justificación |
|------|-------|---------------|
| Empleados | 2:3 (vertical) | Mayor presencia, apego visual |
| Recetas | 1:1 (cuadrado) | Grid coherente, rotaciones |
| Upgrades | 3:2 (horizontal) | Sensación panel/gestión |

### Composición Visual
```
UICard
├── Background (Luz de Faro + glow)
├── HeroImage (60% del espacio)      ← Arte protagonista
├── TitleText (nombre)
├── DescriptionText (stats/rasgos)
└── ActionButton ("Mejorar ahora")
```

### Animaciones Cartoon
```csharp
// Micro-tilt al hover
rectTransform.DORotate(new Vector3(0, 0, 5f), 0.2f);

// Squash/stretch al press
transform.DOScale(0.95f, 0.05f).OnComplete(() => 
    transform.DOScale(1.05f, 0.1f));
```

### Integración GDD → Código
| GDD | Implementación |
|-----|----------------|
| "Hero image 60%" | Layout Group con proporciones fijas |
| "Micro-tilt cartoon" | DOTween animation en UICard |
| "Paleta gastronómica" | ColorPalette ScriptableObject |

---

## 🎨 Paleta Cromática (Implementación)

### ColorPalette.cs (recomendado)
```csharp
[CreateAssetMenu(fileName = "ColorPalette", menuName = "SiKNess/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [Header("Primary")]
    public Color salsaBrava;        // #FF5733 (rojo-naranja)
    
    [Header("Secondary")]
    public Color albahacaFresca;    // #4CAF50 (verde vivo)
    public Color aceitunaNegra;     // #3E4A3C (verde oscuro gris)
    
    [Header("Backgrounds")]
    public Color medianochTerraza;  // #1A1F2E (azul carbón)
    public Color luzDeFaro;         // #C2B8A3 (gris cálido)
    
    [Header("Feedback")]
    public Color aceiteOliva;       // #D4AF37 (dorado)
    public Color guindillaSuave;    // #E67E22 (rojo tenue)
    public Color guindillaPicante;  // #E74C3C (rojo intenso)
    public Color neonHabanero;      // #FF006E (rosa brillante)
}
```

### Uso en UI
```csharp
[SerializeField] private ColorPalette palette;

primaryButton.image.color = palette.salsaBrava;
successOverlay.color = palette.aceiteOliva;
```

---

## 📝 Microtextos (Implementación)

### MicrotextLibrary.cs
Centraliza todo el tono "gamberro ligero":

```csharp
public static class MicrotextLibrary
{
    public static string GetBottleneckMessage(BottleneckType type)
    {
        return type switch
        {
            BottleneckType.Kitchen => "La cocina va a pedales, mete chispa 🔥",
            BottleneckType.Service => "La sala se arrastra... ¿les damos marcha? 😅",
            // ...
        };
    }
    
    public static string GetAFKMessage(float hours) { /* ... */ }
    public static string GetUpgradeMessage(string name) { /* ... */ }
}
```

### Integración
Todos los overlays, botones y tooltips usan MicrotextLibrary:
```csharp
overlayText.text = MicrotextLibrary.GetAFKMessage(hours);
```

---

## 🔄 Flujo de Onboarding (Días 0-3)

### Día 0: Loop Base
```
1. Pantalla Inicio → Ver estado del turno
2. Detectar cuello (cocina/servicio/caja)
3. Mostrar "Mejorar ahora" CTA
4. Gastar Efectivo → Ver mejora inmediata
```

**Implementación:**
- `BottleneckDetector.DetectCurrentBottleneck()`
- Overlay con mensaje: "La cocina va a pedales, mete chispa 🔥"

### Día 1: Personal + Recetas
```
1. Tab "Personal" iluminado
2. Contratar primer empleado
3. Ver rasgo en acción
4. Tab "Menú" iluminado
5. Mejorar receta Bravas
```

### Día 2: AFK
```
1. Jugador cierra el juego
2. Al volver: Icono AFK flotante
3. Overlay: "Tu gente ha tirado del carro 💪"
4. Recaudar recompensa
```

### Día 3: I+D
```
1. Tab "I+D" se desbloquea
2. Mostrar árbol con primer nodo disponible
3. Desbloquear "Marcha de fogones"
4. Ver overlay: "Bien ahí 👌 La cocina ya no se arrastra"
```

---

## 📊 Sistema de Detección de Cuellos de Botella

### Implementación
**Clase:** `BottleneckDetector.cs` (Systems/Economy/)

```csharp
public enum BottleneckType { Kitchen, Service, Cashier, None }

public class BottleneckDetector : MonoBehaviour
{
    public BottleneckType DetectCurrentBottleneck()
    {
        float kitchenLoad = CalculateKitchenLoad();   // 0.0 - 1.0
        float serviceLoad = CalculateServiceLoad();
        float cashierLoad = CalculateCashierLoad();
        
        float maxLoad = Mathf.Max(kitchenLoad, serviceLoad, cashierLoad);
        
        if (maxLoad < 0.7f) return BottleneckType.None;
        
        // Return dominant bottleneck
        // ...
    }
}
```

### Integración con UI
```csharp
BottleneckType cuello = detector.DetectCurrentBottleneck();
string mensaje = MicrotextLibrary.GetBottleneckMessage(cuello);

// Mostrar en card "Estado del turno"
bottleneckText.text = mensaje;

// Sugerir acción
string accion = GetSuggestedAction(cuello);
GameEvents.RaiseActionSuggested(accion, cuello.ToString());
```

---

## 🧪 Testing y Debug Tools

### Context Menus para Testing
```csharp
#if UNITY_EDITOR
[ContextMenu("Debug: Add 10000 Efectivo")]
private void DebugAddEfectivo()
{
    ResourceManager.Instance.AddEfectivo(10000);
}

[ContextMenu("Debug: Unlock All Recipes")]
private void DebugUnlockAllRecipes()
{
    // ...
}

[ContextMenu("Debug: Simulate 8 Hours AFK")]
private void DebugSimulateAFK()
{
    DateTime fake = DateTime.Now.AddHours(-8);
    PlayerPrefs.SetString("LastPlayTime", fake.ToString());
}
#endif
```

### Balance Testing Scene
Crear `DevScene_BalanceTest.unity`:
- Panel con sliders para multiplicadores
- Botones para simular eventos
- Display de cálculos en tiempo real

---

## 📦 Recursos y Assets

### Estructura de Carpetas
```
Assets/
├── Scripts/
│   ├── Core/
│   ├── Data/
│   ├── Systems/
│   └── UI/
├── Prefabs/
│   ├── Cards/
│   ├── Overlays/
│   ├── Buttons/
│   └── Screens/
├── Sprites/
│   ├── Heroes/          # Hero images para cards
│   ├── Icons/           # Iconos UI
│   ├── Backgrounds/     # Fondos nocturnos urbanos
│   └── Effects/         # Glow, particles
├── Data/
│   ├── Recipes/
│   ├── Employees/
│   ├── Upgrades/
│   └── Venues/
└── Scenes/
    ├── MainGame.unity
    └── DevScenes/
```

### Naming Conventions
| Tipo | Formato | Ejemplo |
|------|---------|---------|
| Prefabs | `Type_Name.prefab` | `UICard_Recipe.prefab` |
| Sprites | `category_name.png` | `hero_bravas.png` |
| ScriptableObjects | `Name.asset` | `Bravas.asset` (RecipeData) |
| Scenes | `PascalCase.unity` | `MainGame.unity` |

---

## 🚀 Próximos Pasos

### MVP Priorities
1. ✅ Core systems (Resource, Events, AFK)
2. ✅ Data layer (ScriptableObjects)
3. ⏳ UI Cards implementation
4. ⏳ MainGame scene setup
5. ⏳ First playable (Inicio screen + 1 upgrade)

### Phase 2
- Complete 4-screen navigation
- Save/Load system (JSON)
- Full onboarding flow
- Balance tuning tools

### Phase 3
- Visual polish (animations, particles)
- Audio integration
- Event system expansion
- Venue progression

---

## 📚 Referencias Cruzadas

### Documentos Relacionados
- **GDD Vol. 1**: Mecánicas core, UI Kit, paleta visual
- **GDD Vol. 2**: Progresión, sedes, eventos, onboarding
- `.github/copilot-instructions.md`: Guía general del proyecto
- `Assets/Scripts/AGENTS.md`: Patrones de código
- `Assets/Scenes/AGENTS.md`: Estructura de escenas
- `Assets/Prefabs/AGENTS.md`: Composición de prefabs

### Mapeo GDD → Código
| Concepto GDD | Implementación Unity |
|--------------|----------------------|
| Efectivo, Fama, Estrellas | `ResourceManager.cs` |
| Recetas | `RecipeData.cs` + `MenuSystem.cs` |
| Personal | `EmployeeData.cs` + `StaffSystem.cs` |
| I+D | `UpgradeData.cs` + `RnDSystem.cs` |
| AFK | `AFKSystem.cs` |
| Sedes | `VenueData.cs` + `VenueSystem.cs` |
| Eventos | `EventSystem.cs` |
| Cuellos de botella | `BottleneckDetector.cs` |
| Tono gamberro | `MicrotextLibrary.cs` |

---

## ✅ Checklist de Implementación

### Core Systems
- [x] ResourceManager (3 monedas)
- [x] GameEvents (event bus)
- [x] AFKSystem (offline progress)
- [x] GameManager (state controller)
- [x] MicrotextLibrary (flavor text)

### Data Layer
- [x] RecipeData ScriptableObject
- [x] EmployeeData ScriptableObject
- [x] UpgradeData ScriptableObject
- [x] VenueData ScriptableObject
- [ ] EventData ScriptableObject

### UI Systems
- [ ] UICard component
- [ ] ScreenNavigator
- [ ] OverlayManager
- [ ] BadgeController
- [ ] HeaderBar (Efectivo/Fama display)

### Game Systems
- [ ] MenuSystem (recipe management)
- [ ] StaffSystem (employee hiring/fatigue)
- [ ] RnDSystem (tech tree)
- [ ] EconomySystem (revenue calculation)
- [ ] EventSystem (weekly/micro events)
- [ ] VenueSystem (multi-location)
- [ ] BottleneckDetector

### Scenes
- [ ] MainGame scene setup
- [ ] Screen_Inicio prefab
- [ ] Screen_Menu prefab
- [ ] Screen_Personal prefab
- [ ] Screen_RnD prefab

### Visual Assets
- [ ] Hero images (recipes)
- [ ] Employee portraits
- [ ] Upgrade icons
- [ ] Background sprites (urban nocturnal)
- [ ] UI sprites (buttons, badges, overlays)

### Audio
- [ ] Background music (urban ambient)
- [ ] SFX (button clicks, overlays, achievements)

---

**Última actualización:** 2025-10-22  
**Versión:** 1.0 (sincronizada con GDD Vol. 1 + Vol. 2)  
**Mantenedor:** AI Agent + Development Team
