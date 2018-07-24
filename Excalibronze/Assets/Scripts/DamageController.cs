using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	void Damage(int damage)
    {
        gameObject.GetComponentInParent<PlayerController>().Damage(damage);
    }
}
