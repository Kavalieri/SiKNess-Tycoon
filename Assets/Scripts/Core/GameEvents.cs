using System;

namespace SiKNessTycoon.Core
{
    /// <summary>
    /// Central event bus for decoupled communication between game systems.
    /// Follows the Observer pattern for loose coupling and scalability.
    /// </summary>
    public static class GameEvents
    {
        #region Resource Events

        /// <summary>Fired when any resource changes (Efectivo, Fama, Estrellas)</summary>
        public static event Action<string, int> OnResourceChanged;

        public static void RaiseResourceChanged(string resourceName, int newAmount)
        {
            OnResourceChanged?.Invoke(resourceName, newAmount);
        }

        #endregion

        #region Economy Events

        /// <summary>Fired when revenue is generated from operations</summary>
        public static event Action<int> OnRevenueGenerated;

        /// <summary>Fired when a bottleneck is detected in kitchen/service/cashier</summary>
        public static event Action<string> OnBottleneckDetected;

        /// <summary>Fired when satisfaction level changes</summary>
        public static event Action<float> OnSatisfactionChanged;

        public static void RaiseRevenueGenerated(int amount)
        {
            OnRevenueGenerated?.Invoke(amount);
        }

        public static void RaiseBottleneckDetected(string area)
        {
            OnBottleneckDetected?.Invoke(area);
        }

        public static void RaiseSatisfactionChanged(float newLevel)
        {
            OnSatisfactionChanged?.Invoke(newLevel);
        }

        #endregion

        #region Staff Events

        /// <summary>Fired when an employee is hired</summary>
        public static event Action<string> OnEmployeeHired;

        /// <summary>Fired when an employee becomes fatigued</summary>
        public static event Action<string> OnEmployeeFatigued;

        /// <summary>Fired when employee shifts are optimized</summary>
        public static event Action OnShiftsOptimized;

        public static void RaiseEmployeeHired(string employeeName)
        {
            OnEmployeeHired?.Invoke(employeeName);
        }

        public static void RaiseEmployeeFatigued(string employeeName)
        {
            OnEmployeeFatigued?.Invoke(employeeName);
        }

        public static void RaiseShiftsOptimized()
        {
            OnShiftsOptimized?.Invoke();
        }

        #endregion

        #region Menu Events

        /// <summary>Fired when a new recipe is unlocked</summary>
        public static event Action<string> OnRecipeUnlocked;

        /// <summary>Fired when a recipe is upgraded</summary>
        public static event Action<string, int> OnRecipeUpgraded;

        /// <summary>Fired when "Special of the Day" is set</summary>
        public static event Action<string> OnSpecialDaySet;

        public static void RaiseRecipeUnlocked(string recipeName)
        {
            OnRecipeUnlocked?.Invoke(recipeName);
        }

        public static void RaiseRecipeUpgraded(string recipeName, int newLevel)
        {
            OnRecipeUpgraded?.Invoke(recipeName, newLevel);
        }

        public static void RaiseSpecialDaySet(string recipeName)
        {
            OnSpecialDaySet?.Invoke(recipeName);
        }

        #endregion

        #region R&D Events

        /// <summary>Fired when an I+D upgrade is unlocked</summary>
        public static event Action<string> OnUpgradeUnlocked;

        /// <summary>Fired when I+D multipliers change</summary>
        public static event Action<float> OnMultiplierChanged;

        public static void RaiseUpgradeUnlocked(string upgradeName)
        {
            OnUpgradeUnlocked?.Invoke(upgradeName);
        }

        public static void RaiseMultiplierChanged(float newMultiplier)
        {
            OnMultiplierChanged?.Invoke(newMultiplier);
        }

        #endregion

        #region Event System (Weekly/Micro)

        /// <summary>Fired when a weekly event starts</summary>
        public static event Action<string, string> OnWeeklyEventStarted;

        /// <summary>Fired when a micro event triggers</summary>
        public static event Action<string, float> OnMicroEventTriggered;

        public static void RaiseWeeklyEventStarted(string eventName, string description)
        {
            OnWeeklyEventStarted?.Invoke(eventName, description);
        }

        public static void RaiseMicroEventTriggered(string eventName, float duration)
        {
            OnMicroEventTriggered?.Invoke(eventName, duration);
        }

        #endregion

        #region AFK Events

        /// <summary>Fired when AFK reward is ready to claim</summary>
        public static event Action<int, float> OnAFKRewardReady;

        /// <summary>Fired when AFK reward is claimed</summary>
        public static event Action<int> OnAFKRewardClaimed;

        public static void RaiseAFKRewardReady(int efectivo, float hours)
        {
            OnAFKRewardReady?.Invoke(efectivo, hours);
        }

        public static void RaiseAFKRewardClaimed(int efectivo)
        {
            OnAFKRewardClaimed?.Invoke(efectivo);
        }

        #endregion

        #region Venue Events

        /// <summary>Fired when a new venue is unlocked</summary>
        public static event Action<string> OnVenueUnlocked;

        /// <summary>Fired when the active venue changes</summary>
        public static event Action<string> OnVenueChanged;

        public static void RaiseVenueUnlocked(string venueName)
        {
            OnVenueUnlocked?.Invoke(venueName);
        }

        public static void RaiseVenueChanged(string venueName)
        {
            OnVenueChanged?.Invoke(venueName);
        }

        #endregion

        #region UI Events

        /// <summary>Fired when main screen changes (Inicio, Menú, Personal, I+D)</summary>
        public static event Action<string> OnScreenChanged;

        /// <summary>Fired when an overlay should be shown</summary>
        public static event Action<string, float> OnOverlayShow;

        /// <summary>Fired when a suggested action is available</summary>
        public static event Action<string, string> OnActionSuggested;

        public static void RaiseScreenChanged(string screenName)
        {
            OnScreenChanged?.Invoke(screenName);
        }

        public static void RaiseOverlayShow(string message, float duration)
        {
            OnOverlayShow?.Invoke(message, duration);
        }

        public static void RaiseActionSuggested(string actionName, string targetArea)
        {
            OnActionSuggested?.Invoke(actionName, targetArea);
        }

        #endregion

        #region Progression Events

        /// <summary>Fired when a daily objective is completed</summary>
        public static event Action<string> OnObjectiveCompleted;

        /// <summary>Fired when all daily objectives are complete</summary>
        public static event Action OnAllObjectivesCompleted;

        public static void RaiseObjectiveCompleted(string objectiveName)
        {
            OnObjectiveCompleted?.Invoke(objectiveName);
        }

        public static void RaiseAllObjectivesCompleted()
        {
            OnAllObjectivesCompleted?.Invoke();
        }

        #endregion

        #region Game State Events

        /// <summary>Fired when the game is paused</summary>
        public static event Action<bool> OnGamePaused;

        /// <summary>Fired when the game is saved</summary>
        public static event Action OnGameSaved;

        public static void RaiseGamePaused(bool isPaused)
        {
            OnGamePaused?.Invoke(isPaused);
        }

        public static void RaiseGameSaved()
        {
            OnGameSaved?.Invoke();
        }

        #endregion
    }
}
