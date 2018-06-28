using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour {
    
    public string direction = "up";
    public float time = 0.66F;
    public Sprite up;
    public Sprite right;
    public Sprite down;
    public Sprite left;
    SpriteRenderer spriteRenderer;

    void Start () {
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
                var startPos = transform.position;
                var endPos = new Vector3(transform.position.x, transform.position.y + 1, 0);
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, i);
                    yield return null;
                }
                transform.position = endPos;
            }
            if (direction == "right")
            {
                var startPos = transform.position;
                var endPos = new Vector3(transform.position.x + 1, transform.position.y, 0);
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, i);
                    yield return null;
                }
                transform.position = endPos;
            }
            if (direction == "down")
            {
                var startPos = transform.position;
                var endPos = new Vector3(transform.position.x, transform.position.y - 1, 0);
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, i);
                    yield return null;
                }
                transform.position = endPos;
            }
            if (direction == "left")
            {
                var startPos = transform.position;
                var endPos = new Vector3(transform.position.x - 1, transform.position.y, 0);
                for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
                {
                    transform.position = Vector3.Lerp(startPos, endPos, i);
                    yield return null;
                }
                transform.position = endPos;
            }
        }
    }
}
