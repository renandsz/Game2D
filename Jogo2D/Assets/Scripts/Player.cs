using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public int velocidade = 10;
    void Start()
    {
        TryGetComponent(out rb);
    }
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direcao = new Vector2(x, y);

        rb.velocity = direcao.normalized * velocidade;        
    }
}
