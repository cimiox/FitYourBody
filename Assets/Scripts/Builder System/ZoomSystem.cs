using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomSystem : MonoBehaviour
{
    public static void Zoom(GameObject go)
    {
        Camera.main.orthographicSize = (go.GetComponent<Renderer>().bounds.size.x / Camera.main.aspect) / 3;
        Camera.main.transform.position = new Vector3(go.transform.position.x, go.transform.position.x, -10);
        //
    }
}
