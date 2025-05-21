using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    public Material defaultMaterial;
    private Renderer meshRenderer;
    public Material MultipleShootMaterial;
    public Material ShieldMaterial;
    public bool HasShield;
    public bool HasBullet;

    void Start()
    {
        string directionName = "Direction" + GetPlayerNumber();
        GameObject direction = GameObject.Find(directionName);
        Transform mesh = direction.transform.Find("Direction/Mesh");
        meshRenderer = mesh.GetComponent<Renderer>();
    }

    public void ActivateShield()
    {
        meshRenderer.material = ShieldMaterial;
        HasShield = true;
        Debug.Log($"{gameObject.name} activated shield!");
    }

    public void RemoveShield()
    {
        meshRenderer.material = defaultMaterial;
        HasShield = false;
        Debug.Log($"{gameObject.name} shield removed.");
    }

    public void ActivateBullet()
    {
        meshRenderer.material = MultipleShootMaterial;
        HasBullet = true;
        Debug.Log($"{gameObject.name} activated bullet!");
    }

    public void RemoveBullet()
    {
        meshRenderer.material = defaultMaterial;
        HasBullet = false;
        Debug.Log($"{gameObject.name} bullet removed.");
    }

    void Update() { }

    int GetPlayerNumber()
    {
        return gameObject.name.Contains("1") ? 1 : 2;
    }

    public void ActivatePowerUp(string powerUp)
    {
        if (powerUp == "Shield")
        {
            RemoveAllPowerUp();
            ActivateShield();
        }
        else if (powerUp == "Bullet")
        {
            RemoveAllPowerUp();
            ActivateBullet();
        }
    }

    public void RemoveAllPowerUp()
    {
        RemoveShield();
        RemoveBullet();
    }
}
