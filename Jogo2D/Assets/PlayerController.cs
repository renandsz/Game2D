using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 10;
    public Vector2 direcao;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        direcao = new Vector2(x, 0);
    }

    void FixedUpdate()
    {
        rb.velocity = direcao.normalized * velocidade;
    }
}
