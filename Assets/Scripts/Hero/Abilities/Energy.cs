using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] private float energy;
    public float CurrentEnergy { get; set; }

    private void Start()
    {
        CurrentEnergy = energy;
    }
    public bool CheckEnergyAvailable(float abilitiyCost)
    {
        return CurrentEnergy >= abilitiyCost;
    }

    public void UseEnergy(float cost) 
    {
        CurrentEnergy -= cost;
    }

    public void RestoreEnergy(float value)
    {
        CurrentEnergy += value;
        CurrentEnergy = Mathf.Min(CurrentEnergy, energy);
    }
}
