using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energy;

    public void UseEnergy(float cost) 
    {
        energy -= cost;
    }

    public void RestoreEnergy(float value)
    {
        energy += value;
    }
}
