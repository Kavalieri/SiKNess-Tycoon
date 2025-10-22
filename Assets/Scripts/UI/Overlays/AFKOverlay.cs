using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SiKNessTycoon.Core;
using SiKNessTycoon.Systems.AFK;

namespace SiKNessTycoon.UI.Overlays
{
    /// <summary>
    /// Overlay shown when player returns after being AFK.
    /// Displays offline earnings with gamberro ligero flavor text.
    /// </summary>
    public class AFKOverlay : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject panel;
        [SerializeField] private Image dimmer;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI flavorText;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Button recaudarButton;
        [SerializeField] private Button closeButton;
        
        [Header("Animation")]
        [SerializeField] private GameObject iconAFK; // Floating gota/pin
        [SerializeField] private float bobSpeed = 2f;
        [SerializeField] private float bobAmount = 10f;
        
        private AFKSystem.AFKReward currentReward;
        private Vector3 iconOriginalPos;
        private bool isAnimating = false;
        
        private void Awake()
        {
            if (iconAFK != null)
                iconOriginalPos = iconAFK.transform.localPosition;
            
            // Setup button listeners
            recaudarButton?.onClick.AddListener(OnRecaudarClick);
            closeButton?.onClick.AddListener(OnCloseClick);
            
            // Start hidden
            gameObject.SetActive(false);
        }
        
        private void Update()
        {
            // Animate floating icon
            if (isAnimating && iconAFK != null)
            {
                float offset = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
                iconAFK.transform.localPosition = iconOriginalPos + new Vector3(0, offset, 0);
            }
        }
        
        /// <summary>
        /// Shows the overlay with AFK reward data
        /// </summary>
        public void Show(AFKSystem.AFKReward reward)
        {
            currentReward = reward;
            gameObject.SetActive(true);
            isAnimating = true;
            
            // Set texts
            if (titleText != null)
                titleText.text = "¡De vuelta!";
            
            if (flavorText != null)
                flavorText.text = MicrotextLibrary.GetAFKMessage(reward.duration);
            
            if (rewardText != null)
                rewardText.text = FormatReward(reward);
            
            // Fade in dimmer
            if (dimmer != null)
                StartCoroutine(FadeDimmer(0f, 0.7f, 0.3f));
        }
        
        /// <summary>
        /// Hides the overlay
        /// </summary>
        public void Hide()
        {
            isAnimating = false;
            
            // Fade out
            if (dimmer != null)
            {
                StartCoroutine(FadeDimmer(0.7f, 0f, 0.2f));
                Invoke(nameof(DisableOverlay), 0.2f);
            }
            else
            {
                DisableOverlay();
            }
        }
        
        private void DisableOverlay()
        {
            gameObject.SetActive(false);
        }
        
        private void OnRecaudarClick()
        {
            // Reward already applied by AFKSystem, just close
            GameEvents.RaiseAFKRewardClaimed(currentReward.efectivo, currentReward.duration);
            Hide();
        }
        
        private void OnCloseClick()
        {
            Hide();
        }
        
        private string FormatReward(AFKSystem.AFKReward reward)
        {
            string efectivoStr = reward.efectivo >= 1000
                ? $"{reward.efectivo / 1000f:F1}K€"
                : $"{reward.efectivo}€";
            
            string durationStr = reward.duration >= 1
                ? $"{reward.duration:F1}h"
                : $"{reward.duration * 60:F0}min";
            
            return $"+ {efectivoStr} ({durationStr})";
        }
        
        private System.Collections.IEnumerator FadeDimmer(float from, float to, float duration)
        {
            float elapsed = 0f;
            Color color = dimmer.color;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                color.a = Mathf.Lerp(from, to, t);
                dimmer.color = color;
                yield return null;
            }
            
            color.a = to;
            dimmer.color = color;
        }
        
#if UNITY_EDITOR
        [ContextMenu("Debug: Show Test Reward")]
        private void DebugShowTestReward()
        {
            var testReward = new AFKSystem.AFKReward
            {
                efectivo = 2450,
                fama = 0,
                duration = 8.5f
            };
            Show(testReward);
        }
#endif
    }
}
