using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Camera cam;
    CharacterController characterController;
    float maxSpeed = 10, acceleration = 10, jumpForce = 5;
    public float speed { get; private set; }
    float verticalMovement;
    Vector3 direction, directionForward, directionRight, nextDir;
    Animator animator;

    [SerializeField]
    AudioClip[] stepSoundDirt;
    [SerializeField]
    AudioClip[] stepSoundTile;
    [SerializeField]
    AudioClip[] stepSoundCave;

    public Place actualSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        direction = transform.forward;
        nextDir = transform.forward;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();
        actualSound = Place.PATH;
    }

    void Update()
    {
        gravity();

        Move();


        //apply the calculated movement to the character controller movement system
        characterController.Move((direction * speed + verticalMovement * Vector3.up) * Time.deltaTime);

        animator.SetFloat("Speed", speed / maxSpeed);
    }

    private void Move()
    {
        if ((Input.GetAxisRaw("Vertical")) != 0 || (Input.GetAxisRaw("Horizontal")) != 0)
        {
            //gets the inputs from keyboard arrows and defines the direction depending on the camera's orientation;

            directionForward = cam.transform.forward;
            directionForward.y = 0;
            directionForward *= Input.GetAxisRaw("Vertical");

            directionRight = cam.transform.right;
            directionRight.y = 0;
            directionRight *= Input.GetAxisRaw("Horizontal");

            nextDir = Vector3.Normalize(directionForward + directionRight);

            //Direction interpolation between the current direction and the inputed direction
            direction = Vector3.Lerp(direction, nextDir, Time.deltaTime * 2);

            //Calculate the speed increasement depending on the time spent pushing an arrow button;

            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
            {
                speed = maxSpeed;
            }

        }
        else
        {
            //Calculate the speed decreasement depending on the time since no arrow button is pressed;

            if (speed != 0)
            {
                if (speed <= 2 * acceleration * Time.deltaTime)
                    speed = 0;
                else
                {
                    speed -= 2 * acceleration * Time.deltaTime;
                }
            }
        }

        //make the object rotate toward its movement;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    private void gravity()
    {
        if (verticalMovement <= 0 && characterController.isGrounded)
        {
            verticalMovement = -5;
        }
        else
        {
            verticalMovement -= jumpForce * 2 * Time.deltaTime;
        }
    }

    private void StepSound()
    {
        AudioClip sound = null;
        if (actualSound == Place.CAVE) { Debug.Log("Cave"); sound = stepSoundCave[Random.Range(0, stepSoundCave.Length)]; }
        else if (actualSound == Place.HOUSE) { Debug.Log("House"); sound = stepSoundTile[Random.Range(0, stepSoundTile.Length)]; }
        else if (actualSound == Place.PATH) { Debug.Log("Cemetery"); sound = stepSoundDirt[Random.Range(0, stepSoundDirt.Length)]; }

        if (sound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.1f);
            audioSource.volume = Random.Range(0.5f, 1);
            audioSource.PlayOneShot(sound);
        }
        else
        {
            Debug.Log("Bruit de Pas");
        }
    }

    private void OnTriggerEnter(Collider other) { UpdateStepSound(other); }

    private void UpdateStepSound(Collider other)
    {
        if (other.tag == "InsideHouse") { actualSound = Place.HOUSE; }
        else if (other.tag == "InsideCave") { actualSound = Place.CAVE; }
        else if (other.tag == "OutsideHouse") { actualSound = Place.PATH; }
    }
}

public enum Place
{
    PATH,
    HOUSE,
    CAVE,
}
