using UnityEngine;
using SiKNessTycoon.UI.Cards;
using SiKNessTycoon.Core;
using SiKNessTycoon.Systems.Economy;

namespace SiKNessTycoon.UI.Screens
{
    /// <summary>
    /// Main "Inicio" screen showing current state and suggested action.
    /// Displays bottleneck detection and provides upgrade CTA.
    /// </summary>
    public class InicioScreen : MonoBehaviour
    {
        [Header("Cards")]
        [SerializeField] private UICard estadoCard;
        [SerializeField] private UICard accionCard;
        
        [Header("Upgrade Settings")]
        [SerializeField] private int upgradeCost = 500;
        [SerializeField] private float upgradeBonus = 0.20f; // 20%
        
        private BottleneckDetector bottleneckDetector;
        private float currentKitchenSpeed = 1.0f;
        
        private void Start()
        {
            bottleneckDetector = FindObjectOfType<BottleneckDetector>();
            UpdateDisplay();
        }
        
        private void OnEnable()
        {
            // Refresh display when screen becomes active
            UpdateDisplay();
        }
        
        private void UpdateDisplay()
        {
            UpdateEstadoCard();
            UpdateAccionCard();
        }
        
        private void UpdateEstadoCard()
        {
            if (estadoCard == null) return;
            
            // Get current bottleneck (or default to Kitchen for demo)
            BottleneckType cuello = BottleneckType.Kitchen;
            if (bottleneckDetector != null)
                cuello = bottleneckDetector.DetectCurrentBottleneck();
            
            string mensaje = MicrotextLibrary.GetBottleneckMessage(cuello);
            string detalles = GetBottleneckDetails(cuello);
            
            estadoCard.Setup(
                hero: null, // No hero for status card
                title: mensaje,
                description: detalles,
                buttonLabel: "" // No button on status card
            );
        }
        
        private void UpdateAccionCard()
        {
            if (accionCard == null) return;
            
            // For demo: always suggest kitchen upgrade
            string titulo = "Mejorar Velocidad Cocina";
            string descripcion = $"+ {upgradeBonus * 100:F0}% velocidad de preparación\n\n" +
                                 $"Velocidad actual: {currentKitchenSpeed * 100:F0}%";
            
            string botonTexto = ResourceManager.Instance.Efectivo >= upgradeCost
                ? $"Mejorar ahora ({upgradeCost}€)"
                : $"No hay suficiente Efectivo ({upgradeCost}€)";
            
            accionCard.Setup(
                hero: null, // TODO: Load icon from Resources
                title: titulo,
                description: descripcion,
                buttonLabel: botonTexto,
                onButtonClick: OnMejorarClick
            );
            
            // Disable button if can't afford
            var button = accionCard.GetComponentInChildren<UnityEngine.UI.Button>();
            if (button != null)
                button.interactable = ResourceManager.Instance.Efectivo >= upgradeCost;
        }
        
        private void OnMejorarClick()
        {
            if (ResourceManager.Instance.TrySpendEfectivo(upgradeCost))
            {
                // Apply upgrade
                currentKitchenSpeed += upgradeBonus;
                
                // Show feedback
                string mensaje = MicrotextLibrary.GetUpgradeMessage("Velocidad Cocina");
                Debug.Log($"✅ {mensaje}");
                
                // TODO: Show overlay with success message
                
                // Fire event for other systems
                GameEvents.RaiseUpgradeApplied("kitchen_speed", upgradeBonus);
                
                // Refresh display
                UpdateDisplay();
                
                // Increase cost for next upgrade (scaling)
                upgradeCost = Mathf.RoundToInt(upgradeCost * 1.5f);
            }
            else
            {
                Debug.Log("❌ No hay suficiente Efectivo");
                // TODO: Show overlay with error message
            }
        }
        
        private string GetBottleneckDetails(BottleneckType type)
        {
            return type switch
            {
                BottleneckType.Kitchen => $"🔥 Velocidad: {currentKitchenSpeed * 100:F0}%\n👥 Cocineros: 2/5",
                BottleneckType.Service => "🍽️ Velocidad: 85%\n👥 Camareros: 1/5",
                BottleneckType.Cashier => "💰 Velocidad: 90%\n👥 Cajeros: 1/3",
                _ => "✅ Todo funciona bien"
            };
        }
        
#if UNITY_EDITOR
        [ContextMenu("Debug: Force Refresh")]
        private void DebugRefresh()
        {
            UpdateDisplay();
        }
        
        [ContextMenu("Debug: Reset Kitchen Speed")]
        private void DebugResetSpeed()
        {
            currentKitchenSpeed = 1.0f;
            upgradeCost = 500;
            UpdateDisplay();
        }
#endif
    }
}
