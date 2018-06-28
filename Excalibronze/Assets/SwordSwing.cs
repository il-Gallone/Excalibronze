using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour {

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
	}
	
	void Swing (string direction)
    {
        if(direction == "up")
        {
            transform.eulerAngles = new Vector3(0, 0, -40);
        }
        if (direction == "right")
        {
            transform.eulerAngles = new Vector3(0, 0, -130);
        }
        if (direction == "down")
        {
            transform.eulerAngles = new Vector3(0, 0, 140);
        }
        if (direction == "left")
        {
            transform.eulerAngles = new Vector3(0, 0, 50);
        }
        spriteRenderer.enabled = true;
        StartCoroutine(Animation(new Vector3(0, 0, 90), 0.25F));
    }

    IEnumerator Animation(Vector3 angle, float time)
    {
        var startAngle = transform.rotation;
        var endAngle = Quaternion.Euler(transform.eulerAngles + angle);
        for(float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.rotation = Quaternion.Lerp(startAngle, endAngle, i);
            yield return null;
        }
        spriteRenderer.enabled = false;
    }
}
