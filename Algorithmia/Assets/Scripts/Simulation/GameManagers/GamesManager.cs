using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : MonoBehaviour
{
    [SerializeField] private GameObject memoryGame;

    public void LoadBigOMatchGame()
    {
        memoryGame.SetActive(true);
    }
}
