using UnityEngine;

public class PowerUp_Heart : MonoBehaviour
{
    public AudioManager audioManager;
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
        audioManager.playSound("powerup");
        powerUpHandler.ActivatePowerUp("Heart");

        GetComponent<PowerUp>()?.Collect(current);
    }
}
