using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField]
    private SimManager simulation;

    [SerializeField]
    private GameObject computerScreen;

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

    private void OnMouseDown()
    {
        computerScreen.SetActive(true);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void Shutdown()
    {
        computerScreen.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}
