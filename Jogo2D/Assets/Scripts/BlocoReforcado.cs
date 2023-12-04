using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoReforcado : MonoBehaviour
{
    private SpriteRenderer renderer;
    public int numeroDeHits = 3;

    private void Start()
    {
        TryGetComponent(out renderer);
    }

    public void TomouHit()
    {
        numeroDeHits--;
        renderer.color *= 1.5f;
        if (numeroDeHits <= 0)
        {
            GameManager.instance.SubtrairBloco();
            Destroy(gameObject);
        }
    }
}
