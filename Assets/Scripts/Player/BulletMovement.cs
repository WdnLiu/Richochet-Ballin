using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    public int maxBounces = 3;

    private Vector3 lastNormal;

    public AudioManager audioManager;

    private Rigidbody rb;
    private float summonTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = false;
        summonTime = Time.time;
    }

    void Start() { }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Time.time - summonTime > 0.2f)
        {
            rb.detectCollisions = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls") && collision.contactCount > 0)
            lastNormal = collision.contacts[0].normal;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Vector3 incomingDirection = transform.forward;
            Vector3 normal = new Vector3(lastNormal.x, 0f, lastNormal.z).normalized;

            float dot = Vector3.Dot(incomingDirection, normal);
            Vector3 reflectedDirection = incomingDirection - 2 * dot * normal;

            reflectedDirection.y = 0f;
            reflectedDirection = reflectedDirection.normalized;

            transform.rotation = Quaternion.LookRotation(reflectedDirection, Vector3.up);

            if (--maxBounces <= 0)
                Destroy(gameObject);
            else
                audioManager.playSound("bounce");
        }
    }
}
