using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D computerCursor;

    public bool anyMenuOpened;

    //temporary code
    [SerializeField]
    private Slider energyBar;
    [SerializeField]
    private TMP_Text energyPercent;

    private void Start()
    {
        anyMenuOpened = false;
    }

    public void ChangeMouseCursor(bool computerCursorEnabled)
    {

        if (computerCursorEnabled)
        {
            Cursor.SetCursor(computerCursor, Vector2.zero, CursorMode.Auto);
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        
    }

    public void MenuCloseDetection()
    {
        anyMenuOpened = false;
    }

    //Temporary Functions
    public void IncreaseEnergy()
    {
        energyBar.value = 0.08f;
        energyPercent.text = "4%";
    }
}
