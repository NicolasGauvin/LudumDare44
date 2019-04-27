using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : CharacterController
{
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private float cooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private void Power()
    {
        Debug.Log("Archer Power");
    }
}