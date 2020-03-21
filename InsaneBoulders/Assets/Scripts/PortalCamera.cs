using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        float angluarDiffPortalsRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion PortalRotationDiff = Quaternion.AngleAxis(angluarDiffPortalsRotations, Vector3.up);
        Vector3 newCameraRotation = PortalRotationDiff * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraRotation, Vector3.up);
    }
}
