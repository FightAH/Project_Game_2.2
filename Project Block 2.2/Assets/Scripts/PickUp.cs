using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {


    public Camera mainCam;


    private Vector3 raycastOrigin;
    private Vector3 throwDirection;
    private LayerMask filterMask;
    private GameObject hittedItem;
    private Rigidbody hittedRB;

    bool isCarrying = false;
   
    public float rayDistance = 10f;
    public float throwPower = 25f;
    public float rotateSpeed = 5f;
    public int playerStrenght = 50;

    private float middleX = 0.5f;
    private float middleY = 0.5f;

    public float wiggleStrenght = 1;

    private int selectedItemWeight;

    public bool canCarry;


    void Start() {

        //Filters only for "AbleToPickUp" layer, otherwise it would check the player constantly
        filterMask = 1 << LayerMask.NameToLayer("AbleToPickUp");
    }

    void Update()
    {
        RayCast();      
        if ( isCarrying )
        {
            MoveObject();
        }
        InputCheck();       
    }

    /// <summary>
    /// Shoots a raycast from the camera, that determits if the player is close enough
    /// also checks if there is a object.
    /// </summary>
    public void RayCast()
    {
        raycastOrigin = mainCam.ViewportToWorldPoint(new Vector3(middleX, middleY, 0));
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin, mainCam.transform.forward, out hit, rayDistance, filterMask))
        {
            hittedItem = hit.collider.gameObject;
            selectedItemWeight = hittedItem.GetComponent<PickUpData>().weight;
            Debug.Log(selectedItemWeight);
            if( selectedItemWeight < playerStrenght)
            {
                canCarry = true;
            }
            else
            {
                canCarry = false;
            }
        }
        Debug.DrawRay(raycastOrigin, mainCam.transform.forward * rayDistance, Color.red);
    }

    /// <summary>
    /// Changes the current Pos each time the mainCam is moving
    /// </summary>
    private void MoveObject()
    {
        if (canCarry)
        {
            hittedItem.GetComponent<Rigidbody>().isKinematic = true;
            Vector3 newPos = mainCam.transform.position + mainCam.transform.forward * rayDistance;
            hittedItem.transform.position = newPos;
        }
        else
        {
            StartCoroutine("NotAbleToPickUpEffect");
        }                                          
    }

    private IEnumerator NotAbleToPickUpEffect()
    {
        Vector3 newPos = new Vector3(hittedItem.transform.position.x + Random.Range(-wiggleStrenght, wiggleStrenght), hittedItem.transform.position.y + Random.Range(0.04f, 0.06f), hittedItem.transform.position.z + Random.Range(-wiggleStrenght, wiggleStrenght));
        hittedItem.transform.position = newPos;
        yield return new WaitForSeconds(wiggleStrenght);
    }

    /// <summary>
    /// Throws the object by resetting the Position and add velocity
    /// </summary>
    private void ThrowObject()
    {
        if (canCarry)
        {
            isCarrying = false;
            Vector3 resetPos = hittedItem.transform.position;
            hittedItem.transform.position = resetPos;
            hittedItem.GetComponent<Rigidbody>().isKinematic = false;
            hittedItem.GetComponent<Rigidbody>().velocity = this.transform.forward * throwPower;
        }
        else
        {
            isCarrying = false;
            Vector3 resetPos = hittedItem.transform.position;
            hittedItem.transform.position = resetPos;
            hittedItem.GetComponent<Rigidbody>().isKinematic = false;
        }            
    }

    /// <summary>
    /// Checks whenever a key is pressed amd the condition
    /// Calls the right functions
    /// </summary>
    private void InputCheck()
    {
        if (isCarrying && Input.GetKeyUp(KeyCode.E))
        {
            ThrowObject();
            hittedItem = null;
        }
        else if (isCarrying && Input.GetKey(KeyCode.Q))
        {
            hittedItem.transform.Rotate(0, rotateSpeed, 0);
        }
        else if (hittedItem != null && Input.GetKey(KeyCode.E))
        {
            isCarrying = true;
        }           
    }
}
