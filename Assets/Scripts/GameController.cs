﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private bool isTimeSlowed;
    public int soulTime;
    public int soulCount;

    public static GameObject player;

    public Image soulBar;
    public Sprite[] soulBarStates;
    public Sprite[] soulBarStatesSoulTime;

    public GameObject soul;

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;

    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;

    void Start()
    {
        player = GetPlayerInformation();
        StartCoroutine(StartWave(currentWaveIndex));
    }

    IEnumerator StartWave(int i)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(i));
    }

    IEnumerator SpawnWave(int i)
    {
        currentWave = waves[i];

        for (int a = 0; a < currentWave.count; a++)
        {
            if(player == null)
            {
                yield break;
            }

            GameObject randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

        }

    }

    public void UpdateSoulBar()
    {
        Debug.Log(GetComponent<PlayerInformation>().IsSoulTime());
        if (GetComponent<PlayerInformation>().IsSoulTime())
        {
            if (soulCount == 0)
            {
                soulBar.GetComponent<Image>().enabled = false;
            }
            else
            {
                soulBar.GetComponent<Image>().sprite = soulBarStates[soulCount];
            }
        }
        else
        {
            if (soulCount == 0)
            {
                soulBar.GetComponent<Image>().enabled = false;
            }
            else
            {
                soulBar.GetComponent<Image>().sprite = soulBarStatesSoulTime[soulCount];
            }
        }
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
        if(GetComponent<PlayerInformation>().IsSoulTime())
        {
            GetComponent<PlayerInformation>().UpdateSoulTime();
            UpdateSoulBar();
        }
    }

    public GameObject GetPlayerInformation()
    {
        return GameObject.FindWithTag("Player");
    }

    public bool IsSoulTime()
    {
        return isTimeSlowed;
    }

    void Update()
    {
        if (!GetComponent<PlayerInformation>().IsSoulTime())
        {
            if (Input.GetKey("a") && soulCount > 0)
            {
                soulCount--;
                GetComponent<PlayerInformation>().UpdateSoulTime();
                UpdateSoulBar();
                Invoke("ResetTimeScale", soulTime);
                player = GetPlayerInformation();
                Instantiate(soul, player.transform.position, player.transform.rotation);
            }
        }
    }



}
