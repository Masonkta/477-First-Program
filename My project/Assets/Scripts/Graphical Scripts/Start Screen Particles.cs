using System.Collections;
using UnityEngine;

public class StartScreenParticles : MonoBehaviour
{
    public GameObject Stareffect; 
    public GameObject Warpeffect1; 
    public GameObject Warpeffect2;
    public GameObject WarpShipEffect;

    private void Start()
    {
        StartCoroutine(EffectCycle());
    }

    IEnumerator EffectCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(78f);

            if (Stareffect) Stareffect.SetActive(false);

            yield return new WaitForSeconds(9f);

            if (Warpeffect1) Warpeffect1.SetActive(true);
            if (Warpeffect2) Warpeffect2.SetActive(true);
            if (WarpShipEffect) WarpShipEffect.SetActive(true);

            yield return new WaitForSeconds(223f);

            if (Stareffect) Stareffect.SetActive(true);
            if (Warpeffect1) Warpeffect1.SetActive(false);
            if (Warpeffect2) Warpeffect2.SetActive(false);

            if (WarpShipEffect)
            {
                WarpShipEffect.SetActive(false);
                WarpShipEffect.transform.position = new Vector3(0, -5, 0); 
            }
        }
    }
}
