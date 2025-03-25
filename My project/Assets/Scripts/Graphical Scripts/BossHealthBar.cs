using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Boss boss;

    private void Start()
    {
        if (boss != null && healthBar != null)
        {
            healthBar.maxValue = boss.health;
            healthBar.value = boss.health;
        }
    }

    private void Update()
    {
        if (boss != null && healthBar != null)
        {
            healthBar.value = boss.health;
        }
    }
}