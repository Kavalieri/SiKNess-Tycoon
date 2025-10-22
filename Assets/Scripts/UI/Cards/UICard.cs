using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SiKNessTycoon.UI.Cards
{
    /// <summary>
    /// Generic card component for recipes, employees, upgrades.
    /// Supports hero image (60% layout), title, description, and action button.
    /// Includes cartoon micro-tilt animation on hover.
    /// </summary>
    public class UICard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Visual Components")]
        [SerializeField] private Image heroImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        [Header("Action Button")]
        [SerializeField] private Button actionButton;
        [SerializeField] private TextMeshProUGUI buttonText;
        
        [Header("Optional Badge")]
        [SerializeField] private GameObject badge; // For "NEW", "⭐", etc.
        [SerializeField] private TextMeshProUGUI badgeText;
        
        [Header("Animation Settings")]
        [SerializeField] private bool enableTiltAnimation = true;
        [SerializeField] private float tiltAmount = 3f;
        [SerializeField] private float tiltDuration = 0.15f;
        
        private RectTransform rectTransform;
        private Vector3 originalRotation;
        private bool isHovered = false;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            originalRotation = rectTransform.localEulerAngles;
        }
        
        /// <summary>
        /// Setup card with all data at once
        /// </summary>
        public void Setup(Sprite hero, string title, string description, string buttonLabel = "", System.Action onButtonClick = null)
        {
            SetHeroImage(hero);
            SetTitle(title);
            SetDescription(description);
            
            if (!string.IsNullOrEmpty(buttonLabel))
            {
                SetButtonText(buttonLabel);
                if (actionButton != null)
                    actionButton.gameObject.SetActive(true);
            }
            else
            {
                if (actionButton != null)
                    actionButton.gameObject.SetActive(false);
            }
            
            if (onButtonClick != null && actionButton != null)
            {
                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(() => onButtonClick());
            }
        }
        
        public void SetHeroImage(Sprite sprite)
        {
            if (heroImage != null)
            {
                heroImage.sprite = sprite;
                heroImage.enabled = sprite != null;
            }
        }
        
        public void SetTitle(string text)
        {
            if (titleText != null)
                titleText.text = text;
        }
        
        public void SetDescription(string text)
        {
            if (descriptionText != null)
                descriptionText.text = text;
        }
        
        public void SetButtonText(string text)
        {
            if (buttonText != null)
                buttonText.text = text;
        }
        
        public void ShowBadge(string text)
        {
            if (badge != null)
            {
                badge.SetActive(true);
                if (badgeText != null)
                    badgeText.text = text;
            }
        }
        
        public void HideBadge()
        {
            if (badge != null)
                badge.SetActive(false);
        }
        
        // Hover animation (cartoon micro-tilt)
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enableTiltAnimation) return;
            
            isHovered = true;
            StopAllCoroutines();
            StartCoroutine(AnimateTilt(new Vector3(0, 0, tiltAmount)));
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (!enableTiltAnimation) return;
            
            isHovered = false;
            StopAllCoroutines();
            StartCoroutine(AnimateTilt(originalRotation));
        }
        
        private System.Collections.IEnumerator AnimateTilt(Vector3 targetRotation)
        {
            float elapsed = 0f;
            Vector3 startRotation = rectTransform.localEulerAngles;
            
            while (elapsed < tiltDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / tiltDuration;
                
                // Ease out cubic
                t = 1f - Mathf.Pow(1f - t, 3f);
                
                rectTransform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
                yield return null;
            }
            
            rectTransform.localEulerAngles = targetRotation;
        }
        
        /// <summary>
        /// Squash/stretch animation on button press (call from button's OnClick in Inspector)
        /// </summary>
        public void AnimatePress()
        {
            StopAllCoroutines();
            StartCoroutine(AnimatePressCoroutine());
        }
        
        private System.Collections.IEnumerator AnimatePressCoroutine()
        {
            Vector3 originalScale = transform.localScale;
            
            // Squash
            float elapsed = 0f;
            while (elapsed < 0.05f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / 0.05f;
                transform.localScale = Vector3.Lerp(originalScale, originalScale * 0.95f, t);
                yield return null;
            }
            
            // Stretch
            elapsed = 0f;
            while (elapsed < 0.1f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / 0.1f;
                transform.localScale = Vector3.Lerp(originalScale * 0.95f, originalScale * 1.05f, t);
                yield return null;
            }
            
            // Return
            elapsed = 0f;
            while (elapsed < 0.1f)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / 0.1f;
                transform.localScale = Vector3.Lerp(originalScale * 1.05f, originalScale, t);
                yield return null;
            }
            
            transform.localScale = originalScale;
        }
    }
}
