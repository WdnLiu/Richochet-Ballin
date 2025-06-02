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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.CompareTag("Bullet"));
        if (
            (
                (
                    collision.gameObject.CompareTag("Bullet")
                    || collision.gameObject.CompareTag("Asteroid")
                )
            ) && !gameStateManager.wasHit
        )
        {
            Rigidbody bullet_rb = collision.gameObject.GetComponent<Rigidbody>();
            if (powerUpHandler.HasShield)
            {
                powerUpHandler.RemoveAllPowerUp();

                Vector3 surfaceNormal = -collision.contacts[0].normal;
                surfaceNormal.y = 0f; // keep it flat on the XZ plane

                Vector3 newDirection = surfaceNormal.normalized;

                // Optional: rotate the bullet to face the new direction
                collision.gameObject.transform.rotation = Quaternion.LookRotation(
                    newDirection,
                    Vector3.up
                );

                // Maintain the same speed
                float currentBulletSpeed = bullet_rb.velocity.magnitude;
                bullet_rb.velocity = newDirection * currentBulletSpeed;

                audioManager.playSound("playerShield");
            }
            else
            {
                if (lifePoints != null)
                {
                    lifePoints.GetComponent<LifePoints>()?.TakeDamage(1);
                    gameStateManager.wasHit = true;
                }
                audioManager.playSound("playerHit");
                if (!collision.gameObject.CompareTag("Asteroid"))
                    Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                powerUpHandler.RemoveMeteor();
                return;
            }
        }
    }
}
