using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterController : MonoBehaviour {

    private float speed;
    public float OriginalSpeed;
    public float health;
    public float stopDistance;

    public GameObject gameController;
    private GameObject globalPlayer;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    public float cooldown;
    public float attackDuration;
    public float attackTime;

    public bool isPlayer;
    public bool isDistance;

    public Transform attackUp;
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackRight;

    private float distanceUp;
    private float distanceDown;
    private float distanceLeft;
    private float distanceRight;

    public Vector3 currentTarget;


    void MoveTowardsTarget(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController");
    }

    private int CompareByX(Transform t1, Transform t2)
    {
        if (t1.position.x < t2.position.x)
            return -1;
        else if (t1.position.x > t2.position.x)
            return 1;
        else
            return 0;
    }

    protected virtual void Update()
    {

        if (gameController.GetComponent<PlayerInformation>().IsSoulTime())
        {
            speed = OriginalSpeed * 0.1f;
        }
        else
        {
            speed = OriginalSpeed;
        }

        if (isPlayer && !gameController.GetComponent<PlayerInformation>().IsSoulTime())
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveAmount = moveInput.normalized * speed;
        }
        else if(!isPlayer)
        {
            globalPlayer = gameController.GetComponent<PlayerInformation>().GetPlayer();
            if (globalPlayer != null)
            {
                if (isDistance)
                {
                    distanceUp = Vector2.Distance(transform.position, attackUp.position);
                    distanceDown = Vector2.Distance(transform.position, attackDown.position);
                    distanceRight = Vector2.Distance(transform.position, attackRight.position);
                    distanceLeft = Vector2.Distance(transform.position, attackLeft.position);

                    if (distanceUp < distanceDown && distanceUp < distanceLeft && distanceUp < distanceRight)
                    {
                        currentTarget = new Vector3(globalPlayer.transform.position.x, globalPlayer.transform.position.y - 1, globalPlayer.transform.position.z);
                    }
                    else if (distanceRight < distanceUp && distanceRight < distanceDown && distanceLeft > distanceRight)
                    {
                        currentTarget = new Vector3(globalPlayer.transform.position.x, globalPlayer.transform.position.y + 1, globalPlayer.transform.position.z);
                    }
                    else if (distanceDown < distanceUp && distanceDown < distanceLeft && distanceDown < distanceRight)
                    {
                        currentTarget = new Vector3(globalPlayer.transform.position.x - 1, globalPlayer.transform.position.y, globalPlayer.transform.position.z);
                    }
                    else
                    {
                        currentTarget = new Vector3(globalPlayer.transform.position.x + 1, globalPlayer.transform.position.y, globalPlayer.transform.position.z);
                    }
                }
                else
                {
                    currentTarget = globalPlayer.transform.position;
                }
                MoveTowardsTarget(currentTarget);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPlayer && !gameController.GetComponent<PlayerInformation>().IsSoulTime())
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
