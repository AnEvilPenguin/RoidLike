using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Teleporter teleporter;

    private SpriteRenderer sprite;
    private Rigidbody2D body;

    public List<Sprite> LargeSprites;
    public List<Sprite> SmallSprites;

    public float Size = 8.0f;
    public float Speed = 5f;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
        SetLargeSprite();

        // rotation, size, mass
        transform.eulerAngles = new Vector3 (0, 0, Random.value * 360.0f);
        transform.localScale = Vector3.one * Size;

        body.mass = Size;
    }

    public void SetLargeSprite () =>
        sprite.sprite = LargeSprites[Random.Range(0, LargeSprites.Count)];

    public void SetSmallSprite () =>
        sprite.sprite = SmallSprites[Random.Range(0, SmallSprites.Count)];

    private void Update()
    {
        teleporter.Teleport(gameObject.transform);
    }

    public void Project(Vector2 direction)
    {
        body.AddForce(direction * Speed, ForceMode2D.Impulse);
    }
} 
