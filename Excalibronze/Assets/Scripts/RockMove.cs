using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour {

    public int newPositionX;
    public int newPositionY;

    void Start()
    {
        newPositionX = (int)transform.position.x;
        newPositionY = (int)transform.position.y;
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) >= Mathf.Abs(newPositionX) + 0.1F || Mathf.Abs(transform.position.x) <= Mathf.Abs(newPositionX) - 0.1F ||
            Mathf.Abs(transform.position.y) >= Mathf.Abs(newPositionY) + 0.1F || Mathf.Abs(transform.position.y) <= Mathf.Abs(newPositionY) - 0.1F)
        {
            Vector2 wantedPosition = new Vector2(newPositionX, newPositionY);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, wantedPosition, Time.deltaTime * 4));
        }
        else
        {
            Vector2 wantedPosition = new Vector2(newPositionX, newPositionY);
            gameObject.GetComponent<Rigidbody2D>().MovePosition(wantedPosition);
        }
    }

    void Move(string direction)
    {
        switch (direction)
        {
            case "down":
                {
                    newPositionY -= 1;
                    break;
                }
            case "up":
                {
                    newPositionY += 1;
                    break;
                }
            case "right":
                {
                    newPositionX += 1;
                    break;
                }
            case "left":
                {
                    newPositionX -= 1;
                    break;
                }
        }
    }



        
    
}
