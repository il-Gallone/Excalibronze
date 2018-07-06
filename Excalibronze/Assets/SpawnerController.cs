using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

    public GameObject enemyPrefab;
    public int health = 200;
    float delay;
    public float invincSecs = 0.0F;
    GameObject[] startingSpawners;
    GameObject[] totalEnemies;

	// Use this for initialization
	void Start () {
        startingSpawners = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < startingSpawners.Length; i++)
        {
            if(startingSpawners[i] == gameObject)
            {
                delay = i * 2.5F;
            }
        }
        StartCoroutine(Spawner());
	}

    void FixedUpdate()
    {
        if (invincSecs > 0)
        {
            invincSecs -= Time.deltaTime;
        }
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemies.Length <= 15)
            {
                int offset;
                offset = Random.Range(0, 4);
                switch (offset)
                {
                    case 0:
                        {
                            GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
                            break;
                        }
                    case 1:
                        {
                            GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(0, -1, 0), transform.rotation);
                            break;
                        }
                    case 2:
                        {
                            GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
                            break;
                        }
                    case 3:
                        {
                            GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                            break;
                        }
                }
            }
            yield return new WaitForSeconds((startingSpawners.Length) * 2.5F);
        }
    }

    void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject, 0.0F);
        }
    }
    void FireDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.2F;
            if (health <= 0)
            {
                Destroy(gameObject, 0.0F);
            }
        }
    }
    void WaterDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage * 2;
            invincSecs = 0.2F;
            if (health <= 0)
            {
                Destroy(gameObject, 0.0F);
            }
        }
    }
}
