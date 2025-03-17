using UnityEngine;

public class DevTools : MonoBehaviour
{
    public LaserBeamFade laserBeam; // Assign in the Inspector

    private void Update()
    {
        // Press 'Q' to fire the laser manually for testing
        if (Input.GetKeyDown(KeyCode.Q) && laserBeam)
        {
            laserBeam.FireLaser();
        }
    }
}
