using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.PackageManager;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Renderer myRenderer;
    public Camera myCamera;

    public Teleporter teleporter;

    public float Acceleration = 1;
    public float MaxSpeed = 5;

    public float MaxAngularMomentum = 5;

    public float angularMomentum = 0;

    // Start is called before the first frame update
    void Start()
    {
        teleporter.Configure(myCamera);
        ItemCard.PlayerItemSelected += HandlePlayerItemSelected;
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

        teleporter.Teleport(gameObject.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("A collsion occured");
    }

    private void HandlePlayerItemSelected(object sender, PlayerItemSelectedEventArgs e)
    {
        var item = e.Item;
        switch (item.Type)
        {
            case PlayerItemType.Acceleration:
                {
                    Acceleration += item.Value;
                    return;
                }

            case PlayerItemType.Rotation:
                {
                    MaxAngularMomentum += item.Value;
                    return;
                }

            case PlayerItemType.Speed:
                {
                    MaxSpeed += item.Value;
                    return;
                }

            default:
                {
                    Debug.LogError($"Unknown PlayeItem {item.Type}");
                    return;
                }
        }
    }
}
