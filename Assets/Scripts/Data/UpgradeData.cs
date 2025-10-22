using UnityEngine;

namespace SiKNessTycoon.Data
{
    public enum UpgradeCategory
    {
        Kitchen,    // Cocina - Speed, capacity, quality
        Service,    // Servicio - Flow, customer handling
        Marketing   // Marketing - Reputation, demand
    }

    public enum UpgradeTier
    {
        Tier0,      // Base (initial I+D unlock)
        Tier1,      // Operational (early upgrades)
        Tier2,      // Specialization (mid-game)
        Tier3Plus   // Franchise (late-game)
    }

    /// <summary>
    /// ScriptableObject defining an I+D (R&D) upgrade node.
    /// </summary>
    [CreateAssetMenu(fileName = "NewUpgrade", menuName = "SiKNess/Data/Upgrade")]
    public class UpgradeData : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Unique identifier for save/load")]
        public string upgradeId;

        [Tooltip("Display name (hybrid: thematic + technical)")]
        public string displayName;

        [Tooltip("Technical descriptor (shown in parentheses)")]
        public string technicalDescriptor;

        [Tooltip("Icon for the upgrade node")]
        public Sprite icon;

        [Tooltip("Flavor text ('gamberro ligero' tone)")]
        [TextArea(2, 4)]
        public string flavorText;

        [Header("Categorization")]
        public UpgradeCategory category;
        public UpgradeTier tier;

        [Header("Effects")]
        [Tooltip("Speed multiplier bonus (e.g., 1.08 = +8% speed)")]
        public float speedBonus = 0f;

        [Tooltip("Quality multiplier bonus")]
        public float qualityBonus = 0f;

        [Tooltip("Satisfaction increase")]
        public float satisfactionBonus = 0f;

        [Tooltip("AFK production multiplier (e.g., 1.15 = +15%)")]
        public float afkMultiplier = 1f;

        [Tooltip("Fama generation bonus")]
        public float famaBonus = 0f;

        [Header("Progression")]
        [Tooltip("Cost in Efectivo to unlock")]
        public int efectivoCost = 1000;

        [Tooltip("Fama required to make this upgrade available")]
        public int unlockFamaThreshold = 0;

        [Tooltip("Prerequisite upgrades (must be unlocked first)")]
        public UpgradeData[] prerequisites;

        [Tooltip("Is this upgrade currently unlocked?")]
        public bool isUnlocked = false;

        [Header("Visual")]
        [Tooltip("Position in tech tree (for layout)")]
        public Vector2 treePosition;

        [Tooltip("Child upgrades (for connection lines)")]
        public UpgradeData[] childUpgrades;

        /// <summary>
        /// Gets the full display name with descriptor
        /// </summary>
        public string GetFullName()
        {
            return $"{displayName} ({technicalDescriptor})";
        }

        /// <summary>
        /// Checks if all prerequisites are met
        /// </summary>
        public bool ArePrerequisitesMet()
        {
            if (prerequisites == null || prerequisites.Length == 0)
                return true;

            foreach (var prereq in prerequisites)
            {
                if (prereq != null && !prereq.isUnlocked)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if this upgrade can be unlocked
        /// </summary>
        public bool CanUnlock()
        {
            if (isUnlocked)
                return false;

            if (!ArePrerequisitesMet())
                return false;

            if (ResourceManager.Instance.Fama < unlockFamaThreshold)
                return false;

            if (!ResourceManager.Instance.CanAffordEfectivo(efectivoCost))
                return false;

            return true;
        }

        /// <summary>
        /// Unlocks this upgrade and applies its effects
        /// </summary>
        public bool TryUnlock()
        {
            if (!CanUnlock())
                return false;

            if (ResourceManager.Instance.TrySpendEfectivo(efectivoCost))
            {
                isUnlocked = true;
                GameEvents.RaiseUpgradeUnlocked(displayName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a summary of effects for display
        /// </summary>
        public string GetEffectsSummary()
        {
            string summary = "";

            if (speedBonus > 0)
                summary += $"+{(speedBonus * 100):F0}% velocidad\n";

            if (qualityBonus > 0)
                summary += $"+{(qualityBonus * 100):F0}% calidad\n";

            if (satisfactionBonus > 0)
                summary += $"+{(satisfactionBonus * 100):F0}% satisfacción\n";

            if (afkMultiplier > 1f)
                summary += $"+{((afkMultiplier - 1f) * 100):F0}% AFK\n";

            if (famaBonus > 0)
                summary += $"+{(famaBonus * 100):F0}% fama\n";

            return summary.TrimEnd('\n');
        }
    }
}
