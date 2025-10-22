using UnityEngine;
using SiKNessTycoon.Core;

namespace SiKNessTycoon.Systems.Economy
{
    /// <summary>
    /// Core economy system handling revenue generation, ticket pricing, and satisfaction.
    /// Calculates production rates for AFK system.
    /// </summary>
    public class EconomySystem : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] private float baseTicketPrice = 15f;
        [SerializeField] private float baseCustomersPerHour = 20f;
        
        [Header("Current Multipliers")]
        [SerializeField] private float kitchenSpeedMultiplier = 1.0f;
        [SerializeField] private float serviceSpeedMultiplier = 1.0f;
        [SerializeField] private float satisfactionMultiplier = 1.0f;
        
        [Header("Revenue Tracking")]
        [SerializeField] private float totalRevenue = 0f;
        [SerializeField] private float revenueThisSession = 0f;
        
        private BottleneckDetector bottleneckDetector;
        
        private void Awake()
        {
            bottleneckDetector = GetComponent<BottleneckDetector>();
            if (bottleneckDetector == null)
                bottleneckDetector = gameObject.AddComponent<BottleneckDetector>();
        }
        
        private void Start()
        {
            // Start periodic revenue generation
            InvokeRepeating(nameof(GenerateRevenue), 1f, 1f); // Every second
        }
        
        /// <summary>
        /// Generates revenue based on current multipliers and bottlenecks
        /// </summary>
        private void GenerateRevenue()
        {
            float effectiveSpeed = CalculateEffectiveSpeed();
            float customersServed = (baseCustomersPerHour * effectiveSpeed) / 3600f; // Per second
            float revenue = customersServed * baseTicketPrice * satisfactionMultiplier;
            
            // Add to ResourceManager
            int revenueInt = Mathf.RoundToInt(revenue);
            if (revenueInt > 0)
            {
                ResourceManager.Instance.AddEfectivo(revenueInt);
                totalRevenue += revenue;
                revenueThisSession += revenue;
            }
            
            // Update bottleneck detector
            UpdateBottleneckLoads();
        }
        
        /// <summary>
        /// Calculates effective speed considering bottlenecks
        /// </summary>
        private float CalculateEffectiveSpeed()
        {
            // Effective speed is limited by the slowest process (bottleneck)
            return Mathf.Min(kitchenSpeedMultiplier, serviceSpeedMultiplier);
        }
        
        /// <summary>
        /// Updates load percentages for bottleneck detection
        /// </summary>
        private void UpdateBottleneckLoads()
        {
            // Load is inversely proportional to speed
            // Higher speed = lower load
            float kitchenLoad = 1.0f / Mathf.Max(0.1f, kitchenSpeedMultiplier);
            float serviceLoad = 1.0f / Mathf.Max(0.1f, serviceSpeedMultiplier);
            float cashierLoad = 0.3f; // Cashier is rarely a bottleneck in demo
            
            // Normalize to 0-1 range
            float maxLoad = Mathf.Max(kitchenLoad, serviceLoad, cashierLoad);
            if (maxLoad > 1f)
            {
                kitchenLoad /= maxLoad;
                serviceLoad /= maxLoad;
                cashierLoad /= maxLoad;
            }
            
            bottleneckDetector?.UpdateLoads(kitchenLoad, serviceLoad, cashierLoad);
        }
        
        /// <summary>
        /// Apply an upgrade multiplier to a specific area
        /// </summary>
        public void ApplyUpgrade(string upgradeType, float bonus)
        {
            switch (upgradeType)
            {
                case "kitchen_speed":
                    kitchenSpeedMultiplier += bonus;
                    break;
                case "service_speed":
                    serviceSpeedMultiplier += bonus;
                    break;
                case "satisfaction":
                    satisfactionMultiplier += bonus;
                    break;
            }
            
            UpdateBottleneckLoads();
            GameEvents.RaiseUpgradeApplied(upgradeType, bonus);
        }
        
        /// <summary>
        /// Get current production rate for AFK system
        /// </summary>
        public float GetProductionRate()
        {
            float effectiveSpeed = CalculateEffectiveSpeed();
            return baseCustomersPerHour * baseTicketPrice * effectiveSpeed * satisfactionMultiplier;
        }
        
        /// <summary>
        /// Get revenue statistics
        /// </summary>
        public (float total, float session, float perHour) GetRevenueStats()
        {
            float perHour = GetProductionRate();
            return (totalRevenue, revenueThisSession, perHour);
        }
        
#if UNITY_EDITOR
        [ContextMenu("Debug: +20% Kitchen Speed")]
        private void DebugUpgradeKitchen()
        {
            ApplyUpgrade("kitchen_speed", 0.2f);
            Debug.Log($"Kitchen Speed: {kitchenSpeedMultiplier:P0}");
        }
        
        [ContextMenu("Debug: +20% Service Speed")]
        private void DebugUpgradeService()
        {
            ApplyUpgrade("service_speed", 0.2f);
            Debug.Log($"Service Speed: {serviceSpeedMultiplier:P0}");
        }
        
        [ContextMenu("Debug: Print Stats")]
        private void DebugPrintStats()
        {
            var stats = GetRevenueStats();
            Debug.Log($"<b>Economy Stats:</b>\n" +
                     $"Total Revenue: {stats.total:F0}€\n" +
                     $"Session Revenue: {stats.session:F0}€\n" +
                     $"Production Rate: {stats.perHour:F0}€/hour\n" +
                     $"Kitchen Speed: {kitchenSpeedMultiplier:P0}\n" +
                     $"Service Speed: {serviceSpeedMultiplier:P0}");
        }
        
        [ContextMenu("Debug: Reset Multipliers")]
        private void DebugResetMultipliers()
        {
            kitchenSpeedMultiplier = 1.0f;
            serviceSpeedMultiplier = 1.0f;
            satisfactionMultiplier = 1.0f;
            UpdateBottleneckLoads();
            Debug.Log("Multipliers reset to 1.0");
        }
#endif
    }
}
