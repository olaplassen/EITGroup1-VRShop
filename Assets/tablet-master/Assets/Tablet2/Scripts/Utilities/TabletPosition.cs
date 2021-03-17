/* Copyright (C) 2020 IMTEL NTNU - All Rights Reserved
 * Developer: Abbas Jafari
 * Ask your questions by email: a85jafari@gmail.com
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This class will handle the position of tablet
/// This class should be change if you need to open the tablet by other ways like grapping
/// or changing the tablet position
/// </summary>
public class TabletPosition : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float DistanceFromPlayer = 0.4f;

    Vector3 originalAngles;

    private bool tabletIsOpened;
    private int currentTablet;
    public XRNode inputSource1;
    public XRNode inputSource2;

    Camera cam;

    /// <summary>
    /// Open or close the tablet
    /// </summary>
    /// <param name="status"></param>
    public void SelectTablet(bool status)
    {
        tabletIsOpened = status;
    }

    public void setCurrentTablet(int current)
    {
        currentTablet = current;
    }

    public int getCurrentTablet()
    {
        return currentTablet;
    }



    /// <summary>
    /// Unity start method
    /// </summary>
    private void Start()
    {
        originalAngles = transform.eulerAngles;

        if (!Camera.main)
            cam = GameObject.FindObjectOfType<Camera>();
        else
            cam = Camera.main;
    }



    /// <summary>
    /// Unity update method
    /// </summary>
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputSource1);
        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);
        if (tabletIsOpened)
        {
            transform.position = (cam.transform.position  + new Vector3(0,-0.05f,0)) + cam.transform.forward * DistanceFromPlayer;
            transform.LookAt(cam.transform.position);
            transform.Rotate(originalAngles);
            
                bool triggerValue;

                if ((device1.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue) && triggerValue) || (device2.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue) && triggerValue))
                {
                    SelectTablet(false);
                }
                if(currentTablet == 1 && (device2.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue))
                {
                    setCurrentTablet(2);
                }
                if (currentTablet == 2 && (device1.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue))
                {
                    setCurrentTablet(1);
                }

        }
        else
        {
            transform.position = cam.transform.position - new Vector3(0.5f, 2f, 0);
            transform.rotation = Quaternion.Euler(0, 180, 90);

            
                bool triggerValue;
                if (device1.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue)
                {
                    SelectTablet(true);
                    setCurrentTablet(1);
                }
                if (device2.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out triggerValue) && triggerValue)
                {
                    SelectTablet(true);
                    setCurrentTablet(2);
                }


        }

    }
}