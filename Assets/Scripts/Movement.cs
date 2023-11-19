using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] int mainThrustPower = 10;
    [SerializeField] int rotationThrustPower = 10;

    private Rigidbody rb;
    private AudioSource mainThrustAudio;

    private Transform leftThruster;
    private Transform rightThruster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainThrustAudio = GetComponent<AudioSource>();

        leftThruster = transform.Find("LeftThruster");
        rightThruster = transform.Find("RightThruster");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrustPower * Time.deltaTime);
            if (!mainThrustAudio.isPlaying)
            {
                mainThrustAudio.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if(mainThrustAudio.isPlaying)
            {
                mainThrustAudio.Stop();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Get the position and rotation of the left thruster in world space
            Vector3 thrusterPosition = rightThruster.position;
            Quaternion thrusterRotation = rightThruster.rotation;

            // Calculate the force direction in the local Y axis of the thruster
            Vector3 forceDirection = thrusterRotation * Vector3.up;

            rb.AddForceAtPosition(rotationThrustPower * forceDirection * Time.deltaTime, thrusterPosition);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 thrusterPosition = leftThruster.position;
            Quaternion thrusterRotation = leftThruster.rotation;

            Vector3 forceDirection = thrusterRotation * Vector3.up;

            rb.AddForceAtPosition(rotationThrustPower * forceDirection * Time.deltaTime, thrusterPosition);
        }
    }
}
