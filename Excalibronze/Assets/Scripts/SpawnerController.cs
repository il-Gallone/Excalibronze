using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerController : EnemyBase {

    public GameObject enemyPrefab;
    float delay;
    GameObject[] startingSpawnersGlobal;
    List<GameObject> startingSpawnersLocal = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        health = 100;
        startingSpawnersGlobal = GameObject.FindGameObjectsWithTag("Enemy");
        int j = 0;
        for(int i = 0; i < startingSpawnersGlobal.Length;  i++)
        {
            if((startingSpawnersGlobal[i].GetComponent<EnemyBase>().onScreenX == onScreenX && 
                startingSpawnersGlobal[i].GetComponent<EnemyBase>().onScreenY == onScreenY))
            {
                startingSpawnersLocal.Add(startingSpawnersGlobal[i]);
                j++;
            }
        }
        for(int i = 0;  i < startingSpawnersLocal.Count; i++)
        {
            if(startingSpawnersLocal[i] == gameObject)
            {
                delay = 2.5F * i;
            }
        }
        StartCoroutine(Spawner());
	}


    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            if (onScreenX == GameManager.instance.currentScreenX && onScreenY == GameManager.instance.currentScreenY && !GameManager.instance.isScreenMoving)
            {
                totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                totalEnemiesOnScreen = new List<GameObject>();
                int j = 0;
                for (int i = 0; i < totalEnemies.Length; i++)
                {
                    if ((totalEnemies[i].GetComponent<EnemyBase>().onScreenX == onScreenX &&
                        totalEnemies[i].GetComponent<EnemyBase>().onScreenY == onScreenY))
                    {
                        totalEnemiesOnScreen.Add(totalEnemies[i]);
                        j++;
                    }
                }
                if (totalEnemiesOnScreen.Count < 10)
                {
                    int offset;
                    offset = Random.Range(0, 4);
                    switch (offset)
                    {
                        case 0:
                            {
                                GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
                                enemy.GetComponent<EnemyBase>().onScreenX = onScreenX;
                                enemy.GetComponent<EnemyBase>().onScreenY = onScreenY;
                                break;
                            }
                        case 1:
                            {
                                GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(0, -1, 0), transform.rotation);
                                enemy.GetComponent<EnemyBase>().onScreenX = onScreenX;
                                enemy.GetComponent<EnemyBase>().onScreenY = onScreenY;
                                break;
                            }
                        case 2:
                            {
                                GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(1, 0, 0), transform.rotation);
                                enemy.GetComponent<EnemyBase>().onScreenX = onScreenX;
                                enemy.GetComponent<EnemyBase>().onScreenY = onScreenY;
                                break;
                            }
                        case 3:
                            {
                                GameObject enemy = (GameObject)GameObject.Instantiate(enemyPrefab, transform.position + new Vector3(-1, 0, 0), transform.rotation);
                                enemy.GetComponent<EnemyBase>().onScreenX = onScreenX;
                                enemy.GetComponent<EnemyBase>().onScreenY = onScreenY;
                                break;
                            }
                    }
                }
            }
            yield return new WaitForSeconds((startingSpawnersLocal.Count) * 2.5F);
            
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
    public override void EarthDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.2F;
            earthSecs = 1.5F;
            if (health <= 0)
            {
                Death();
            }
        }
    }

    public override void Death()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemiesOnScreen = new List<GameObject>();
        int j = 0;
        for (int i = 0; i < totalEnemies.Length; i++)
        {
            if ((totalEnemies[i].GetComponent<EnemyBase>().onScreenX == onScreenX &&
                totalEnemies[i].GetComponent<EnemyBase>().onScreenY == onScreenY))
            {
                totalEnemiesOnScreen.Add(totalEnemies[i]);
                j++;
            }
        }
        if (totalEnemiesOnScreen.Count <= 1)
        {
            GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");
            for (int i = 0; i < Doors.Length; i++)
            {
                if (Doors[i].GetComponent<DoorController>().onScreenX == onScreenX && Doors[i].GetComponent<DoorController>().onScreenY == onScreenY)
                {
                    Doors[i].SendMessage("Unlock");
                }
            }
        }
        Destroy(gameObject, 0.0F);
    }
}
