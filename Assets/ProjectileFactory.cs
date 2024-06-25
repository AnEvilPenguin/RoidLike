using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public GameObject Visual;
    public Projectile Projectile;

    // Consider changing to list for multiple guns.
    private Transform _firingPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFiringPoint(Transform firingPoint)
    {
        _firingPoint = firingPoint;
    }

    public void Launch()
    {
        
        var fired = Instantiate(Projectile, _firingPoint.position, _firingPoint.rotation);
        var visual = Instantiate(Visual, fired.transform.position, fired.transform.rotation, fired.transform);

        // Apply any additional effects
    }
}
