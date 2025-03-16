using System.Collections;
using UnityEngine;

public class LaserBeamFade : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip laserSound;

    public float chargeTime = 4.0f; // Charging time before firing
    public float expandDuration = 0.5f; // Laser expansion time
    public float holdDuration = 1.0f; // How long the laser stays visible
    public float fadeSpeed = 1.5f; // Laser fade-out speed
    private float startWidth = 0f;
    private float endWidth = 2f;

    public GameObject chargeEffect1; // First charge particle system
    public GameObject chargeEffect2; // Second charge particle system
    private float maxChargeScale = 4f; // Max scale for particles
    private float targetLocalX = -1.2f; // Target local X position

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Start the laser at zero width
        lineRenderer.startWidth = 0f;
        lineRenderer.endWidth = 0f;

        // Play the charging + firing sound
        if (audioSource && laserSound)
        {
            audioSource.clip = laserSound;
            audioSource.Play();
        }

        // Start scaling and shifting the charging particles
        StartCoroutine(ScaleAndMoveChargingEffects());

        // After charging completes, fire the laser
        StartCoroutine(WaitForFire());
    }

    private IEnumerator ScaleAndMoveChargingEffects()
    {
        float elapsedTime = 0f;

        // Get initial local positions
        Vector3 startLocalPos1 = chargeEffect1 ? chargeEffect1.transform.localPosition : Vector3.zero;
        Vector3 startLocalPos2 = chargeEffect2 ? chargeEffect2.transform.localPosition : Vector3.zero;

        while (elapsedTime < chargeTime)
        {
            float t = elapsedTime / chargeTime;
            float newScale = Mathf.Lerp(1f, maxChargeScale, t); // Scale up to maxChargeScale
            float newX = Mathf.Lerp(startLocalPos1.x, targetLocalX, t); // Move to -2 locally

            // Apply new scale & local position
            if (chargeEffect1)
            {
                chargeEffect1.transform.localScale = new Vector3(newScale, chargeEffect1.transform.localScale.y, chargeEffect1.transform.localScale.z);
                chargeEffect1.transform.localPosition = new Vector3(newX, startLocalPos1.y, startLocalPos1.z);
            }
            if (chargeEffect2)
            {
                chargeEffect2.transform.localScale = new Vector3(newScale, chargeEffect2.transform.localScale.y, chargeEffect2.transform.localScale.z);
                chargeEffect2.transform.localPosition = new Vector3(newX, startLocalPos2.y, startLocalPos2.z);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator WaitForFire()
    {
        yield return new WaitForSeconds(chargeTime);

        // Disable charge effects when firing
        if (chargeEffect1) chargeEffect1.SetActive(false);
        if (chargeEffect2) chargeEffect2.SetActive(false);

        StartCoroutine(ExpandBeam());
    }

    private IEnumerator ExpandBeam()
    {
        float elapsedTime = 0f;

        while (elapsedTime < expandDuration)
        {
            float t = elapsedTime / expandDuration;
            float newWidth = Mathf.Lerp(0f, endWidth, t);

            lineRenderer.startWidth = newWidth * 0.375f;
            lineRenderer.endWidth = newWidth;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.startWidth = startWidth + (endWidth * 0.375f);
        lineRenderer.endWidth = endWidth;

        yield return new WaitForSeconds(holdDuration);

        StartCoroutine(FadeOutBeam());
    }

    private IEnumerator FadeOutBeam()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            float t = elapsedTime / fadeSpeed;
            float newWidth = Mathf.Lerp(endWidth, 0f, t);

            lineRenderer.startWidth = newWidth * 0.375f;
            lineRenderer.endWidth = newWidth;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lineRenderer.startWidth = 0f;
        lineRenderer.endWidth = 0f;
    }
}
