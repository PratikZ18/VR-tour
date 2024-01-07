using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position : MonoBehaviour
{

    public GameObject Rig;
    public Transform Sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        
        {
            Rig.transform.position=Sphere.position;
        }
    }
}
