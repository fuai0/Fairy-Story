using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI healthText;
    private CharacterStats stats;
    private Entity entity;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
        entity = GetComponentInParent<Entity>();
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        slider = GetComponent<Slider>();

        entity.onFlipped += FlipUI;
        stats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetHealth();
        slider.value = stats.currentHealth;
        healthText.text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }

    private void FlipUI() => slider.transform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        stats.onHealthChanged -= UpdateHealthUI;
    }
}
