using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public XRController leftRay, rightRay;
    public InputHelpers.Button teleportButton;
    public float activationTreshold = 0.1f;
    

    // Update is called once per frame
    void Update()
    {
        if (leftRay)
        {
            leftRay.gameObject.SetActive(CheckIfActivated(leftRay));
        }
        if (rightRay)
        {
            rightRay.gameObject.SetActive(CheckIfActivated(rightRay));
        }
    }

    public bool CheckIfActivated (XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportButton, out bool isActivated, activationTreshold);
        return isActivated;
    }
}
