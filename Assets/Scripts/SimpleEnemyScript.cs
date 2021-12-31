using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyScript : MonoBehaviour
{
    public GameObject player;
    public float maxDistance = 10000;
    public float sphereRadius = 15f;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("PlayerBody");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckVisibility() || CheckSphere())
            Debug.Log("Lose");
    }

    private bool CheckSphere()
    {
        Vector3 position = transform.position;
        return Vector3.Distance(position, player.transform.position) < sphereRadius;
    }

    private bool CheckVisibility()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        bool didHit = Physics.Raycast(position, player.transform.position - position, out hit, maxDistance);
        if (!didHit)
            return false;
        return hit.collider.gameObject == player;
    }
}
