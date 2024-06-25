using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Teleporter teleporter;

    public float speed = 10f;
    public float timeToLive = 2f;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void FixedUpdate()
    {
        body.velocity = transform.up * speed;

        teleporter.Teleport(gameObject.transform);
    }
}
