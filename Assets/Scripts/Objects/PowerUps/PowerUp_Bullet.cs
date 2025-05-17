using UnityEngine;

public class PowerUp_Bullet : MonoBehaviour
{
    public int num_shots = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerCollider"))
            return;

        Transform current = other.transform;

        while (current != null && !current.CompareTag("Player"))
            current = current.parent;

        if (current == null)
        {
            return;
        }

        HeightChecker checker = current.GetComponentInChildren<HeightChecker>();
        if (checker != null)
        {
            checker.EnableDoubleShot(num_shots);
        }

        GetComponent<PowerUp>()?.Collect(current);
    }
}
