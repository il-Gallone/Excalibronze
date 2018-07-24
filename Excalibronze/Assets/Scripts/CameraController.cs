using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Update is called once per frame
    void Update () {

        GameObject player = GameObject.FindWithTag("Player");
        if (transform.position.x -8 >= player.transform.position.x)
        {
            player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(MoveLeft(3.0F));
        }
        if (transform.position.x + 8 <= player.transform.position.x)
        {
            player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(MoveRight(3.0F));
        }
        if (transform.position.y - 5 >= player.transform.position.y)
        {
            player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(MoveDown(3.0F));
        }
        if (transform.position.y + 5 <= player.transform.position.y)
        {
            player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(MoveUp(3.0F));
        }
    }
    IEnumerator MoveLeft(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");
        var startPos = transform.position;
        var endPos = new Vector3(transform.position.x - 16, transform.position.y, -10);
        for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        transform.position = endPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator MoveRight(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");
        var startPos = transform.position;
        var endPos = new Vector3(transform.position.x + 16, transform.position.y, -10);
        for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        transform.position = endPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator MoveUp(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");
        var startPos = transform.position;
        var endPos = new Vector3(transform.position.x, transform.position.y + 10, -10);
        for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        transform.position = endPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
    IEnumerator MoveDown(float time)
    {
        GameObject player = GameObject.FindWithTag("Player");
        var startPos = transform.position;
        var endPos = new Vector3(transform.position.x, transform.position.y - 10, -10);
        for (float i = 0.0F; i < 1; i += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        transform.position = endPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
}
