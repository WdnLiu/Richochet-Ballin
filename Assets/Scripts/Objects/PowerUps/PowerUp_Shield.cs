using UnityEngine;

public class PowerUp_Shield : MonoBehaviour
{
    private HeightChecker checker;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            180f
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;
        while (current != null && !current.CompareTag("Direction1"))
            current = current.parent;

        if (current == null)
            return;

        PlayerCollisions player = current.GetComponentInChildren<PlayerCollisions>();

        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        if (current == null)
            return;

        checker = current.GetComponentInChildren<HeightChecker>();

        if (player != null)
        {
            checker.RemoveMultipleShoot();
            player.ActivateShield();
        }

        GetComponent<PowerUp>()?.Collect(current);
    }
}
