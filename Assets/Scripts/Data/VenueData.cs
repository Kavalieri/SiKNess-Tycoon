using UnityEngine;

namespace SiKNessTycoon.Data
{
    public enum VenueType
    {
        BarDelBarrio,   // Starting venue
        BistroUrbano,   // Second venue
        FoodCourt,      // Third venue
        Estacion        // Fourth venue (airport/station)
    }

    /// <summary>
    /// ScriptableObject defining a venue/location's properties and modifiers.
    /// </summary>
    [CreateAssetMenu(fileName = "NewVenue", menuName = "SiKNess/Data/Venue")]
    public class VenueData : ScriptableObject
    {
        [Header("Identity")]
        [Tooltip("Unique identifier for this venue")]
        public string venueId;

        [Tooltip("Display name of the venue")]
        public string venueName;

        [Tooltip("Type classification")]
        public VenueType venueType;

        [Tooltip("Hero image for venue card")]
        public Sprite venueImage;

        [Tooltip("Description or theme of this venue")]
        [TextArea(2, 4)]
        public string description;

        [Header("Context")]
        [Tooltip("Setting description (e.g., 'Ambiente local', 'Calle transitada')")]
        public string context;

        [Tooltip("Player fantasy (e.g., 'Levantar algo muerto', 'Ya somos referencia')")]
        public string fantasy;

        [Header("Modifiers")]
        [Tooltip("Base demand multiplier (affects customer flow)")]
        public float demandMultiplier = 1f;

        [Tooltip("Speed multiplier (kitchen/service pace)")]
        public float speedMultiplier = 1f;

        [Tooltip("Pressure level (how fast bottlenecks appear)")]
        [Range(0.5f, 2f)]
        public float pressureLevel = 1f;

        [Tooltip("Base AFK production rate for this venue")]
        public float afkProductionRate = 100f;

        [Header("Progression")]
        [Tooltip("Fama required to unlock this venue")]
        public int unlockFamaThreshold = 0;

        [Tooltip("Efectivo cost to unlock this venue")]
        public int unlockEfectivoCost = 0;

        [Tooltip("Is this venue currently unlocked?")]
        public bool isUnlocked = false;

        [Tooltip("Is this the currently active venue?")]
        public bool isActive = false;

        [Header("Staff Slots")]
        [Tooltip("Maximum staff in kitchen area")]
        public int maxKitchenStaff = 3;

        [Tooltip("Maximum staff in service area")]
        public int maxServiceStaff = 3;

        [Header("Visual")]
        [Tooltip("Background scene sprite (urban nocturnal theme)")]
        public Sprite backgroundSprite;

        [Tooltip("Ambient color tint for this venue")]
        public Color ambientTint = Color.white;

        /// <summary>
        /// Checks if this venue can be unlocked
        /// </summary>
        public bool CanUnlock()
        {
            if (isUnlocked)
                return false;

            if (ResourceManager.Instance.Fama < unlockFamaThreshold)
                return false;

            if (!ResourceManager.Instance.CanAffordEfectivo(unlockEfectivoCost))
                return false;

            return true;
        }

        /// <summary>
        /// Unlocks this venue
        /// </summary>
        public bool TryUnlock()
        {
            if (!CanUnlock())
                return false;

            if (ResourceManager.Instance.TrySpendEfectivo(unlockEfectivoCost))
            {
                isUnlocked = true;
                GameEvents.RaiseVenueUnlocked(venueName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Activates this venue as the current one
        /// </summary>
        public void Activate()
        {
            if (!isUnlocked)
            {
                Debug.LogWarning($"Cannot activate locked venue: {venueName}");
                return;
            }

            isActive = true;
            GameEvents.RaiseVenueChanged(venueName);
        }

        /// <summary>
        /// Deactivates this venue
        /// </summary>
        public void Deactivate()
        {
            isActive = false;
        }

        /// <summary>
        /// Calculates passive AFK contribution from this venue
        /// (when not active, venues contribute percentage of their production)
        /// </summary>
        public float GetPassiveAFKContribution()
        {
            if (isActive)
                return afkProductionRate;
            else if (isUnlocked)
                return afkProductionRate * 0.3f; // Passive venues contribute 30%
            else
                return 0f;
        }

        /// <summary>
        /// Gets unlock message with flavor text
        /// </summary>
        public string GetUnlockMessage()
        {
            return venueType switch
            {
                VenueType.BistroUrbano => "Bistró Urbano desbloqueado 🏙️ Ya somos referencia, jefe.",
                VenueType.FoodCourt => "Food Court desbloqueado 🏢 Alto volumen, ritmo intenso. Vamos.",
                VenueType.Estacion => "Estación desbloqueada 🚉 Marca reconocible, esto es grande.",
                _ => $"{venueName} desbloqueado 🎉 Nueva sede, nuevo rollo."
            };
        }
    }
}
