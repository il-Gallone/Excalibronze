using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 3.0F;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	void FixedUpdate () {
        if (Input.GetAxis("Vertical") <= -0.2)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            spriteRenderer.sprite = down;
        }
        if (Input.GetAxis("Vertical") >= 0.2)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            spriteRenderer.sprite = up;
        }
        if (Input.GetAxis("Horizontal") <= -0.2)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            spriteRenderer.sprite = left;
        }
        if (Input.GetAxis("Horizontal") >= 0.2)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            spriteRenderer.sprite = right;
        }
    }
}
