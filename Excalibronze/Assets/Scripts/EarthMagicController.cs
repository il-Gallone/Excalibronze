using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMagicController : MonoBehaviour {

    public Sprite Stage1;
    public Sprite Stage2;
    bool colliding = false;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Run());
	}
	
	IEnumerator Run()
    {
        for(int i = 0; i < 3; i++)
        {
            if(i != 1)
            {
                spriteRenderer.sprite = Stage1;
                colliding = false;
            }
            else
            {
                spriteRenderer.sprite = Stage2;
                colliding = true;
                yield return new WaitForSeconds(0.75F);
            }
            yield return new WaitForSeconds(0.25F);
        }
        Destroy(gameObject, 0.0F);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (colliding)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.SendMessage("EarthDamage", 4);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (colliding)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.SendMessage("EarthDamage", 4);
            }
        }
    }
}
