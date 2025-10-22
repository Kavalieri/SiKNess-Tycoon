using UnityEngine;

namespace SiKNessTycoon.Data
{
    public enum RecipeCategory
    {
        Entrantes,
        Principales,
        Postres,
        Bebidas
    }

    public enum TimeOfDay
    {
        Day,        // ☀️ Popular during daytime
        Night,      // 🌙 Popular during nighttime
        Both        // Popular at all times
    }

    /// <summary>
    /// ScriptableObject defining a recipe's properties and progression.
    /// </summary>
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "SiKNess/Data/Recipe")]
    public class RecipeData : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Display name of the recipe")]
        public string recipeName;

        [Tooltip("Hero image for card (60% of card visual)")]
        public Sprite heroImage;

        [Tooltip("Brief description or flavor text")]
        [TextArea(2, 4)]
        public string description;

        [Header("Categorization")]
        public RecipeCategory category;

        [Tooltip("When this recipe is most popular")]
        public TimeOfDay popularity;

        [Header("Economy")]
        [Tooltip("Base price in Efectivo")]
        public int basePrice = 10;

        [Tooltip("Time to prepare in seconds")]
        public float preparationTime = 30f;

        [Tooltip("How popular this recipe is (affects demand)")]
        [Range(0f, 1f)]
        public float demandMultiplier = 1f;

        [Header("Progression")]
        [Tooltip("Fama required to unlock this recipe")]
        public int unlockFamaThreshold = 0;

        [Tooltip("Current level of this recipe (upgradeable)")]
        public int currentLevel = 1;

        [Tooltip("Maximum level this recipe can reach")]
        public int maxLevel = 10;

        [Header("Synergies")]
        [Tooltip("Tags for combo detection (e.g., 'picante', 'vegetariano')")]
        public string[] synergyTags;

        [Tooltip("Recipes that pair well with this one")]
        public RecipeData[] synergisticRecipes;

        [Header("Visual")]
        [Tooltip("Card background color tint (optional)")]
        public Color cardTint = Color.white;

        /// <summary>
        /// Calculates the final price based on current level
        /// </summary>
        public int GetCurrentPrice()
        {
            return Mathf.RoundToInt(basePrice * (1 + (currentLevel - 1) * 0.15f));
        }

        /// <summary>
        /// Calculates the final preparation time based on current level
        /// </summary>
        public float GetCurrentPreparationTime()
        {
            return preparationTime * Mathf.Pow(0.95f, currentLevel - 1);
        }

        /// <summary>
        /// Gets the cost to upgrade to the next level
        /// </summary>
        public int GetUpgradeCost()
        {
            return basePrice * currentLevel * 2;
        }

        /// <summary>
        /// Checks if this recipe can be upgraded
        /// </summary>
        public bool CanUpgrade()
        {
            return currentLevel < maxLevel;
        }

        /// <summary>
        /// Upgrades the recipe to the next level
        /// </summary>
        public void Upgrade()
        {
            if (CanUpgrade())
            {
                currentLevel++;
            }
        }
    }
}
