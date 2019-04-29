﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private bool isTimeSlowed;
    public int soulTime;

    public static GameObject player;

    void Start()
    {
        player = GetPlayerInformation();
    }

    public void GameEnd(bool isWon)
    {
        if (isWon)
        {
            Debug.Log("Game is won");
        }
        else
        {
            Debug.Log("Game is lost");
        }
    }

    public void ResetTimeScale()
    {
        if(isTimeSlowed == true)
        {
            Time.timeScale = 1.0f;
            isTimeSlowed = false;
        }
    }

    public GameObject GetPlayerInformation()
    {
        return GameObject.FindWithTag("Player");
    }

    private void SwapCharacters(GameObject target)
    {
        player.GetComponent<CharacterController>().UpdateIsPlayer();
        player.GetComponent<CharacterController>().UpdateTag();
        target.GetComponent<CharacterController>().UpdateIsPlayer();
        target.GetComponent<CharacterController>().UpdateTag();
        player = GetPlayerInformation();
    }

    void Update()
    {
        if (isTimeSlowed == false)
        {
            if (Input.GetKey("e"))
            {
                isTimeSlowed = true;
                Time.timeScale = 0.1f;
                Invoke("ResetTimeScale", soulTime * 0.1f);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    SwapCharacters(hit.collider.gameObject);
                }
            }
        }
    }
}