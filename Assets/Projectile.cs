using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D body;
    public Teleporter teleporter;

    public GameObject hitEffect;

    public float timeToLive = 2.4f;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void Update()
    {
        teleporter.Teleport(gameObject.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
