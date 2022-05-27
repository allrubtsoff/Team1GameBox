using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private float hpRestore;
    [SerializeField] private float energyRestore;

    private Health playersHealth;
    private Energy playersEnergy;

    public UnityEvent UpdateUi;

    public Sprite ItemSprite { get { return itemSprite; }  }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Inventory>(out Inventory inventory) && PickUp(inventory))
        {
                playersHealth = other.GetComponent<Health>();
                playersEnergy = other.GetComponent<Energy>();
        }
    }

    private bool PickUp(Inventory inventory)
    {
        return inventory.AddItem(this);
    }

    public void Use()
    {
        playersHealth.RestoreHealth(hpRestore);
        playersEnergy.RestoreEnergy(energyRestore);
        UpdateUi.Invoke();
    }
}
