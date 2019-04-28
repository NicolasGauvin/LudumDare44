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

        if (isPlayer)
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
                    Instantiate(arrow, spawner3.position, weapon.rotation);
                    Invoke("HideWeapon", attackDuration);
                }
            }
        }
        else
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
        
    }

    private void HideWeapon()
    {
        weapon.GetComponent<SpriteRenderer>().enabled = !weapon.GetComponent<SpriteRenderer>().enabled;
    }

}