using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Renderer myRenderer;
    public Camera myCamera;

    public float Acceleration = 1;
    public float MaxSpeed = 5;

    public float MaxAngularMomentum = 5;

    public float angularMomentum = 0;
    public float velocity = 0;

    public bool flippedX = false;
    public bool flippedY = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rads = myRigidbody.rotation * Mathf.Deg2Rad;
        Vector2 vec = Acceleration * new Vector2(Mathf.Cos(rads), Mathf.Sin(rads));

        var up = Input.GetKey(KeyCode.UpArrow);

        var left = Input.GetKey(KeyCode.LeftArrow);
        var right = Input.GetKey(KeyCode.RightArrow);

        if (up)
        {
            myRigidbody.velocity = Vector2.ClampMagnitude(vec + myRigidbody.velocity, MaxSpeed);
        }

        if (left && right)
        {
            angularMomentum = 0;
        }
        else if (left)
        {
            angularMomentum = angularMomentum >= MaxAngularMomentum ? MaxAngularMomentum : angularMomentum + 1;
            myRigidbody.rotation += angularMomentum * Time.deltaTime;
        }
        else if (right)
        {
            angularMomentum = angularMomentum >= MaxAngularMomentum ? MaxAngularMomentum : angularMomentum + 1;
            myRigidbody.rotation -= angularMomentum * Time.deltaTime;
        }


        Teleport();
    }

    private void Teleport()
    {
        var vertExtent = myCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        var absX = Mathf.Abs(transform.position.x);
        var absY = Mathf.Abs(transform.position.y);

        if (absX >= horzExtent)
        {
            var x = FlipSide(transform.position.x, horzExtent);

            transform.position = new Vector2(x, transform.position.y);
        }

        if (absY >= vertExtent)
        {
            var y = FlipSide(transform.position.y, vertExtent);

            transform.position = new Vector2(transform.position.x, y);

        }
    }

    private float FlipSide(float position, float limit)
    {
        var flip = position * -1;

        return Mathf.Clamp(flip, limit * -1, limit);
    }
}
