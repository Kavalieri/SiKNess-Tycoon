using UnityEngine;

namespace SiKNessTycoon.Data
{
    public enum EmployeeArea
    {
        Kitchen,    // Cocina
        Service     // Sala
    }

    public enum EmployeeTrait
    {
        Fast,       // Rápido/a - Reduces task time
        Precise,    // Preciso/a - Reduces errors, increases quality
        Organized,  // Ordenado/a - Better resource management
        Charismatic,// Simpático/a - Increases customer satisfaction
        QueueMaster // Gestiona colas - Reduces wait times
    }

    /// <summary>
    /// ScriptableObject defining an employee's properties and traits.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEmployee", menuName = "SiKNess/Data/Employee")]
    public class EmployeeData : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Display name of the employee")]
        public string employeeName;

        [Tooltip("Portrait image for card (60% of card visual)")]
        public Sprite portrait;

        [Tooltip("Brief personality description or backstory")]
        [TextArea(2, 4)]
        public string description;

        [Header("Assignment")]
        public EmployeeArea workArea;

        [Tooltip("Primary trait that defines this employee")]
        public EmployeeTrait mainTrait;

        [Header("Stats")]
        [Tooltip("Base speed multiplier (1.0 = normal)")]
        [Range(0.5f, 2f)]
        public float speedMultiplier = 1f;

        [Tooltip("Base quality multiplier (1.0 = normal)")]
        [Range(0.5f, 2f)]
        public float qualityMultiplier = 1f;

        [Tooltip("Customer satisfaction boost (0.0 - 1.0)")]
        [Range(0f, 1f)]
        public float satisfactionBoost = 0f;

        [Header("Fatigue System")]
        [Tooltip("Hours worked before fatigue sets in")]
        public float hoursBeforeFatigue = 8f;

        [Tooltip("Current fatigue level (0 = fresh, 1 = exhausted)")]
        [Range(0f, 1f)]
        public float currentFatigue = 0f;

        [Header("Progression")]
        [Tooltip("Hiring cost in Efectivo")]
        public int hiringCost = 500;

        [Tooltip("Fama required to unlock this employee")]
        public int unlockFamaThreshold = 0;

        [Tooltip("Is this employee currently hired?")]
        public bool isHired = false;

        [Header("Visual")]
        [Tooltip("Card background color tint (optional)")]
        public Color cardTint = Color.white;

        /// <summary>
        /// Gets the trait description for UI display
        /// </summary>
        public string GetTraitDescription()
        {
            return mainTrait switch
            {
                EmployeeTrait.Fast => "Rápido/a - Tareas más veloces",
                EmployeeTrait.Precise => "Preciso/a - Mayor calidad",
                EmployeeTrait.Organized => "Ordenado/a - Mejor gestión",
                EmployeeTrait.Charismatic => "Simpático/a - Clientes más contentos",
                EmployeeTrait.QueueMaster => "Gestiona colas - Menos espera",
                _ => "Sin rasgo especial"
            };
        }

        /// <summary>
        /// Gets the trait emoji icon
        /// </summary>
        public string GetTraitIcon()
        {
            return mainTrait switch
            {
                EmployeeTrait.Fast => "⚡",
                EmployeeTrait.Precise => "🎯",
                EmployeeTrait.Organized => "📋",
                EmployeeTrait.Charismatic => "😊",
                EmployeeTrait.QueueMaster => "👥",
                _ => "⭐"
            };
        }

        /// <summary>
        /// Applies trait-specific bonus to speed
        /// </summary>
        public float GetEffectiveSpeed()
        {
            float speed = speedMultiplier;

            if (mainTrait == EmployeeTrait.Fast)
                speed *= 1.3f;

            // Fatigue penalty
            speed *= (1f - currentFatigue * 0.5f);

            return speed;
        }

        /// <summary>
        /// Applies trait-specific bonus to quality
        /// </summary>
        public float GetEffectiveQuality()
        {
            float quality = qualityMultiplier;

            if (mainTrait == EmployeeTrait.Precise)
                quality *= 1.3f;

            // Fatigue penalty
            quality *= (1f - currentFatigue * 0.3f);

            return quality;
        }

        /// <summary>
        /// Gets effective satisfaction boost accounting for fatigue
        /// </summary>
        public float GetEffectiveSatisfaction()
        {
            float satisfaction = satisfactionBoost;

            if (mainTrait == EmployeeTrait.Charismatic)
                satisfaction += 0.2f;

            // Fatigue penalty
            satisfaction *= (1f - currentFatigue * 0.4f);

            return Mathf.Clamp01(satisfaction);
        }

        /// <summary>
        /// Increases fatigue based on hours worked
        /// </summary>
        public void AddFatigue(float hours)
        {
            currentFatigue = Mathf.Clamp01(currentFatigue + hours / hoursBeforeFatigue);
        }

        /// <summary>
        /// Resets fatigue (after rest period)
        /// </summary>
        public void Rest()
        {
            currentFatigue = 0f;
        }

        /// <summary>
        /// Checks if employee is too fatigued to work effectively
        /// </summary>
        public bool IsTooFatigued()
        {
            return currentFatigue >= 0.8f;
        }
    }
}
