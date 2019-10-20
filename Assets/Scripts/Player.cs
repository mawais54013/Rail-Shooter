using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 18f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] GameObject[] guns;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    print("tiggered");
    //}

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * xSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            var emmissionModule = gun.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isActive;
        }
    }
}


// -9.81 gravity
