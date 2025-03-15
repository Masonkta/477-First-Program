using UnityEngine;

public class MoonLighting : MonoBehaviour
{
    public Transform sun; // Reference to the Sun object
    public Material moonMaterial; // The material of the Moon (must support HDR color)
    public float maxIntensity = 5f; // Max intensity when near the sun
    public float minIntensity = 0.2f; // Min intensity (eclipse effect)
    public float effectRange = 5f; // Range of intensity effect
    public float moveSpeed = 2f; // Moon's movement speed

    private Vector4 emissionColor; // Color + intensity

    void Start()
    {
        // Set base emission color with default intensity (1)
        emissionColor = new Vector4(90f / 255f, 90f / 255f, 90f / 255f, 1f);
        moonMaterial.SetVector("_Color", emissionColor);
    }

    void Update()
    {
        // Move the moon at a constant speed
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // Get positions
        float moonX = transform.position.x;
        float sunX = sun.position.x;
        float distance = Mathf.Abs(moonX - sunX);

        // Normalize intensity based on distance to the sun
        float intensity = Mathf.Lerp(maxIntensity, minIntensity, Mathf.InverseLerp(0, effectRange, distance));

        // Adjust only the intensity (fourth value of Vector4)
        emissionColor.w = intensity;

        // Apply the color with updated intensity
        moonMaterial.SetVector("_Color", emissionColor);
    }
}
