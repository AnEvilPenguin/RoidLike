using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Camera myCamera;

    public void Configure (Camera camera)
    {
        myCamera = camera;
    }

    public void Teleport(Transform subject)
    {
        var vertExtent = myCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        var absX = Mathf.Abs(subject.position.x);
        var absY = Mathf.Abs(subject.position.y);

        if (absX >= horzExtent)
        {
            var x = FlipSide(subject.position.x, horzExtent);

            subject.position = new Vector2(x, subject.position.y);
        }

        if (absY >= vertExtent)
        {
            var y = FlipSide(subject.position.y, vertExtent);

            subject.position = new Vector2(subject.position.x, y);
        }
    }

    private float FlipSide(float position, float limit)
    {
        var flip = position * -1;

        return Mathf.Clamp(flip, limit * -1, limit);
    }
}
