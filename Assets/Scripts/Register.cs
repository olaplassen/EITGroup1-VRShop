using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera CC;
    public TabletPosition tabpos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CC.transform.position.x >= -4 && CC.transform.position.x <= -3 && CC.transform.position.z >= -8.4 && CC.transform.position.z <= -4.4)
        {
            tabpos.SelectTablet(true);
            tabpos.setCurrentTablet(3);
        }
    }
}
