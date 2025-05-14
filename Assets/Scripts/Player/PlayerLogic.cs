using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public GameObject[] elementsSetMaterial;
    // Start is called before the first frame update


    private void setMaterials()
    {
        for (int i = 0; i < elementsSetMaterial.Length; i++)
            elementsSetMaterial[i].GetComponent<Renderer>().material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        setMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
