using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class QueueBlockManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public RaycastHit2D GlobalHit;

    public GameObject emptyQueueBlockPrefab;
    public GameObject enqueueBlockPrefab;


    public GameObject dequeueBlockPrefab;

    private GameObject currentPrefabInstance;

    private GameObject queueBlock;

    //private Transform queuedBlockSnapPoint;  

    private bool isDragging = false;

    private bool isMoved;

    private bool isOnTopOfEnqueueBlock = false;

    private GameObject DataBlockTopOfEnqueueBlock;

    List<string> enqueueDataBlocks = new List<string>(4);

    Vector3 mousePos1;

    Vector3 mousePos2; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero, 20f, layerMask);
           
            GlobalHit = hit; 



            if (hit.collider != null)
            {

                if (hit.collider.name == "Empty Queue Block")
                {
                    currentPrefabInstance = Instantiate(emptyQueueBlockPrefab, hit.point, Quaternion.identity);
                    currentPrefabInstance.tag = "Respawn";
                    isDragging = true;
                }
                else if (hit.collider.name == "Empty Queue Block(Clone)")
                {
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;

                    mousePos1 = mousePos;
                    //print("mousePos1 :" + mousePos1);
                }
                else if (hit.collider.name == "Enqueue")
                {
                    currentPrefabInstance = Instantiate(enqueueBlockPrefab, hit.point, Quaternion.identity);
                    currentPrefabInstance.tag = "Finish";
                    isDragging = true;
                }
                else if (hit.collider.name == "Enqueue(Clone)")
                {
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;

                    mousePos1 = mousePos;

                }

                else if (hit.collider.name == "Dequeue")
                {
                    currentPrefabInstance = Instantiate(dequeueBlockPrefab, hit.point, Quaternion.identity);
                    currentPrefabInstance.tag = "Finish";
                    isDragging = true;

                }
                else if (hit.collider.name == "Dequeue(Clone)")
                {
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;

                    mousePos1 = mousePos;

                }

                else if (hit.collider.name == "Data block1")
                {
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;

                    mousePos1 = mousePos;
                }
                else if (hit.collider.name == "Data block2")
                {
                    currentPrefabInstance = hit.collider.gameObject;
                    isDragging = true;

                    mousePos1 = mousePos;
                }

                //print(GlobalHit.collider.name);
            }
        }

        if (isDragging && Input.GetMouseButton(0) && currentPrefabInstance != null)
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos2 = mousePos;


            if(Math.Abs(mousePos1.x - mousePos2.x)>0.1 || Math.Abs(mousePos1.y - mousePos2.y)>0.1){

              currentPrefabInstance.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);

              isMoved = true;
         }
        } 

        if (Input.GetMouseButtonUp(0) && currentPrefabInstance != null)
        {
            isDragging = false;
            if (currentPrefabInstance.transform.position.x < -4.8)
            {
                Destroy(currentPrefabInstance);
            }
        }

        if (currentPrefabInstance != null && (currentPrefabInstance.name == "Data block1" || currentPrefabInstance.name == "Data block2"))
        {
            GameObject[] enqueueObjects = GameObject.FindGameObjectsWithTag("Finish");
            Collider2D dataBlockCollider = currentPrefabInstance.GetComponent<Collider2D>();

            if (enqueueObjects.Length > 0)
            {
                bool touchingEnqueueObject = false; // Flag to check if touching any enqueueObject

                foreach (GameObject enqueueObject in enqueueObjects)
                {
                    Collider2D enqueueCollider = enqueueObject.GetComponent<Collider2D>();
                    SpriteRenderer dataBlockSpriteRenderer = currentPrefabInstance.GetComponent<SpriteRenderer>();
                    SpriteRenderer enqueueBlockSpriteRenderer = enqueueObject.GetComponent<SpriteRenderer>();

                    Bounds enqueueBlock = enqueueBlockSpriteRenderer.bounds;
                    Bounds dataBlock = dataBlockSpriteRenderer.bounds;

                    float enqueueLeftEdge = enqueueBlock.min.x;
                    float enqueueRightEdge = enqueueBlock.max.x;
                    float enqueueTopEdge = enqueueBlock.max.y;
                    float enqueueBottomEdge = enqueueBlock.min.y;

                    float dataBlockLeftEdge = dataBlock.min.x;
                    float dataBlockRightEdge = dataBlock.max.x;
                    float dataBlockTopEdge = dataBlock.max.y;
                    float dataBlockBottomEdge = dataBlock.min.y;

                    if (dataBlockCollider.IsTouching(enqueueCollider))
                    {
                        if (Input.GetMouseButtonUp(0) && (enqueueTopEdge - 0.5f) <= dataBlockBottomEdge)
                        {
                            float dataBlockNewX = (enqueueRightEdge + enqueueLeftEdge) / 2;
                            float dataBlockNewY = enqueueTopEdge + 0.4f;

                            currentPrefabInstance.transform.position = new Vector3(dataBlockNewX, dataBlockNewY, 0f);

                             
                            DataBlockTopOfEnqueueBlock = currentPrefabInstance;


                            touchingEnqueueObject = true;
                            isOnTopOfEnqueueBlock = true;
                        }
                    }
                }

                if (touchingEnqueueObject == false && Input.GetMouseButtonUp(0))
                {
                    if (currentPrefabInstance.name == "Data block1")
                    {
                        currentPrefabInstance.transform.position = new Vector3(-4f, -3.8f, 0f); // Default position for Data block1
                        isOnTopOfEnqueueBlock = false;
                    }
                    else if (currentPrefabInstance.name == "Data block2")
                    {
                        currentPrefabInstance.transform.position = new Vector3(-2.5f, -3.8f, 0f); // Default position for Data block2
                        isOnTopOfEnqueueBlock = false;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (currentPrefabInstance.name == "Data block1")
                {
                    currentPrefabInstance.transform.position = new Vector3(-4f, -3.8f, 0f); // Default position for Data block1
                    isOnTopOfEnqueueBlock = false;
                }
                else if (currentPrefabInstance.name == "Data block2")
                {
                    currentPrefabInstance.transform.position = new Vector3(-2.5f, -3.8f, 0f); // Default position for Data block2
                    isOnTopOfEnqueueBlock = false;
                }
            }
        }


        if (currentPrefabInstance != null && currentPrefabInstance.name == "Empty Queue Block(Clone)")
        {
            GameObject[] enqueueObjects = GameObject.FindGameObjectsWithTag("Finish");
            Collider2D queueBlockCollider = currentPrefabInstance.GetComponent<Collider2D>();

            if (enqueueObjects.Length > 0)
            {


                foreach (GameObject enqueueObject in enqueueObjects)
                {
                    Collider2D enqueueCollider = enqueueObject.GetComponent<Collider2D>();
                    SpriteRenderer queueBlockSpriteRenderer = currentPrefabInstance.GetComponent<SpriteRenderer>();
                    SpriteRenderer enqueueBlockSpriteRenderer = enqueueObject.GetComponent<SpriteRenderer>();

                    Bounds enqueueBlock = enqueueBlockSpriteRenderer.bounds;
                    Bounds queueBlock = queueBlockSpriteRenderer.bounds;

                    float enqueueLeftEdge = enqueueBlock.min.x;
                    float enqueueRightEdge = enqueueBlock.max.x;
                    float enqueueTopEdge = enqueueBlock.max.y;
                    float enqueueBottomEdge = enqueueBlock.min.y;

                    float queueBlockLeftEdge = queueBlock.min.x;
                    float queueBlockRightEdge = queueBlock.max.x;
                    float queueBlockTopEdge = queueBlock.max.y;
                    float queueBlockBottomEdge = queueBlock.min.y;

                    if (queueBlockCollider.IsTouching(enqueueCollider))
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            float queueBlockNewX = (enqueueRightEdge + enqueueLeftEdge) / 2;
                            float queueBlockNewY = enqueueBottomEdge - 1.75f;

                            currentPrefabInstance.transform.position = new Vector3(queueBlockNewX, queueBlockNewY, 0f);


                        }
                    }
                }

            }
        }









        if (GlobalHit.collider != null)
        {

            if (GlobalHit.collider.name == "EnqueueBtn" && Input.GetMouseButtonDown(0) && isOnTopOfEnqueueBlock == true)
            {


                GameObject enquBlock = GlobalHit.collider.transform.parent.gameObject;

                Collider2D enquBlockCollider = enquBlock.GetComponent<Collider2D>();


                GameObject[] queueBlocks = GameObject.FindGameObjectsWithTag("Respawn");

                if (queueBlocks.Length > 0)
                {

                    foreach (GameObject queueBlock in queueBlocks)
                    {

                        Collider2D queueBlockCollider = queueBlock.GetComponent<Collider2D>();

                        if (enquBlockCollider.IsTouching(queueBlockCollider))
                        {

                            if (enqueueDataBlocks.Count <= 3)
                            {

                                Transform snappoints = queueBlock.transform.Find("snap points");
                                Transform Rear = queueBlock.transform.Find("Rear");
                                Transform queuedBlockSnapPoint = snappoints.transform.Find(enqueueDataBlocks.Count.ToString());
                                enqueueDataBlocks.Add(DataBlockTopOfEnqueueBlock.name);

                                DataBlockTopOfEnqueueBlock.transform.position = queuedBlockSnapPoint.position;
                                currentPrefabInstance = null;

                                isOnTopOfEnqueueBlock = false;

                                if (Rear != null && (enqueueDataBlocks.Count > 1))
                                {

                                    Vector3 currentPosition = Rear.transform.position;
                                    currentPosition.y += 1;
                                    Rear.transform.position = currentPosition;

                                }

                            }


                        }

                    }
                }

            }

            if (GlobalHit.collider.name == "Empty Queue Block(Clone)" && isMoved == true)
            {

                int index = 0;

                foreach (string item in enqueueDataBlocks)
                {

                    GameObject foundObject = GameObject.Find(item);

                    if (foundObject != null)
                    {

                        Transform snappoints = currentPrefabInstance.transform.Find("snap points");
                        Transform queuedBlockSnapPoint = snappoints.transform.Find(index.ToString());
                        foundObject.transform.position = queuedBlockSnapPoint.position;
                    }

                    index++;

                }



            }

            if (GlobalHit.collider.name == "DequeueBtn" && Input.GetMouseButtonDown(0) && enqueueDataBlocks.Count > 0)
            {
          
                GameObject frontBlock = GameObject.Find(enqueueDataBlocks[0]);
                enqueueDataBlocks.RemoveAt(0); 
                Destroy(frontBlock); 

               
                GameObject[] queueBlocks = GameObject.FindGameObjectsWithTag("Respawn");
                if (queueBlocks.Length > 0)
                {
                    int index = 0;
                    foreach (GameObject queueBlock in queueBlocks)
                    {
                        if (index < enqueueDataBlocks.Count)
                        {
                            GameObject blockToMove = GameObject.Find(enqueueDataBlocks[index]);
                            Transform snappoints = queueBlock.transform.Find("snap points");
                            Transform targetSnapPoint = snappoints.transform.Find(index.ToString());
                            blockToMove.transform.position = targetSnapPoint.position;
                            index++;
                        }
                    }
                }


                GameObject rearQueueBlock = queueBlocks[queueBlocks.Length - 1]; 
                Transform Rear = rearQueueBlock.transform.Find("Rear");
                if (Rear != null && enqueueDataBlocks.Count > 0)
                {
                    Vector3 currentPosition = Rear.transform.position;
                    currentPosition.y -= 1; 
                    Rear.transform.position = currentPosition;
                }
            }




            //print(isOnTopOfEnqueueBlock);
            if (Input.GetMouseButtonDown(1))
            {
                print("---------------------------------");
                foreach (string item in enqueueDataBlocks)
                {
                    print(item);
                }
                print("---------------------------------");

                //enqueueDataBlocks.Insert(3,"hello");
            }
        }

    }
    
  }
}
