using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttatchLocalPos;
    private Quaternion initialAttatchLocalRot;
    // Start is called before the first frame update test
    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttatchLocalPos = attachTransform.localPosition;
        initialAttatchLocalRot = attachTransform.localRotation;
       
    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        if(interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            initialAttatchLocalPos = attachTransform.localPosition;
            initialAttatchLocalRot = attachTransform.localRotation;
        }

        base.OnSelectEntering(interactor);
    }
}
