using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartExperience : MonoBehaviour
{
    public bool player1Trigger = false;
    public bool player2Trigger = false;
    bool startExperience = false;

    // Start is called before the first frame update
    void Start() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
            player1Trigger = true;
        if (other.tag == "Player2")
            player2Trigger = true;
        Debug.Log(other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
            player1Trigger = false;
        if (other.tag == "Player2")
            player2Trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Trigger && player2Trigger && !startExperience)
        {
            startExperience = true;
            Debug.Log("Start experience");
        }
    }
}
