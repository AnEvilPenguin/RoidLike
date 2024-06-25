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

    private Vector2 _direction;

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
        CalculateSize();
    }

    public void CalculateSize()
    {
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
        _direction = direction;
        body.AddForce(direction * Speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Do a thing
            Debug.Log("Hit Bullet");

            var size = Size / 2;

            if (size < 1)
            {
                Destroy(gameObject);
                return;
            }

            Asteroid roid1 = Instantiate(this, transform.position, Random.rotation);
            Asteroid roid2 = Instantiate(this, transform.position, Random.rotation);

            roid1.Size = size;
            roid2.Size = size;

            roid1.Speed = Speed;
            roid2.Speed = Speed;

            roid1.CalculateSize();
            roid2.CalculateSize();

            float random = Random.value * 45.0f;

            var dir1 = Quaternion.AngleAxis(random, Vector3.forward) * _direction;
            var dir2 = Quaternion.AngleAxis(-random, Vector3.forward) * _direction;

            roid1.Project(dir1);
            roid2.Project(dir2);

            // FIXME do this better
            Destroy(gameObject);
            return;
        }

        // TODO spawn two smaller asteroids
        // TODO split them off at ~ 45 deg from current direction
        // Fire off event for score?
    }
} 
