using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    private float newSpeed;
    public float distance;
    public int damage;
    public float spawnTime;
    public Vector3 spawnPosition;
    public GameObject origin;
    public GameObject gameController;

    void Start()
    {
        spawnPosition = transform.position;
        gameController = GameObject.Find("GameController");
    }


    void Update()
    {
        Debug.Log(Vector3.Distance(spawnPosition, transform.position));
        if (Vector3.Distance(spawnPosition, transform.position) > distance)
        {
            Destroy(gameObject);
        }
        //Debug.Log(transform.position);
        if (gameController.GetComponent<PlayerInformation>().IsSoulTime())
        {
            newSpeed = speed * 0.1f;
        }
        else
        {
            newSpeed = speed;
        }
        transform.Translate(Vector2.left * newSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != origin)
        {
            if (collision.tag == "Untagged" || collision.tag == "Player")
            {
                Destroy(gameObject);
                collision.GetComponent<CharacterController>().TakeDamage(damage);
            }
            else if (collision.tag == "terrain")
            {
                Destroy(gameObject);
            }
        }
    }
}
