using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direcao;
    public int velocidade = 10;

    public bool voando = false;
    void Start()
    {
        TryGetComponent(out rb);
    }

    public void LancarBolinha(Vector2 dir)
    {
        voando = true;
        direcao = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D prb = collision.gameObject.GetComponent<Rigidbody2D>();
            //prb.velocity = prb.velocity;
            direcao = new Vector2((prb.velocity.x + direcao.x)/2, direcao.y);
        }

        if (collision.gameObject.CompareTag("Bloco"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.SubtrairBloco();
        }
        
        if (collision.gameObject.CompareTag("Reforcado"))
        {
            collision.gameObject.GetComponent<BlocoReforcado>().TomouHit();
        }

        if (collision.gameObject.CompareTag("Destruir"))
        {
            GameManager.instance.PerderVida();
            Destroy(gameObject);
        }

        if (collision.contacts.Length == 1)
        {
            direcao = Vector2.Reflect(direcao, collision.contacts[0].normal);
        }
        else
        {
            Vector2 normalMedia = Vector2.zero;
            foreach (var contact in collision.contacts)
            {
                normalMedia = (normalMedia + contact.normal) / 2;
            }
            direcao = Vector2.Reflect(direcao, normalMedia);
        }
        
    }

    void Update()
    {
        if (!voando) return;
        
        rb.velocity = direcao.normalized * velocidade;
    }
}
