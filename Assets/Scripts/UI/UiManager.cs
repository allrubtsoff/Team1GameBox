using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image mightyPunchImage;
    [SerializeField] private Image axeThrowImage;
    [SerializeField] private Image[] Slots = new Image[2];
    [SerializeField] private Sprite emptyInventorySprite;

    [Header("Player")]
    [SerializeField] private GameObject player;

    private void OnEnable()
    {
        Inventory.UpdateUI += UpdateInventorySlotSprite;
        Health.HPChanged += CheckHpBar;
    }

    private void OnDestroy()
    {
        Inventory.UpdateUI -= UpdateInventorySlotSprite;
        Health.HPChanged -= CheckHpBar;
    }

    public void UpdateInventorySlotSprite(int slotId, Item item, bool itemUsed)
    {
        if (itemUsed)
            Slots[slotId].sprite = emptyInventorySprite;
        else
            Slots[slotId].sprite = item.ItemSprite;
    }

    public void CheckHpBar()
    {
        hpBar.fillAmount = player.GetComponent<Health>().Hp / 100;
        Debug.Log("hpBarChanged");

    }
    
    public void CheckEnergyBar()
    {
        energyBar.fillAmount = player.GetComponent<Energy>().CurrentEnergy / 100;
    }

    public void AxeThrowCooldownSprite(float cooldown)
    {
        StartCoroutine(UpdateAxeThrowCooldownSprite(cooldown));
    }
    
    private IEnumerator UpdateAxeThrowCooldownSprite(float cooldown)
    {
        axeThrowImage.fillAmount = 0;
        for (float i=0; i<cooldown;i+=Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            axeThrowImage.fillAmount = i / cooldown;
        }
    }
    
    public void MightyPunchCooldownSprite(float cooldown)
    {
        StartCoroutine(UpdateMightyPunchCooldownSprite(cooldown));
    }

    private IEnumerator UpdateMightyPunchCooldownSprite(float cooldown)
    {
        mightyPunchImage.fillAmount = 0;
        for (float i = 0; i < cooldown; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            mightyPunchImage.fillAmount = i / cooldown;
        }
    }
}
