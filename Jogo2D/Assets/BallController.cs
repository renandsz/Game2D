using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 5;
    private Vector2 direcao;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
        direcao = Random.insideUnitCircle.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direcao = Vector2.Reflect(direcao, collision.contacts[0].normal);        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direcao * velocidade;
    }
}
