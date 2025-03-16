using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    public Transform moon; 
    public Transform sun;
    public Color startColor; 
    public Color brightestColor; 

    public float speed = 1f; 
    private float totalDistance; 

    private Renderer moonRenderer;

    void Start()
    {
        
        moonRenderer = moon.GetComponent<Renderer>();

       
        totalDistance = Vector3.Distance(moon.position, sun.position);
    }

    void Update()
    {

        moon.position = Vector3.MoveTowards(moon.position, new Vector3(sun.position.x + totalDistance, moon.position.y, moon.position.z), speed * Time.deltaTime);
        float distanceToSun = Vector3.Distance(moon.position, sun.position);
        float eclipseFactor = Mathf.InverseLerp(0, totalDistance, distanceToSun); 
        float darkeningSpeed = Mathf.Lerp(1f, 2f, eclipseFactor); 
        Color targetColor = Color.Lerp(startColor, Color.black, Mathf.Pow(1 - eclipseFactor, darkeningSpeed)); 

        if (moon.position.x >= sun.position.x - 0.1f && moon.position.x <= sun.position.x + 0.1f)
        {
            targetColor = Color.black;
        }
        
        else if (moon.position.x > sun.position.x)
        {    
            float brighteningFactor = Mathf.InverseLerp(sun.position.x, sun.position.x + totalDistance, moon.position.x);
            targetColor = Color.Lerp(Color.black, brightestColor, brighteningFactor);
        }
 
        moonRenderer.material.color = targetColor;
    }
}
