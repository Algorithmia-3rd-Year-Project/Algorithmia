using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private Transform parentBlock;

    [SerializeField] private GameObject singleBlockPrefab;

    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private Sprite visibleImage;

    [SerializeField] private List<Sprite> flippedImages;
    
    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject block = Instantiate(singleBlockPrefab);
            block.name = "" + i;
            block.transform.SetParent(parentBlock, false);
        }
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
    }

    private void GetButtons()
    {
        for (int i = 0; i < parentBlock.transform.childCount; i++)
        {
            
            Transform block = parentBlock.transform.GetChild(i);
            GameObject frontImage = block.transform.Find("Front Image").gameObject;
            frontImage.GetComponent<Image>().sprite = visibleImage;
            buttons.Add(block.GetComponent<Button>());
        }

    }

    private void AddListeners()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => PickABlock());
        }
    }

    public void PickABlock()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(name);
    }
}
