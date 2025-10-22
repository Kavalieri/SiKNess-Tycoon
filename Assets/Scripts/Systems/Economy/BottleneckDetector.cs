using UnityEngine;

namespace SiKNessTycoon.Systems.Economy
{
    /// <summary>
    /// Types of bottlenecks that can limit production
    /// </summary>
    public enum BottleneckType
    {
        None,       // Everything running smoothly
        Kitchen,    // Food preparation is the slowest
        Service,    // Waiters/delivery is the slowest
        Cashier     // Payment processing is the slowest
    }
    
    /// <summary>
    /// Detects which area (Kitchen, Service, Cashier) is the current bottleneck.
    /// Idle games thrive on showing the player where to optimize next.
    /// </summary>
    public class BottleneckDetector : MonoBehaviour
    {
        [Header("Load Thresholds")]
        [SerializeField] private float bottleneckThreshold = 0.7f; // 70% load = bottleneck
        
        [Header("Debug")]
        [SerializeField] private bool showDebugInfo = false;
        
        // Current loads (0.0 = idle, 1.0 = maxed out)
        private float kitchenLoad = 0.8f;  // Default: kitchen is bottleneck
        private float serviceLoad = 0.5f;
        private float cashierLoad = 0.4f;
        
        /// <summary>
        /// Detects the current bottleneck based on load percentages
        /// </summary>
        public BottleneckType DetectCurrentBottleneck()
        {
            // Find highest load
            float maxLoad = Mathf.Max(kitchenLoad, serviceLoad, cashierLoad);
            
            // If all loads are below threshold, no bottleneck
            if (maxLoad < bottleneckThreshold)
                return BottleneckType.None;
            
            // Return the area with highest load
            if (maxLoad == kitchenLoad)
                return BottleneckType.Kitchen;
            else if (maxLoad == serviceLoad)
                return BottleneckType.Service;
            else
                return BottleneckType.Cashier;
        }
        
        /// <summary>
        /// Update load values from economy system
        /// </summary>
        public void UpdateLoads(float kitchen, float service, float cashier)
        {
            kitchenLoad = Mathf.Clamp01(kitchen);
            serviceLoad = Mathf.Clamp01(service);
            cashierLoad = Mathf.Clamp01(cashier);
            
            if (showDebugInfo)
            {
                Debug.Log($"🔍 Bottleneck: {DetectCurrentBottleneck()} | " +
                         $"Kitchen: {kitchenLoad:P0}, Service: {serviceLoad:P0}, Cashier: {cashierLoad:P0}");
            }
        }
        
        /// <summary>
        /// Get load percentage for specific area
        /// </summary>
        public float GetLoad(BottleneckType type)
        {
            return type switch
            {
                BottleneckType.Kitchen => kitchenLoad,
                BottleneckType.Service => serviceLoad,
                BottleneckType.Cashier => cashierLoad,
                _ => 0f
            };
        }
        
        /// <summary>
        /// Get suggested upgrade for current bottleneck
        /// </summary>
        public string GetSuggestedUpgrade()
        {
            var bottleneck = DetectCurrentBottleneck();
            return bottleneck switch
            {
                BottleneckType.Kitchen => "kitchen_speed",
                BottleneckType.Service => "service_speed",
                BottleneckType.Cashier => "cashier_speed",
                _ => "none"
            };
        }
        
#if UNITY_EDITOR
        [ContextMenu("Debug: Simulate Kitchen Bottleneck")]
        private void DebugKitchenBottleneck()
        {
            UpdateLoads(0.9f, 0.4f, 0.3f);
        }
        
        [ContextMenu("Debug: Simulate Service Bottleneck")]
        private void DebugServiceBottleneck()
        {
            UpdateLoads(0.4f, 0.85f, 0.3f);
        }
        
        [ContextMenu("Debug: Simulate No Bottleneck")]
        private void DebugNoBottleneck()
        {
            UpdateLoads(0.5f, 0.5f, 0.5f);
        }
        
        [ContextMenu("Debug: Print Current State")]
        private void DebugPrintState()
        {
            var bottleneck = DetectCurrentBottleneck();
            Debug.Log($"<b>Current Bottleneck:</b> {bottleneck}\n" +
                     $"Kitchen Load: {kitchenLoad:P0}\n" +
                     $"Service Load: {serviceLoad:P0}\n" +
                     $"Cashier Load: {cashierLoad:P0}\n" +
                     $"Suggested Upgrade: {GetSuggestedUpgrade()}");
        }
#endif
    }
}
