using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour {


    void Jump(float height)
    {
        transform.localScale = new Vector3(14 * (1 - height / 2), 3 * (1 - height / 2), 1);
    }

    void Land()
    {
        transform.localScale = new Vector3(14, 3, 0);
    }
}
