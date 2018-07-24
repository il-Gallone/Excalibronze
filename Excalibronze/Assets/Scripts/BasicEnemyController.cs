using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEnemyController : EnemyBase {
    
    public string direction = "up";
    public float speed = 2.0F;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;

	private Rigidbody2D rigid2D;


    void Start () {
		health = 10;
		// Init rigid2d
		rigid2D = gameObject.GetComponent<Rigidbody2D>();

        StartCoroutine(Wander());

	}

    

    IEnumerator Wander()
    {
        int directionChoice;
        while (true)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            directionChoice = Random.Range(0, 4);
            switch (directionChoice)
            {
                case 0:
                    {
                        direction = "up";
                        spriteRenderer.sprite = up;
                        break;
                    }
                case 1:
                    {
                        direction = "right";
                        spriteRenderer.sprite = right;
                        break;
                    }
                case 2:
                    {
                        direction = "down";
                        spriteRenderer.sprite = down;
                        break;
                    }
                case 3:
                    {
                        direction = "left";
                        spriteRenderer.sprite = left;
                        break;
                    }
                default:
                    break;
            }
            if(direction == "up")
            {
				for (float i = transform.position.y; i <= transform.position.y + 1; i += speed*Time.deltaTime)
                {

                    if (stunSecs > 0)
                    {
                        i -= speed * Time.deltaTime;
                    }
                    else
                    {
                        Vector2 force = new Vector2(0, speed);
                        rigid2D.MovePosition(rigid2D.position + force * Time.deltaTime);
                    }
                    yield return null;
                }
            }
            if (direction == "right")
			{
				for (float i = transform.position.x; i <= transform.position.x + 1; i += speed*Time.deltaTime)
				{

                    if (stunSecs > 0)
                    {
                        i -= speed * Time.deltaTime;
                    }
                    else
                    {
                        Vector2 force = new Vector2(speed, 0);
                        rigid2D.MovePosition(rigid2D.position + force * Time.deltaTime);
                    }
                    yield return null;
				}
            }
            if (direction == "down")
			{
				var startPos = transform.position.y;
				var endPos = transform.position.y - 1;
				for (float i = transform.position.y; i >= transform.position.y - 1; i -= speed*Time.deltaTime)
                {
                    if (stunSecs > 0)
                    {
                        i += speed * Time.deltaTime;
                    }
                    else
                    {
                        Vector2 force = new Vector2(0, speed);
                        rigid2D.MovePosition(rigid2D.position - force * Time.deltaTime);
                    }
                    yield return null;
				}
            }
            if (direction == "left")
			{
				var startPos = transform.position.x;
				var endPos = transform.position.x - 1;
				for (float i = transform.position.x; i >= transform.position.x - 1; i -= speed*Time.deltaTime)
                {
                    if (stunSecs > 0)
                    {
                        i += speed * Time.deltaTime;
                    }
                    else
                    {
                        Vector2 force = new Vector2(speed, 0);
                        rigid2D.MovePosition(rigid2D.position - force * Time.deltaTime);
                    }
                    yield return null;
				}
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamageController")
        {
            col.gameObject.SendMessage("Damage", 1);
        }
        
    }
}
