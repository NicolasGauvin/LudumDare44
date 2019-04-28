using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;
    public float health;
    public float stopDistance;

    public Transform player;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    private float cooldown;

    public bool isPlayer;

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetPlayerInformation();
    }

    void Update()
    {
        if (isPlayer)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        }
    }

    public void GetPlayerInformation()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTag()
    {
        if (isPlayer)
        {
            gameObject.tag = "Player";
        }
        else
        {
            gameObject.tag = "None";
        }
    }

    public void UpdateIsPlayer()
    {
        isPlayer = !isPlayer;
    }

}
