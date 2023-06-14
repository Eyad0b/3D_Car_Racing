using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Wheels collider")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    [Header("Wheels Transform")]
    public Transform frontLeftwheelTransform;
    public Transform frontRightwheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    [Header("Car Engine")]
    public float accelerationForce = 200f;
    public float breakingForce = 3880f;
    private float presentBreakForce = 0f;
    private float presentAcceleration = 0f;

    [Header("Car Steering")]
    public float wheelsTorque;
    private float presentTurnAngle = 0f;

    [Header("Car Sounds")]
    public AudioSource audioSource;
    public AudioClip accelerationSound;
    public AudioClip slowAccelerationSound;
    public AudioClip stopSound;


    public GameObject Break;
    public GameObject ArrowsVertical;
    public GameObject SteeringWheel;
    // public bool isSteeringModeEnabled;
    // public bool isTouchModeEnabled = true;
    public CarSelection carSelection;
    public static bool habed;
    public static bool habed2 = true;



    // public void Start()
    // {
    //     Input.gyro.enabled = true;
    // }

    private void Update()
    {
        MoveCar();
        CarSteering();
    }
    private void MoveCar() {

        if (habed)
        {
            MoveCarByTouch();
        }else if (habed2)
        {  
            Break.SetActive(true);
            ArrowsVertical.SetActive(true);
            SteeringWheel.SetActive(true);
            //FWD
            frontLeftWheelCollider.motorTorque = presentAcceleration;
            frontRightWheelCollider.motorTorque = presentAcceleration;
            backLeftWheelCollider.motorTorque = presentAcceleration;
            backRightWheelCollider.motorTorque = presentAcceleration;

            presentAcceleration = accelerationForce * SimpleInput.GetAxis("Vertical");

            if (presentAcceleration >0)
            {
                audioSource.PlayOneShot(accelerationSound, 0.6f);
            }
            else if (presentAcceleration < 0)
            {
                audioSource.PlayOneShot(slowAccelerationSound, 0.2f);
            }
            else if (presentAcceleration == 0)
            {
                audioSource.PlayOneShot(stopSound, 0.1f);
            }
        }  
    }

    private void CarSteering()
    {
        if (habed2)
        {
            presentTurnAngle = wheelsTorque * SimpleInput.GetAxis("Horizontal");
                    // Get the gyroscope data
        // float gyroX = Input.gyro.rotationRateUnbiased.x;
        // float gyroY = Input.gyro.rotationRateUnbiased.y;
        //         presentTurnAngle = wheelsTorque * gyroX;

        }
        else if (habed)
        {
            presentTurnAngle = wheelsTorque * Input.acceleration.x;
		    frontLeftwheelTransform.position = new Vector2 (Mathf.Clamp (frontLeftwheelTransform.position.x, -7.5f, 7.5f), frontLeftwheelTransform.position.y);
        }
        
       frontLeftWheelCollider.steerAngle = presentTurnAngle;
       frontRightWheelCollider.steerAngle = presentTurnAngle;

       SteeringWheels(frontLeftWheelCollider, frontLeftwheelTransform);
       SteeringWheels(frontRightWheelCollider, frontRightwheelTransform);
       SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
       SteeringWheels(backRightWheelCollider, backRightWheelTransform);
    }

    private void MoveCarByTouch()
    {
        Break.SetActive(false);
        ArrowsVertical.SetActive(false);
        SteeringWheel.SetActive(false);
        // Check if the screen is being touched
        if (Input.touches.Length > 0)
        {
            // Get the first touch on the screen
            Touch touch = Input.touches[0];

            // Check if the touch is on the left or right side of the screen
            if (touch.position.x < Screen.width / 2)
            {
                // Left side of the screen - apply breaking force
                // presentBreakForce = breakingForce;
                presentAcceleration = -accelerationForce;
            }
            else
            {
                // Right side of the screen - apply acceleration force
                presentAcceleration = accelerationForce;
            }

                        if (presentAcceleration >0)
            {
                audioSource.PlayOneShot(accelerationSound, 0.6f);
            }
            else if (presentAcceleration < 0)
            {
                audioSource.PlayOneShot(slowAccelerationSound, 0.2f);
            }
            else if (presentAcceleration == 0)
            {
                audioSource.PlayOneShot(stopSound, 0.1f);
            }
        }
        else
        {
            // No touch input - reset acceleration and breaking forces
            presentBreakForce = 0f;
            presentAcceleration = 0f;
        }
        //FWD
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        frontRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
    }

    // private void CarSteering()
    // {
    //     // Get the gyroscope data
    //     float gyroX = Input.gyro.rotationRateUnbiased.x;
    //     float gyroY = Input.gyro.rotationRateUnbiased.y;

    //     // Use the gyroscope data to control the car's steering
    //     presentTurnAngle = wheelsTorque * gyroX;
    //     frontLeftWheelCollider.steerAngle = presentTurnAngle;
    //     frontRightWheelCollider.steerAngle = presentTurnAngle;

    //     SteeringWheels(frontLeftWheelCollider, frontLeftwheelTransform);
    //     SteeringWheels(frontRightWheelCollider, frontRightwheelTransform);
    //     SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
    //     SteeringWheels(backRightWheelCollider, backRightWheelTransform);
    // }

    void SteeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;

        WC.GetWorldPose(out position, out rotation);


        WT.position = position;
        WT.rotation = rotation;
    }

    public void ApplyBreaks()
    {
        StartCoroutine(carBreaks());
    }

    IEnumerator carBreaks()
    {
        presentBreakForce= breakingForce;

        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;

        yield return new WaitForSeconds(2f);

        presentBreakForce= 0f;


        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        frontRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
    }
}//Main Class
