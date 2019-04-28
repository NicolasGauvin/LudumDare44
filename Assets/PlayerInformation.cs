using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {
   public GameObject GetPlayer()
    {
        return GameObject.FindWithTag("Player");
    }
}
