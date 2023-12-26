using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoRef : MonoBehaviour
{
    public int hp = 3;
    private SpriteRenderer renderer;
    void Start()
    {
        TryGetComponent(out renderer);
    }
    public void TomouHit()
    {
        hp--;
        renderer.color *= 1.5f;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
