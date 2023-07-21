using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Bomba : MonoBehaviour
{
    void Update()
    {
        var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            if (child.GetComponent<Collider>() != null)
            {
                DestroyImmediate(child.GetComponent<Collider>());
            }


        }
    }
}
