using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Transform hardwareCategoryParent;
    [SerializeField] private Transform[] hardwareItemsParents;
    [SerializeField] private List<string> hardwareCategories;
    [SerializeField] private GameObject categoryPrefab;

    private int j;
    private int k;

    [SerializeField] private List<int> hardwarePartCounts;
    [SerializeField] private List<string> hardwarePartNames;
    [SerializeField] private List<string> hardwarePartDescriptions;
    [SerializeField] private List<int> hardwarePartPrice;
    [SerializeField] private List<Sprite> hardwarePartLogos;
    
    private void Start()
    {
        GenerateCategories(hardwareCategories);
        AddListeners();

        foreach (Transform hardwareParent in hardwareItemsParents)
        {   
            
            for (int i = 0; i < hardwarePartCounts[j]; i++)
            {
                InstantiateItems(hardwareParent, k);
                k++;
            }

            j++;
        }
        
    }

    //Instantiate Categories in hardware shop category
    private void GenerateCategories(List<string> categoryList)
    {
        foreach (string category in categoryList)
        {
            GameObject generatedCategory = Instantiate(categoryPrefab);
            generatedCategory.transform.SetParent(hardwareCategoryParent);
            generatedCategory.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            GameObject childObject = generatedCategory.transform.Find("Text").gameObject;
            childObject.GetComponent<TMP_Text>().text = category;
        }
    }
    
    private void AddListeners()
    {
        for (int i = 0; i < hardwareCategoryParent.childCount; i++)
        {
            GameObject childCategory = hardwareCategoryParent.GetChild(i).gameObject;
            Button categoryButton = childCategory.GetComponent<Button>();
            categoryButton.onClick.AddListener(() => SwitchHardwarePages());
        }
    }

    private void AddPurchaseListeners(GameObject currentItem)
    {
        GameObject buttonObjectParent =
            currentItem.transform.Find("Item Logo Background").gameObject;
        GameObject buttonObject = buttonObjectParent.transform.GetChild(3).gameObject;
        Button buyButton = buttonObject.GetComponent<Button>();
        buyButton.onClick.AddListener(() => PurchaseItem(buttonObject));
    }

    private void PurchaseItem(GameObject clickedBuyButton)
    {
        int itemPrice = int.Parse(clickedBuyButton.transform.Find("Price").gameObject.GetComponent<TMP_Text>().text);
        Debug.Log(itemPrice);
    }

    private void SwitchHardwarePages()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        string buttonText = clickedButton.transform.GetChild(0).GetComponent<TMP_Text>().text;

        for (int i = 0; i < hardwareItemsParents.Length; i++)
        {
            if (hardwareItemsParents[i].gameObject.name == buttonText)
            {
                hardwareItemsParents[i].gameObject.SetActive(true);
                continue;
            }
            hardwareItemsParents[i].gameObject.SetActive(false);
        }
    }

    //Instantiate Items in the hardware shop category
    private void InstantiateItems(Transform parentItem, int index)
    {
        Addressables.LoadAssetAsync<GameObject>("Hardware Item").Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject shopItem = Instantiate(asyncOperationHandle.Result);
                shopItem.transform.SetParent(parentItem);
                shopItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //Debug.Log("Object Instantiated");
                HardwareItemDetails(shopItem, hardwarePartNames[index], hardwarePartDescriptions[index], hardwarePartPrice[index], hardwarePartLogos[index]);
            }
            else
            {
                Debug.Log("Failed to Load");
            }
        };
    }

    private void HardwareItemDetails(GameObject currentItem, string itemName, string itemDescription, int itemPrice, Sprite itemLogo)
    {
        currentItem.GetComponent<HardwareItem>().itemName = itemName;
        currentItem.GetComponent<HardwareItem>().itemDescription = itemDescription;
        currentItem.GetComponent<HardwareItem>().itemPrice = itemPrice;
        currentItem.GetComponent<HardwareItem>().itemLogo = itemLogo;
        AddPurchaseListeners(currentItem);
    }
    
    /*
    private void InstantiateItems()
    {
        AsyncOperationHandle<GameObject>
            asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>("Hardware Item");
        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    }*/

    /*
    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject shopItem = Instantiate(asyncOperationHandle.Result);
            shopItem.transform.SetParent(hardwareItemsParent);
            shopItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Debug.Log("Object Instantiated");
        }
        else
        {
            Debug.Log("Failed to Load");
        }
    }*/
}
