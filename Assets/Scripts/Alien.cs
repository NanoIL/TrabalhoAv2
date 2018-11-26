using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

    [Multiline(10)]
    public string alienDF;

    [Header("Informações do Alien")]
    public float speed;

    public string Tiro { get; set; }

    public Rigidbody2D rigidBody;

    public Sprite startingImage;

    public Sprite altImage;

    private SpriteRenderer spriteRenderer;

    public float secBeforeSpriteChange;

    public GameObject alienBullet;

    public float minFireRateTime;

    public float maxFireRateTime;

    public float baseFireWaitTime;

    public Sprite explodedShipImage;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(ChangeAlienSprite());

        baseFireWaitTime = baseFireWaitTime + Random.Range
          (minFireRateTime, maxFireRateTime);
    }

    void Turn(int direction)
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
}
    void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }
        if (col.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }
        if (col.gameObject.name == "Bullet")
        {
            SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.alienDies);
            Destroy(gameObject);
        }
    }
    public IEnumerator ChangeAlienSprite()
    {
        while (true)
        {
            if (spriteRenderer.sprite == startingImage)
            {
                spriteRenderer.sprite = altImage;
               
            }
            else
            {
                spriteRenderer.sprite = startingImage;
                SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.alienBuzz2);
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);
        }

    }

    void FixedUpdate()
    {
        if (Time.time > baseFireWaitTime)
        {
            baseFireWaitTime = baseFireWaitTime + Random.Range
                     (minFireRateTime, maxFireRateTime);

            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(gameObject);
            Object.Destroy(col.gameObject, 0.5f);
        }
    }
}
