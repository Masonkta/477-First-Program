using UnityEngine;

public class LevelCompleteEffect : MonoBehaviour
{
    public RectTransform mainText;   
    public RectTransform accentText; 

    public void EndText()
    {
        mainText.gameObject.SetActive(true);
        accentText.gameObject.SetActive(true);
        mainText.localScale = Vector3.zero;
        accentText.localScale = Vector3.zero;

       
        LeanTween.scale(mainText, new Vector3(1.04f, 1.04f, 1f), 1f) 
            .setEase(LeanTweenType.easeOutBounce);  

        
        LeanTween.scale(accentText, Vector3.one, 1f) 
            .setEase(LeanTweenType.easeOutElastic);  

    }
}
