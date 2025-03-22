using UnityEngine;
using TMPro;

public class EnergyBar: MonoBehaviour
{
    public Transform energyBar;  // Assign the energy bar's transform
    public TextMeshProUGUI percentageText;  // Assign the percentage text
    public Movement movementScript;  // Reference to the Movement script

    private float fireRate;
    private float elapsedTime = 0f;
    private bool isRecharging = false;

    void Start()
    {
        if (movementScript == null)
            movementScript = FindObjectOfType<Movement>();

        UpdatePercentageText(100); // Initialize text
    }

    void Update()
    {
        if (isRecharging)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / fireRate); // Normalize to 0 - 1
            float newScaleX = Mathf.Lerp(0f, 17f, progress);
            energyBar.localScale = new Vector3(newScaleX, energyBar.localScale.y, energyBar.localScale.z);

            UpdatePercentageText(progress * 100); // Convert to percentage

            if (elapsedTime >= fireRate)
            {
                isRecharging = false;
                UpdatePercentageText(100); // Ensure it reaches 100%
            }
        }
    }

    public void FireLaser(float fireRateValue)
    {
        fireRate = fireRateValue;
        elapsedTime = 0f;
        isRecharging = true;

        energyBar.localScale = new Vector3(0f, energyBar.localScale.y, energyBar.localScale.z);
        UpdatePercentageText(0); // Reset percentage to 00%
    }

    void UpdatePercentageText(float value)
    {
        if (percentageText != null)
        {
            percentageText.text = Mathf.RoundToInt(value) + "%"; // Round to whole number
        }
    }
}
