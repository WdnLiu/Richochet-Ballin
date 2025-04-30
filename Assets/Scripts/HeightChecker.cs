using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    private bool upwards = false;
    private bool downwards = false;
    private float timer = 0.0f;
    private float timeLimit = 1.0f; 



    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootBullet();
        }
    }

    private void shootBullet(){
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.tag);
        if (other.tag == "LowGround") {
            if (downwards && timer < timeLimit) Debug.Log("Vertical downwards movement");
            upwards = downwards = false;
            shootBullet();
        }
        if (other.tag == "HighGround"){
            if (upwards && timer < timeLimit) Debug.Log("Vertical upwards movement");
            upwards = downwards = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collision left with: " + other.tag);
        if (other.tag == "LowGround") {
            upwards = true;
            timer = 0.0f;
        }
        if (other.tag == "HighGround") {
            downwards = true;
            timer = 0.0f;
        }
    }
}
