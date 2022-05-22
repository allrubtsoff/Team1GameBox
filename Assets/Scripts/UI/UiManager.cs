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
    private Health health;
    private Energy energy;
    private MIghtyPunch punch;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
        health = player.GetComponent<Health>();
        energy = player.GetComponent<Energy>();
        punch = player.GetComponent<MIghtyPunch>();
    }

    private void Update()
    {
        CheckStates();
    }

    private void CheckStates()
    {
        hpBar.fillAmount = health.Hp / 100;
        energyBar.fillAmount = energy.CurrentEnergy / 100;
        var items = inventory.GetItems();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                Slots[i].sprite = items[i].ItemSprite;
        }
        if (items.Count < 2)
        {

        }
    }
}
