using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //change the score text

public class PlayerController : MonoBehaviour
{
    //"Public" variables
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 750f;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Transform dangerCheckPosition; //a little higher than where the groundCheckPosition is
    [SerializeField] private LayerMask whatIsGround; //LevelTilemap
    [SerializeField] private LayerMask whatIsDanger; //DangerTilemap
    [SerializeField] private LayerMask whatIsEnd; //EndTilemap
    public Text scoreUI; //for score
    public AudioSource source;

    //Private Variables
    private Rigidbody2D rBody;
    private Animator anim; //need this to be able to make transition parameters for the animtor. 
    private bool isGrounded = false;
    private bool touchDanger = false;
    private bool endLevel = false;
    private int points = 0; //for score

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        points = 0;
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Physics
    private void FixedUpdate()
    {
        scoreUI.text = points.ToString(); //update score text
        //Movement right at a constant velocity
        rBody.velocity = new Vector2(speed, rBody.velocity.y);
        //float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck(); //check if the player is on the ground
        touchDanger = DangerCheck(); //check if the player touched death pit
        endLevel = EndCheck(); //check if the player has touched the end of the level

        //Jump code
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
        }

        if (touchDanger) //if the player touched danger, load death scene
        {
            touchDanger = true;
            SceneManager.LoadScene("Dead");
        }

        if (endLevel) //if the player gets to the end
        {
            SceneManager.LoadScene("End");
        }

        anim.SetFloat("yVelocity", rBody.velocity.y); //no absoulte value for jumping because JumpDown should be negative. 
        anim.SetBool("Grounded", isGrounded); //pass this to shake parameter
        anim.SetBool("Danger", touchDanger);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }

    private bool DangerCheck() //check if the player has touched part of the DangerTilemap
    {
        return Physics2D.OverlapCircle(dangerCheckPosition.position, groundCheckRadius, whatIsDanger);
    }

    private bool EndCheck() //check if the player has touched part of the DangerTilemap
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsEnd);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("pinkStarBit")) //player touches star bit
        {
            points += 5;
            Debug.Log("Points: " + points);
            scoreUI.text = points.ToString();
            source.Play();
        }
        else if (other.CompareTag("greenStarBit")) //player touches star bit
        {
            points += 10;
            Debug.Log("Points: " + points);
            scoreUI.text = points.ToString();
            source.Play();
        }
        else if (other.CompareTag("purpleStarBit")) //player touches star bit
        {
            points += 20;
            Debug.Log("Points: " + points);
            scoreUI.text = points.ToString();
            source.Play();
        }
    }
}
