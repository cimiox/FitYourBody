using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomSystem : MonoBehaviour
{
    public static bool isClick { get; set; }

    public static void Zoom(GameObject go)
    {
        Camera.main.orthographicSize = (go.GetComponent<Renderer>().bounds.size.x / Camera.main.aspect) / 12;
        Camera.main.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
        isClick = true;
    }
}
