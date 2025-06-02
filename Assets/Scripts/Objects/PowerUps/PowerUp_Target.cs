using UnityEngine;

public class PowerUp_Target : MonoBehaviour
{
    public GameObject meteorPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;
        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        PlayerPowerUp powerUpHandler = current.GetComponent<PlayerPowerUp>();
        powerUpHandler.ActivatePowerUp("Target");
        GetComponent<PowerUp>()?.Collect(current);
    }
}
