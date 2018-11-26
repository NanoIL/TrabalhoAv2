using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
  
    [Header("Informações da Bullet")]
    public float speed;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

    public delegate void MyDelegate(int num);
    MyDelegate MyFunction;

    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed;
        MyFunction = Printar;
        MyFunction(1); 

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Alien")
        {
            SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.alienDies);

            IncreaseTextUIScore();

            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;
            Destroy(gameObject);

            Object.Destroy(col.gameObject, 0.5f);
        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            Object.Destroy(col.gameObject);

            Stack credits = new Stack();
            credits.Push("Não acerte o escudo, ele te protege!"); 

            foreach (var credit in credits)
                print(credit);

        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    void IncreaseTextUIScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUIComp.text);

        score += 10;

        textUIComp.text = score.ToString();

        if(score >= 210)
        {
            print("Você ganhou!");
        } 
    }
    public void Printar(int num)
     {
     print("Você está jogando algo próximo ao Space Invaders! Seja o jogador número " + num + "!");
}
}
