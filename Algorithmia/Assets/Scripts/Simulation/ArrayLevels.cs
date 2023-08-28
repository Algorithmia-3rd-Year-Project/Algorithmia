using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrayLevels : MonoBehaviour
{
    public void LoadLevels(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
