using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineController : MonoBehaviour {

    public int health = 20;
    public float invincSecs = 0;

    void FixedUpdate()
    {
        if (invincSecs > 0)
        {
            invincSecs -= Time.deltaTime;
        }
    }

    public void FireDamage(int damage)
    {
        if (invincSecs <= 0)
        {
            health -= damage;
            invincSecs = 0.1f;
            if (health <= 0)
            {
                Destroy(gameObject, 0.0F);
            }
        }
    }
}
