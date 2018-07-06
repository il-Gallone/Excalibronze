using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Collider2D[] colliders;
    public Sprite shortSword;
    public Sprite hookedScimitar;
    public Sprite broadSword;
    public int damage = 2;
    public int mode = 0; //0 = Shortsword, 1 = Hooked Scimitar, 2 = Broadsword, 3 = Dual Daggers.
    
    bool colliding = false;

    // Use this for initialization
    void Start ()
    {
        colliders = gameObject.GetComponents<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        colliders[1].enabled = false;
    }
	
	void Swing (string direction)
    {
        if (mode != 3)
        {
            if (direction == "up")
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
            colliding = true;
            if (mode == 0)
            {
                spriteRenderer.sprite = shortSword;
                StartCoroutine(SwordAnimation(new Vector3(0, 0, 90), 0.25F));
            }
            if (mode == 1)
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
    }

    void ChangeMode(int modeSelect)
    {
        mode = modeSelect;
        if (mode != 2 && mode != 3)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = false;
            damage = 2;
        }
        if (mode == 2)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true;
            damage = 4;
        }
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
        colliding = false;
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
        colliding = false;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (colliding)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.SendMessage("Damage", damage);
            }
        }
    }
}
