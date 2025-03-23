using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{

    public Transform playerPos;
    public float moveSpeed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(target.position * moveSpeed * Time.deltaTime);

        if (transform.position.x < target.position.x)
        {
            // explodes
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Border")) {
            Destroy(this.gameObject);
        }
    }
}
