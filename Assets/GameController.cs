using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameEnd(bool isWon)
    {
        if (isWon == true)
        {
            Debug.Log("Game is won");
        }
        else
        {
            Debug.Log("Game is lost");
        }
    }
}
