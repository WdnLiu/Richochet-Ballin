using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    public int maxBounces = 3;

    private Vector3 lastNormal;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
