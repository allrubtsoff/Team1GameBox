using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseEnergy(float cost) 
    {
        energy -= cost;
    }

    public void RestoreEnergy(float value)
    {
        energy += value;
    }
}
