using System;
using UnityEngine;

namespace SiKNessTycoon.Core
{
    /// <summary>
    /// Manages the three main currencies: Efectivo (cash), Fama (fame), Estrellas (stars).
    /// Provides methods for adding, spending, and querying resources with event notifications.
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        // Currency values
        public int Efectivo { get; private set; }    // Immediate upgrades, daily operations
        public int Fama { get; private set; }        // Venue unlocks, reputation milestones
        public int Estrellas { get; private set; }   // Meta-progression, prestige

        // Currency change events
        public event Action<int> OnEfectivoChanged;
        public event Action<int> OnFamaChanged;
        public event Action<int> OnEstrellasChanged;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // Load saved values (if save system exists)
            LoadResources();
        }

        #region Efectivo Management

        public void AddEfectivo(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Use SpendEfectivo for negative amounts");
                return;
            }

            Efectivo += amount;
            OnEfectivoChanged?.Invoke(Efectivo);
            GameEvents.RaiseResourceChanged("Efectivo", Efectivo);
        }

        public bool TrySpendEfectivo(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Amount must be positive");
                return false;
            }

            if (Efectivo >= amount)
            {
                Efectivo -= amount;
                OnEfectivoChanged?.Invoke(Efectivo);
                GameEvents.RaiseResourceChanged("Efectivo", Efectivo);
                return true;
            }

            Debug.Log("Not enough Efectivo");
            return false;
        }

        public bool CanAffordEfectivo(int amount)
        {
            return Efectivo >= amount;
        }

        #endregion

        #region Fama Management

        public void AddFama(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Fama cannot be spent, only earned");
                return;
            }

            Fama += amount;
            OnFamaChanged?.Invoke(Fama);
            GameEvents.RaiseResourceChanged("Fama", Fama);
        }

        public bool HasFama(int threshold)
        {
            return Fama >= threshold;
        }

        #endregion

        #region Estrellas Management

        public void AddEstrellas(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Use SpendEstrellas for negative amounts");
                return;
            }

            Estrellas += amount;
            OnEstrellasChanged?.Invoke(Estrellas);
            GameEvents.RaiseResourceChanged("Estrellas", Estrellas);
        }

        public bool TrySpendEstrellas(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Amount must be positive");
                return false;
            }

            if (Estrellas >= amount)
            {
                Estrellas -= amount;
                OnEstrellasChanged?.Invoke(Estrellas);
                GameEvents.RaiseResourceChanged("Estrellas", Estrellas);
                return true;
            }

            Debug.Log("Not enough Estrellas");
            return false;
        }

        public bool CanAffordEstrellas(int amount)
        {
            return Estrellas >= amount;
        }

        #endregion

        #region Save/Load

        private void LoadResources()
        {
            // TODO: Integrate with SaveSystem
            Efectivo = PlayerPrefs.GetInt("Efectivo", 1000); // Starting amount
            Fama = PlayerPrefs.GetInt("Fama", 0);
            Estrellas = PlayerPrefs.GetInt("Estrellas", 0);

            // Notify initial values
            OnEfectivoChanged?.Invoke(Efectivo);
            OnFamaChanged?.Invoke(Fama);
            OnEstrellasChanged?.Invoke(Estrellas);
        }

        public void SaveResources()
        {
            // TODO: Integrate with SaveSystem
            PlayerPrefs.SetInt("Efectivo", Efectivo);
            PlayerPrefs.SetInt("Fama", Fama);
            PlayerPrefs.SetInt("Estrellas", Estrellas);
            PlayerPrefs.Save();
        }

        #endregion

        private void OnApplicationQuit()
        {
            SaveResources();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveResources();
            }
        }
    }
}
