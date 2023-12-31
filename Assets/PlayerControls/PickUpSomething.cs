using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSomething : MonoBehaviour
{
    public GameObject Сamera;

    public float distance = 15f;
    GameObject currentItem;
    bool canPickUp;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUpItem();
        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }
    void PickUpItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Сamera.transform.position, Сamera.transform.forward, out hit, distance))

        {
            if (hit.transform.tag == "Pickable") 
            {
                if (canPickUp) Drop();
                currentItem = hit.transform.gameObject;
                currentItem.GetComponent<Rigidbody>().isKinematic = true;
                currentItem.transform.parent = transform;
                currentItem.transform.localPosition = Vector3.zero;
                currentItem.transform.localEulerAngles = new Vector3(0f, 0f, 0f); // если предмет в руке должен лежить по-другому
                canPickUp = true;
            }
        }
    }
    void Drop() 
    {
        currentItem.transform.parent = null;
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        canPickUp = false;
    }
}
