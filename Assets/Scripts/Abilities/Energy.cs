using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energy;

    public bool CheckEnergyAvailable(float abilitiyCost)
    {
        return energy >= abilitiyCost;
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
