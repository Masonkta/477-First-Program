using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Vector2 xRange = new Vector2(-5.9f, 5.9f);
    public Vector2 yRange = new Vector2(-3.5f, 3.5f);
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float maxRotation = 45f;
    public float returnDelay = 1f;  

    private Vector3 defaultPosition = new Vector3(0, -1.7f, 0); 
    private Camera mainCam;
    private float timeOutsideRange = 0f; 

    void Start()
    {
        mainCam = Camera.main;
        transform.position = defaultPosition;
    }

    void Update()
    {
        if (mainCam == null) return;
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        if (Input.mousePresent) 
        {
            if (IsMouseWithinRange(mouseWorldPos))
            {
                timeOutsideRange = 0f; 
                MoveToMouse(mouseWorldPos); 
            }
            else
            {
                timeOutsideRange += Time.deltaTime;
                if (timeOutsideRange >= returnDelay)
                {
                    MoveToDefaultPosition(); 
                }
            }
        }
        else
        {
            timeOutsideRange = 0f; 
            MoveToDefaultPosition(); 
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCam.transform.position.z; 
        Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);
        return new Vector3(worldPos.x, worldPos.y, 0);
    }

    void MoveToMouse(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, xRange.x, xRange.y);
        targetPos.y = Mathf.Clamp(targetPos.y, yRange.x, yRange.y);
        transform.position = Vector2.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        RotateTowards(targetPos);
    }

    void MoveToDefaultPosition()
    {
        transform.position = Vector2.Lerp(transform.position, defaultPosition, 1 * Time.deltaTime);
        RotateTowards(defaultPosition);
    }

    void RotateTowards(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetRotation = Mathf.Clamp(targetRotation, -maxRotation, maxRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), rotationSpeed * Time.deltaTime);
    }

    bool IsMouseWithinRange(Vector3 mouseWorldPos)
    {
        bool isWithinX = mouseWorldPos.x >= xRange.x && mouseWorldPos.x <= xRange.y;
        bool isWithinY = mouseWorldPos.y >= yRange.x && mouseWorldPos.y <= yRange.y;
        return isWithinX && isWithinY;
    }
}
