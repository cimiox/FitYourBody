using System.Collections;
using UnityEngine;

public class ExperienceCircleController : MonoBehaviour
{
    public float Speed { get; set; } = 0.05f;
    public float DeltaTime { get; set; }
    public float TimeForEndPoint { get; set; } = 1f;

    private void Awake()
    {
        StartCoroutine(MovementCircle());
    }

    private IEnumerator MovementCircle()
    {
        Vector2 startPosition = transform.localPosition;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / TimeForEndPoint);
            transform.localPosition = new Vector2(
                Mathf.SmoothStep(startPosition.x, 0, fraction),
                Mathf.SmoothStep(startPosition.y, 0, fraction));
            yield return null;
        }

        Destroy(gameObject);
    }
}
