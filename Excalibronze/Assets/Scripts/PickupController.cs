using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public string pickupType;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (pickupType == "health" && col.gameObject.GetComponent<PlayerController>().health < col.gameObject.GetComponent<PlayerController>().healthCap)
            {
                col.gameObject.SendMessage("Damage", -1);
                Destroy(gameObject, 0.0F);
            }
            if (pickupType == "mana" && col.gameObject.GetComponent<PlayerController>().mana < col.gameObject.GetComponent<PlayerController>().manaCap)
            {
                col.gameObject.SendMessage("Recharge");
                Destroy(gameObject, 0.0F);
            }
        }

    }
}
