using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private Transform parentBlock;

    [SerializeField] private GameObject singleBlockPrefab;

    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private Sprite visibleImage;

    [SerializeField] private List<Sprite> flippedImages;

    private bool firstGuess;
    private bool secondGuess;

    public GameObject firstGuessObject;
    public GameObject secondGuessObject;
    
    public string firstGuessName;
    public string secondGuessName;

    [SerializeField] private TMP_Text matchesCountText;
    [SerializeField] private TMP_Text movesCountText;

    private int movesCount;
    private int matchesCount;

    [SerializeField] private GameObject dataStructuresList;
    
    private void Awake()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject block = Instantiate(singleBlockPrefab);
            block.name = "" + i;
            block.transform.SetParent(parentBlock, false);
        }
        ShuffleList(flippedImages);
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
    }

    private void Update()
    {
        movesCountText.text = (movesCount < 10) ? "0" + movesCount : movesCount.ToString();
        matchesCountText.text = (matchesCount < 10) ? "0" + matchesCount : matchesCount.ToString();
    }

    private void GetButtons()
    {
        for (int i = 0; i < parentBlock.transform.childCount; i++)
        {
            
            Transform block = parentBlock.transform.GetChild(i);
            GameObject frontImage = block.transform.Find("Front Image").gameObject;
            frontImage.GetComponent<Image>().sprite = visibleImage;
            
            GameObject flippedImage = block.transform.Find("Flipped Image").gameObject;
            flippedImage.GetComponent<Image>().sprite = flippedImages[i];
            
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
        //Debug.Log(name);
        Transform currentObj = EventSystem.current.currentSelectedGameObject.transform;
        GameObject frontImage = currentObj.Find("Front Image").gameObject;
        GameObject flippedImage = currentObj.Find("Flipped Image").gameObject;
        
        string flippedImageName = flippedImage.GetComponent<Image>().sprite.name;
        string[] substrings = flippedImageName.Split('-');
        string checkName = substrings[0] + " " + substrings[1];
        
        movesCount++;
        
        if (!firstGuess)
        {
            frontImage.SetActive(false);
            flippedImage.SetActive(true);
            firstGuess = true;
            firstGuessName = checkName;
            firstGuessObject = currentObj.gameObject;
        } else if (!secondGuess)
        {
            frontImage.SetActive(false);
            flippedImage.SetActive(true);
            secondGuess = true;
            secondGuessName = checkName;
            secondGuessObject = currentObj.gameObject;

            StartCoroutine(CheckStatus());
        }
    }

    private IEnumerator CheckStatus()
    {
        //yield return new WaitForSeconds(1f);

        if (firstGuessName == secondGuessName)
        {
            yield return new WaitForSeconds(0.8f);

            firstGuessObject.GetComponent<Button>().interactable = false;
            secondGuessObject.GetComponent<Button>().interactable = false;

            //change buttons color to greyish color
            CorrectBoxEffect(firstGuessObject);
            CorrectBoxEffect(secondGuessObject);
            
            matchesCount++;
            
            firstGuessName = "";
            secondGuessName = "";
        }
        else
        {
            yield return new WaitForSeconds(1.3f);

            ResetGuessedBlocks(firstGuessObject);
            ResetGuessedBlocks(secondGuessObject);
            firstGuessName = "";
            secondGuessName = "";
        }

        
        //yield return new WaitForSeconds(0.5f);
        firstGuess = secondGuess = false;
        firstGuessObject = secondGuessObject = null;
        //firstGuessName = secondGuessName = "";
        
    }

    private void ResetGuessedBlocks(GameObject currentObj)
    {
        GameObject frontImage = currentObj.transform.Find("Front Image").gameObject;
        frontImage.SetActive(true);
        GameObject flippedImage = currentObj.transform.Find("Flipped Image").gameObject;
        flippedImage.SetActive(false);
    }

    private void CorrectBoxEffect(GameObject currentObj)
    {
        GameObject flippedImage = currentObj.transform.Find("Flipped Image").gameObject;
        flippedImage.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
    }
    
    //Shuffle the list using Fisher-Yates Shuffle Algorithm
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int randIndex = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    public void SelectDataStructure()
    {
        if (!dataStructuresList.activeSelf)
        {
            dataStructuresList.SetActive(true);
        }
        else
        {
            dataStructuresList.SetActive(false);
        }
    }
}
