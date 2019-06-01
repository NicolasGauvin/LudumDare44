using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    public GameObject gameController;
    public float speed;
    public float waitTime;
    private bool canEnterOrigin;
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    public GameObject origin;

    // Start is called before the first frame update
    void Start()
    {
        canEnterOrigin = false;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            if (canEnterOrigin == true || collision.gameObject != origin)
            {
                gameController.GetComponent<PlayerInformation>().EnterBody(collision.gameObject);
                Destroy(gameObject);
                gameController.GetComponent<PlayerInformation>().UpdateSoulTime();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        canEnterOrigin = true;
    }
}
