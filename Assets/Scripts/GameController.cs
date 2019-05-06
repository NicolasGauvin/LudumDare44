using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private bool isSoulTime;
    public int soulTime;
    public int soulCount;

    public static GameObject player;

    public Image soulBar;

    public Image Defeat;
    public Image Victory;

    public Sprite[] soulBarStates;
    public Sprite[] soulBarStatesSoulTime;

    public GameObject soul;

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public float timeBetweenSpawns;
    }
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;
    private Wave currentWave;
    public int currentWaveIndex;
    private int killCount;

    void Start()
    {
        player = GetPlayerInformation();
        currentWave = waves[0];
        StartCoroutine(SpawnWave(currentWaveIndex));
        UpdateSoulBar();
    }

    IEnumerator SpawnWave(int i)
    {

        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave = waves[currentWaveIndex];


        for (int a = 0; a < currentWave.enemies.Length; a++)
        {
            if(player == null)
            {
                yield break;
            }

            GameObject enemy = currentWave.enemies[a];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, randomSpot.position, randomSpot.rotation);
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);

        }

    }

    public void UpdateSoulBar()
    {
        if (soulCount > 0)
        {
            if (GetComponent<PlayerInformation>().IsSoulTime())
            {
                soulBar.GetComponent<Image>().sprite = soulBarStates[soulCount];
            }
            else
            {
                soulBar.GetComponent<Image>().sprite = soulBarStatesSoulTime[soulCount];
            }
        }
        else
        {
            soulBar.GetComponent<Image>().enabled = false;
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

    public void VictoryScreen()
    {
        Victory.GetComponent<Image>().enabled = true;
    }

    public void DefeatScreen()
    {
        Defeat.GetComponent<Image>().enabled = true;
    }

    void Update()
    {
        UpdateSoulBar();

        if (GetComponent<PlayerInformation>().IsGameOver())
        {
            Debug.Log("over");
            DefeatScreen();
        }

        if (GetComponent<PlayerInformation>().IsSoulTime() == false)
        {
            if (Input.GetKey("a") && soulCount > 0)
            {
                soulCount--;
                GetComponent<PlayerInformation>().UpdateSoulTime();
                Invoke("ResetTimeScale", soulTime);
                player = GetPlayerInformation();
                Instantiate(soul, player.transform.position, player.transform.rotation);
            }

        }

        if(GetComponent<PlayerInformation>().GetKillCount() == currentWave.enemies.Length)
        {
            GetComponent<PlayerInformation>().ResetKillCount();
            currentWaveIndex++;
            if (currentWaveIndex == waves.Length)
            {
                VictoryScreen();
            }
            else
            {
                StartCoroutine(SpawnWave(currentWaveIndex));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Scene1");
        }

    }



}
