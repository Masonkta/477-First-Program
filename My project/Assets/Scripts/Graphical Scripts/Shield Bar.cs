using UnityEngine;
using TMPro;

public class ShieldBar : MonoBehaviour
{
    public Transform shieldBar;  // Assign the shield bar's transform
    public TextMeshProUGUI percentageText;  // Assign the percentage text
    private float shieldDuration;
    private float elapsedTime = 0f;
    private bool isActive = false;

    void Start()
    {
        UpdatePercentageText(0);
    }
    void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(1 - (elapsedTime / shieldDuration)); // Reverse progress (starts full, goes to 0)
            float newScaleX = Mathf.Lerp(0f, 17f, progress);
            shieldBar.localScale = new Vector3(newScaleX, shieldBar.localScale.y, shieldBar.localScale.z);

            UpdatePercentageText(progress * 100); // Convert to percentage

            if (elapsedTime >= shieldDuration)
            {
                isActive = false;
                UpdatePercentageText(0); // Ensure it reaches 0%
            }
        }
    }

    public void ActivateShield(float duration)
    {
        Debug.Log(duration);
        shieldDuration = duration;
        elapsedTime = 0f;
        isActive = true;

        shieldBar.localScale = new Vector3(17f, shieldBar.localScale.y, shieldBar.localScale.z); // Set full bar
        UpdatePercentageText(100); // Reset percentage to 100%
    }

    void UpdatePercentageText(float value)
    {
        if (percentageText != null)
        {
            percentageText.text = Mathf.RoundToInt(value) + "%"; // Round to whole number
        }
    }
}
