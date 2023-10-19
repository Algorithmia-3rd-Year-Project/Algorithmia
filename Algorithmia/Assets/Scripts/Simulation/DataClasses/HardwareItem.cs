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

    private void Update()
    {
        
        itemLogoImage.sprite = itemLogo;
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemPriceText.text = itemPrice.ToString();
        
    }
}
