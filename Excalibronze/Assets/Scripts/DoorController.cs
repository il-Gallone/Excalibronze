using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public int onScreenX;
    public int onScreenY;

	
    void Unlock()
    {
        Destroy(gameObject, 0.0F);
    }
}
