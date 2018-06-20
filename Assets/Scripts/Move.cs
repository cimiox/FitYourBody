using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
        StartCoroutine(MovementCircle());
	}

    private IEnumerator MovementCircle()
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / 100);
            transform.position = Vector3.Lerp(startPosition, target.transform.position, fraction);
            yield return null;
        }

        Destroy(gameObject);
    }
}
