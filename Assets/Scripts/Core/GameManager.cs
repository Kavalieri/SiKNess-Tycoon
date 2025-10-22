using UnityEngine;

namespace SiKNessTycoon.Core
{
    /// <summary>
    /// Master game controller. Manages game state, initialization order, and high-level systems.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State")]
        [SerializeField] private bool isGamePaused = false;

        public bool IsGamePaused => isGamePaused;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeGame();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeGame()
        {
            // Ensure core managers exist
            EnsureManagerExists<ResourceManager>();
            EnsureManagerExists<AFKSystem>();

            // Subscribe to key events
            GameEvents.OnGamePaused += HandleGamePaused;

            Debug.Log("GameManager initialized");
        }

        private void EnsureManagerExists<T>() where T : Component
        {
            if (FindObjectOfType<T>() == null)
            {
                GameObject managerObj = new GameObject(typeof(T).Name);
                managerObj.AddComponent<T>();
                Debug.Log($"Created missing manager: {typeof(T).Name}");
            }
        }

        public void PauseGame()
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                Time.timeScale = 0f;
                GameEvents.RaiseGamePaused(true);
            }
        }

        public void ResumeGame()
        {
            if (isGamePaused)
            {
                isGamePaused = false;
                Time.timeScale = 1f;
                GameEvents.RaiseGamePaused(false);
            }
        }

        private void HandleGamePaused(bool isPaused)
        {
            // Handle any pause-related logic
            Debug.Log($"Game {(isPaused ? "paused" : "resumed")}");
        }

        public void SaveGame()
        {
            ResourceManager.Instance.SaveResources();
            // TODO: Add more save logic when SaveSystem is implemented
            GameEvents.RaiseGameSaved();
            Debug.Log("Game saved");
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                GameEvents.OnGamePaused -= HandleGamePaused;
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

#if UNITY_EDITOR
        [ContextMenu("Debug: Save Game Now")]
        private void DebugSaveGame()
        {
            SaveGame();
        }
        
        [ContextMenu("Debug: Toggle Pause")]
        private void DebugTogglePause()
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }
#endif
    }
}
