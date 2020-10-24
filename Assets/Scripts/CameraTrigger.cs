using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cameraToActivate;
    [SerializeField] private GameObject cameraOut;

    public CameraController camController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //if you enter the bounding box - camera trigger
            camController.TransitionTo(cameraToActivate); //using the method from VirtualCameraController
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //if you exit the bounding box
            camController.TransitionTo(cameraOut);
    }
}
