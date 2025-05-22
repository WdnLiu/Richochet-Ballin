using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Action onCollected;

    [Header("Idle Animation")]
    public float rotationSpeed = 3f;

    [Header("Lifetime")]
    private float lifetime = 10f;
    public float blinkDuration = 3f;
    public float blinkInterval = 0.2f;

    private float timer = 0f;
    private Renderer objRenderer;
    private float blinkTimer = 0f;
    private bool isVisible = true;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * rotationSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (lifetime - timer <= blinkDuration)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
                isVisible = !isVisible;
                if (objRenderer != null)
                    objRenderer.enabled = isVisible;
            }
        }
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    public void Collect(Transform collector)
    {
        onCollected?.Invoke();
        Destroy(gameObject);
    }
}
