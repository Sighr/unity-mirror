using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 10;
    public float maxSpeed = 100;
    
    public float rotationSpeed = 0.1f;
    public float maxRotation = 45f;
    
    public Rigidbody body;
    public GameObject mirror;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-transform.right * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(-transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(transform.forward * speed);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            mirror.SetActive(!mirror.activeSelf);
        }

        float eulerAnglesY = mirror.transform.localRotation.eulerAngles.y;
        if (Input.GetKey(KeyCode.Q))
        {
            if (eulerAnglesY < maxRotation || eulerAnglesY > 350 - maxRotation)
                mirror.transform.Rotate(Vector3.back, -rotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (eulerAnglesY > 360 - maxRotation || eulerAnglesY < maxRotation + 10)
                mirror.transform.Rotate(Vector3.back, rotationSpeed);
        }
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = Vector3.zero;
        }

        Vector3 bodyVelocity = body.velocity;
        float bodySpeed = bodyVelocity.magnitude;
        if (bodySpeed > maxSpeed)
            body.velocity = bodyVelocity / bodySpeed * maxSpeed;
    }
}
