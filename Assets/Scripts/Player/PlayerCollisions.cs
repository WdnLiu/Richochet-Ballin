using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject lifePoints;

    [Header("Shield Visuals")]
    public Renderer meshRenderer; // Assign Direction → Mesh
    public Material shieldMaterial; // Shared shield material

    private Material originalMaterial; // Player-specific material
    private bool hasShield = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (hasShield)
            {
                Debug.Log($"{gameObject.name} blocked damage with shield!");
                hasShield = false;

                if (meshRenderer != null && originalMaterial != null)
                    meshRenderer.material = originalMaterial;
            }
            else
            {
                if (lifePoints != null)
                    lifePoints.GetComponent<LifePoints>()?.TakeDamage(1);
            }

            Destroy(other.gameObject);
        }
    }

    public void ActivateShield()
    {
        if (!hasShield && meshRenderer != null)
        {
            originalMaterial = meshRenderer.material;

            if (shieldMaterial != null)
                meshRenderer.material = shieldMaterial;

            hasShield = true;

            Debug.Log($"{gameObject.name} activated shield!");
        }
    }
}
