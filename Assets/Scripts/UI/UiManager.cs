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


    [Header("Player")]
    [SerializeField] private GameObject player;

    private Inventory inventory;


    private void OnEnable()
    {
        AxeThrow.UpdateUI += AxeThrowCooldownSprite;
        MIghtyPunch.UpdateUI += MightyPunchCooldownSprite;
    }
    private void OnDisable()
    {
        AxeThrow.UpdateUI -= AxeThrowCooldownSprite;
        MIghtyPunch.UpdateUI -= MightyPunchCooldownSprite;
    }

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        CheckStates();
    }

    private void CheckStates()
    {
        var invent = inventory.GetItems();
        for (int i = 0; i < invent.Count; i++)
        {
            if (invent[i] != null)
                Slots[i].sprite = invent[i].ItemSprite;
            else
                Slots[i].sprite = inventory.CommonSlotImage;
        }
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
