using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : EnemyBase {

    public GameObject enemyPrefab;
    float delay;
    GameObject[] startingSpawners;

	// Use this for initialization
	void Start ()
    {
        health = 200;
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
    
    public override void WaterDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage * 2;
            invincSecs = 0.2F;
            if (health <= 0)
            {
                Death();
            }
        }
    }

    public override void Death()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length <= 1)
        {
            SceneManager.LoadScene("WinState");
        }
        Destroy(gameObject, 0.0F);
    }
}
