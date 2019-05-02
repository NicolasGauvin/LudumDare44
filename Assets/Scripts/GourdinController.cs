using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GourdinController : MonoBehaviour
{

    public GameObject gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Untagged")
        {
            collision.GetComponent<CharacterController>().TakeDamage(5);
        }
    }
}
