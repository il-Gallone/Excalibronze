using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Collider2D[] colliders;
    public Sprite shortSword;
    public Sprite hookedScimitar;
    public Sprite broadSword;
    public Sprite dagger;
    public int damage = 2;
    public int mode = 0; //0 = Shortsword, 1 = Hooked Scimitar, 2 = Broadsword, 3 = Dual Daggers.
    public bool swingRight = false;
    
    bool colliding = false;

    // Use this for initialization
    void Start ()
    {
        colliders = gameObject.GetComponents<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
    }
	
	void Swing (string direction)
    {
        if (direction == "up")
        {
            transform.eulerAngles = new Vector3(0, 0, -40);
            spriteRenderer.sortingOrder = 2;
        }
        if (direction == "right")
        {
            transform.eulerAngles = new Vector3(0, 0, -130);
            spriteRenderer.sortingOrder = 4;
        }
        if (direction == "down")
        {
            transform.eulerAngles = new Vector3(0, 0, 140);
            spriteRenderer.sortingOrder = 4;
        }
        if (direction == "left")
        {
            transform.eulerAngles = new Vector3(0, 0, 50);
            spriteRenderer.sortingOrder = 2;
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
        if(mode == 3)
        {
            spriteRenderer.sprite = dagger;
            StartCoroutine(DaggerAnimation(new Vector3(0, 0, 90), 0.12F));
        }
    }

    void ChangeMode(int modeSelect)
    {
        mode = modeSelect;
        if (mode != 2 && mode != 3)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = false;
            colliders[2].enabled = false;
            damage = 2;
        }
        if (mode == 2)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true;
            colliders[2].enabled = false;
            damage = 4;
        }
        if (mode == 3)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = false;
            colliders[2].enabled = true;
            damage = 1;
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


    IEnumerator DaggerAnimation(Vector3 angle, float time)
    {
        var startAngle = transform.rotation;
        var endAngle = Quaternion.Euler(transform.eulerAngles + angle);
        while (GameObject.FindWithTag("Player").GetComponent<PlayerController>().daggerSwinging)
        {
            if (!swingRight)
            {
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.rotation = Quaternion.Lerp(startAngle, endAngle, i);
                    swingRight = true;
                    yield return null;
                }
            }
            else
            {
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.rotation = Quaternion.Lerp(endAngle, startAngle, i);
                    swingRight = false;
                    yield return null;
                }
            }
        }
        spriteRenderer.enabled = false;
        colliding = false;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (colliding)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.SendMessage("Damage", damage);
                if(mode == 3)
                {
                    int critChance = Random.Range(0, 4);
                    if(critChance == 3)
                    {
                        col.gameObject.SendMessage("Damage", damage*5);
                    }
                }
            }
        }
    }
}
