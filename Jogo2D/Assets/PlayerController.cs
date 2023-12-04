using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public int velocidade = 10;
    private Vector2 direcao;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direcao = new Vector2(x, y).normalized;
       
    }
    void FixedUpdate()
    {
         rb.velocity = direcao * velocidade;
    }
}
