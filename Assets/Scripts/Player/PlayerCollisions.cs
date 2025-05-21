using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public GameObject lifePoints;

    public AudioManager audioManager;
    private GameStateManager gameStateManager;
    private bool hasBullet = false;
    private PlayerPowerUp powerUpHandler;

    void Start()
    {
        gameStateManager = GameObject.Find("Game State Manager").GetComponent<GameStateManager>();
        powerUpHandler = GetComponentInParent<PlayerPowerUp>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !gameStateManager.wasHit)
        {
            if (powerUpHandler.HasShield)
            {
                powerUpHandler.RemoveShield();
            }
            else
            {
                if (lifePoints != null)
                {
                    lifePoints.GetComponent<LifePoints>()?.TakeDamage(1);
                    gameStateManager.wasHit = true;
                }
                audioManager.playSound("playerHit");
            }

            Destroy(other.gameObject);
        }
    }
}
