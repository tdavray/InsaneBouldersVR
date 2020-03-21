using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float dmg = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "target")
        {
            Target target = other.transform.GetComponent<Target>();
            target.isDamaged(dmg);
        }
    }
}
