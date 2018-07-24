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
        transform.position = new Vector3((manaBar.transform.position.x + findPosition), manaBar.transform.position.y, 0);
        float charge = GameObject.FindWithTag("Player").GetComponent<PlayerController>().magicCharge;
        transform.localScale = new Vector3(charge * 16F, 1, 0);
	}
}
