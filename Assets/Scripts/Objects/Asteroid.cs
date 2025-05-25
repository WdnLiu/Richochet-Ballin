using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Meteor Settings")]
    public float spawnHeight;
    public float targetY;
    public float delayBeforeFall;
    public float fallDuration;
    public GameObject shadowPrefab;
    private Transform target;
    private GameObject shadowInstance;

    public ParticleSystem explosionEffect;

    public Transform rotatingChild;
    public MeshRenderer meteoriteMesh;

    private void Start()
    {
        explosionEffect.Stop();
    }

    private void Update()
    {
        if (transform.position.y <= targetY)
        {
            explosionEffect.Play();
            Debug.Log("Meteor hit the ground!");
            shadowInstance.GetComponent<MeshRenderer>().enabled = false;
            meteoriteMesh.enabled = false;
            Destroy(shadowInstance, 1f);
            Destroy(gameObject, 1f);
        }
    }

    public void Initialize(Vector3 targetPosition)
    {
        target = new GameObject("MeteorTarget").transform;
        target.position = new Vector3(targetPosition.x, targetY, targetPosition.z);
        StartCoroutine(FallAfterDelay());
        Vector3 shadowPosition = new Vector3(
            target.position.x,
            target.position.y + 5f,
            target.position.z
        );
        Quaternion shadowRotation = Quaternion.Euler(180f, 0f, 0f);
        shadowInstance = Instantiate(shadowPrefab, shadowPosition, shadowRotation);
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
            rotatingChild.Rotate(Vector3.right * 360 * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    public void RemoveAsteroid()
    {
        Destroy(shadowInstance);
        shadowInstance = null;
        Destroy(gameObject);
    }
}
