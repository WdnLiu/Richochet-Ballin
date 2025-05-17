using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    private float lastHeight;
    private float peakHeight;
    private float fallStartTime;
    private float lastShootTime = -Mathf.Infinity;

    private bool isMoving = false;
    private bool hasTriggered = false;

    private float shootCooldown = 1f;
    public float minDistance = 0.5f;
    public float maxDuration = 0.5f;

    public AudioManager audioManager;
    public GameObject bulletPrefab;

    public GameObject cooldownIndicator;
    private int remainingDoubleShots = 0;

    public float doubleShotOffset = 4f; // Space between bullets

    void Start()
    {
        lastHeight = transform.position.y;
        peakHeight = lastHeight;
    }

    void Update()
    {
        bool fastFall = DetectFastMovement(-1, minDistance, maxDuration, Time.deltaTime);

        if (fastFall && Time.time - lastShootTime >= shootCooldown)
            shootBullet();

        cooldownIndicator.transform.localScale = new Vector3(
            Mathf.Clamp(lastShootTime + 1 - Time.time, 0.0f, 1.0f),
            1f,
            1f
        );

        lastHeight = transform.position.y;
    }

    bool DetectFastMovement(int direction, float minDistance, float maxDuration, float deltaTime)
    {
        float currentHeight = transform.position.y;
        float deltaY = currentHeight - lastHeight;
        float currentTime = Time.time;

        bool isMovingInDesiredDirection =
            (direction == -1 && deltaY < -0.2f * deltaTime)
            || (direction == 1 && deltaY > 0.2f * deltaTime);
        bool isOppositeDirection =
            (direction == -1 && deltaY > 0.2f * deltaTime)
            || (direction == 1 && deltaY < -0.2f * deltaTime);

        if (isOppositeDirection)
        {
            isMoving = false;
            hasTriggered = false;
            return false;
        }

        if (!isMoving && isMovingInDesiredDirection)
        {
            hasTriggered = false;
            isMoving = true;
            peakHeight = lastHeight;
            fallStartTime = currentTime;
            return false;
        }

        if (isMoving)
        {
            float distance = Mathf.Abs(currentHeight - peakHeight);
            float duration = currentTime - fallStartTime;

            if (!hasTriggered && distance >= minDistance && duration <= maxDuration)
            {
                hasTriggered = true;
                return true;
            }

            if (Mathf.Abs(deltaY) < 0.2f * deltaTime)
            {
                isMoving = false;
                hasTriggered = false;
                return false;
            }
        }

        return false;
    }

    public void EnableDoubleShot(int numberOfShots)
    {
        remainingDoubleShots = numberOfShots;
    }

    public void shootBullet()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Quaternion flatRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Vector3 forwardDirection = flatRotation * Vector3.forward;
        Vector3 rightDirection = flatRotation * Vector3.right;

        if (remainingDoubleShots > 0)
        {
            Vector3 offset = rightDirection * doubleShotOffset * 1f;

            GameObject bullet1 = Instantiate(
                bulletPrefab,
                (spawnPosition + offset) + (forwardDirection * 10f),
                flatRotation
            );
            GameObject bullet2 = Instantiate(
                bulletPrefab,
                (spawnPosition - offset) + (forwardDirection * 10f),
                flatRotation
            );

            bullet1.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            bullet2.GetComponent<Renderer>().material = GetComponent<Renderer>().material;

            remainingDoubleShots--;
        }
        else
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                spawnPosition + (forwardDirection * 10f),
                flatRotation
            );
            bullet.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
        }

        lastShootTime = Time.time;
        audioManager.playSound("shoot");
    }
}
