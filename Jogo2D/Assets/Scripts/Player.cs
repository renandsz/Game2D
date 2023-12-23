using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public int velocidade = 10;
    public float x;
    public Vector2 direcaoPlayer;
    
    void Start()
    {
        TryGetComponent(out rb);
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        direcaoPlayer = new Vector2(x, 0);

    }

    private void FixedUpdate()
    {
        rb.velocity = direcaoPlayer * velocidade;
    }
}
