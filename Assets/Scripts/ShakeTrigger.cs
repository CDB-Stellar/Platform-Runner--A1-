using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeTrigger : MonoBehaviour
{
    private Animator anim; //need this to be able to make transition parameters for the animtor. 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //&& other.CompareTag("Player")
        if (other.CompareTag("Player")) //if you enter the bounding box - camera shake animation
            anim.SetBool("shake", true); //pass this to shake parameter
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //if you exit the bounding box
            anim.SetBool("shake", false); //pass this to shake parameter
    }
}
