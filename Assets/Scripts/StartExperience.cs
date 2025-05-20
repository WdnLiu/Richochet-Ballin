using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartExperience : MonoBehaviour
{
    public bool playerTrigger = false;
    private Color lastColor = Color.white;

    private HashSet<Color> colorSet = new HashSet<Color>();
    private Color originalColor = Color.white;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = originalColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            playerTrigger = !playerTrigger;

            GameObject player = other.transform.parent.parent.gameObject;
            lastColor = player.GetComponent<Renderer>().material.color;
            colorSet.Add(lastColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            GameObject player = other.transform.parent.parent.gameObject;

            colorSet.Remove(player.GetComponent<Renderer>().material.color);

            if (player.GetComponent<Renderer>().material.color == lastColor)
            {
                lastColor = (colorSet.Count > 0) ? colorSet.FirstOrDefault() : originalColor;
            }

            playerTrigger = !playerTrigger;
        }
        // Debug.Log("PlayerCollider exited the trigger.");
    }

    void Update()
    {
        if (lastColor != Color.white)
        {
            GetComponent<Renderer>().material.color = lastColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = originalColor;
        }
    }
}
