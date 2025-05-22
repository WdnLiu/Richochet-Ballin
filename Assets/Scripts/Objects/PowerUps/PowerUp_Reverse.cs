using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Reverse : MonoBehaviour
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
        powerUpHandler.ActivatePowerUp("Reverse");
        GetComponent<PowerUp>()?.Collect(current);
    }
}
