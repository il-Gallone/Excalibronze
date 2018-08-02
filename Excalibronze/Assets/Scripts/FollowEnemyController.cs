using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyController : EnemyBase {

    
    public float speed = 1.5F;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;

    public float targetX;
    public float targetY;

    private Rigidbody2D rigid2D;

    // Use this for initialization
    void Start ()
    {
        health = 16;
        // Init rigid2d
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onScreenX == GameManager.instance.currentScreenX && onScreenY == GameManager.instance.currentScreenY && !GameManager.instance.isScreenMoving)
        {
            targetX = GameObject.FindWithTag("Player").transform.position.x;
            targetY = GameObject.FindWithTag("Player").transform.position.y;
            Vector2 target = new Vector2(targetX, targetY);
            if (stunSecs <= 0)
            {
                rigid2D.MovePosition(Vector2.MoveTowards(rigid2D.position, target, speed * Time.deltaTime));
                if (Mathf.Abs(targetX) >= Mathf.Abs(targetY))
                {
                    if (targetX > 0)
                    {
                        spriteRenderer.sprite = right;
                    }
                    else
                    {
                        spriteRenderer.sprite = left;
                    }
                }
                else
                {
                    if (targetY > 0)
                    {
                        spriteRenderer.sprite = up;
                    }
                    else
                    {
                        spriteRenderer.sprite = down;
                    }
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamageController")
        {
            if (stunSecs <= 0)
            {
                col.gameObject.SendMessage("Damage", 1);
            }
        }

    }
}
