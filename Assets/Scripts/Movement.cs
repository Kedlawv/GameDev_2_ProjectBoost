using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] int mainThrustPower = 10;
    [SerializeField] int rotationThrustPower = 10;
    [SerializeField] AudioSource mainEngineAudioSource;
    [SerializeField] AudioSource thrusterAudioSource;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftSecThrusterParticles;
    [SerializeField] ParticleSystem rightSecThrusterParticles;

    private Rigidbody rb;

    private Transform leftThruster;
    private Transform rightThruster;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
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
            if (!mainEngineAudioSource.isPlaying)
            {
                mainEngineAudioSource.Play();
            }

            mainEngineParticles.Emit(1);
            
        }
        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (mainEngineAudioSource.isPlaying)
            {
                mainEngineAudioSource.Stop();
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

            if (!thrusterAudioSource.isPlaying)
            {
                thrusterAudioSource.Play();
            }

            rightThrusterParticles.Emit(1);
            //rightSecThrusterParticles.Emit(2);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 thrusterPosition = leftThruster.position;
            Quaternion thrusterRotation = leftThruster.rotation;

            Vector3 forceDirection = thrusterRotation * Vector3.up;

            rb.AddForceAtPosition(rotationThrustPower * forceDirection * Time.deltaTime, thrusterPosition);

            if (!thrusterAudioSource.isPlaying)
            {
                thrusterAudioSource.Play();
            }

            leftThrusterParticles.Emit(1);
            //leftSecThrusterParticles.Emit(2);
        }
        else
        {
            thrusterAudioSource.Stop();
        } 
    }
}
