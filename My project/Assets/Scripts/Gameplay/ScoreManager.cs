using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private float score = 0;
    private float timer = 0;
    private float timeBonus = 1;

    public void updateScore(float pointVal)
    {
        score += pointVal;
        scoreText.text = score.ToString("0000000");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            updateScore(timeBonus * 100);
            timeBonus++;
            timer = 0;
        }
    }
}
