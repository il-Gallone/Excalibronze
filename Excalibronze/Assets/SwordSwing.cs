﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Sprite shortSword;
    public Sprite hookedScimitar;
    public Sprite broadSword;
    public int mode = 0; //0 = Shortsword, 1 = Hooked Scimitar, 2 = Broadsword, 3 = Dual Daggers.

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
        if(mode == 0)
        {
            spriteRenderer.sprite = shortSword;
            StartCoroutine(SwordAnimation(new Vector3(0, 0, 90), 0.25F));
        }
        if(mode == 1)
        {
            spriteRenderer.sprite = hookedScimitar;
            StartCoroutine(HookedScimitarAnimation(new Vector3(0, 0, 90), 0.15F));
        }
        if (mode == 2)
        {
            spriteRenderer.sprite = broadSword;
            StartCoroutine(SwordAnimation(new Vector3(0, 0, 90), 0.5F));
        }
    }

    void ChangeMode(int modeSelect)
    {
        mode = modeSelect;
    }

    IEnumerator SwordAnimation(Vector3 angle, float time)
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

    IEnumerator HookedScimitarAnimation(Vector3 angle, float time)
    {
        var startAngle = transform.rotation;
        var endAngle = Quaternion.Euler(transform.eulerAngles + angle);
        for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.rotation = Quaternion.Lerp(startAngle, endAngle, i);
            yield return null;
        }
        spriteRenderer.flipX = true;
        for (float i = 0.0F; i < 1; i += Time.deltaTime / (time*2))
        {
            transform.rotation = Quaternion.Lerp(endAngle, startAngle, i);
            yield return null;
        }
        spriteRenderer.flipX = false;
        spriteRenderer.enabled = false;
    }
}
