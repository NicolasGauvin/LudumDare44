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

    public void SwapCharacters(GameObject target)
    {
        GameObject player = GetPlayer();

        target.GetComponent<CharacterController>().UpdateIsPlayer();
        target.GetComponent<CharacterController>().UpdateTag();
        player.GetComponent<CharacterController>().UpdateIsPlayer();
        player.GetComponent<CharacterController>().UpdateTag();
    }

}
