using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public int onScreenX;
    public int onScreenY;
    SpriteRenderer spriteRenderer;
    Collider2D col;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> totalEnemiesOnScreen = new List<GameObject>();
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
        if (totalEnemiesOnScreen.Count <= 0)
        {
            Unlock();
        }
    }

    void Unlock()
    {
        Destroy(gameObject, 0.0F);
    }

    void Lock()
    {
        spriteRenderer.enabled = true;
        col.enabled = true;
    }
}
