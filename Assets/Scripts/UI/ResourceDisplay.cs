using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SiKNessTycoon.Core;

namespace SiKNessTycoon.UI
{
    /// <summary>
    /// Displays current resources (Efectivo, Fama, Estrellas) in the header.
    /// Updates automatically when ResourceManager values change.
    /// </summary>
    public class ResourceDisplay : MonoBehaviour
    {
        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI efectivoText;
        [SerializeField] private TextMeshProUGUI famaText;
        [SerializeField] private TextMeshProUGUI estrellasText; // Future use
        
        [Header("Optional Icons")]
        [SerializeField] private Image efectivoIcon;
        [SerializeField] private Image famaIcon;
        
        private void OnEnable()
        {
            // Subscribe to resource changes
            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.OnEfectivoChanged += UpdateEfectivo;
                ResourceManager.Instance.OnFamaChanged += UpdateFama;
                ResourceManager.Instance.OnEstrellasChanged += UpdateEstrellas;
                
                // Initial update
                UpdateAll();
            }
        }
        
        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.OnEfectivoChanged -= UpdateEfectivo;
                ResourceManager.Instance.OnFamaChanged -= UpdateFama;
                ResourceManager.Instance.OnEstrellasChanged -= UpdateEstrellas;
            }
        }
        
        private void UpdateEfectivo(int amount)
        {
            if (efectivoText != null)
                efectivoText.text = FormatCurrency(amount);
        }
        
        private void UpdateFama(int amount)
        {
            if (famaText != null)
                famaText.text = $"{amount:N0}⭐";
        }
        
        private void UpdateEstrellas(int amount)
        {
            if (estrellasText != null)
                estrellasText.text = $"{amount}✨"; // Future feature
        }
        
        private void UpdateAll()
        {
            UpdateEfectivo(ResourceManager.Instance.Efectivo);
            UpdateFama(ResourceManager.Instance.Fama);
            UpdateEstrellas(ResourceManager.Instance.Estrellas);
        }
        
        private string FormatCurrency(int amount)
        {
            // Format with thousands separator
            if (amount >= 1000000)
                return $"{amount / 1000000f:F1}M€";
            else if (amount >= 1000)
                return $"{amount / 1000f:F1}K€";
            else
                return $"{amount}€";
        }
        
#if UNITY_EDITOR
        [ContextMenu("Test: Add 1000 Efectivo")]
        private void TestAddEfectivo()
        {
            ResourceManager.Instance?.AddEfectivo(1000);
        }
        
        [ContextMenu("Test: Add 50 Fama")]
        private void TestAddFama()
        {
            ResourceManager.Instance?.AddFama(50);
        }
#endif
    }
}
