using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoints : MonoBehaviour
{
    public int lifePoints = 3; 
    public GameObject[] lifePointIcons;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        lifePoints -= damage;
        UpdateLifePointIcons(); 
    }

    public void Heal(int healAmount)
    {
        lifePoints += healAmount; 
        UpdateLifePointIcons();
    }


    private void UpdateLifePointIcons()
    {
        for (int i = 0; i < lifePointIcons.Length; i++)
        {
            if (i < lifePoints)
            {
                lifePointIcons[i].SetActive(true); 
            }
            else
            {
                lifePointIcons[i].SetActive(false); 
            }
        }
    }
}
