using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score = 0;

    public void updateScore(float pointVal)
    {
        score += pointVal;
        scoreText.text = score.ToString("0000000");
    }
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        scoreText = GameObject.Find("Score Value").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("Score Value").GetComponent<TextMeshProUGUI>();
            updateScore(0);
        }
    }
}
