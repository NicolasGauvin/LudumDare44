using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float speed;
    public float health;
    public float stopDistance;

    public GameObject gameController;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    private float cooldown;

    public bool isPlayer;

    void MoveTowardsPlayer(Transform player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Debug.Log(gameController.GetComponent<PlayerInformation>().GetPlayer());
            MoveTowardsPlayer(gameController.GetComponent<PlayerInformation>().GetPlayer().transform);
        }
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        }
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
            gameObject.tag = "Untagged";
        }
    }

    public void UpdateIsPlayer()
    {
        isPlayer = !isPlayer;
    }

}
