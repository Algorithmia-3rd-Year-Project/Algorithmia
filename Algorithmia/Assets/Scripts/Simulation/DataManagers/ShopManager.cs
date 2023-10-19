using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Transform hardwareCategoryParent;
    [SerializeField] private List<string> hardwareCategories;
    [SerializeField] private GameObject categoryPrefab;

    private void Start()
    {
        GenerateCategories(hardwareCategories);
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
}
