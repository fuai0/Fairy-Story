using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class HealthBar_UI : MonoBehaviour
{
    private Slider slider;
    private CharacterStats stats;
    private Entity entity;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponent<Slider>();

        entity.onFlipped += FlipUI;
        stats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.GetHealth();
        slider.value = stats.currentHealth;
    }

    private void FlipUI() => slider.transform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        stats.onHealthChanged -= UpdateHealthUI;
    }
}
