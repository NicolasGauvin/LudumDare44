using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {

    public bool isTimeSlowed;

    public GameObject GetPlayer()
    {
        return GameObject.FindWithTag("Player");
    }
    public bool IsSoulTime()
    {
        return isTimeSlowed;
    }
    public void UpdateSoulTime()
    {
        isTimeSlowed = !isTimeSlowed;
    }

}
