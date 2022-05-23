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
    [SerializeField] PlayerAbilitiesConfigs configs;

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


    public void CheckHpBar()
    {
        hpBar.fillAmount = player.GetComponent<Health>().Hp / 100;
    }
    
    public void CheckEnergyBar()
    {
        energyBar.fillAmount = player.GetComponent<Energy>().CurrentEnergy / 100;
    }

    public void AxeThrowCooldownSprite()
    {
        StartCoroutine(UpdateAxeThrowCooldownSprite());
    }
    
    private IEnumerator UpdateAxeThrowCooldownSprite()
    {
        axeThrowImage.fillAmount = 0;
        for (float i=0; i<configs.axeThrowCooldown;i+=Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            axeThrowImage.fillAmount = i / configs.axeThrowCooldown;
        }
    }
    
    public void MightyPunchCooldownSprite()
    {
        StartCoroutine(UpdateMightyPunchCooldownSprite());
    }

    private IEnumerator UpdateMightyPunchCooldownSprite()
    {
        mightyPunchImage.fillAmount = 0;
        for (float i = 0; i < configs.mightyPunchCooldown; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            mightyPunchImage.fillAmount = i / configs.mightyPunchCooldown;
        }
    }
}
