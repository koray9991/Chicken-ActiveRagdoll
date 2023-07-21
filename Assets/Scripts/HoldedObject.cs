using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldedObject : MonoBehaviour
{
    public bool meRight, meLeft;
    public CharacterCollider holdChicken;
   [HideInInspector] public bool forTutorial;

    private void Update()
    {
        if (meRight)
        {
            // transform.position = holdChicken.rightArmChild.position;
      
            
        }
        if (meLeft)
        {
         //   transform.position = holdChicken.leftArmChild.position;
        
        }
    }
}
