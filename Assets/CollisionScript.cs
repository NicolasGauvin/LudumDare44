using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    void OnCollisionExit(Collision other)
    {
        print("No longer in contact with " + other.transform.name);
    }
}
