using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour
{
    public ControlsForShip Input { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Input = new ControlsForShip();
        Input.Enable();
    }

    // Update is called once per frame
    void OnDestroy()  // This ensures cleanup when the object is destroyed or scene changes
    {
        if (Input != null)
        {
            Input.Disable();
        }
    }
}
