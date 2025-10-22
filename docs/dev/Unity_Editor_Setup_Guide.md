# 🎮 Guía Unity Editor - Setup MainGame Scene

## ✅ Prerrequisitos
- Unity Editor 6.2.8f1 abierto
- Proyecto SiKNess-Tycoon cargado
- Scripts compilados sin errores (comprueba Console)

---

## 📋 PASO 1: Crear Scene MainGame

1. **File → New Scene** (o `Ctrl+N`)
2. Template: **2D (URP)**
3. **File → Save As** → `Assets/Scenes/MainGame.unity`

---

## 📋 PASO 2: Setup Persistent Managers

### 2.1 - Crear GameObject Managers
1. Hierarchy → **Right-click → Create Empty**
2. Rename: `[Persistent]`
3. Reset Transform (Inspector → Transform → ⚙️ → Reset)

### 2.2 - Añadir Scripts Core
**Selecciona `[Persistent]` y añade estos componentes:**

```
[Persistent] GameObject
├─ GameManager (script)
├─ ResourceManager (script)
├─ AFKSystem (script)
└─ EconomySystem (script)
```

**Cómo añadir:**
- Select `[Persistent]`
- Inspector → **Add Component**
- Buscar: `GameManager` → Add
- Repetir para ResourceManager, AFKSystem, EconomySystem

### 2.3 - Configurar Initial Resources
**En ResourceManager component:**
- Starting Efectivo: `500`
- Starting Fama: `0`
- Starting Estrellas: `0`

**En EconomySystem component:**
- Base Ticket Price: `15`
- Base Customers Per Hour: `20`
- Kitchen Speed Multiplier: `1.0`
- Service Speed Multiplier: `1.0`

---

## 📋 PASO 3: Crear UI Canvas

### 3.1 - Crear Canvas Principal
1. Hierarchy → **Right-click → UI → Canvas**
2. Rename: `Canvas_Main`

**Configurar Canvas:**
- Canvas component:
  - Render Mode: **Screen Space - Overlay**
- Canvas Scaler component:
  - UI Scale Mode: **Scale With Screen Size**
  - Reference Resolution: **1080 x 1920** (portrait)
  - Match: **0.5** (width/height)

### 3.2 - Crear Event System
Si no existe automáticamente:
- Hierarchy → **Right-click → UI → Event System**

---

## 📋 PASO 4: Crear Header (Resource Display)

### 4.1 - Crear GameObject Header
1. Select `Canvas_Main`
2. Right-click → **UI → Panel**
3. Rename: `Header`

**Configurar RectTransform de Header:**
- Anchor Preset: **Top Stretch** (⚓ top-center)
- Pos Y: `0`
- Height: `120`
- Left: `0`, Right: `0`

### 4.2 - Crear Efectivo Text
1. Select `Header`
2. Right-click → **UI → Text - TextMeshPro**
3. Rename: `EfectivoText`

**Configurar:**
- RectTransform:
  - Anchor: **Left** (⚓ left-middle)
  - Pos X: `100`, Pos Y: `0`
  - Width: `200`, Height: `60`
- TextMeshProUGUI component:
  - Text: `500€`
  - Font Size: `36`
  - Alignment: **Left & Middle**
  - Color: **White**

### 4.3 - Crear Fama Text
1. Select `Header`
2. Right-click → **UI → Text - TextMeshPro**
3. Rename: `FamaText`

**Configurar:**
- RectTransform:
  - Anchor: **Right** (⚓ right-middle)
  - Pos X: `-100`, Pos Y: `0`
  - Width: `200`, Height: `60`
- TextMeshProUGUI:
  - Text: `0⭐`
  - Font Size: `36`
  - Alignment: **Right & Middle**
  - Color: **#FFD700** (gold)

### 4.4 - Añadir ResourceDisplay Script
1. Select `Header`
2. Inspector → **Add Component** → `ResourceDisplay`
3. Drag references:
   - Efectivo Text: arrastrar `EfectivoText` (hijo)
   - Fama Text: arrastrar `FamaText` (hijo)

---

## 📋 PASO 5: Crear Navigation Tabs

### 5.1 - Crear Panel Navigation
1. Select `Canvas_Main`
2. Right-click → **UI → Panel**
3. Rename: `Navigation`

**Configurar RectTransform:**
- Anchor: **Bottom Stretch** (⚓ bottom-center)
- Pos Y: `0`
- Height: `100`
- Left: `0`, Right: `0`

### 5.2 - Crear 4 Botones de Tab
**Para cada tab (repetir 4 veces):**

1. Select `Navigation`
2. Right-click → **UI → Button - TextMeshPro**
3. Rename: `Tab_Inicio` (luego Tab_Menu, Tab_Personal, Tab_RnD)

**Configurar RectTransform (para distribuir uniformemente):**
- Tab_Inicio: Left `0`, Right `810`, Width `270`, Height `80`
- Tab_Menu: Left `270`, Right `540`, Width `270`, Height `80`
- Tab_Personal: Left `540`, Right `270`, Width `270`, Height `80`
- Tab_RnD: Left `810`, Right `0`, Width `270`, Height `80`

**Configurar cada Button:**
- Button component:
  - Target Graphic: su `Image` hijo
  - Normal Color: `#C2B8A3` (Luz de Faro)
  - Highlighted Color: `#FF5733` (Salsa Brava)
  - Selected Color: `#FF5733`
- Text hijo:
  - Text: `Inicio`, `Menú`, `Personal`, `I+D` (según corresponda)
  - Font Size: `28`
  - Alignment: **Center & Middle**

---

## 📋 PASO 6: Crear Screens Container

### 6.1 - Crear Panel Screens
1. Select `Canvas_Main`
2. Right-click → **UI → Panel**
3. Rename: `Screens`

**Configurar RectTransform:**
- Anchor: **Stretch All** (⚓ full screen)
- Left: `0`, Top: `-120` (debajo del header)
- Right: `0`, Bottom: `100` (encima de navigation)

**Configurar Image:**
- Color: **Transparent** (Alpha = 0) o quitar component

### 6.2 - Crear 4 Screens
**Para cada screen:**

1. Select `Screens`
2. Right-click → **UI → Panel**
3. Rename: `Screen_Inicio` (luego Screen_Menu, Screen_Personal, Screen_RnD)

**Configurar cada Screen:**
- RectTransform: **Stretch All** (fill parent)
- Left: `0`, Top: `0`, Right: `0`, Bottom: `0`
- Image component: Background color o transparent

**Desactivar 3 screens:**
- Select `Screen_Menu` → Inspector checkbox (arriba a la izquierda): **OFF**
- Select `Screen_Personal` → **OFF**
- Select `Screen_RnD` → **OFF**
- Solo `Screen_Inicio` debe estar **ON**

---

## 📋 PASO 7: Setup Screen_Inicio Content

### 7.1 - Crear Card Estado del Turno
1. Select `Screen_Inicio`
2. Right-click → **UI → Panel**
3. Rename: `Card_EstadoTurno`

**Configurar RectTransform:**
- Anchor: **Top Center** (⚓)
- Pos X: `0`, Pos Y: `-200`
- Width: `900`, Height: `300`

**Configurar Image:**
- Color: `#1A1F2E` (Medianoche Terraza)
- Material: (ninguno por ahora)

### 7.2 - Añadir UICard Component
1. Select `Card_EstadoTurno`
2. Inspector → **Add Component** → `UICard`

### 7.3 - Crear Content de Card_EstadoTurno
**Title Text:**
1. Select `Card_EstadoTurno`
2. Right-click → **UI → Text - TextMeshPro**
3. Rename: `TitleText`
4. Configure:
   - RectTransform: Top Center, Width `800`, Height `80`, Pos Y `-40`
   - Text: `La cocina va a pedales`
   - Font Size: `42`
   - Alignment: Center & Middle
   - Color: `#FF5733` (Salsa Brava)

**Description Text:**
1. Select `Card_EstadoTurno`
2. Right-click → **UI → Text - TextMeshPro**
3. Rename: `DescriptionText`
4. Configure:
   - RectTransform: Center, Width `800`, Height `150`, Pos Y `-50`
   - Text: `🔥 Velocidad: 80%\n👥 Empleados: 2/5`
   - Font Size: `32`
   - Alignment: Left & Middle
   - Color: White

### 7.4 - Drag References en UICard
1. Select `Card_EstadoTurno`
2. Inspector → UICard component:
   - Hero Image: (ninguno)
   - Title Text: drag `TitleText`
   - Description Text: drag `DescriptionText`
   - Action Button: (ninguno)

### 7.5 - Crear Card Acción Sugerida
**Repetir proceso similar para `Card_AccionSugerida`:**

1. Create Panel hijo de `Screen_Inicio`
2. Rename: `Card_AccionSugerida`
3. RectTransform: Top Center, Pos Y `-600`, Width `900`, Height `400`
4. Add Component: `UICard`
5. Crear hijos:
   - `TitleText`: "Mejorar Velocidad Cocina"
   - `DescriptionText`: "+ 20% velocidad de preparación"
   - `ActionButton`: Button con texto "Mejorar ahora (500€)"
6. Drag references en UICard component

**ActionButton setup:**
- RectTransform: Bottom Center, Pos Y `50`, Width `600`, Height `80`
- Button colors:
  - Normal: `#FF5733` (Salsa Brava)
  - Highlighted: `#FF6B47`
  - Pressed: `#E74C3C`
- Text: `Mejorar ahora (500€)`, Font Size `32`

### 7.6 - Añadir InicioScreen Component
1. Select `Screen_Inicio`
2. Inspector → **Add Component** → `InicioScreen`
3. Drag references:
   - Estado Card: drag `Card_EstadoTurno`
   - Accion Card: drag `Card_AccionSugerida`

---

## 📋 PASO 8: Crear AFKOverlay

### 8.1 - Crear Overlay GameObject
1. Select `Canvas_Main`
2. Right-click → **UI → Panel**
3. Rename: `Overlay_AFK`

**Configurar RectTransform:**
- Anchor: **Stretch All** (full screen)
- Left `0`, Top `0`, Right `0`, Bottom `0`

**Configurar Image (Dimmer):**
- Color: Black `#000000`
- Alpha: `180` (semi-transparent)

### 8.2 - Crear Panel Content
1. Select `Overlay_AFK`
2. Right-click → **UI → Panel**
3. Rename: `Panel`

**Configurar:**
- RectTransform: Center, Width `800`, Height `600`
- Image Color: `#C2B8A3` (Luz de Faro)

### 8.3 - Añadir Content al Panel
**Crear estos Text hijos de `Panel`:**

**TitleText:**
- Text: `¡De vuelta!`
- Font Size: `56`
- Color: `#FF5733`
- Position: Top Center, Y `-80`

**FlavorText:**
- Text: `Tu gente ha tirado del carro 💪`
- Font Size: `36`
- Color: Black
- Position: Center, Y `50`

**RewardText:**
- Text: `+ 2,450€ (8h)`
- Font Size: `48`
- Color: `#4CAF50` (Albahaca)
- Position: Center, Y `-50`

**Button_Recaudar:**
- Button hijo de Panel
- RectTransform: Bottom Center, Y `80`, Width `500`, Height `100`
- Text: `Recaudar`
- Button colors: Salsa Brava theme

### 8.4 - Añadir AFKOverlay Component
1. Select `Overlay_AFK`
2. Inspector → **Add Component** → `AFKOverlay`
3. Drag references:
   - Panel: drag hijo `Panel`
   - Dimmer: drag el Image del propio `Overlay_AFK`
   - Title Text: drag `TitleText`
   - Flavor Text: drag `FlavorText`
   - Reward Text: drag `RewardText`
   - Recaudar Button: drag `Button_Recaudar`

### 8.5 - Desactivar Overlay
- Select `Overlay_AFK`
- Inspector checkbox (arriba): **OFF**
- (Se activará automáticamente cuando haya AFK reward)

---

## 📋 PASO 9: Conectar ScreenNavigator

1. Select `Canvas_Main`
2. Inspector → **Add Component** → `ScreenNavigator`
3. Drag references:
   - **Screens:**
     - Screen Inicio: drag `Screen_Inicio`
     - Screen Menu: drag `Screen_Menu`
     - Screen Personal: drag `Screen_Personal`
     - Screen RnD: drag `Screen_RnD`
   - **Tab Buttons:**
     - Tab Inicio: drag `Tab_Inicio` (button component)
     - Tab Menu: drag `Tab_Menu`
     - Tab Personal: drag `Tab_Personal`
     - Tab RnD: drag `Tab_RnD`
4. Configure colors:
   - Tab Active Color: `#FF5733` (Salsa Brava)
   - Tab Inactive Color: `#C2B8A3` (Luz de Faro)

---

## 📋 PASO 10: Configurar OnClick Events

### 10.1 - Tab Buttons Navigation
**Para cada Tab Button:**
1. Select `Tab_Inicio`
2. Inspector → Button component → **OnClick()**
3. Click `+` para añadir evento
4. Drag `Canvas_Main` (que tiene ScreenNavigator) al campo Object
5. Dropdown: `ScreenNavigator → ShowInicio()`

**Repetir para otros tabs:**
- `Tab_Menu` → `ShowMenu()`
- `Tab_Personal` → `ShowPersonal()`
- `Tab_RnD` → `ShowRnD()`

### 10.2 - Action Button en Card
1. Select `Card_AccionSugerida` → hijo `ActionButton`
2. Inspector → Button component → **OnClick()**
3. Click `+`
4. **DEJAR VACÍO** (lo maneja InicioScreen.cs en código)

---

## 📋 PASO 11: Crear Background (Opcional)

### 11.1 - Placeholder Background
1. Hierarchy → Right-click → **2D Object → Sprite**
2. Rename: `BG_TerrazaNoche`
3. Inspector → Sprite Renderer:
   - Sprite: (ninguno por ahora)
   - Color: `#1A1F2E` (Medianoche Terraza)
4. Transform:
   - Position: `0, 0, 10` (Z atrás del Canvas)
   - Scale: `15, 20, 1` (cubrir viewport)

---

## 📋 PASO 12: Save & Test

### 12.1 - Guardar Scene
- `Ctrl+S` o **File → Save**

### 12.2 - Set as Active Scene
- **File → Build Settings**
- Drag `MainGame` a "Scenes In Build"
- Cerrar ventana

### 12.3 - Test Play Mode
1. Click **▶️ Play** (o `Ctrl+P`)
2. **Verificar Console:**
   - "GameManager initialized" ✅
   - "ResourceManager initialized" ✅
   - No errores rojos ❌

3. **Probar funcionalidad:**
   - Header muestra `500€` y `0⭐` ✅
   - Tabs cambian de color al hover ✅
   - Click Tab_Menu → Screen_Menu se activa ✅
   - Click "Mejorar ahora" → Efectivo baja a `0€` ✅

### 12.4 - Test AFK System
1. **Play Mode activo**
2. Console → buscar `[Persistent]` object
3. Inspector → AFKSystem component
4. Right-click → **Context Menu → Debug: Simulate 4h AFK**
5. Click OK en dialog
6. **Stop Play Mode** (⏹️)
7. **Play de nuevo** (▶️)
8. Debería aparecer `Overlay_AFK` con recompensa ✅

---

## ✅ Checklist Final

- [ ] Scene `MainGame.unity` creada y guardada
- [ ] `[Persistent]` con 4 managers (GameManager, ResourceManager, AFKSystem, EconomySystem)
- [ ] Canvas con Header (ResourceDisplay funcional)
- [ ] Navigation con 4 tabs (botones conectados)
- [ ] `Screen_Inicio` con 2 cards (Estado + Acción)
- [ ] `Screen_Inicio` tiene InicioScreen component con references
- [ ] `Overlay_AFK` creado y configurado (desactivado por defecto)
- [ ] ScreenNavigator en Canvas con todas las references
- [ ] Play Mode sin errores en Console
- [ ] Botón "Mejorar ahora" funciona (gasta Efectivo)
- [ ] Tabs cambian screens correctamente
- [ ] AFK overlay aparece después de simular AFK

---

## 🆘 Troubleshooting

### Problema: "NullReferenceException" en Console
**Solución:**
- Verificar que TODAS las references en components están asignadas
- Check: ResourceDisplay, InicioScreen, ScreenNavigator, AFKOverlay
- Si falta alguna, drag & drop el GameObject/Component correspondiente

### Problema: Botón "Mejorar ahora" no funciona
**Solución:**
- Verificar que `InicioScreen` component tiene la reference `accionCard` asignada
- El código ya maneja el OnClick, NO configurar manualmente en Inspector

### Problema: Overlay AFK no aparece
**Solución:**
- Verificar que `Overlay_AFK` está desactivado al inicio
- En GameManager, revisar método `CheckAFKReward()` (debe buscar AFKOverlay)
- Context Menu → Debug: Simulate 4h AFK → Restart Play Mode

### Problema: Tabs no cambian screens
**Solución:**
- Verificar OnClick events de cada Tab Button
- Asegurarse de que apuntan a `Canvas_Main` (ScreenNavigator component)
- Dropdown debe mostrar `ShowInicio()`, `ShowMenu()`, etc.

### Problema: ResourceDisplay no actualiza valores
**Solución:**
- Play Mode → Inspector → Header object → ResourceDisplay component
- Check references: Efectivo Text y Fama Text deben estar asignadas
- Context Menu en ResourceDisplay → Test: Add 1000 Efectivo

---

## 🎯 Siguiente Paso

**Una vez completado esto, estarás listo para:**
1. Crear prefabs reutilizables (UICard_Generic.prefab)
2. Añadir ScriptableObjects de ejemplo (Bravas.asset, Lola.asset)
3. Implementar arte placeholder (sprites colored squares)
4. Build WebGL demo

**¿Necesitas ayuda con algún paso específico?** Ping me! 🚀
