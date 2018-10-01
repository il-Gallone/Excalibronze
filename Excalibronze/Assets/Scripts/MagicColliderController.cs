using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicColliderController : MonoBehaviour {

    PlayerController playerController;
    int damage;
    Collider2D fireCol;
    Collider2D waterCol;
    Collider2D airCol;
    Rigidbody2D rigid2D;
	
	void Start () {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        fireCol = gameObject.GetComponent<PolygonCollider2D>();
        waterCol = gameObject.GetComponent<BoxCollider2D>();
        airCol = gameObject.GetComponent<CircleCollider2D>();
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        damage = 5;
        GameObject[] EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < EnemyArray.Length; i++)
        {
            if(EnemyArray[i].GetComponent<LimblessController>() != null)
            {
                Physics2D.IgnoreCollision(airCol, EnemyArray[i].GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(fireCol, EnemyArray[i].GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(waterCol, EnemyArray[i].GetComponent<BoxCollider2D>());
            }
        }
    }
	void Update () {
        if(playerController.direction == "up")
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (playerController.direction == "left")
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (playerController.direction == "down")
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if (playerController.direction == "right")
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        if(playerController.isCasting)
        {
            switch (playerController.magicMode)
            {
                case 0:
                    {
                        fireCol.enabled = true;
                        waterCol.enabled = false;
                        airCol.enabled = false;
                        damage = 3;
                        break;
                    }
                case 1:
                    {
                        fireCol.enabled = false;
                        waterCol.enabled = true;
                        airCol.enabled = false;
                        damage = 2;
                        break;
                    }
                case 3:
                    {
                        fireCol.enabled = false;
                        waterCol.enabled = false;
                        airCol.enabled = true;
                        break;
                    }
                default:
                    {
                        fireCol.enabled = false;
                        waterCol.enabled = false;
                        airCol.enabled = false;
                        damage = 0;
                        break;
                    }
            }
        }
        else
        {
            fireCol.enabled = false;
            waterCol.enabled = false;
            airCol.enabled = false;
            damage = 0;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (playerController.magicMode == 0)
            {
                col.gameObject.SendMessage("FireDamage", damage);
            }
            if (playerController.magicMode == 1)
            {
                col.gameObject.SendMessage("WaterDamage", damage);
            }
        }
        if (col.gameObject.tag == "FireDestructible")
        {
            if(playerController.magicMode == 0)
            {
                col.gameObject.SendMessage("FireDamage", 1);
            }
        }
    }
}
