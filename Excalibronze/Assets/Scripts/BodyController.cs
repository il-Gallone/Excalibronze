using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    public Sprite dormant;
    public Sprite toothy;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void OpenMouth()
    {
        spriteRenderer.sprite = toothy;
    }

    void Jump(float height)
    {
        transform.localPosition = new Vector3(0, height, 0);
    }

    void Land()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Roll(float targetX)
    {
        spriteRenderer.sprite = dormant;
        if (targetX - transform.position.x > 0)
        {
            transform.Rotate(new Vector3(0, 0, -180 * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));
        }
    }
    
    void Straighten()
    {
        spriteRenderer.sprite = toothy;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
