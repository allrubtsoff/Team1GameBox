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
    }

    private void OnDisable()
    {
        Inventory.UpdateUI -= UpdateInventorySlotSprite;
    }

    public void UpdateInventorySlotSprite(int slotId, Item item, bool itemUsed)
    {
        if (itemUsed)
            Slots[slotId].sprite = emptyInventorySprite;
        else
            Slots[slotId].sprite = item.ItemSprite;
    }


    public void CheckHpBar(float hpValue)
    {
        hpBar.fillAmount = hpValue / 100;
    }
    
    public void CheckEnergyBar(float energyValue)
    {
        energyBar.fillAmount = energyValue / 100;
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
