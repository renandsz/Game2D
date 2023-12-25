using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 10;
    public Vector2 direcao;
    
    void Start()
    {
        TryGetComponent(out rb);
        direcao = Vector2.zero;
    }

    public void DispararBolinha(float inputX)
    {
        direcao = new Vector2(inputX, 1);
        rb.velocity = direcao.normalized * velocidade;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        direcao = Vector2.Reflect(direcao, col.contacts[0].normal);
        rb.velocity = direcao.normalized * velocidade;
    }
}
