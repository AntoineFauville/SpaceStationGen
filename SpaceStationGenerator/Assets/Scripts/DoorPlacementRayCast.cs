using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DoorPlacementRayCast : MonoBehaviour
{
    public GameObject Door;

    public float castLenght = 1;

    public bool doorPlacementChecked;

    public Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        if (!doorPlacementChecked)
            {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.blue);

            if (Physics.Raycast(ray, out hit, castLenght))
            {
                if (hit.collider.tag == "Connection")
                {
                    Debug.Log("Don't Create Door here");
                    goto Skip;
                }
            }
            Debug.Log("Create Door here");
            PlaceDoor();

            Skip:
            doorPlacementChecked = true;
        }
    }

    void PlaceDoor()
    {
        GameObject door = Object.Instantiate(Door);
        door.transform.SetParent(this.transform);
        door.transform.localPosition = new Vector3(0, 0, 0);
        door.transform.rotation = this.transform.rotation;
        door.name = "Door";
    }
}
