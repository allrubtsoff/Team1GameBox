using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private float hpRestore;
    [SerializeField] private float energyRestore;

    public Sprite ItemSprite { get { return itemSprite; }  }

    private Health playersHealth;
    private Energy playersEnergy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Inventory>(out Inventory inventory))
        {
            PickUp(inventory);
            playersHealth = other.GetComponent<Health>();
            playersEnergy = other.GetComponent<Energy>();
        }
    }

    public void PickUp(Inventory inventory)
    {
        inventory.AddItem(this);
    }

    public void Use()
    {
        playersHealth.RestoreHealth(hpRestore);
        playersEnergy.RestoreEnergy(energyRestore);
    }
}
