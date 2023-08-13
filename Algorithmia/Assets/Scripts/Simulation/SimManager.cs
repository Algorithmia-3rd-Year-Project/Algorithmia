using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D computerCursor;

    public bool anyMenuOpened;

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

}
