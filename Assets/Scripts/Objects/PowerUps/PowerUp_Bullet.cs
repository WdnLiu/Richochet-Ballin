using UnityEngine;

public class PowerUp_Bullet : MonoBehaviour
{
    public int num_shots = 3;
    public Material powerUpIndicatorMaterial;
    private Material defaultMaterial;
    private HeightChecker checker;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;

        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        if (current == null)
            return;

        Renderer meshRenderer = null;
        foreach (Transform child in current.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Direction1"))
            {
                Transform mesh = child.Find("Direction/Mesh");
                if (mesh != null)
                    meshRenderer = mesh.GetComponent<Renderer>();
                break;
            }
        }

        checker = current.GetComponentInChildren<HeightChecker>();
        PlayerCollisions playerCol = current.GetComponentInChildren<PlayerCollisions>();

        if (checker != null && meshRenderer != null)
        {
            ActivateMultipleShoot(checker, meshRenderer, playerCol);
        }

        GetComponent<PowerUp>()?.Collect(current);
    }

    private void ActivateMultipleShoot(
        HeightChecker checker,
        Renderer meshRenderer,
        PlayerCollisions playerCol
    )
    {
        if (playerCol != null)
            playerCol.RemoveShield();
        if (
            powerUpIndicatorMaterial != null
            && meshRenderer != null
            && checker.remainingMultipleShoots <= 0
        )
        {
            defaultMaterial = meshRenderer.material;
            meshRenderer.material = powerUpIndicatorMaterial;
            checker.EnableMultipleShoot(num_shots, meshRenderer, defaultMaterial);
        }
    }

    public void RemoveMultipleShoot()
    {
        checker.RemoveMultipleShoot();
    }
}
