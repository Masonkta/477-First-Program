using System.Collections;
using UnityEngine;

public class StartScreenParticles : MonoBehaviour
{
    public GameObject effect1; 
    public GameObject effect2; 
    public GameObject effect3; 

    private void Start()
    {
        StartCoroutine(EffectCycle());
    }

    IEnumerator EffectCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(78f);

            if (effect1) effect1.SetActive(false);

            yield return new WaitForSeconds(9f);

            if (effect2) effect2.SetActive(true);
            if (effect3) effect3.SetActive(true);

            yield return new WaitForSeconds(223f);

            if (effect1) effect1.SetActive(true);
            if (effect2) effect2.SetActive(false);
            if (effect3) effect3.SetActive(false);
        }
    }
}
