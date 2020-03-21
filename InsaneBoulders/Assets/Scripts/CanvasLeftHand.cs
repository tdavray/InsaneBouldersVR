using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLeftHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 9;
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 9;
        }
    }
}
