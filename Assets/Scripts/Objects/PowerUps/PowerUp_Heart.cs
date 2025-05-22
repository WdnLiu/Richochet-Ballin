using UnityEngine;

public class PowerUp_Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;
        while (current != null && !current.CompareTag("Player"))
        {
            current = current.parent;
        }
        PlayerPowerUp powerUpHandler = current.GetComponent<PlayerPowerUp>();
        powerUpHandler.ActivatePowerUp("Heart");
        GetComponent<PowerUp>()?.Collect(current);
    }
}
