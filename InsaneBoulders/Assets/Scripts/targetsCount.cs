using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class targetsCount : MonoBehaviour
{
    public Text textTargets;
    public bool multidimentions = false;

    // Update is called once per frame
    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        if (multidimentions)
            textTargets.text = "Targets left in this dimension: " + targets.Length;
        else
            textTargets.text = "Targets left: " + targets.Length;
    }
}
