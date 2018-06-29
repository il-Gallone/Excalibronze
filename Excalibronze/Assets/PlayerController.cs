using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 3.0F;
    public float attackCooldown = 0.25F;
    public float mana = 25.0F;
    public float manaCap = 25.0F;
    public float magicCharge = 0.0F;
    public int magicMode = 0;
    public bool canAttack = true;
    public bool burnOut = false;
    public string direction = "up";
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject earthPrefab;
    public GameObject particlePrefab;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(AttackCooldown());
        StartCoroutine(MagicCooldown());
    }
	
	void FixedUpdate () {
        if (mana < manaCap)
        {
            mana += Time.deltaTime;
            if(mana > manaCap)
            {
                mana = manaCap;
            }
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
            if((Input.GetKey("joystick 1 button 1") || Input.GetKey("c") || Input.GetKey("l")) && mana > 0 && !burnOut)
            {
                if(magicMode == 0)
                {
                    mana -= Time.deltaTime * 5;
                    for (int i = -1; i < 2; i++)
                    {
                        GameObject fire = (GameObject)GameObject.Instantiate(firePrefab, transform.position, transform.rotation);
                        if (direction == "up")
                        {
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2 + i * 2, 2 + i * 2), 9 - Mathf.Abs(i * 2), 0);
                        }
                        if (direction == "right")
                        {
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(9 - Mathf.Abs(i * 2), Random.Range(-2 + i * 2, 2 + i * 2), 0);
                        }
                        if (direction == "left")
                        {
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(-9 + Mathf.Abs(i*2), Random.Range(-2 + i * 2, 2 + i * 2), 0);
                        }
                        if (direction == "down")
                        {
                            fire.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-2 + i * 2, 2 + i * 2), -9 + Mathf.Abs(i * 2), 0);
                        }

                        Destroy(fire, 0.3F);
                    }
                    if(mana <= 0)
                    {
                        burnOut = true;
                    }
                }
                if(magicMode == 1)
                {
                    mana -= Time.deltaTime * 3;
                    for (float i = -1.0F; i < 2.0F; i++)
                    {
                        GameObject water = (GameObject)GameObject.Instantiate(waterPrefab, transform.position, transform.rotation);
                        if (direction == "up")
                        {
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(i/3.0F, 11 - Mathf.Abs(i * 2), 0);
                        }
                        if (direction == "right")
                        {
                            water.transform.Rotate(0, 0, -90);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(11 - Mathf.Abs(i * 2), i / 3.0F, 0);
                        }
                        if (direction == "left")
                        {
                            water.transform.Rotate(0, 0, 90);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(-11 + Mathf.Abs(i * 2), i / 3.0F, 0);
                        }
                        if (direction == "down")
                        {
                            water.transform.Rotate(0, 0, 180);
                            water.GetComponent<Rigidbody2D>().velocity = new Vector3(i / 3.0F, -11 + Mathf.Abs(i * 2), 0);
                        }
                        Destroy(water, 0.5F);
                    }
                    if (mana <= 0)
                    {
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
            if((Input.GetKeyUp("joystick 1 button 1") || Input.GetKeyUp("c") || Input.GetKeyUp("l") ||burnOut) && magicCharge > 0)
            {
                mana -= magicCharge;
                if(magicCharge == 10)
                {
                    StartCoroutine(Earth(3));
                }
                if (magicCharge >= 7.5F)
                {
                    StartCoroutine(Earth(2));
                }
                if (magicCharge >= 5)
                {
                    StartCoroutine(Earth(1));
                }
                if (magicCharge >= 2.5F)
                {
                    StartCoroutine(Earth(0));
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
    IEnumerator Earth(int stage)
    {
        yield return new WaitForSeconds(0.25F * stage);
        for (int i = 0; i <= stage; i++)
        {
            if (direction == "up")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, transform.position + new Vector3(0.5F*stage - i, 1* (stage + 1), 0), transform.rotation);
            }
            if (direction == "right")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, transform.position + new Vector3(1 * (stage+1), 0.5F * stage - i, 0), transform.rotation);
            }
            if (direction == "left")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, transform.position - new Vector3(1 * (stage + 1), 0.5F * stage - i, 0), transform.rotation);
            }
            if (direction == "down")
            {
                GameObject earth = (GameObject)GameObject.Instantiate(earthPrefab, transform.position - new Vector3(0.5F * stage - i, 1 * (stage + 1), 0), transform.rotation);
            }
        }
    }


}
