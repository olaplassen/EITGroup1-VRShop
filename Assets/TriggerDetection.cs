using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour { 

    public TabletPosition tabletpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider triggerObject)
    {
        if (triggerObject.name == "Player")
        {
            tabletpos.SelectTablet(true);
            //execute your code 
            //you can create a reference for the triggerObject for more complicated functions like:
            
        }
    }
}
