using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    public Slider slider;
    public float duration = 120f; 

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        float value = Mathf.Clamp01(timeElapsed / duration);
        slider.value = value;
    }
}
