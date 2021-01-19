using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    //AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float fa = 1f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    enum State {Alive, Dying, Trany }
    State state = State.Alive;
    private static float su;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (state != State.Alive) 
        {
            return;
        }

        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Trany;
                Invoke("LoadNextLevel", 1f);
                StartSuccessSequence();
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstScene", 1f);
                break;
        }
    }


    private void StartSuccessSequence()
    {
        state = State.Trany;
        Invoke("LoadNextLevel", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        Invoke("LoadFirstLevel", 1f);
    }



    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    /*private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            /*if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        }
    }*/

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            mainEngineParticles.Play();
            ApplyThrust();
        }
        else
            mainEngineParticles.Stop();
    }


    /*private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame * fa);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame * fa);
        }

        rigidBody.freezeRotation = false;

    }*/

    private void ApplyThrust()
    {
        mainEngineParticles.Play();
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame * fa);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame * fa);
        }
        rigidBody.freezeRotation = false;
    }
}
