# 🎮 **SiKNess Tycoon** — Game Design Document (v0.2)

> Nombre definitivo del juego aplicado.


## 1. Concepto General
**Elevator pitch:** Crea y gestiona tu propio bar y conviértelo en una franquicia. Optimiza personal, recetas y horarios, incluso mientras estás offline. Estilo casual, humor ligero, progreso honesto AFK.

**Género:** Idle Tycoon (gestión simplificada)
**Estilo:** Casual caricaturesco
**Protagonista:** Gestor/emprendedor
**Plataformas:** Mobile (principal) + PC (port UI expandido)

---

## 2. Fantasía del Jugador
El jugador no "cocina" platos, **dirige y automatiza** un restaurante. Su fantasía es ver crecer algo pequeño hasta convertirse en una red de franquicias reconocible.

---

## 3. Monedas y Recursos
| Recurso | Uso | Fuente |
|--------|------|--------|
| **Efectivo** | Mejoras inmediatas | Ventas online/AFK |
| **Fama** | Desbloqueos por sede | Servicio y satisfacción |
| **Estrellas** | Metaprogreso (prestigio) | Objetivos / Evento |
| **AFK** | Tiempo offline recuperable | Jornada offline |

---

## 4. Pantallas Principales
### **Barra inferior (4 secciones)**
1. **Inicio (Servicio)** — núcleo del juego
2. **Menú (Recetas)** — ticket medio y variedad
3. **Personal** — contratación/rasgos
4. **I+D** — mejoras globales

### **Menú Lateral (Burger)**
- Sedes
- Eventos
- Tienda
- Informe
- Ajustes

---

## 5. Pantalla Inicio (Servicio)
Elementos clave:
- Header compacto (Efectivo / Fama / AFK listo)
- Card "Estado del turno": clientes/h, ticket medio, satisfacción, **cuello dominante**
- Botón **Mejorar ahora** (acción sugerida)
- Tarjetas de área: Cocina / Servicio / Caja
- Objetivos diarios (3)

---

## 6. Pantalla Menú (Recetas)
- Tabs: Entrantes / Principales / Postres / Bebidas
- Cada receta: nivel, precio, tiempo, popularidad (☀️/🌙), sinergia, CTA "Mejorar"
- Slot táctico: **Especial del día**

---

## 7. Pantalla Personal
- Tabs: Cocina / Sala
- Rasgos por empleado
- Fatiga ligera
- Botón global: **Optimizar turnos**

---

## 8. Pantalla I+D
Lista plana en 3 columnas:
- Cocina
- Servicio
- Marketing

Cada nodo = coste + bonificación clara (sin ramas complejas)

---

## 9. Sedes (Progresión)
- Bar del barrio → futuras sedes (Bistró / Food Court / Aeropuerto)
- Cada sede tiene modificadores propios

---

## 10. Eventos Semanales
Un único modificador simple. Ej:
**Semana de la Pasta**: +20% demanda Spaghetti

---

## 11. Contenido MVP
### Recetas Iniciales
**Entrantes:** Ensaladilla, Bravas, Croquetas
**Principales:** Spaghetti, Pollo plancha, Hamburguesa
**Postres:** Flan, Helado

### Personal Inicial
**Cocina:** Lola (Precisa), Rafa (Rápido), Marta (Ordenada)
**Sala:** Lía (Rápida), Paco (Simpatía), Zoe (Filas)

### I+D inicial
- Cocina: placas, mise en place, almacén
- Servicio: rutina sala, TPV ágil, señalética
- Marketing: carteles, menú del día, fidelidad

---

## 12. Objetivos Diarios
Pool inicial de 6 objetivos, se muestran 3 por día.

---

## 13. Tono y Microtextos (Ejemplos)
- "La cocina va a pedales, mete chispa 🔥"
- "Lía está on fire, la sala vuela"
- "Tu bar curró por ti 7h12m, recauda ya 💰"

---

## 14. Regla de Oro del Diseño
1 cuello → 1 acción sugerida → 1 mejora útil por sesión.

---

## 15. Próximos Pasos (para la versión 0.2 del GDD)
- Añadir 24 recetas extra y 12 empleados más
- Definir detalle de sedes futuras y modificadores
- Crear pool extendido de eventos
- Describir onboarding Día 0 - Día 3

## 16. Sprites y UI mínima requerida
- Botón principal (Mejorar ahora)
- Tabs inferiores x4 (Inicio / Menú / Personal / I+D)
- Iconos de cuello (cocina/servicio/caja)
- Iconos de moneda (Efectivo / Fama / Estrellas)
- Card recetas (con marco común)
- Card empleados (con avatar)
- Icono "Especial del día"
- Popups objetivos y recompensas

## 17. Próximos bloques de contenido (preproducción)
- 24 recetas adicionales (4 categorías)
- 12 empleados adicionales
- 3 sedes futuras detalladas
- 10 eventos rotativos
- Objetivos diarios ampliados (20)
- Onboarding Día 0–3 guiado

## 18. Estilo visual elegido
- **Estilo:** Cartoon 2D con outlines
- **Motivación:** Mayor personalidad en empleados, recetas y eventos; mejor legibilidad móvil; estilo casual encaja con marca "SiKNess".

## 19. Línea artística base (pre-wireframe)
- Personajes con expresiones simples (feliz / neutro / cansado)
- Objetos y comida con outline negro fino (1-2 px)
- Colores planos con sombras suaves (no rendering complejo)
- Animaciones opcionales futuras, no requeridas en MVP

## 20. Formato UI definitivo
- **Cards como elemento base** para recetas, personal y mejoras.
- Justificación: Reforzar sensación de coleccionables, mayor engagement visual, encaja con cartoon 2D.
- Listas solo para menús secundarios (informes / logs / tienda avanzada).

## 21. Proporciones de Cards
Se utilizarán **3 proporciones según el tipo de elemento**:
- **Personajes / Personal:** Vertical 2:3 → mayor presencia y apego visual
- **Recetas / Ingredientes:** Cuadrada 1:1 → coherente con catálogo y rotaciones
- **Mejoras / I+D / Sedes:** Horizontal 3:2 → sensación de panel/gestión

Justificación: Permite transmitir rol visual distinto sin complejidad extra para el jugador.

## 22. Estilo de Bordes
- **Bordes redondeados** en toda la UI (8–16px según tamaño del elemento)
- Refuerza tono cartoon amable, legible en móvil
- Coherente con estética "coleccionable" y cards

## 23. Tono de Humor
- **Gamberro ligero (con chispa)**
- Microtextos con picardía pero sin vulgaridad
- Refuerza identidad SiKNess como marca con personalidad
- Ejemplo de tono:
  - "La cocina va a pedales, mete chispa o se duerme el barrio 🤨"
  - "Ese camarero va en modo tortuga... ¿le damos marcha o qué?"

## 24. Wireframes - Configuración
- Los wireframes de la v0.3 usarán **TEXTOS REALES**, no placeholders.
- Los microtextos reflejarán tono gamberro ligero.
- Esto definirá ya la voz del juego desde el diseño, no como añadido posterior.

## 25. Formato de Wireframes
- Se usarán **wireframes EXPLICADOS** (modo B)
- Incluyen: estructura + texto final + notas de intención
- Ideal para preproducción y trabajo posterior con IA para los assets

## 26. Paleta cromática base
- Estilo: **Vibrante** (alto contraste, colores vivos)
- Justificación: refuerza tono cartoon + gamberro, destaca en móvil y capturas promocionales
- Uso previsto:
  - Colores cálidos en cocina (rojos / naranjas)
  - Colores vivos en servicio (amarillos / verdes)
  - Acentos en Fama / Estrellas (azules / dorados)
  - Fondos luminosos pero no planos, con sutil gradiente o texturas suaves

## 27. Plan de entrega de la versión v0.3
- Método elegido: **C) Mixto**
- Orden de despliegue en el GDD:
  1. **Wireframes + Guía visual completa** (próxima actualización)
  2. Tras validación → **Contenido ampliado** (recetas, personal, sedes, eventos, objetivos)
  3. Finalmente → **Onboarding Día 0–3**

Este método permite corregir UI antes de producir el contenido definitivo.

## 28. Navegación del Menú Lateral
- **Método elegido:** C) Gesto + icono discreto
- El menú lateral se abrirá deslizando desde el lado izquierdo
- El icono burger seguirá presente para señal visual (esquina superior izquierda), pero el **gesto será prioritario**
- Refuerza sensación moderna, fluida y accesible en móviles idle

## 29. Niveles de detalle para wireframes
- **Modo elegido:** B) Sin números
- Los wireframes mostrarán textos y estructura, pero sin cifras
- Justificación: economía y balance se definirán después, evitando anclar tuning prematuro

## 30. Layout persistente
- Header y barra inferior serán **persistentes** en todas las pantallas principales
- Refuerza identidad idle: progreso siempre visible y accesible
- Las pantallas secundarias (tienda / informe / ajustes) pueden usar overlay

## 31. Orden de documentación de wireframes
- **Orden elegido:** B) Jerarquía UI (diseñador)
- Secuencia de descripción en v0.3 Etapa 1:
  1. Header
  2. Footer / barra inferior
  3. Pantalla Inicio / Servicio
  4. Pantalla Menú (Recetas)
  5. Pantalla Personal
  6. Pantalla I+D
  7. Menú lateral y navegación secundaria (Sedes / Eventos / Tienda / Informe)
  8. Overlays / popups básicos (objetivos, recompensas, AFK)

## 32. Transiciones UI
- Navegación entre pestañas principales (**Inicio / Menú / Personal / I+D**): **B) Desplazamiento lateral tipo slide** → sensación de "app fluida".
- Acciones sobre elementos individuales (ej. **“Mejorar ahora”**, abrir card de receta/personal): **C) Expansión tipo card (zoom-in)** → refuerza sensación coleccionable.
- Coherencia UX: desplazamiento = cambio de pantalla; expansión = foco en elemento.

## 33. AFK - Ubicación y Comportamiento
- AFK se mostrará como **botón flotante** sólo cuando haya recompensa disponible
- Refuerza "sensación de premio" y hace espacio en el header visual
- También mantiene consistencia con futuros flotantes (eventos / boosts temporales)

## 34. Distribución del Header
- **Diseño elegido: D**
- Efectivo = elemento principal (izquierda, tamaño mayor)
- Fama = badge menor al lado, refuerzo de progreso
- Icono de información contextual a la derecha (inspector breve)
- AFK queda flotante fuera del header cuando está disponible

## 35. Nivel de detalle para wireframes
- Se utilizará **nivel 2 (medio)**: explicación de intención + motivo UX
- No se documentan alternativas descartadas dentro del wireframe (para evitar ruido visual)
- Foco en claridad de diseño y transmisión de propósito al jugador

## 36. Tipografía base
- Estilo elegido: **A) Redondeada / Amigable**
- Justificación: alta legibilidad en móvil + refuerzo del tono cartoon casual
- Uso: titulares + UI general (permite variantes bold para feedback)

## 37. Grid y Spacing
- Estilo elegido: **B) UI que respira**
- Más espacio entre elementos, lectura más limpia
- Refuerza aspecto cartoon y jerarquía visual
- Cards mejor diferenciadas, sensación "coleccionable"

## 38. Fondo y contraste
- Fondo general elegido: **Oscuro** (gris carbón / azul noche híbrido)
- Justificación:
  - Resalta colores vibrantes
  - Mayor contraste para iconos y cards
  - Refuerza la marca SiKness como "más gamberro" que infantil
- Los elementos principales (cards / botones) usarán fondos claros o medianos sobre fondo oscuro

## 39. Estilo del Fondo Oscuro
- Tipo elegido: **C) Textura sutil**
- Justificación:
  - Añade “sensación de local / ambiente real” sin ruido visual
  - Refuerza identidad de restaurante frente a look puramente app
  - Separa visualmente el mundo del jugador del puro panel informativo
- Nivel de textura: bajo contraste, patrón suave (muro/tela)

## 40. Estilo de las Cards
- Estilo elegido: **A) Ilustración grande (hero visual)**
- La imagen será el foco principal en cada card
- El texto actúa como soporte (nombre + rasgo / stats clave)
- Refuerza sensación coleccionable y cartoon vibrante
- Especialmente útil para recetas y personal

## 41. Tamaño de ilustración en cards
- Proporción elegida: **C) ~60%** de la card
- Imagen grande, pero deja espacio suficiente para texto y rasgos
- Equilibrio entre aspecto coleccionable y legibilidad
- Recomendado para móvil en formato vertical

## 42. Estilo de redacción para Guía Visual
- Estilo elegido: **B) Diseñador pedagógico**
- La guía visualizará intención + sensación, no solo reglas frías
- Sirve también como prompt base para generación IA posterior
- Apoya coherencia estética a largo plazo

## 43. Referencias visuales (textuales)
- Se incluirán **comparaciones de estilo** dentro de la Guía Visual
- Objetivo: facilitar consistencia en generación IA
- Nivel: descripciones textuales (sin imágenes)
- Referencias se usarán para transmitir intención, no copiar estilo

## 44. Outline final
- Estilo elegido: **B) Más grueso en ilustraciones que en UI**
- Justificación:
  - Refuerza separación entre "mundo del juego" (recetas/personajes) y la UI
  - Hace que las cards se sientan más vivas/coleccionables
  - Mantiene interfaz limpia y legible sin ruido

## 45. Nomenclatura cromática
- Estilo elegido: **B) Temática gastronómica**
- Los nombres de color usarán referencias culinarias (ej: "tomate", "albahaca", "plancha", "cobre", "aceituna")
- Mejora la inmersión y sirve como prompt natural para IA
- Refuerza identidad de marca y coherencia de estilo

## 46. Estructura de Guía Visual
- Organización elegida: **B) Subbloques**
- La Guía Visual se documentará en secciones 1.1 / 1.2 / 1.3 etc.
- Mejora escalabilidad y lectura técnica + artística
- Permite actualizaciones futuras sin mezclar capas visuales

## 47. Color Primario
- Color principal: **"Salsa Brava"** (rojo-anaranjado vibrante)
- Justificación:
  - Evoca apetito y energía
  - Encaja con tono gamberro ligero
  - Asociado a tapas / bar / cocina viva

## 48. Ambientación del Fondo Oscuro
- Estilo elegido: **C) Noche urbana / exterior**
- Sensación: terraza nocturna / ciudad viva / franquicia en expansión
- Refuerza el tono vibrante + ligeramente irreverente de SiKNess Tycoon
- Apoya la idea de crecimiento de pequeño bar → marca urbana reconocible

## 49. Tipo de textura urbana
- Estilo elegido: **B) Terraza exterior nocturna**
- Sensación: restaurante vivo, ambiente cálido aunque en contexto urbano nocturno
- Soporte visual: madera/piedra suave + iluminación ambiente tenue
- Crea contraste acogedor frente a ciudad, sin perder vibra “franquicia moderna”

## 50. Formato de presentación de la paleta
- Estilo elegido: **C) Mixto (tabla + explicación)**
- Tabla para claridad técnica + descripción narrativa para tono e IA
- Facilita exportar como prompt y documentación visual coherente

## 51. Matices luminosos
- Opción elegida: **A) Sí, incluir versiones luminosas**
- Los colores tendrán variantes "highlight" para hover / feedback
- Esto da sensación de UI viva sin animaciones complejas
- Apoya look cartoon vibrante y navegación responsiva

*Fin del documento v0.3-visual-ready-to-write*


## 1. Guía Visual

### 1.1 Paleta Cromática Base
| Rol | Nombre Temático | Descripción | Uso Principal |
|-----|------------------|-------------|---------------|
| Primario | **Salsa Brava** | Rojo-anaranjado vibrante, tono picante y enérgico | Botones principales, acentos clave, highlights positivos |
| Secundario 1 | **Albahaca Fresca** | Verde vivo brillante | Confirmaciones suaves, detalles en cards cooperativas ||
| Secundario 2 | **Aceituna Negra** | Verde oscuro grisáceo | Separadores, bordes UI sutiles, texto secundario |
| Fondo Base | **Medianoche Terraza** | Azul carbón nocturno con matiz cálido | Fondo principal del juego |
| Fondo Elevado | **Luz de Faro** | Gris cálido medio translúcido | Superficie base de cards y overlays |

**Intención artística:**
El uso de nombres culinarios tematiza el tono general y crea coherencia con el universo del juego. "Salsa Brava" transmite energía y acción; "Albahaca Fresca" refuerza vitalidad; "Medianoche Terraza" fija el ambiente urbano-acogedor; “Luz de Faro” crea sensación de foco visual sobre el contenido.

### 1.2 Paleta de Estados / Feedback
| Estado | Nombre Temático | Descripción | Uso |
|--------|------------------|-------------|-----|
| Éxito | **Aceite de Oliva** | Dorado suave y positivo | Subidas de nivel, mejoras completadas |
| Alerta leve | **Guindilla Suave** | Rojo tenue | Advertencias no críticas |
| Alerta fuerte | **Guindilla Picante** | Rojo intenso | Cuellos urgentes, bajada de satisfacción |
| Evento | **Neón Habanero** | Rosa/rojo brillante | Llamadas de evento semanal |
| Cooldown | **Hielo Cítrico** | Azul claro desaturado | Tiempos de espera / rasgos en descanso |

**Intención UX:** los jugadores deben identificar estados sin leer texto; la cromática lleva la emoción por delante.

### 1.3 Jerarquía de Uso
1. **Fondo (Medianoche Terraza)**: crea ambiente y estabilidad
2. **Cards (Luz de Faro)**: deben "salir" visualmente del fondo
3. **Acciones clave (Salsa Brava)**: siempre llamativas
4. **Éxito y progreso (Aceite de Oliva)**: sensaciones positivas
5. **Advertencias (Guindilla)**: ligeras → fuertes según necesidad
6. **Decoradores / ambiente (Albahaca / Aceituna)**: capas secundarias

La jerarquía asegura que **primero se ve acción, luego progreso, luego ambiente**, manteniendo UX clara.

---

## 2. Fondo y Textura
- **Estilo:** Terraza exterior nocturna (acogedor pero urbano)
- **Material visual:** mezcla suave de muro + madera/piedra ligera
- **Profundidad:** baja, sin ruido, textura sutil (no patrón agresivo)
- **Luz ambiente:** sugerencia cálida (farolillos/terraza)

**Efecto emocional:**
La base oscura simula el “mundo” en el que existe el restaurante, mientras las cards funcionan como "islas de luz" donde suceden las decisiones. Da calidez sin perder modernidad.

---

## 3. Outline y Profundidad
### 3.1 Jerarquía de contorno
| Elemento | Grosor | Motivo |
|----------|---------|--------|
| Personajes / Recetas (arte) | **grueso** | refuerza aspecto coleccionable, más presencia |
| UI (botones / cards / ventanas) | **fino** | mantiene interfaz legible y elegante |

### 3.2 Sombras
- Sombra cartoon suave, difusa
- Luz siempre "desde arriba" metáfora de foco en terraza
- Eleva cards sobre fondo sin romper lectura

### 3.3 Elevación
- **Cards principales**: elevación media
- **Popups**: elevación alta
- **Botones**: micro-elevación al hover/click (sensación táctil)

**Resultado emocional:** se percibe que el ARTE es el protagonista y la UI es la "bandeja" donde ocurre la acción.

---

## 52. Radio de Bordes en Cards
- Estilo elegido: **A) Radio medio (8–12px)**
- Transmite suavidad sin llegar al estilo "pastilla"
- Coherente con tono cartoon pero manteniendo elegancia visual
- Facilita lectura en cards con hero image (60% espacio)

*Fin de v0.3 Etapa 1 — Parte 1 (Guía Visual: Paleta + Fondo + Outline + Bordes)**

## 53. Botón Primario
- Estilo elegido: **B) Ligero relieve cartoon**
- Sensación táctil sin caer en efecto "stickers"
- Acompañado de highlight en hover/touch
- Refuerza interacción rápida + refuerzo visual de acción principal

*Con esto queda cerrada la base para el UI Kit. Próxima actualización: v0.3 Etapa 1 — Parte 2 (Tipografía + Spacing + Componentes base).*

## 54. Tipografía (familias)
- Estilo elegido: **B) Dos familias**
- Títulos: más expresivos / cartoon ligero (refuerza humor y personalidad)
- Cuerpo y UI: redondeada y muy legible (refuerza claridad en gestión)
- Justificación: mejora identidad visual sin sacrificar accesibilidad

## 55. Estilo de Títulos (personalidad)
- Estilo elegido: **B) Urbana**
- Referencia estética: señalética moderna / foodtruck / franquicia joven
- Refuerza el concepto de expansión y aire callejero nocturno
- Combina con el fondo "terraza urbana" y paleta vibrante

## 56. Jerarquía de tamaño de títulos
- Estilo elegido: **A) Grande (hero)**
- Tamaño equivalente: 35–48px según contexto
- Objetivo: lectura rápida + impacto cartoon
- Reforzará identidad y peso visual del contenido destacado

## 57. Microinteracción táctil
- Estilo elegido: **A) Rebote ligero (squash/stretch)**
- Aplicado a: botones primarios, cards, confirmaciones visuales
- Intensidad controlada: sensación viva sin excesos
- Refuerzo cartoon + satisfacción al toque

## 58. Prompt base de UI (ubicación)
- Opción elegida: **A) Incluir el prompt al final del UI Kit**
- Justificación: accesibilidad inmediata durante producción de assets
- El prompt servirá como plantilla generativa coherente con la guía visual

*Listo para comenzar la inserción del UI KIT completo en el siguiente bloque.***

## 59. Modo de inserción del UI Kit
- Opción elegida: **B) Dividido en 2 bloques**
  - Bloque 1: Tipografía + Spacing + Botones
  - Bloque 2: Cards + Badges + Overlays + Tabs + Prompt IA
- Justificación: facilita revisión y ajustes incrementales

*Próxima actualización: v0.3 Etapa 1 — Parte 2 (UI Kit – Bloque 1).*

## 60. Tipografía responsiva
- Estilo elegido: **A) Fluida**
- Los tamaños se adaptarán a la resolución del dispositivo
- Beneficio: mejora legibilidad en pantallas pequeñas sin romper jerarquía visual
- Se aplicará especialmente a títulos hero y textos largos en cards

## 61. Feedback visual de botones (press)
- Estilo elegido: **C) Secuencia oscurecer → highlight**
- Golpe visual breve: confirma acción táctil
- Consistente con rebote cartoon
- Intensidad moderada para no saturar

*UI Kit listo para inserción — Bloque 1 comenzará en la siguiente actualización.*

## 62. Estructura de encabezado para UI Kit
- Estilo elegido: **A) Subtítulo explícito**
- Cada bloque del UI Kit llevará una cabecera clara para búsqueda rápida
- Facilita mantenimiento y futuras expansiones

*Próxima actualización: inserción del UI KIT — Bloque 1 (Tipografía + Spacing + Botones).*

## 63. Tono textual en UI
- Opción elegida: **A) Incluir tono textual en esta sección**
- La guía tipográfica incorporará ejemplos de voz y estilo
- Esto asegura coherencia antes de los wireframes
- El tono "gamberro ligero" estará reflejado en CTA y microtextos

## 64. Estilo de texto en botones
- Estilo elegido: **B) Frase/Título ("Mejorar ahora")**
- Justificación:
  - Más natural y cercano
  - Evita agresividad visual
  - Mantiene tono gamberro sin gritar visualmente
- MAYÚSCULAS se reservan para llamadas de evento o refuerzo excepcional

## 65. Branding en UI
- Decisión: **B) Sin branding del título en UI in‑game**
- El nombre del juego se reservará para pantalla de título / menú principal
- La UI in‑game será funcional y diegética, no promocional
- Refuerza limpieza y enfoque en jugabilidad

*UI Kit listo para inserción en la próxima actualización.*

## UI KIT — BLOQUE 1 (Tipografía + Spacing + Botones)

### 1. Tipografía y Jerarquía

**1.1 Familias tipográficas**
- **Títulos (Hero / Urbana):** estilo urbano-modern casual, ligeramente informal, con personalidad. Debe sentirse "marca de franquicia joven" y no "texto de oficina".
- **Cuerpo/UI (Redondeada):** muy legible, amable y clara. Redondeada suave, manteniendo tono cartoon accesible.

**1.2 Jerarquía**
| Nivel | Uso | Estilo | Sensación |
|------|-----|--------|-----------|
| H1 | Secciones clave / Headers grandes | Urbana Hero | Impacto + identidad |
| H2 | Subsecciones | Urbana medio | Flujo visual |
| H3 | Etiquetas internas | Redondeada bold | Guía rápida |
| Body | Texto general / descripciones | Redondeada regular | Legibilidad |
| Mini-copy | Notas / tooltips | Redondeada ligera | Soporte visual |

**1.3 Tono en UI (voz gamberra ligera)**
- Frases directas, cercanas y con chispa.
- Sin vulgaridad, pero con actitud.
- Botones hablan "al jugador" y no "sobre el sistema".

**Ejemplos:**
- CTA: "Mejorar ahora" en lugar de "Aplicar mejora"
- Microcopy: "La cocina va a pedales, dale un meneo" → tono dinámico
- Confirmación: "Bien ahí, tu gente se nota más fina ahora"

**1.4 Formato textual**
- Frase/Título (capitalización inicial)
- MAYÚSCULAS solo en eventos, recompensas grandes o logros

---

### 2. Spacing & Grid

**2.1 Filosofía general**
"UI que respira" → menos densidad, más claridad. El contenido debe sentirse colocado, no apretado.

**2.2 Padding / Margen**
| Elemento | Padding | Margen |
|----------|---------|--------|
| Botones | medio | pequeño |
| Cards | medio/grande (más foco en arte) | medio |
| Headers | medio | grande |
| Secciones | medio | grande |

**2.3 Ritmo visual**
- Separaciones consistentes entre bloques
- Alternancia fondo oscuro / cards iluminadas
- Cards = “islas de información”

**Resultado UX:** El jugador identifica secciones de un vistazo sin esfuerzo mental.

---

### 3. Botones

**3.1 Primario (acción decisiva)**
- Color: **Salsa Brava** (primario)
- Forma: rectangular redondeado (radio medio)
- Estilo: ligero relieve cartoon
- Tacto: rebote ligero (squash/stretch)
- Feedback: primero oscurecer → luego highlight breve

**Ejemplo textual:** "Mejorar ahora"

**3.2 Secundario**
- Color: Albahaca Fresca o Luz de Faro
- Uso: acciones complementarias (ver detalle, info adicional)
- Feedback: sutil, sin rebote

**Ejemplo textual:** "Ver detalles"

**3.3 Ghost / Pasivo**
- Contorno fino (Aceituna Negra)
- Fondo transparente
- Se usa cuando la acción no es prioridad

**Ejemplo textual:** "Volver luego"

**3.4 Estados**
| Estado | Apariencia | Intención |
|--------|-------------|-----------|
| Normal | color base cartoon | disposición a actuar |
| Hover/press | oscurece → highlight | confirma que “escucha” |
| Disabled | desaturado / contorno gris tenue | no disponible |

---

*Fin UI KIT — Bloque 1*

## 66. Estilo de elevación de cards
- Estilo elegido: **B) Halo / Glow suave**
- Refuerza estética urbana + vibrante
- Sensación: "iluminadas" sobre fondo nocturno
- Coherente con experiencia mobile moderna

## 67. Forma de badges
- Elección híbrida confirmada:
  - **AFK / Eventos / Alertas → Forma tipo gota/pin (B)**
  - **Contadores / Numeración → Círculo perfecto (A)**
- Motivo: el pin llama atención (evento/estado), el círculo informa (cantidad)
- Se refuerza lectura emocional → primero la señal, después el dato

## 68. Microinteracción en Cards
- Estilo elegido: **A) Micro-tilt al tocar**
- Sensación táctil: ligero giro/inclinación + halo reforzado
- Refuerza percepción coleccionable y jerarquía visual del arte
- Coherente con rebote en botones y estética cartoon urbana

*UI Kit listo para inserción — Bloque 2 comenzará en la próxima actualización.*

## 69. Referencias visuales para cards
- Estilo elegido: **B) Referencias directas**
- Se usará comparación textual a juegos como Clash Royale / AFK Arena para estructura de card coleccionable
- Diferencia clave: atmósfera urbana nocturna + paleta gastronómica vibrante
- Esto mejora precisión al generar arte con IA

*UI KIT — Bloque 2 listo para inserción en la próxima actualización.*

## 70. Orden interno del Bloque 2
- Orden elegido: **A) Cards → Badges → Overlays → Tabs → Prompt IA**
- Justificación: prioriza el elemento visual central del juego (cards)
- Favorece lectura natural antes de elementos secundarios

*Todo preparado para la inserción del UI KIT — Bloque 2 en la próxima actualización.*

## UI KIT — BLOQUE 2 (Cards + Badges + Overlays + Tabs + Prompt IA)

### 4. Cards (estructura y comportamiento)
Las cards son el núcleo visual del juego. Su estilo está inspirado en el lenguaje "coleccionable" tipo **Clash Royale / AFK Arena**, pero con ambientación **urbana nocturna + temática gastronómica**.

#### 4.1 Tipologías por proporción
| Tipo | Proporción | Uso | Enfoque |
|------|-------------|------|---------|
| Personajes | 2:3 vertical | Empleados / staff | refuerzo de identidad y rol |
| Recetas | 1:1 cuadrada | Platos / alimentos | lectura directa visual |
| I+D / Sedes | 3:2 horizontal | Gestión / progreso | sensación "panel" pero cartoon |

#### 4.2 Composición (hero layout)
- La **ilustración ocupa aprox. 60%** de la card (zona superior o central dominante)
- 40% restante reservado a nombre + atributo clave
- Margen respirado para no saturar texto
- Halo suave para resaltar sobre fondo oscuro
- Outline más grueso que la UI (diferencia jerárquica)

#### 4.3 Comportamiento táctil
| Acción | Respuesta | Sensación |
|--------|-----------|-----------|
| Hover/Tap | micro-tilt (ligera inclinación) | coleccionable vivo |
| Press | halo reforzado brevemente | energía visual |
| Selección | realce ligero + badge o borde temático | estado activo |

---

### 5. Badges (notificación y contexto)
Los badges muestran **estado** o **urgencia**, no solo información.

| Tipo | Forma | Uso |
|------|--------|-----|
| Evento / AFK / Alertas | **Gota/pin** | LLAMA atención primero |
| Cantidad / numeración | **Círculo perfecto** | Transmite dato rápido |

**Posicionamiento:**
- Esquinas superiores (dependiendo del tipo de card)
- Nunca sobre el texto: siempre sobre el área visual
- AFK y eventos tienen prioridad sobre numeración

---

### 6. Overlays (popups y confirmaciones)

Los overlays deben sentirse "ligeros" y no invadir la pantalla entera.

**Estilo visual**
- Fondo translúcido suave (Luz de Faro + blur mínimo)
- Bordes redondeados medio
- Halo suave en lugar de sombra física
- Ilustración o icono arriba → texto debajo

**Tono textual**
- Gamberro suave, cercano
- Ej: "Buen chute al local, esto empieza a oler a gloria" en upgrades

**Interacción**
- Un botón primario / uno secundario máximo

---

### 7. Tabs (barra inferior de navegación)

- Persistente en todas las pantallas
- Íconos simplificados (outline fino)
- Glow sutil en la pestaña activa
- Etiqueta corta opcional (solo en primeras sesiones)
- Refuerza bucle: Inicio → Menú → Personal → I+D

---

### 8. Prompt IA (para generación de componentes)
Este prompt se utilizará como plantilla base para generar UI coherente con el estilo visual del juego.

```
UI cartoon urbana, fondo oscuro con textura tipo terraza nocturna, colores vibrantes culinarios (Salsa Brava, Albahaca, Aceituna), halo suave en lugar de sombra, bordes redondeados medios (8–12px), estilo táctil con micro-tilt y rebote ligero, iluminación cálida desde arriba, estética coleccionable inspirada en Clash Royale pero temática restaurante moderno, tipografía urbana en títulos y redondeada en cuerpo.
```

*Fin UI KIT — Bloque 2*

---

*UI KIT FINALIZADO ✅ — Próximo paso: wireframes (Header → Footer → Pantalla Inicio).*

## 71. Icono de menú lateral
- Estilo elegido: **B) Burger cartoon redondeado**
- Motivo: mantiene legibilidad universal pero con personalidad blanda/cartoon
- Se reserva opción C (utensilios) para evolución futura o skins temáticos

*Listo para comenzar con Wireframes — HEADER en la próxima actualización.*

## 72. Alineación de bloque económico en header
- Opción elegida: **A) Efectivo + Fama en una sola línea**
- Motivo: lectura inmediata sin dividir jerarquía visual
- Refuerza simplicidad y evita competir con las cards
- Encaja con tono cartoon + respirado

*Listo para inserción del Wireframe — HEADER en la próxima actualización.*

## 73. Icono de ayuda contextual en header
- Estilo elegido: **B) Interrogación (?)**
- Motivo: más amigable y comunicativo que "i"
- Apoya onboarding y consultas rápidas
- Refuerza tono cercano / cartoon accesible

*Listo para insertar el Wireframe — HEADER en la próxima actualización.*

## WIREFRAME — HEADER

### 1. Objetivo del header
El header permite al jugador ver **de un vistazo** su estado económico y su progreso general (fama), sin distraer ni rivalizar con las cards. Debe ser **ligero**, persistente y siempre legible.

### 2. Estructura visual
```
┌────────────────────────────────────────┐
│  ☰   [ Efectivo ]  [ Fama ⭐ ]         [?] │
└────────────────────────────────────────┘
```
- **☰ (burger cartoon)** → esquina superior izquierda (menú lateral por gesto + icono)
- **Efectivo (grande)** → elemento principal
- **Fama (badge pequeño "⭐")** → junto a efectivo, no compite
- **? (ayuda contextual)** → extremo derecho

### 3. Intención UX
- Mostrar progreso sin bloquear la vista del contenido principal
- Evitar distracciones → sin decoraciones extra
- Económico = primera lectura / Fama = progreso social

### 4. Comportamiento
| Acción | Respuesta |
|--------|-----------|
| Tap en ☰ | Abre menú lateral (slide desde izquierda) |
| Tap en efectivo | (en futuro) abre desglose de economía |
| Tap en fama | (en futuro) abre sección de reputación/bonificaciones |
| Tap en ❓ | Explica brevemente los elementos del header |

### 5. Microtextos (tono real)
- Tooltips ejemplo:
  - Efectivo: “Tu caja: lo que manda en la cocina.”
  - Fama: “Lo que dice la gente de tu chiringuito.”
  - Ayuda ❓: “¿Perdido? Tranqui, que esto tiene menos misterio que un bocata jamón.”

### 6. Animaciones sutiles
- Aparición con leve fade + elevación inicial al cargar
- Sin rebotes (para no competir con cards)
- Efectivo puede pulsat ligeramente al recibir grandes cobros (evento futuro)

### 7. Relación con AFK
- AFK aparece **como badge flotante independiente**, nunca dentro del header
- Evita saturación visual y refuerza que el AFK es "evento-premio"

### 8. Estilo gráfico aplicado
- Fondo: transparente / sin caja contenedora
- Texto en color claro sobre fondo oscuro
- Espaciado amplio para facilitar lectura rápida

*Header listo: pendiente de validación antes de pasar a Footer.*

## 74. Etiquetas en barra inferior
- Opción elegida: **C) Icono + texto al inicio → luego solo icono**
- Durante onboarding: mejora comprensión y reduce curva de aprendizaje
- Tras habituación: mayor limpieza visual y foco en pantalla
- Se eliminarán progresivamente una vez el jugador identifique funciones

*Listo para inserción del Wireframe — FOOTER en la próxima actualización.*

## 75. Ubicación de la pestaña Inicio
- Opción elegida: **A) Inicio en el centro de la barra inferior**
- Motivo: el loop principal debe ser el más accesible
- Refuerza hábito del jugador → "volver al core"
- Encaja con UX de idle modernos

*Listo para inserción del Wireframe — FOOTER en la próxima actualización.*

## 76. Icono principal de la pestaña Inicio
- Estilo elegido: **B) Plato / campana de servicio**
- Motivo: refuerza acción principal → servir / cocinar / producir
- Mensaje psicológico: "aquí ocurre el dinero"
- Coherente con idle impulsado por ciclo de servicio

*Listo para inserción del Wireframe — FOOTER en la próxima actualización.*

## WIREFRAME — FOOTER (Barra inferior de navegación)

### 1. Objetivo del footer
Ser la **base del bucle de navegación**. Permite moverse entre las secciones principales sin interrumpir el flujo idle. Es persistente y siempre accesible.

### 2. Estructura visual
```
┌────────────────────────────────────────┐
|  [Menú]   [Personal]   [INICIO]   [I+D] |
└────────────────────────────────────────┘
```
- **INICIO** está en el centro (acción principal)
- **Recetas/Menú** a la izquierda → simboliza producción/preparación
- **Personal** a la izquierda inmediata del centro → gestión del staff
- **I+D** a la derecha → expansión/progresión

### 3. Iconografía
| Sección | Icono | Motivo |
|---------|--------|--------|
| Inicio | 🍽️ Campana/plato | Loop principal / servicio |
| Menú (recetas) | 📜 o 🍲 | Selección culinaria |
| Personal | 👤 / 🍳 cartoon | Gestión de staff |
| I+D | ⚙️ / bombilla cartoon | Mejora / investigación |

### 4. Etiquetas textuales (onboarding → retirada)
- Durante primeras sesiones: aparece debajo del icono (“Inicio”, “Recetas”, “Personal”, “I+D”).
- Cuando el jugador ya identifica iconos (tras X interacciones): las etiquetas desaparecen dejando solo iconos.

**Intención UX:** enseñar → retirar ruido visual después.

### 5. Estado activo / navegación
- Pestaña activa recibe **glow suave** + color primario
- Las demás quedan en modo outline sutil
- Transición entre pestañas: **slide lateral**

### 6. Microinteracciones
| Acción | Respuesta |
|--------|-----------|
| Tap | micro-tilt breve + glow |
| Mantener pulsado | abre tooltip contextual (texto corto) |

Ejemplo tooltip: "Aquí está el meollo, jefe."

### 7. Relación con otras capas
- No invade header ni cards
- Está siempre visible
- Eventos flotantes aparecen **por encima** si es necesario

### 8. Estilo gráfico aplicado
- Fondo translúcido suave (Luz de Faro) para separarlo del fondo “Medianoche”
- Bordes redondeados superiores (8–12px)
- Iconografía con outline fino para diferenciar de arte

*Footer listo: pendiente de validación antes de continuar con Pantalla Inicio.*

## 77. Disposición inicial en Pantalla Inicio
- Elección: **C) Mix (estado + card cuello)**
- Estructura base:
  1. Pequeño bloque de estado del restaurante (texto vivo)
  2. Card principal del cuello (acción central del idle)
- Motivo: equilibrio entre contexto y progreso
- UX: primero entiendes la situación → luego actúas

*Listo para insertar Wireframe — Pantalla Inicio en la próxima actualización.*

## 78. Estilo del mensaje de estado en Pantalla Inicio
- Elección: **B) Burbuja tipo globo de chat**
- Motivo: refuerza tono gamberro, cercano y vivo
- Hace que el "estado" parezca "una voz del restaurante" y no un panel frío
- Mayor expresividad sin ocupar demasiado espacio

*Listo para insertar el Wireframe — Pantalla Inicio en la próxima actualización.*

## WIREFRAME — PANTALLA INICIO (Servicio)

### 1. Objetivo de la pantalla
Es el **centro del loop idle**: muestra el cuello de producción/servicio y ofrece una acción inmediata para avanzar (mejorar / corregir / empujar el progreso). Aquí el jugador percibe si “el local tira” o “se atasca”.

### 2. Estructura visual
```
[ Burbuja de estado ]   ← humor gamberro / feedback vivo

[ Card Cuello principal ]  ← la mejora/acción inmediata

[ Botón CTA: "Mejorar ahora" ]

(espacio respirado)

[ Indicadores secundarios / futuros boosts / AFK flotante ]
```

### 3. Burbuja de estado (parte alta)
- Estilo chat / comentario vivo
- Aparece con micro-animación (fade + ligero slide desde izquierda)
- Mensajes rotativos según cuello del local

**Ejemplos de microtextos**:
- “La cocina va a pedales, jefe… mete caña.”
- “El camarero está en modo vacaciones. Cero prisas.”
- “Esto está más lento que una caña sin gas.”

### 4. Card del cuello
- Proporción: depende del tipo (2:3 si cuello = personal / 1:1 si cuello = receta / 3:2 si cuello = logística)
- Hero image visible → identidad del cuello
- Glow suave + micro-tilt al pulsar
- Badge opcional (gota/pin) si urgente

**Intención UX:** el jugador *siente* qué está fallando antes de leer.

### 5. Botón CTA (acción principal)
- Texto: "Mejorar ahora"
- Estado siempre claro (visible / bloqueado / en espera)
- En hover/press: oscurece → highlight suave
- Al mejorar: breve feedback positivo (micro-animación futura)

### 6. AFK / recompensas
- Flotante independiente (no dentro de la UI fija)
- Aparece lateral inferior-derecha cuando disponible
- Forma pin (llamativo)
- Micro-balance: no tapa el CTA ni la card principal

### 7. Flujo
| Paso | Qué ocurre | Percepción |
|------|-------------|-------------|
| 1 | Jugador entra | ve burbuja → entiende estado |
| 2 | Ve card cuello | identifica causa visual |
| 3 | Toca CTA | avanza progreso |
| 4 | Burbuja cambia | nuevo estado / satisfacción |

### 8. Estilo gráfico aplicado
- Card = héroe visual del layout
- Burbuja = voz del restaurante (tono humorístico)
- CTA = impulso inmediato
- AFK = premio paralelo

---

*Pantalla Inicio lista. Próximo paso: wireframe de pantallas secundarias empezando por MENÚ (Recetas).*

## 79. Disposición de Recetas (pantalla MENÚ)
- Elección: **C) Categoría en carrusel + Grid de cards debajo**
- Justificación:
  - Escalable cuando hay muchas recetas
  - UX moderno: selección rápida por categoría
  - Permite mantener protagonismo visual de las cards
- Refuerza sensación "colección" + navegación clara
*

## 80. Estado de recetas bloqueadas
- Elección: **A) Card visible con candado y baja opacidad**
- Motivo: el jugador ve lo que podría tener → refuerza deseo de progreso
- Comunicación inmediata: "todavía no, pero existe" en vez de "secreto"
- Compatible con halo y outline (pero desaturado)

*Listo para inserción del Wireframe — MENÚ (Recetas) en la próxima actualización.*

## 81. Representación del carrusel de categorías
- Elección: **C) Icono + texto**
- Motivo: aprendizaje visual + verbal durante primeras horas
- Garantiza claridad incluso con nuevas categorías en el futuro
- Más adelante se podría ocultar el texto para usuarios avanzados

*Listo para insertar Wireframe — MENÚ (Recetas) en la próxima actualización.*

## WIREFRAME — MENÚ (Recetas)

### 1. Objetivo de la pantalla
Permitir al jugador **visualizar, seleccionar y mejorar recetas**. Refuerza la sensación de colección y progresión culinaria, mostrando qué domina el restaurante y qué falta por desbloquear.

### 2. Estructura visual
```
[ Carrusel de categorías ]  ← icono + texto (scroll horizontal)

[ Grid de cards 1:1 ]
[ Grid de cards 1:1 ]  ← scroll vertical si hay muchas recetas
```

### 3. Comportamiento del carrusel
| Acción | Respuesta |
|--------|-----------|
| Deslizar | cambia categoría visible |
| Tap en categoría | filtra grid instantáneamente |
| Mantener | tooltip breve (ej. "Postres: azúcar y gloria") |

**Más adelante** → el texto podría ocultarse para usuarios avanzados.

### 4. Estados de las cards de recetas
| Estado | Apariencia | Intención |
|--------|------------|-----------|
| Disponible | imagen hero + halo + nombre | usable |
| Bloqueada | baja opacidad + candado | objetivo futuro visible |
| Mejorable | badge (pin) + brillo suave | aviso de acción |

### 5. Microinteracción
- Tap: micro-tilt + halo reforzado
- Mantener: abre detalle (popup) → descripción / ingredientes / mejora

### 6. Ejemplos de microtextos
- Tooltip categoría: "Fritos mágicos que curan el hambre y el alma."
- Bloqueada: "Todavía no tienes nivel para sacar esta joyita."
- Mejorable: "Esta receta pide mimo, ¡dale chispa!"

### 7. Navegación
- Botón atrás **no existe** → se usa footer persistente
- Consistencia: jugador nunca se siente “encerrado”

### 8. Estilo gráfico aplicado
- Cards flotan sobre el fondo (halo)
- Grid respira, no compacto (lectura inmediata)
- Categorías estilizadas: icono + texto = onboarding natural

---

*Wireframe de MENÚ listo. Próximo paso: pantalla PERSONAL (staff).*

## 82. Representación visual del personal
- Elección: **C) Mixto: card coleccionable + stats en detalle**
- Personal visible en grid 2:3 estilo coleccionable (coherente con recetas)
- Stats y rasgos aparecen al abrir popup de detalle
- Resultado: identidad visual + claridad funcional

*Listo para inserción del Wireframe — PERSONAL en la próxima actualización.*

## 83. Estado visual del personal agotado
- Elección: **C) Pin + ligera desaturación**
- Comunicación doble:
  - Pin = alerta inmediata (hay un problema)
  - Desaturación = baja de rendimiento visible
- No requiere leer texto para entender que rinde peor
- Mantiene coherencia con badges definidos en UI Kit

*Listo para insertar Wireframe — PERSONAL en la próxima actualización.*

## WIREFRAME — PERSONAL (Staff)

### 1. Objetivo de la pantalla
Mostrar al personal del restaurante como **activos vivos** que impulsan (o frenan) el servicio. Refuerza identidad y progreso humano: no solo mejoras edificios o recetas, también mejoras a quienes trabajan en ellas.

### 2. Estructura visual
```
[ Grid 2:3 (cards de empleados) ]
[ Grid 2:3 (scroll si hay muchos) ]
```

Cada card = un empleado con
- Retrato hero (60% parte superior)
- Nombre / rol
- Estado (óptimo / cansado / en mejora)

### 3. Estados visibles
| Estado | Apariencia | Intención UX |
|--------|------------|---------------|
| Óptimo | card normal con halo | rinde bien |
| Agotado | **desaturación + pin** | baja rendimiento visible |
| Mejorable | badge/pin “chispa” | invita a acción |

### 4. Interacción
| Acción | Respuesta |
|--------|-----------|
| Tap | abre popup de detalle (stats + rasgos) |
| Mantener | muestra tooltip breve (ej: "Este va arrastrando los pies...") |

### 5. Popup de detalle del empleado (preview)
Incluye:
- Retrato ampliado
- Nombre + rol
- 2–3 stats clave (velocidad / eficiencia / carisma)
- Botón de mejora cuando esté disponible
- Personalidad gamberra ligera (texto)

**Ejemplos de microtextos**
- "Este tío saca platos como si tuviera turbo… o hambre gratis."
- "Hoy va flojísimo, parece que le pesa el delantal."

### 6. Jerarquía visual
- El retrato manda → refuerza conexión con el jugador
- Stats en segundo plano → aparecen sólo al abrir popup
- Esto evita saturación visual en la vista principal

### 7. Relación con el cuello
Si el cuello está causado por el personal → esta pantalla refuerza la causa visualmente:
- El empleado agotado puede tener **badge especial** coincidiendo con la burbuja de estado del Inicio.

### 8. Futuras extensiones
- Turnos / descanso / relevos
- Personal legendario (drop raro / contratación premium)
- Habilidades activas / pasivas

---

*Wireframe PERSONAL listo. Próximo paso: pantalla I+D.*

## 84. Representación de I+D
- Elección: **C) Árbol tipo nodos conectados**
- Motivo: refuerza crecimiento visual y sensación de expansión real
- Mejora la motivación a largo plazo (metajuego)
- Facilita escalado futuro hacia franquicias y especializaciones

*Listo para inserción del Wireframe — I+D en la próxima actualización.*

## 85. Estilo de conexión entre nodos I+D
- Elección: **C) Luz / neón urbano**
- Justificación: refuerza atmósfera nocturna + temática moderna
- Visualmente comunica "expansión / energía / progreso"
- Coherente con paleta vibrante y halo/glow ya presentes en UI

*Listo para insertar Wireframe — I+D en la próxima actualización.*

## WIREFRAME — I+D (Investigación y Desarrollo) — BLOQUE 1/2

### 1. Objetivo de la pantalla
Centralizar la **progresión estructural** del restaurante: no solo se mejora la ejecución (personal) o la oferta (recetas), sino la **capacidad base del sistema**. Representa crecimiento a medio/largo plazo.

### 2. Disposición general
```
   [ Nodo raíz / Tier 0 ]
         │
   [ Tier 1 ]  —  mejoras iniciales (capacidad / velocidad base)
     │    │
   [ Tier 2 ]  —  rutas especializadas (cocina / servicio / reputación)
     │    │
   [ Tier 3+ ] — expansiones profundas (automatizaciones / franquicias futuras)
```

### 3. Visualización del árbol
- Nodo = card pequeña con icono + breve texto
- Conexiones = **líneas de neón urbano** (confirmado)
- Crece vertical/híbrido (scroll vertical + leve zoom posible en futuro)

### 4. Estados de los nodos
| Estado | Apariencia |
|--------|------------|
| Bloqueado | nodo opaco + línea tenue |
| Disponible | nodo iluminado + glow suave |
| Mejorado | nodo con halo brillante / detalle extra |

**Objetivo UX:** progreso debe ser *visible y deseable* incluso sin tocar.
