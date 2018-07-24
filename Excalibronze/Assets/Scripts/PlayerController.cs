﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 3.0F;
    public float attackCooldown = 0.25F;
    public int health = 10;
    public int healthCap = 10;
    public float mana = 25.0F;
    public float manaCap = 25.0F;
    public float magicCharge = 0.0F;
    public int magicMode = 0;
    public bool isCasting = false;
    public bool canAttack = true;
    public bool burnOut = false;
    public float invincSecs = 0.0F;
    public string direction = "up";
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid2D;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject earthPrefab;
    public GameObject particlePrefab;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(AttackCooldown());
        StartCoroutine(MagicCooldown());
    }
	
	void FixedUpdate () {
        if (mana < manaCap)
        {
            mana += Time.deltaTime;
        }
        if (mana > manaCap)
        {
            mana = manaCap;
        }
        if (invincSecs > 0)
        {
            invincSecs -= Time.deltaTime;
        }
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
                Vector2 force = new Vector2(0, speed);
                rigid2D.MovePosition(rigid2D.position - force * Time.deltaTime);
                spriteRenderer.sprite = down;
                direction = "down";
            }
            if (Input.GetAxis("Vertical") >= 0.2)
            {
                Vector2 force = new Vector2(0, speed);
                rigid2D.MovePosition(rigid2D.position + force * Time.deltaTime);
                spriteRenderer.sprite = up;
                direction = "up";
            }
            if (Input.GetAxis("Horizontal") <= -0.2)
            {
                Vector2 force = new Vector2(speed, 0);
                rigid2D.MovePosition(rigid2D.position - force * Time.deltaTime);
                spriteRenderer.sprite = left;
                direction = "left";
            }
            if (Input.GetAxis("Horizontal") >= 0.2)
            {
                Vector2 force = new Vector2(speed, 0);
                rigid2D.MovePosition(rigid2D.position + force * Time.deltaTime);
                spriteRenderer.sprite = right;
                direction = "right";
            }
            if((Input.GetKey("joystick 1 button 1") || Input.GetKey("c") || Input.GetKey("l")) && mana > 0 && !burnOut)
            {
                isCasting = true;
                if (magicMode == 0)
                {
                    mana -= Time.deltaTime * 5;
                    for (int i = -1; i < 2; i++)
                    {
                        GameObject fire;
                        if (direction == "up")
                        {
                            fire = (GameObject)GameObject.Instantiate(firePrefab, transform.position + new Vector3(0, 0.5F, 0), transform.rotation);
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2 + i * 2, 2 + i * 2), 9 - Mathf.Abs(i * 2), 0);
                            if (Input.GetAxis("Vertical") >= 0.2)
                            {
                                fire.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 3);
                            }
                            Destroy(fire, 0.3F);
                        }
                        if (direction == "right")
                        {
                            fire = (GameObject)GameObject.Instantiate(firePrefab, transform.position + new Vector3(0.5F, 0, 0), transform.rotation);
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(9 - Mathf.Abs(i * 2), Random.Range(-2 + i * 2, 2 + i * 2), 0);
                            if (Input.GetAxis("Horizontal") >= 0.2)
                            {
                                fire.GetComponent<Rigidbody2D>().velocity += new Vector2(3, 0);
                            }
                            Destroy(fire, 0.3F);
                        }
                        if (direction == "left")
                        {
                            fire = (GameObject)GameObject.Instantiate(firePrefab, transform.position + new Vector3(-0.5F, 0, 0), transform.rotation);
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(-9 + Mathf.Abs(i*2), Random.Range(-2 + i * 2, 2 + i * 2), 0);
                            if (Input.GetAxis("Horizontal") <= -0.2)
                            {
                                fire.GetComponent<Rigidbody2D>().velocity += new Vector2(-3, 0);
                            }
                            Destroy(fire, 0.3F);
                        }
                        if (direction == "down")
                        {
                            fire = (GameObject)GameObject.Instantiate(firePrefab, transform.position + new Vector3(0, -0.5F, 0), transform.rotation);
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2 + i * 2, 2 + i * 2), -9 + Mathf.Abs(i * 2), 0);
                            if (Input.GetAxis("Vertical") <= -0.2)
                            {
                                fire.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -3);
                            }
                            Destroy(fire, 0.3F);
                        }

                    }
                    if(mana <= 0)
                    {
                        mana = 0;
                        burnOut = true;
                    }
                }
                if(magicMode == 1)
                {
                    mana -= Time.deltaTime * 3;
                    for (float i = -1.0F; i < 2.0F; i++)
                    {
                        GameObject water;
                        if (direction == "up")
                        {
                            water = (GameObject)GameObject.Instantiate(waterPrefab, transform.position + new Vector3(0, 0.5F, 0), transform.rotation);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(i/3.0F, 11 - Mathf.Abs(i * 2), 0);
                            if (Input.GetAxis("Vertical") >= 0.2)
                            {
                                water.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 3);
                            }
                            Destroy(water, 0.5F);
                        }
                        if (direction == "right")
                        {
                            water = (GameObject)GameObject.Instantiate(waterPrefab, transform.position + new Vector3(0.5F, 0, 0), transform.rotation);
                            water.transform.Rotate(0, 0, -90);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(11 - Mathf.Abs(i * 2), i / 3.0F, 0);
                            if (Input.GetAxis("Horizontal") >= 0.2)
                            {
                                water.GetComponent<Rigidbody2D>().velocity += new Vector2(3, 0);
                            }
                            Destroy(water, 0.5F);
                        }
                        if (direction == "left")
                        {
                            water = (GameObject)GameObject.Instantiate(waterPrefab, transform.position + new Vector3(-0.5F, 0, 0), transform.rotation);
                            water.transform.Rotate(0, 0, 90);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(-11 + Mathf.Abs(i * 2), i / 3.0F, 0);
                            if (Input.GetAxis("Horizontal") <= -0.2)
                            {
                                water.GetComponent<Rigidbody2D>().velocity += new Vector2(-3, 0);
                            }
                            Destroy(water, 0.5F);
                        }
                        if (direction == "down")
                        {
                            water = (GameObject)GameObject.Instantiate(waterPrefab, transform.position + new Vector3(0, -0.5F, 0), transform.rotation);
                            water.transform.Rotate(0, 0, 180);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(i / 3.0F, -11 + Mathf.Abs(i * 2), 0);
                            if (Input.GetAxis("Vertical") <= -0.2)
                            {
                                water.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -3);
                            }
                            Destroy(water, 0.5F);
                        }
                    }
                    if (mana <= 0)
                    {
                        mana = 0;
                        burnOut = true;
                    }
                }
                if(magicMode == 2)
                {
                    if (magicCharge < 10)
                        magicCharge += Time.deltaTime * 2;
                    if (magicCharge > 10)
                        magicCharge = 10;
                    if(magicCharge > mana)
                    {
                        burnOut = true;
                    }
                    float range = Random.Range(-0.5F, 0.5F);
                    GameObject particle = (GameObject)GameObject.Instantiate(particlePrefab, transform.position + new Vector3(range, -0.5F, 0), transform.rotation);
                    particle.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
                    Destroy(particle, 0.5F);
                    if(magicCharge >= 2.5F)
                    {
                        GameObject particle2 = (GameObject)GameObject.Instantiate(particlePrefab, transform.position + new Vector3(range, -0.5F, 0), transform.rotation);
                        particle2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
                        Destroy(particle2, 0.5F);
                    }
                    if (magicCharge >= 5.0F)
                    {
                        GameObject particle3 = (GameObject)GameObject.Instantiate(particlePrefab, transform.position + new Vector3(range, -0.5F, 0), transform.rotation);
                        particle3.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
                        Destroy(particle3, 0.5F);
                    }
                    if (magicCharge >= 7.5F)
                    {
                        GameObject particle4 = (GameObject)GameObject.Instantiate(particlePrefab, transform.position + new Vector3(range, -0.5F, 0), transform.rotation);
                        particle4.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 2, 0);
                        Destroy(particle4, 0.5F);
                    }
                }
            }
            else if ((Input.GetKeyDown("joystick 1 button 2") || Input.GetKeyDown("x") || Input.GetKeyDown("k")) && canAttack)
            {
                isCasting = false;
                GameObject sword = GameObject.FindWithTag("Weapon");
                sword.SendMessage("Swing", direction);
                canAttack = false;
            }
            else
            {
                isCasting = false;
            }
            if ((Input.GetKeyUp("joystick 1 button 1") || Input.GetKeyUp("c") || Input.GetKeyUp("l") ||burnOut) && magicCharge > 0)
            {
                mana -= magicCharge;
                if (mana < 0)
                {
                    mana = 0;
                }
                if (magicCharge == 10)
                {
                    StartCoroutine(Earth(3, direction, transform.position));
                }
                if (magicCharge >= 7.5F)
                {
                    StartCoroutine(Earth(2, direction, transform.position));
                }
                if (magicCharge >= 5)
                {
                    StartCoroutine(Earth(1, direction, transform.position));
                }
                if (magicCharge >= 2.5F)
                {
                    StartCoroutine(Earth(0, direction, transform.position));
                }
                magicCharge = 0;
            }
        }
    }
    IEnumerator AttackCooldown()
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
    IEnumerator MagicCooldown()
    {
        while (true)
        {
            if (burnOut)
            {
                yield return new WaitForSeconds(5.0F);
                burnOut = false;
            }
            yield return new WaitForSeconds(0.0F);
        }
    }
    IEnumerator Earth(int stage, string originalDirection, Vector3 originalPosition)
    {
        yield return new WaitForSeconds(0.25F * stage);
        for (int i = 0; i <= stage; i++)
        {
            if (originalDirection == "up")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, originalPosition + new Vector3(0.5F*stage - i, 1* (stage + 1), 0), transform.rotation);
            }
            if (originalDirection == "right")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, originalPosition + new Vector3(1 * (stage+1), 0.5F * stage - i, 0), transform.rotation);
            }
            if (originalDirection == "left")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, originalPosition - new Vector3(1 * (stage + 1), 0.5F * stage - i, 0), transform.rotation);
            }
            if (originalDirection == "down")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, originalPosition - new Vector3(0.5F * stage - i, 1 * (stage + 1), 0), transform.rotation);
            }
        }
    }


    public void Damage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 2.0F;
        }

        /*if (health <= 0)
        {
            Destroy(gameObject, 0.0F);
        }*/
    }
    void Recharge()
    {
        mana += 5;
    }
}