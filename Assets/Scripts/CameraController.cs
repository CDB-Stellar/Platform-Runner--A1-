using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //to be able to get access to the camera

public class CameraController : MonoBehaviour
{
    public List<GameObject> cameras; //use a list because they can grow and shrink in size easier than array

    // Start is called before the first frame update
    void Start()
    {
        cameras.Clear(); //clear list in case there is something in there already
        for (int i = 0; i < transform.childCount; i++) //transform.childCount is how many children of virtual cameras there are
        {
            //go through each child and add them to the list of virtual cameras
            cameras.Add(transform.GetChild(i).gameObject); //bad?
        }
    }

    //Change the camera
    public void TransitionTo(GameObject cameraToTransitionTo)
    {
        foreach (GameObject g in cameras) //go through each object in the virtualCameras list
        {
            if (g == cameraToTransitionTo) //if a camera in the list(g) is the same as the one to transition to
                g.GetComponent<CinemachineVirtualCamera>().Priority = 10; //transition to this camera
            else
                g.GetComponent<CinemachineVirtualCamera>().Priority = 5; //de-prioritize the camera

        }
    }
}
