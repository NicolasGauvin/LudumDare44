using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnnemyController : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public float spawnTime;
    public float defenseTime;
    public GameObject origin;
    private int touchCount;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        spawnTime = Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }


    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DestroyProjectile();
            collision.GetComponent<CharacterController>().TakeDamage(damage);
        }
        else if (collision.tag == "Terrain")
        {
            DestroyProjectile();
        }
    }
}
