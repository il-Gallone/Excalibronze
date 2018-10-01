using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimblessController : EnemyBase {

    
    public float targetX;
    public float targetY;

    public float speed = 2.0F;

    public bool awake = false;
    public bool hitWall = false;

    public int attackIter = 0;

    private Rigidbody2D rigid2D;

    private Collider2D boxCol;

    private Collider2D circleCol;

    // Use this for initialization
    void Start ()
    {
        health = 0;
        // Init rigid2d
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        circleCol = gameObject.GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(circleCol, GameObject.FindWithTag("DamageController").GetComponent<Collider2D>());
        boxCol.enabled = false;
    }
	
	void Awaken ()
    {
        GameObject.FindWithTag("BossDoor").SendMessage("Lock");
        gameObject.BroadcastMessage("OpenMouth");
        StartCoroutine("HealthActivate");
    }

    IEnumerator HealthActivate()
    {
        for (float i = health; i <= 300; i += 150*Time.deltaTime)
        {
            health = (int)i;
            yield return new WaitForSeconds(0.1F);
        }
        health = 300;
        awake = true;
        StartCoroutine("AttackSequence");
    }

    IEnumerator AttackSequence()
    {
        while(awake)
        {
            if(attackIter != 6)
            {
                attackIter++;
                targetX = GameObject.FindWithTag("Player").transform.position.x;
                targetY = GameObject.FindWithTag("Player").transform.position.y;
                Vector2 target = new Vector2(targetX - transform.position.x, targetY - transform.position.y);
                target = target.normalized*16 + new Vector2(32, 20);
                for(float i = -1F; i <= 1; i+=2*Time.deltaTime)
                {
                    rigid2D.MovePosition(Vector2.MoveTowards(rigid2D.position, target, speed * Time.deltaTime));
                    gameObject.BroadcastMessage("Jump", 1 - Mathf.Abs(i));
                    yield return null;
                }
                gameObject.BroadcastMessage("Land");
                boxCol.enabled = true;
                yield return new WaitForSeconds(1F);
                boxCol.enabled = false;
            }
            if(attackIter == 6)
            {
                hitWall = false;
                attackIter = 0;
                boxCol.enabled = true;
                targetX = GameObject.FindWithTag("Player").transform.position.x;
                targetY = GameObject.FindWithTag("Player").transform.position.y;
                Vector2 target = new Vector2(targetX-transform.position.x, targetY- transform.position.y);
                target = target.normalized*16 + new Vector2(32, 20);
                while(!hitWall)
                {
                    rigid2D.MovePosition(Vector2.MoveTowards(rigid2D.position, target, speed* 3 * Time.deltaTime));
                    gameObject.BroadcastMessage("Roll", target.x);
                    yield return null;
                }
                gameObject.BroadcastMessage("Straighten");
                yield return new WaitForSeconds(2F);
                boxCol.enabled = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "DamageController")
        {
            collider.gameObject.SendMessage("Damage", 1);
        }
        if (collider.gameObject.tag == "Obstacle")
        {
            hitWall = true;
        }
        if (collider.gameObject.tag == "Door")
        {
            hitWall = true;
        }
        if (collider.gameObject.tag == "BossDoor")
        {
            hitWall = true;
        }
    }
}
