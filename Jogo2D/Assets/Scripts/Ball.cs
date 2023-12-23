using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direcao;
    public int velocidadeBase = 10;
    private int velocidadeAtual;

   // public bool voando = false;
    void Start()
    {
        TryGetComponent(out rb);
        velocidadeAtual = 0;
    }

    public void LancarBolinha(Vector2 dir)
    {
        direcao = new Vector2(dir.x, 1);
        velocidadeAtual = velocidadeBase;
        rb.velocity = direcao.normalized * velocidadeAtual;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            direcao += GameManager.instance._player.direcaoPlayer;
            direcao.Normalize();
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

        direcao = Vector2.Reflect(direcao, collision.contacts[0].normal);
        rb.velocity = direcao.normalized * velocidadeAtual;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direcao,10);
       
        Debug.DrawRay(transform.position, direcao * 10,Color.magenta);
        Debug.DrawRay(hit.point, hit.normal * 5,Color.yellow);
        if (hit.collider)
        {
            Vector2 r = Vector2.Reflect(direcao, hit.normal);
            Debug.DrawRay(hit.point, r * 5,Color.blue);
        }
    }
}
