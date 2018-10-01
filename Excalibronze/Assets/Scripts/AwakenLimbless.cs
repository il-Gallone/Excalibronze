using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenLimbless : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Enemy").SendMessage("Awaken");
            Destroy(gameObject, 0.0F);
        }
    }
}
