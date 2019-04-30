using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {

    public bool isTimeSlowed;
    public int killCount;
    public bool gameOver;

    public GameObject GetPlayer()
    {
        return GameObject.FindWithTag("Player");
    }
    void Start()
    {
        gameOver = false;
        killCount = 0;
    }

    public bool IsSoulTime()
    {
        return isTimeSlowed;
    }
    public void UpdateSoulTime()
    {
        isTimeSlowed = !isTimeSlowed;
    }

    public void UpdateKillCount()
    {
        killCount++;
    }

    public void ResetKillCount()
    {
        killCount = 0;
    }

    public void UpdateGameOver()
    {
        gameOver = true;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public int GetKillCount()
    {
        return killCount;
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
