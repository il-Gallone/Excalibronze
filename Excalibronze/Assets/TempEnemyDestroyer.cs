using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyDestroyer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(transform.position.x >= 16 || transform.position.x <= -16 || transform.position.y >= 9 || transform.position.x <= -9)
        {
            gameObject.SendMessage("Death");
        }

    }
}
