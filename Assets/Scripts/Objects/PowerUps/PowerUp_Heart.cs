using UnityEngine;

public class PowerUp_Heart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        // Go up the hierarchy until we find the object tagged "Direction1" (the individual player)
        Transform current = other.transform;
        while (current != null && !current.CompareTag("Direction1"))
        {
            current = current.parent;
        }

        if (current == null)
        {
            Debug.LogWarning("Player root not found.");
            return;
        }

        PlayerCollisions playerCollisions = current.GetComponentInChildren<PlayerCollisions>();

        if (playerCollisions != null && playerCollisions.lifePoints != null)
        {
            LifePoints lifePoints = playerCollisions.lifePoints.GetComponent<LifePoints>();
            if (lifePoints != null)
            {
                lifePoints.Heal(1);
            }
        }

        GetComponent<PowerUp>()?.Collect(current);
    }
}
