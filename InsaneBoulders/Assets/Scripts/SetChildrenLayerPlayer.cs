using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildrenLayerPlayer : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 9;
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = 9;
        }
    }
}
