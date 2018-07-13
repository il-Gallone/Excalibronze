using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {

    int health;
    GameObject[] heartGroup;
    SpriteRenderer[] heartRenderer;

	// Use this for initialization
	void Start () {
        heartGroup = GameObject.FindGameObjectsWithTag("HalfHeart");
	}
	
	// Update is called once per frame
	void Update () {
        health = GameObject.FindWithTag("Player").GetComponent<PlayerController>().health;
        for(int i = 0; i < heartGroup.Length; i++)
        {
            int identifier = heartGroup[i].GetComponent<HeartIdentifier>().heartNumber;
            if (health < identifier+1)
            {
                heartGroup[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                heartGroup[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        if(health <= 0)
        {
            SceneManager.LoadScene("LoseState");
        }
	}
}
