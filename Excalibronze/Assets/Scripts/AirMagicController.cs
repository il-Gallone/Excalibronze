using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMagicController : MonoBehaviour {

    public float speed = 4.0F;
    public float upSpeed = 0.4F;
    


    void FixedUpdate () {
        transform.position += (transform.right+transform.up*upSpeed) * speed * Time.deltaTime;
        upSpeed += 0.6F*Time.deltaTime;
        transform.Rotate(0 ,0, -540.0F*Time.deltaTime);
	}
}
