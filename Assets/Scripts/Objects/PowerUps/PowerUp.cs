using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Action onCollected;

    [Header("Idle Animation")]
    public float rotationSpeed = 3f; // degrees per second

    void Update()
    {
        // Rotate around all axes slowly
        transform.Rotate(new Vector3(15, 30, 45) * rotationSpeed * Time.deltaTime);
    }

    public void Collect(Transform collector)
    {
        Debug.Log($"Power-up collected by: {collector.name}");
        onCollected?.Invoke();
        Destroy(gameObject);
    }
}
