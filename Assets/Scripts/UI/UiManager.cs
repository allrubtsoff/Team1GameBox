using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image mightyPunchSprite;
    [SerializeField] private Image axeThrowSprite;

    [Header("Player")]
    [SerializeField] private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateSprite(float coolDown)
    {
        mightyPunchSprite.fillAmount = coolDown;
    }

    //private IEnumerator FillAmountImage()
    //{

    //}
}
