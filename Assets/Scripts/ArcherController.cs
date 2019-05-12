using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : CharacterController
{

    public Transform weapon;
    public Transform upSource;
    public Transform downSource;
    public Transform leftSource;
    public Transform rightSource;

    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;
    public Transform spawner5;

    public GameObject arrow;

    public Transform shotPoint;

    private void Power()
    {
        Debug.Log("Archer Power");
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayer && !gameController.GetComponent<PlayerInformation>().IsSoulTime())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + cooldown;
                    HideWeapon();
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        weapon.position = upSource.position;
                        weapon.eulerAngles = new Vector3(0, 0, -90);
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        weapon.position = downSource.position;
                        weapon.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        weapon.position = leftSource.position;
                        weapon.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        weapon.position = rightSource.position;
                        weapon.eulerAngles = new Vector3(0, 0, 180);
                    }
                    GameObject arrowObject =  Instantiate(arrow, spawner3.position, weapon.rotation);
                    arrowObject.GetComponent<ArrowController>().origin = gameObject;
                    Invoke("HideWeapon", attackDuration);
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, globalPlayer.gameObject.transform.position) < stopDistance && Time.time >= attackTime && !gameController.GetComponent<PlayerInformation>().IsSoulTime())
            {
                attackTime = Time.time + cooldown;
                Vector2 direction = globalPlayer.gameObject.transform.position - shotPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                shotPoint.rotation = rotation;
                GameObject arrowObject = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
                arrowObject.GetComponent<ArrowController>().origin = gameObject;
            }
        }
    }

    public void ComputerAttack()
    {
        if (Time.time >= attackTime)
        {
            attackTime = Time.time + cooldown;
            HideWeapon();
            weapon.position = leftSource.position;
            weapon.eulerAngles = new Vector3(0, 0, 0);
            Instantiate(arrow, spawner3.position, weapon.rotation);
            Invoke("HideWeapon", attackDuration);
        }
    }

    private void HideWeapon()
    {
        weapon.GetComponent<SpriteRenderer>().enabled = !weapon.GetComponent<SpriteRenderer>().enabled;
    }

}