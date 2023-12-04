using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public int velocidade = 10;

   
    
    void Start()
    {
        TryGetComponent(out rb);
        // Physics2D.(thisCollider,ballCollider);
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity =  new Vector2(x, 0)* velocidade;

        
    }

    
}
