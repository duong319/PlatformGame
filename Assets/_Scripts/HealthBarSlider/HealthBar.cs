using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    public RectTransform fillRect;
    public float maxWidth = 350f;  
    public float minWidth = 50f;   

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        UpdateFillLength();
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        UpdateFillLength();
    }

    void UpdateFillLength()
    {
        float percent = slider.value / slider.maxValue;
        float newWidth = Mathf.Lerp(minWidth, maxWidth, percent);
        fillRect.sizeDelta = new Vector2(newWidth, fillRect.sizeDelta.y);
    }
}
