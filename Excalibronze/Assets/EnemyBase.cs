using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour {

    public int health;
    public float invincSecs = 0.0F;
    public GameObject hpPickUpPrefab;
    public GameObject manaPickUpPrefab;
    public GameObject[] totalEnemies;

    public virtual void FixedUpdate()
    {
        if (invincSecs > 0)
        {
            invincSecs -= Time.deltaTime;
        }
    }

    public void Damage(int damage)
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
        if (totalEnemies.Length <= 1)
        {
            SceneManager.LoadScene("WinState");
        }
        Destroy(gameObject, 0.0F);
    }
}
