using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxhealth;

    int health;
    // Start is called before the first frame update
    void Start()
    {   
        health = maxhealth;
        
    }

    public void DealDamage(int damage)
    {
        if (health == 0) {
            return;
        }
        health =  Mathf.Max(health - damage,0 );

        Debug.Log(health);
    }


}
