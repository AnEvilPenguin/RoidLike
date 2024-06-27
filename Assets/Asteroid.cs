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

    public AsteroidSpawner Source;

    public float Size = 8.0f;
    public float Speed = 5f;
    public float SmallestSize = 2.0f;

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
    }

    public void CalculateSize()
    {
        transform.localScale = Vector3.one * Size / 3;

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
        body.AddForce(direction * Mathf.Max(Speed, 1), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Do a thing
            Debug.Log("Hit Bullet");


            if (Size / 2 < SmallestSize)
            {
                Destroy(gameObject);
                return;
            }

            float random = Random.value * 45.0f;

            NewAsteroid(random);
            NewAsteroid(-random);

            // FIXME do this better
            Destroy(gameObject);
            return;
        }

        // TODO spawn two smaller asteroids
        // TODO split them off at ~ 45 deg from current direction
        // Fire off event for score?
    }

    private void Split()
    {
        float randomAngle = Random.value * 45.0f;
    }

    private void NewAsteroid(float angle)
    {
        Asteroid roid = Instantiate(this, transform.position, Random.rotation);

        Source.TrackAsteroid(roid);

        roid.Size = Size / 2;
        roid.Speed = Speed;
        roid.Source = Source;

        roid.CalculateSize();

        var dir = Quaternion.AngleAxis(angle, Vector3.forward) * _direction;
        roid.Project(dir);
    }
} 
