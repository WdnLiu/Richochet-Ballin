using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{

    public GameObject directionIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        directionIndicator.transform.rotation = Quaternion.LookRotation(forward);


        Vector3 flatPosition = transform.position;
        flatPosition.y = 0f;
        directionIndicator.transform.position = flatPosition;

    }


}
