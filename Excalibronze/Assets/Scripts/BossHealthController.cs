using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour {

    GameObject Tom;

    void Start()
    {
        GameObject[] AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < AllEnemies.Length; i++)
        {
            if (AllEnemies[i].GetComponent<LimblessController>() != null)
            {
                Tom = AllEnemies[i];
            }
        }

    }
    // Update is called once per frame
    void Update ()
    {
        if (Tom == null)
        {
            Destroy(gameObject, 0.0F);
        }
        else
        {
            int health = Tom.GetComponent<LimblessController>().health;
            gameObject.transform.localScale = new Vector3(health, 1, 0);
        }
    }
}
