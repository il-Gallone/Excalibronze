using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour {

    GameObject manaBar;

    void Start()
    {
        manaBar = GameObject.FindWithTag("Manabar");
    }


    void Update () {
        float findPosition = manaBar.transform.localScale.x / 100F;
        transform.position = new Vector3((-1.995F + findPosition-5.505F), 0F+3.5F, 0);
        float charge = GameObject.FindWithTag("Player").GetComponent<PlayerController>().magicCharge;
        transform.localScale = new Vector3(charge * 16F, 1, 0);
	}
}
