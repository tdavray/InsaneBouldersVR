using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionsManager : MonoBehaviour
{

    public List<Dimension> dimensions;
    public Light Sun;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Dimension dimension in dimensions)
        {
            if (!dimension.currentDimention)
            {
                Transform[] childrenTransforms = dimension.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in childrenTransforms)
                {
                    if (t.tag == "target")
                    {
                        t.gameObject.SetActive(false);
                    }
                    if (t.tag == "wall")
                        t.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                setNightOrDay(dimension.night);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Dimension dimension in dimensions)
        {
            if (dimension.currentDimention)
            {

                Transform[] childrenTransforms = dimension.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in childrenTransforms)
                {
                    if (t.tag == "target")
                    {
                        t.gameObject.SetActive(true);
                    }
                    if (t.tag == "wall")
                        t.GetComponent<Collider>().enabled = true;
                }
            }
            else
            {
                Transform[] childrenTransforms = dimension.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in childrenTransforms)
                {
                    if (t.tag == "target")
                    {
                        t.gameObject.SetActive(false);
                    }

                    if (t.tag == "wall")
                        t.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }

    public void UpdateDimentions(Dimension currentDim)
    {
        foreach (Dimension dimension in dimensions)
        {
            if (dimension == currentDim)
            {
                setNightOrDay(dimension.night);
                dimension.currentDimention = true;
            }
            else
            {
                dimension.currentDimention = false;
            }
        }
    }

    public void setNightOrDay(bool night)
    {
        if (night)
        {
            //Sun.gameObject.SetActive(false);
            Sun.gameObject.transform.rotation = Quaternion.Euler(-80f,20f,13f);
            //Sun.gameObject.transform.Rotate(40f, 0f, 0f);
            RenderSettings.fog = true;
        }
        else
        {
            Sun.gameObject.transform.rotation = Quaternion.Euler(20f, 20f, 13f);
            //Sun.gameObject.transform.Rotate(-40f, 0f, 0f);
            //Sun.gameObject.SetActive(true);
            RenderSettings.fog = false;
        }
    }
}
