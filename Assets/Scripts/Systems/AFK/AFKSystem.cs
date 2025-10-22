using System;
using UnityEngine;

namespace SiKNessTycoon.Systems.AFK
{
    /// <summary>
    /// Represents the reward earned during offline time
    /// </summary>
    [Serializable]
    public struct AFKReward
    {
        public int efectivo;
        public float duration;
        public string flavorText;
    }

    /// <summary>
    /// Manages offline progression (AFK system).
    /// Calculates earnings based on venue production, I+D multipliers, and active events.
    /// </summary>
    public class AFKSystem : MonoBehaviour
    {
        [Header("AFK Configuration")]
        [SerializeField] private float baseProductionRate = 100f; // Efectivo per hour
        [SerializeField] private float maxAFKHours = 24f;         // Maximum accumulated time

        private DateTime lastPlayTime;
        private bool hasClaimedAFK = false;

        private void Start()
        {
            LoadLastPlayTime();
            CheckForAFKReward();
        }

        /// <summary>
        /// Checks if player has been offline and calculates reward
        /// </summary>
        private void CheckForAFKReward()
        {
            if (hasClaimedAFK) return;

            TimeSpan elapsed = DateTime.Now - lastPlayTime;

            // Only show AFK if offline for at least 5 minutes
            if (elapsed.TotalMinutes < 5) return;

            AFKReward reward = CalculateOfflineReward(lastPlayTime);

            if (reward.efectivo > 0)
            {
                GameEvents.RaiseAFKRewardReady(reward.efectivo, reward.duration);
            }
        }

        /// <summary>
        /// Calculates the offline reward based on elapsed time and multipliers
        /// </summary>
        public AFKReward CalculateOfflineReward(DateTime lastPlayTime)
        {
            TimeSpan elapsed = DateTime.Now - lastPlayTime;
            float hours = Mathf.Min((float)elapsed.TotalHours, maxAFKHours);

            if (hours <= 0)
            {
                return new AFKReward { efectivo = 0, duration = 0 };
            }

            // Base production
            float production = baseProductionRate * hours;

            // Apply multipliers
            production *= GetRnDMultiplier();        // I+D upgrades
            production *= GetVenueMultiplier();      // Multiple venues contribution
            production *= GetEventMultiplier();       // Active event bonuses

            int finalEfectivo = Mathf.RoundToInt(production);

            return new AFKReward
            {
                efectivo = finalEfectivo,
                duration = hours,
                flavorText = MicrotextLibrary.GetAFKMessage(hours)
            };
        }

        /// <summary>
        /// Claims the AFK reward and adds it to player resources
        /// </summary>
        public void ClaimAFKReward()
        {
            AFKReward reward = CalculateOfflineReward(lastPlayTime);

            if (reward.efectivo > 0)
            {
                ResourceManager.Instance.AddEfectivo(reward.efectivo);
                GameEvents.RaiseAFKRewardClaimed(reward.efectivo);
                hasClaimedAFK = true;

                // Update last play time to now
                lastPlayTime = DateTime.Now;
                SaveLastPlayTime();
            }
        }

        #region Multiplier Queries

        private float GetRnDMultiplier()
        {
            // TODO: Query RnDSystem for current global multiplier
            // For now, return base multiplier
            return 1.0f;
        }

        private float GetVenueMultiplier()
        {
            // TODO: Query VenueSystem for passive production from other venues
            // Active venue = 1.0, each additional venue adds percentage
            return 1.0f;
        }

        private float GetEventMultiplier()
        {
            // TODO: Query EventSystem for active AFK-boosting events
            return 1.0f;
        }

        #endregion

        #region Save/Load Last Play Time

        private void LoadLastPlayTime()
        {
            string lastPlayTimeString = PlayerPrefs.GetString("LastPlayTime", "");

            if (string.IsNullOrEmpty(lastPlayTimeString))
            {
                // First time playing
                lastPlayTime = DateTime.Now;
                SaveLastPlayTime();
            }
            else
            {
                if (DateTime.TryParse(lastPlayTimeString, out DateTime parsedTime))
                {
                    lastPlayTime = parsedTime;
                }
                else
                {
                    Debug.LogWarning("Failed to parse LastPlayTime, using current time");
                    lastPlayTime = DateTime.Now;
                }
            }
        }

        private void SaveLastPlayTime()
        {
            PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        #endregion

        private void OnApplicationQuit()
        {
            SaveLastPlayTime();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveLastPlayTime();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Debug: Simulate 1 Hour Offline")]
        private void DebugSimulate1HourOffline()
        {
            DateTime fakeLastPlay = DateTime.Now.AddHours(-1);
            PlayerPrefs.SetString("LastPlayTime", fakeLastPlay.ToString());
            PlayerPrefs.Save();
            
            Debug.Log("Simulated 1 hour offline. Restart Play Mode to see AFK reward.");
        }
        
        [ContextMenu("Debug: Simulate 8 Hours Offline")]
        private void DebugSimulate8HoursOffline()
        {
            DateTime fakeLastPlay = DateTime.Now.AddHours(-8);
            PlayerPrefs.SetString("LastPlayTime", fakeLastPlay.ToString());
            PlayerPrefs.Save();
            
            Debug.Log("Simulated 8 hours offline. Restart Play Mode to see AFK reward.");
        }
#endif
    }
}
