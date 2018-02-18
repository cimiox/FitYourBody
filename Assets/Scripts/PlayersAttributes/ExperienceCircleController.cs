using System;
using System.Collections;
using UnityEngine;

public class ExperienceCircleController : MonoBehaviour
{
    public float Speed { get; set; } = 0.5f;

    private IEnumerator MovementCircle()
    {
        Vector2 startPosition = transform.position;
        var target = transform.parent.Find("Target");
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / Speed);
            transform.position = Vector3.Lerp(startPosition, transform.parent.position, fraction);
            yield return null;
        }

        Destroy(gameObject);
    }

    internal void StartMove()
    {
        StartCoroutine(MovementCircle());
    }
}
