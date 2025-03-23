using UnityEngine;
using TMPro;

public class ShieldBar : MonoBehaviour
{
    public Transform shieldBar; 
    public TextMeshProUGUI percentageText;  
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

            float progress = Mathf.Clamp01(1 - (elapsedTime / shieldDuration)); 
            float newScaleX = Mathf.Lerp(0f, 17f, progress);
            shieldBar.localScale = new Vector3(newScaleX, shieldBar.localScale.y, shieldBar.localScale.z);

            UpdatePercentageText(progress * 100); 

            if (elapsedTime >= shieldDuration)
            {
                isActive = false;
                UpdatePercentageText(0);
            }
        }
    }

    public void ActivateShield(float duration)
    {
        Debug.Log(duration);
        shieldDuration = duration;
        elapsedTime = 0f;
        isActive = true;

        shieldBar.localScale = new Vector3(17f, shieldBar.localScale.y, shieldBar.localScale.z); 
        UpdatePercentageText(100);
    }

    void UpdatePercentageText(float value)
    {
        if (percentageText != null)
        {
            percentageText.text = Mathf.RoundToInt(value) + "%"; 
        }
    }
}
