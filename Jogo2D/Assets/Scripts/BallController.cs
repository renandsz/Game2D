using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 5;
    public Vector2 direcao;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out rb);
        direcao = Random.insideUnitCircle;
        direcao = new Vector2(direcao.x, 1).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bloco"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Ref"))
        {
            collision.gameObject.GetComponent<BlocoRef>().TomouHit();
        }
        if(collision.gameObject.CompareTag("Destruir"))
        {
           GameManager.instance.ReiniciarCena();
        }
        if (collision.contacts.Length == 1)
        {
            direcao = Vector2.Reflect(direcao, collision.contacts[0].normal);
        }
        else
        {
            Vector2 normalMedia = Vector2.zero;
            foreach (var ponto in collision.contacts)
            {
                normalMedia = (normalMedia + ponto.normal) / 2;
            }
            direcao = Vector2.Reflect(direcao, normalMedia);
        }
       
    }
    void FixedUpdate()
    {
        rb.velocity = direcao * velocidade;
    }
}
