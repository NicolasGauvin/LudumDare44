﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
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
            DestroyProjectile();
            collision.GetComponent<CharacterController>().TakeDamage(2);
        }else if (collision.tag == "Terrain")
        {
            DestroyProjectile();
        }
    }
}