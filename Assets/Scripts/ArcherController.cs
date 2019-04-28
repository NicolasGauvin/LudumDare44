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

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
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
            Invoke("HideWeapon", 1.0f);
        }
    }


    private void HideWeapon()
    {
        weapon.GetComponent<SpriteRenderer>().enabled = !weapon.GetComponent<SpriteRenderer>().enabled;
    }

}