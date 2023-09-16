using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueBlockManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public GameObject emptyQueueBlockPrefab;

    private GameObject currentPrefabInstance; // Store the current instance

    private bool isDragging = false; // Track whether dragging is in progress

  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, layerMask);

            if (hit.collider != null){

                if (hit.collider.name == "Empty Queue Block"){
                   
                    currentPrefabInstance = Instantiate(emptyQueueBlockPrefab, hit.point, Quaternion.identity);  // Instantiate the prefab at the hit point
                    isDragging = true; // Start dragging
                }
                if (hit.collider.name == "Empty Queue Block(Clone)"){
                    
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;
                }
            }
        }

        if (isDragging && Input.GetMouseButton(0) && currentPrefabInstance != null)
        {
            // While the left mouse button is held down and dragging is in progress, update the position of the prefab
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            currentPrefabInstance.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);

        }

        if (Input.GetMouseButtonUp(0) && currentPrefabInstance != null){
            
            isDragging = false;

            if(currentPrefabInstance.transform.position.x < -4.8){
              Destroy(currentPrefabInstance);
            }
        }
    }
}
