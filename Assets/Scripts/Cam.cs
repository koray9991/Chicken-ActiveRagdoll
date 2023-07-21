using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target;
    public float minZ, maxZ;
    public float indexZ;
    public float indexX;
    void FixedUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y,target.transform.position.z+indexZ);
        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(target.transform.position.x + indexX, transform.position.y, maxZ);
        }
        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(target.transform.position.x + indexX, transform.position.y, minZ);
        }
    }
     
}

