using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCircleController : MonoBehaviour
{
    public float Speed { get; set; } = 0.05f;
    public float DeltaTime { get; set; }
    public bool IsMove { get; set; }

    private void Start()
    {
        StartCoroutine(MovementCircle());
    }

    private void Update()
    {
        if (IsMove)
            transform.localPosition = Vector3.Slerp(transform.localPosition, Vector3.zero, Speed);
    }

    private IEnumerator MovementCircle()
    {
        yield return new WaitForSeconds(0.1f);
        IsMove = true;

        yield return new WaitUntil(() => Vector3.Distance(transform.localPosition, Vector3.zero) < 5);
        Destroy(gameObject);
    }
}
