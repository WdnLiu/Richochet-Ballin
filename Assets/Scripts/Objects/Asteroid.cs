using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Meteor Settings")]
    public float spawnHeight = 50f;
    public float targetY = -5f;
    public float delayBeforeFall = 5f;
    public float fallDuration = 3f;

    private Transform target;

    private void Update()
    {
        if (transform.position.y <= targetY)
        {
            Debug.Log("Meteor hit the ground!");
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 targetPosition)
    {
        target = new GameObject("MeteorTarget").transform;
        target.position = new Vector3(targetPosition.x, targetY, targetPosition.z);
        StartCoroutine(FallAfterDelay());
    }

    private IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeFall);

        Vector3 start = transform.position;
        Vector3 end = target.position;
        float elapsed = 0f;

        while (elapsed < fallDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / fallDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        Debug.Log("Meteor hit the ground!");
    }
}
