using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed;
    private float newSpeed;
    public float lifeTime;
    public int damage;
    public float spawnTime;
    public float defenseTime;
    public GameObject origin;
    private int touchCount;
    public GameObject gameController;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        spawnTime = Time.deltaTime;
        gameController = GameObject.Find("GameController");
        Debug.Log(origin);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


    void Update()
    {
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
                DestroyProjectile();
                collision.GetComponent<CharacterController>().TakeDamage(damage);
            }
            else if (collision.tag == "terrain")
            {
                DestroyProjectile();
            }
        }
    }
}
