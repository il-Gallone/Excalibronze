using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour {


    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update () {
        float mana = GameObject.FindWithTag("Player").GetComponent<PlayerController>().mana;
        gameObject.transform.localScale = new Vector3(mana*16F, 1, 0);
        bool burnOut = GameObject.FindWithTag("Player").GetComponent<PlayerController>().burnOut;
        if (burnOut)
        {
            spriteRenderer.color = Color.gray;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
