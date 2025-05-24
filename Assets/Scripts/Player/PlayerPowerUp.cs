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
    private GameStateManager gameStateManager;
    public GameObject asteroidPrefab;

    public AudioManager audioManager;

    void Start()
    {
        string directionName = "Direction" + GetPlayerNumber();
        GameObject direction = GameObject.Find(directionName);
        Transform mesh = direction.transform.Find("Direction/Mesh");
        meshRenderer = mesh.GetComponent<Renderer>();
        gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
    }

    private void ActivateShield()
    {
        meshRenderer.material = ShieldMaterial;
        HasShield = true;
        Debug.Log($"{gameObject.name} activated shield!");

        audioManager.playSound("shield");  
    }

    private void RemoveShield()
    {
        meshRenderer.material = defaultMaterial;
        HasShield = false;
        Debug.Log($"{gameObject.name} shield removed.");
    }

    private void ActivateBullet()
    {
        meshRenderer.material = MultipleShootMaterial;
        HasBullet = true;
        Debug.Log($"{gameObject.name} activated bullet!");

        audioManager.playSound("bullet");  
    }

    private void RemoveBullet()
    {
        meshRenderer.material = defaultMaterial;
        HasBullet = false;
        Debug.Log($"{gameObject.name} bullet removed.");
    }

    private void ActivateReverse()
    {
        LifePoints life1 = gameStateManager.player1.GetComponentInChildren<LifePoints>();
        LifePoints life2 = gameStateManager.player2.GetComponentInChildren<LifePoints>();

        int temp = life1.lifePoints;
        life1.setLifePoints(life2.lifePoints);
        life2.setLifePoints(temp);

        audioManager.playSound("reverse");  
    }

    private void ActivateHeart()
    {
        string directionName = "Direction" + GetPlayerNumber();
        GameObject direction = GameObject.Find(directionName);

        PlayerCollisions playerCollisions = direction.GetComponentInChildren<PlayerCollisions>();
        LifePoints lifePoints = playerCollisions.lifePoints.GetComponent<LifePoints>();
        lifePoints.Heal(1);

        audioManager.playSound("heart");  
    }

    private void ActivateTarget()
    {
        GameObject enemy = GetEnemyPlayer();
        if (enemy != null)
        {
            StartCoroutine(SpawnMeteor(enemy.transform));
            Debug.Log(
                $"{gameObject.name} activated Target power-up! Meteor will fall in 5 seconds."
            );
        }
    }

    private IEnumerator SpawnMeteor(Transform enemyTransform)
    {
        yield return new WaitForSeconds(0.1f);
        if (enemyTransform != null)
        {
            Vector3 spawnPos = new Vector3(
                enemyTransform.position.x,
                asteroidPrefab.GetComponent<Asteroid>().spawnHeight,
                enemyTransform.position.z
            );

            GameObject meteor = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            meteor.GetComponent<Asteroid>().Initialize(enemyTransform.position);
        }
    }

    public void RemoveMeteor() { }

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
        else if (powerUp == "Reverse")
        {
            ActivateReverse();
        }
        else if (powerUp == "Heart")
        {
            ActivateHeart();
        }
        else if (powerUp == "Target")
        {
            ActivateTarget();
        }
    }

    public void RemoveAllPowerUp()
    {
        RemoveShield();
        RemoveBullet();
    }

    public GameObject GetEnemyPlayer()
    {
        return gameObject.name.Contains("1") ? gameStateManager.player2 : gameStateManager.player1;
    }
}
