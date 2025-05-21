using UnityEngine;

public class PowerUp_Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;

        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        PlayerPowerUp playerPowerUp = current.GetComponent<PlayerPowerUp>();
        playerPowerUp.ActivatePowerUp("Bullet");

        GetComponent<PowerUp>()?.Collect(current);
    }
}
