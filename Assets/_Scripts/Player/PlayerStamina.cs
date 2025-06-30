using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegenRate = 15f;
    

    public float staminaUseCooldown = 0.3f; 
    private float lastStaminaUseTime = -Mathf.Infinity;

    [Header("UI")]
    public Slider staminaSlider;

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    private void Update()
    {
        
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            UpdateStaminaUI();
        }
    }

    public bool HasEnoughStamina(float amount)
    {
        return currentStamina >= amount;
    }

    public bool CanUseStamina()
    {
        return Time.time >= lastStaminaUseTime + staminaUseCooldown;
    }

    public void TryUseStamina(float amount)
    {
        if (HasEnoughStamina(amount) && CanUseStamina())
        {
            currentStamina -= amount;
            lastStaminaUseTime = Time.time;
            UpdateStaminaUI();
        }
        else
        {
            Debug.Log("NotEnoughStamina");
        }
    }


    void UpdateStaminaUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina / maxStamina;

        }
    }
}