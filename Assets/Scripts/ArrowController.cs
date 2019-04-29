using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public int damage;
    private float spawnTime;
    public float defenseTime;

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
        if (collision.tag == "Untagged")
        {
            if (Time.deltaTime > spawnTime + defenseTime)
            {
                DestroyProjectile();
                collision.GetComponent<CharacterController>().TakeDamage(2);
            }
        }else if (collision.tag == "Terrain")
        {
            DestroyProjectile();
        }
    }
}
