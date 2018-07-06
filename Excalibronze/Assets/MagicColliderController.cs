using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicColliderController : MonoBehaviour {

    PlayerController playerController;
    bool colliding = false;
    int damage;
    Collider2D fireCol;
    Collider2D waterCol;
	
	void Start () {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        fireCol = gameObject.GetComponent<PolygonCollider2D>();
        waterCol = gameObject.GetComponent<BoxCollider2D>();
        waterCol.enabled = false;
        damage = 5;
    }
	void Update () {
        switch (playerController.magicMode)
        {
            case 0:
                {
                    fireCol.enabled = true;
                    waterCol.enabled = false;
                    damage = 3;
                    break;
                }
            case 1:
                {
                    fireCol.enabled = false;
                    waterCol.enabled = true;
                    damage = 2;
                    break;
                }
            default:
                {
                    fireCol.enabled = false;
                    waterCol.enabled = false;
                    damage = 0;
                    break;
                }
        }
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
            colliding = true;
        }
        else
        {
            colliding = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (colliding)
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
        }
    }
}
