using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int currentScreenX = 0;
    public int currentScreenY = 0;

    public bool isScreenMoving = false;


	void Start () {
        instance = this;
	}
}
