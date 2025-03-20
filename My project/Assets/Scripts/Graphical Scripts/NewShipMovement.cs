using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public Vector2 xRange = new Vector2(-5.5f, 5.5f); 
    public Vector2 yRange = new Vector2(0.8f, -1.7f); 
    public float enterSpeed = 1.5f; 
    public float minPatrolSpeed = 0.2f; 
    public float maxPatrolSpeed = 0.5f; 
    public float swayIntensity = 0.5f; 
    public float rotationSpeed = 1.5f; 
    public float transitionDelay = 1.0f; 
    public float maxRotation = 45f; 

    private Vector3 targetPos; 
    private bool movingLeft = true; 
    private float currentSpeed; 

    void Start()
    {
        transform.position = new Vector3(0, -5f, 0); 
        StartCoroutine(EnterScreen());
    }

    IEnumerator EnterScreen()
    {
 
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, -1.7f, 0); 

        float elapsedTime = 0;
        float duration = Mathf.Abs(startPos.y - endPos.y) / enterSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;


        targetPos = GetNextPatrolTarget();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            currentSpeed = Random.Range(minPatrolSpeed, maxPatrolSpeed);

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                float sway = Mathf.Sin(Time.time * 2) * swayIntensity;
                Vector3 swayPosition = new Vector3(targetPos.x + sway, targetPos.y, targetPos.z);
                transform.position = Vector3.MoveTowards(transform.position, swayPosition, currentSpeed * Time.deltaTime);
                Vector3 direction = (targetPos - transform.position).normalized;
                float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                targetRotation = Mathf.Clamp(targetRotation, -maxRotation, maxRotation);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), rotationSpeed * Time.deltaTime);

                yield return null;
            }
            yield return new WaitForSeconds(transitionDelay);
            targetPos = GetNextPatrolTarget();
        }
    }

    Vector3 GetNextPatrolTarget()
    {
        if (movingLeft)
        {
            movingLeft = false;
            return new Vector3(xRange.x, Random.Range(yRange.y, yRange.x), transform.position.z); // Move left
        }
        else
        {
            movingLeft = true;
            return new Vector3(xRange.y, Random.Range(yRange.y, yRange.x), transform.position.z); // Move right
        }
    }
}
