using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    public GameObject gameController;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameController.GetComponent<PlayerInformation>().IsSoulTime())
        {
            Destroy(gameObject);
        }
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;
    }

    void FixedUpdate()
    {
       rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Untagged")
        {
            gameController.GetComponent<PlayerInformation>().SwapCharacters(collision.gameObject);
            Destroy(gameObject);
            gameController.GetComponent<PlayerInformation>().UpdateSoulTime();
        }
    }

}
