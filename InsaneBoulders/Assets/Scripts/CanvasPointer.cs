using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasPointer : MonoBehaviour
{
    public float defaultlength = 3.0f;

    private LineRenderer lineRenderer = null;

    public StandaloneInputModule inputModule = null;

    public EventSystem eventSystem = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLength();
    }

    private void UpdateLength()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, GetEnd());
    }

    public Vector3 GetEnd()
    {
        float distance = GetCanvasDistance();
        Vector3 endPosition = CalculateEnd(defaultlength);
        if (distance != 0.0f)
            endPosition = CalculateEnd(distance);
        return endPosition;
    }

    public float GetCanvasDistance()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = inputModule.inputOverride.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);

        RaycastResult closestResult = FindFirstRaycast(results);
        float distance = closestResult.distance;

        distance = Mathf.Clamp(distance, 0.0f, defaultlength);
        return distance;
    }

    private RaycastResult FindFirstRaycast(List<RaycastResult> results)
    {
        foreach(RaycastResult result in results)
        {
            if (!result.gameObject)
                continue;
            return result;
        }
        return new RaycastResult();
    }

    private Vector3 CalculateEnd(float length)
    {
        return transform.position + (transform.forward * length);
    }
}
