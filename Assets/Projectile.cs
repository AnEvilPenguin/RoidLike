using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Teleporter teleporter;

    public float failsafeTime = 2;
    public float failsafeDistance = 1;

    private float distance;
    private float previousDistance;

    public Transform Origin;

    void Start()
    {
        Destroy(gameObject, failsafeTime);
    }

    private void Update()
    {
        FailSafe();

        teleporter.Teleport(gameObject.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void FailSafe()
    {
        previousDistance = distance;
        distance = (Origin.position - gameObject.transform.position).magnitude;

        if (previousDistance > distance && distance < 1)
        {
            Debug.Log($"Failsafe at {distance}");
            Destroy(gameObject);
        }
    }
}
