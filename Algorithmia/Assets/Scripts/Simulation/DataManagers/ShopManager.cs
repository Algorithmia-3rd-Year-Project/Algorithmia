using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Transform hardwareCategoryParent;
    [SerializeField] private Transform[] hardwareItemsParents;
    [SerializeField] private List<string> hardwareCategories;
    [SerializeField] private GameObject categoryPrefab;

    private void Start()
    {
        GenerateCategories(hardwareCategories);

        foreach (Transform hardwareParent in hardwareItemsParents)
        {
            for (int i = 0; i < 4; i++)
            {
                InstantiateItems(hardwareParent);
            }
        }
        
    }

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

    private void InstantiateItems(Transform parentItem)
    {
        Addressables.LoadAssetAsync<GameObject>("Hardware Item").Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject shopItem = Instantiate(asyncOperationHandle.Result);
                shopItem.transform.SetParent(parentItem);
                shopItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Debug.Log("Object Instantiated");
            }
            else
            {
                Debug.Log("Failed to Load");
            }
        };
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
