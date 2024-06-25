using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform firingPoint;

    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        var body = bullet.GetComponent<Rigidbody2D>();

        bullet.Origin = firingPoint;

        body.AddForce(firingPoint.up * speed, ForceMode2D.Impulse);
    }
}
