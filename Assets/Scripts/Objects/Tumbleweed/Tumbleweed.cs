using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    private TumbleweedSpawner tumbleweedSpawner;
    public float moveSpeed = 2f;
    public float bounceAmplitude = 0.5f;
    public float bounceFrequency = 2f;
    public float rotationSpeed = 180f; // Degrees per second for rolling
    private float startY;
    private float bounceTimer;

    public AudioManager audioManager;

    public void SetSpawner(TumbleweedSpawner spawner)
    {
        tumbleweedSpawner = spawner;
    }

    void Start()
    {
        startY = transform.position.y;
        bounceTimer = Random.Range(0f, Mathf.PI * 2f); // Offset added so they don't always bounce exactly the same
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Horizontal movement
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        // Vertical movement
        bounceTimer += Time.deltaTime * bounceFrequency;
        float bounceOffset = Mathf.Sin(bounceTimer) * bounceAmplitude;
        Vector3 pos = transform.position;
        pos.y = startY + bounceOffset;
        transform.position = pos;
        // Rotational movement (simulate rolling)
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        manageSFX(bounceOffset);
    }

    private void manageSFX(float amplitude)
    {
        if (amplitude <= -bounceAmplitude + 0.5f)
            audioManager.playSound("Tumbleweed");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dropper"))
        {
            if (tumbleweedSpawner != null)
            {
                tumbleweedSpawner.RemoveTumbleweedFromList(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
