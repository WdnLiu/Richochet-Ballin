using UnityEngine;

public class PowerUp_Shield : MonoBehaviour
{
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
        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        PlayerPowerUp powerUpHandler = current.GetComponent<PlayerPowerUp>();
        powerUpHandler.ActivatePowerUp("Shield");
        GetComponent<PowerUp>()?.Collect(current);
    }
}
