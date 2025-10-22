using UnityEngine;
using UnityEngine.UI;
using SiKNessTycoon.Core;

namespace SiKNessTycoon.UI
{
    /// <summary>
    /// Manages navigation between the 4 main screens: Inicio, Menú, Personal, I+D.
    /// Handles tab buttons and screen activation in a single-scene architecture.
    /// </summary>
    public class ScreenNavigator : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private GameObject screenInicio;
        [SerializeField] private GameObject screenMenu;
        [SerializeField] private GameObject screenPersonal;
        [SerializeField] private GameObject screenRnD;
        
        [Header("Tab Buttons")]
        [SerializeField] private Button tabInicio;
        [SerializeField] private Button tabMenu;
        [SerializeField] private Button tabPersonal;
        [SerializeField] private Button tabRnD;
        
        [Header("Visual Feedback")]
        [SerializeField] private Color tabActiveColor = new Color(1f, 0.34f, 0.2f); // Salsa Brava
        [SerializeField] private Color tabInactiveColor = new Color(0.76f, 0.72f, 0.64f); // Luz de Faro dimmed
        
        private GameObject[] screens;
        private Button[] tabs;
        private int currentScreenIndex = 0;
        
        private void Awake()
        {
            // Cache arrays for easy iteration
            screens = new GameObject[] { screenInicio, screenMenu, screenPersonal, screenRnD };
            tabs = new Button[] { tabInicio, tabMenu, tabPersonal, tabRnD };
            
            // Setup button listeners
            tabInicio?.onClick.AddListener(() => ShowScreen(0));
            tabMenu?.onClick.AddListener(() => ShowScreen(1));
            tabPersonal?.onClick.AddListener(() => ShowScreen(2));
            tabRnD?.onClick.AddListener(() => ShowScreen(3));
        }
        
        private void Start()
        {
            // Show Inicio by default
            ShowScreen(0);
        }
        
        /// <summary>
        /// Switches to the specified screen index (0=Inicio, 1=Menú, 2=Personal, 3=I+D)
        /// </summary>
        public void ShowScreen(int index)
        {
            if (index < 0 || index >= screens.Length)
            {
                Debug.LogWarning($"Invalid screen index: {index}");
                return;
            }
            
            currentScreenIndex = index;
            
            // Activate/deactivate screens
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] != null)
                    screens[i].SetActive(i == index);
            }
            
            // Update tab button visuals
            UpdateTabVisuals();
            
            // Fire event for analytics/other systems
            GameEvents.RaiseScreenChanged(GetScreenName(index));
        }
        
        private void UpdateTabVisuals()
        {
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i] != null)
                {
                    var colors = tabs[i].colors;
                    colors.normalColor = (i == currentScreenIndex) ? tabActiveColor : tabInactiveColor;
                    colors.selectedColor = tabActiveColor;
                    tabs[i].colors = colors;
                }
            }
        }
        
        private string GetScreenName(int index)
        {
            return index switch
            {
                0 => "Inicio",
                1 => "Menú",
                2 => "Personal",
                3 => "I+D",
                _ => "Unknown"
            };
        }
        
        /// <summary>
        /// Public API for direct screen navigation
        /// </summary>
        public void ShowInicio() => ShowScreen(0);
        public void ShowMenu() => ShowScreen(1);
        public void ShowPersonal() => ShowScreen(2);
        public void ShowRnD() => ShowScreen(3);
        
#if UNITY_EDITOR
        [ContextMenu("Debug: Cycle Screens")]
        private void DebugCycleScreens()
        {
            ShowScreen((currentScreenIndex + 1) % screens.Length);
        }
#endif
    }
}
