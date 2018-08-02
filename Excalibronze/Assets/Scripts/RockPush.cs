using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPush : MonoBehaviour {

    public string direction;
    public float pushTime = 2.5F;
    GameObject[] moveableObstacles;


    void Start()
    {
        moveableObstacles = GameObject.FindGameObjectsWithTag("MoveableObstacle");
        for(int i = 0; i < moveableObstacles.Length; i++)
        {
            Physics2D.IgnoreCollision(moveableObstacles[i].GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            switch (direction)
            {
                case "down":
                    {
                        if(Input.GetAxis("Vertical") <= -0.2 && !Input.GetKey("tab") && !Input.GetKey("backspace") && !Input.GetKey("joystick button 8"))
                        {
                            pushTime -= Time.deltaTime;
                        }
                        else
                        {
                            pushTime = 2.5F;
                        }
                        break;
                    }
                case "up":
                    {
                        if (Input.GetAxis("Vertical") >= 0.2 && !Input.GetKey("tab") && !Input.GetKey("backspace") && !Input.GetKey("joystick button 8"))
                        {
                            pushTime -= Time.deltaTime;
                        }
                        else
                        {
                            pushTime = 2.5F;
                        }
                        break;
                    }
                case "right":
                    {
                        if (Input.GetAxis("Horizontal") >= 0.2 && !Input.GetKey("tab") && !Input.GetKey("backspace") && !Input.GetKey("joystick button 8"))
                        {
                            pushTime -= Time.deltaTime;
                        }
                        else
                        {
                            pushTime = 2.5F;
                        }
                        break;
                    }
                 case "left":
                    {
                        if (Input.GetAxis("Horizontal") <= -0.2 && !Input.GetKey("tab") && !Input.GetKey("backspace") && !Input.GetKey("joystick button 8"))
                        {
                            pushTime -= Time.deltaTime;
                        }
                        else
                        {
                            pushTime = 2.5F;
                        }
                        break;
                    }
            }
            if(pushTime <= 0)
            {
                gameObject.SendMessageUpwards("Move", direction);
                pushTime = 2.5F;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            pushTime = 2.5F;
        }
    }
}
