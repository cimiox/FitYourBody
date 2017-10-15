using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomSystem : ScriptableObject
{
    public static bool isClick { get; set; }
    private static Vector3 startCameraPosition = Vector3.zero;
    private static float startOthographicSize = 5f;

    public static void Zoom(GameObject go)
    {
        Camera.main.orthographicSize = (go.GetComponent<Renderer>().bounds.size.x / Camera.main.aspect) / 1.1f;
        Camera.main.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
        isClick = true;
    }

    public static void Detach()
    {
        PlayerAttributes.ZoomableGO.GetComponent<Muscle>().IsZoom = false;
        Camera.main.orthographicSize = startOthographicSize;
        Camera.main.transform.position = startCameraPosition;
    }
}
