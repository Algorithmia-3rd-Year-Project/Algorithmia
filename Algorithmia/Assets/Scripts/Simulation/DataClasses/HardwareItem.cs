using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardwareItem : MonoBehaviour
{

    [SerializeField] private Image itemLogoImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_Text itemPriceText;

    public string itemName;
    public string itemDescription;
    public int itemPrice;
    public Sprite itemLogo;

    [SerializeField] private Button buyButton;
    [SerializeField] private Image buttonBackground;
    private SimManager simulation;

    public string itemCategoryName;
    private string deactiveColor = "#52634F";
    private string activeColor = "#61CA4D";

    private Color deactiveRGBcolor;
    private Color activeRGBcolor;
    
    private void Start()
    {
        simulation = FindObjectOfType<SimManager>();
        ColorUtility.TryParseHtmlString(deactiveColor, out deactiveRGBcolor);
        ColorUtility.TryParseHtmlString(activeColor, out activeRGBcolor);
    }

    private void Update()
    {
        
        itemLogoImage.sprite = itemLogo;
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemPriceText.text = itemPrice.ToString();

        if (simulation.coins < itemPrice)
        {
            buyButton.interactable = false;
            buttonBackground.color = deactiveRGBcolor;
        }
        else
        {
            buyButton.interactable = true;
            buttonBackground.color = activeRGBcolor;
        }
        
    }
}
