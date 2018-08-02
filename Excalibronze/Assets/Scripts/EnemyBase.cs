using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour {

    public int health;
    public float invincSecs = 0.0F;
    public float stunSecs = 0.0F;
    public float earthSecs = 0.0F;
    public int onScreenX;
    public int onScreenY;
    public GameObject hpPickUpPrefab;
    public GameObject manaPickUpPrefab;
    public GameObject[] totalEnemies;
    public List<GameObject> totalEnemiesOnScreen = new List<GameObject>();

    public virtual void FixedUpdate()
    {
        if (invincSecs > 0)
        {
            invincSecs -= Time.deltaTime;
        }
        if (stunSecs > 0)
        {
            stunSecs -= Time.deltaTime;
        }
        if (earthSecs > 0)
        {
            earthSecs -= Time.deltaTime;
        }
    }

    public virtual void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }
    public virtual void FireDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.2F;
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public virtual void WaterDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.2F;
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public virtual void EarthDamage(int damage)
    {
        if (invincSecs <= 0 && earthSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.2F;
            earthSecs = 1.5F;
            stunSecs = 1.6F;
            if (health <= 0)
            {
                Death();
            }
        }
    }
    public virtual void Death()
    {
        int dropChance;
        dropChance = Random.Range(0, 10);
        if (dropChance == 0 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().health < GameObject.FindWithTag("Player").GetComponent<PlayerController>().healthCap)
        {
            GameObject pickup = (GameObject)GameObject.Instantiate(hpPickUpPrefab, transform.position, transform.rotation);
        }
        if (dropChance == 1 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().mana < GameObject.FindWithTag("Player").GetComponent<PlayerController>().manaCap)
        {
            GameObject pickup = (GameObject)GameObject.Instantiate(manaPickUpPrefab, transform.position, transform.rotation);
        }
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
            for(int i = 0; i < Doors.Length; i++)
            {
                if(Doors[i].GetComponent<DoorController>().onScreenX == onScreenX && Doors[i].GetComponent<DoorController>().onScreenY == onScreenY)
                {
                    Doors[i].SendMessage("Unlock");
                }
            }
        }
        Destroy(gameObject, 0.0F);
    }
}
