using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMagicController : MonoBehaviour {

    public Sprite Stage1;
    public Sprite Stage2;
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

            }
            else
            {
                spriteRenderer.sprite = Stage2;
                yield return new WaitForSeconds(0.75F);
            }
            yield return new WaitForSeconds(0.25F);
        }
        Destroy(gameObject, 0.0F);
    }
}
