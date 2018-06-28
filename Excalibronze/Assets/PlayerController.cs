using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 3.0F;
    public float attackCooldown = 0.25F;
    public int magicMode = 0;
    public bool canAttack = true;
    public string direction = "up";
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Cooldowns());
    }
	
	void FixedUpdate () {
        if (Input.GetKey("joystick 1 button 8") || Input.GetKey("tab") || Input.GetKey("backspace"))
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
            {
                if (Input.GetAxis("Horizontal") < 0.0F)
                {
                    GameObject sword = GameObject.FindWithTag("Weapon");
                    sword.SendMessage("ChangeMode", 3);
                    magicMode = 3;
                }
                if (Input.GetAxis("Horizontal") > 0.0F)
                {
                    GameObject sword = GameObject.FindWithTag("Weapon");
                    sword.SendMessage("ChangeMode", 1);
                    magicMode = 1;
                    attackCooldown = 0.60F;
                }
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) < Mathf.Abs(Input.GetAxis("Vertical")))
            {
                if (Input.GetAxis("Vertical") < 0.0F)
                {
                    GameObject sword = GameObject.FindWithTag("Weapon");
                    sword.SendMessage("ChangeMode", 2);
                    magicMode = 2;
                    attackCooldown = 0.5F;
                }
                if (Input.GetAxis("Vertical") > 0.0F)
                {
                    int zero = 0; //Potential work around for bug
                    GameObject sword = GameObject.FindWithTag("Weapon");
                    sword.SendMessage("ChangeMode", zero);
                    magicMode = 0;
                    attackCooldown = 0.25F;
                }
            }
        }
        else
        {
            if (Input.GetAxis("Vertical") <= -0.2)
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
                spriteRenderer.sprite = down;
                direction = "down";
            }
            if (Input.GetAxis("Vertical") >= 0.2)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                spriteRenderer.sprite = up;
                direction = "up";
            }
            if (Input.GetAxis("Horizontal") <= -0.2)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = left;
                direction = "left";
            }
            if (Input.GetAxis("Horizontal") >= 0.2)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                spriteRenderer.sprite = right;
                direction = "right";
            }
            if ((Input.GetKeyDown("joystick 1 button 2") || Input.GetKeyDown("x") || Input.GetKeyDown("k")) && canAttack)
            {
                GameObject sword = GameObject.FindWithTag("Weapon");
                sword.SendMessage("Swing", direction);
                canAttack = false;
            }
        }
    }
    IEnumerator Cooldowns()
    {
        while (true)
        {
            if (!canAttack)
            {
                yield return new WaitForSeconds(attackCooldown);
                canAttack = true;
            }
            yield return new WaitForSeconds(0.0F);
        }
    }

}
