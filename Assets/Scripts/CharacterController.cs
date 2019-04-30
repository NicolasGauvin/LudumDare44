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
    public GameObject globalPlayer;

    private Rigidbody2D rb;
    private Vector2 moveAmount;

    public float cooldown;
    public float attackDuration;
    public float attackTime;

    public bool isPlayer;
    public bool isDistance;

    private Transform attackUp;
    private Transform attackDown;
    private Transform attackLeft;
    private Transform attackRight;

    private float distanceUp;
    private float distanceDown;
    private float distanceLeft;
    private float distanceRight;

    public Vector3 currentTarget;

    public Sprite playerSprite;
    public Sprite notPlayerSprite;

    public float invulnerability;
    private float nextAttack;

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
        globalPlayer = gameController.GetComponent<PlayerInformation>().GetPlayer();
        if (isPlayer)
        {
            GetComponent<SpriteRenderer>().sprite = playerSprite; 
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = notPlayerSprite;
        }

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
            if (globalPlayer != null)
            {
                if (Vector2.Distance(transform.position, globalPlayer.gameObject.transform.position) > stopDistance)
                {
                    currentTarget = globalPlayer.transform.position;
                    MoveTowardsTarget(currentTarget);
                }
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

    IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isPlayer)
        {
            nextAttack = Time.deltaTime + invulnerability;
            health -= damageAmount;
            if (health <= 0)
            {
                Destroy(gameObject);
                gameController.GetComponent<PlayerInformation>().UpdateKillCount();
            }
            StartCoroutine(Blink());
        }
        else
        {
            nextAttack = Time.deltaTime + invulnerability;
            health -= damageAmount;
            if (health <= 0)
            {
                gameController.GetComponent<PlayerInformation>().UpdateGameOver();
                Destroy(gameObject);
            }
            StartCoroutine(Blink());
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
