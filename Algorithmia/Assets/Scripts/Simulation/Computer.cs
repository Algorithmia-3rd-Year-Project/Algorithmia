using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField]
    private SimManager simulation;

    private void OnMouseEnter()
    {
        simulation.ChangeMouseCursor(true);
    }

    private void OnMouseOver()
    {
        simulation.ChangeMouseCursor(true);
    }

    private void OnMouseExit()
    {
        simulation.ChangeMouseCursor(false);
    }
}
