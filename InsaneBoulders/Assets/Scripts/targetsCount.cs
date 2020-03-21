using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetsCount : MonoBehaviour
{
    public Text textTargets;

    // Update is called once per frame
    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        textTargets.text = "Targets left : " + targets.Length;
    }
}
