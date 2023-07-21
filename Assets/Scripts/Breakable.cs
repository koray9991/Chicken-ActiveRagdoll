using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Breakable : MonoBehaviour
{

    public bool addRb;
    public bool addCollider;
    public bool isTrigger;
    public bool timeIsTrigger;
    public float timerIsTrigger;
    public bool close;
    public float closeTime;
    public bool parentDisactive;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterCollider>())
        {
            if (addRb)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if(transform.GetChild(i).GetComponent<Rigidbody>() == null)
                    {
                        transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                    }
                  
                }
            }
            if (addCollider)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (!transform.GetChild(i).gameObject.GetComponent<BoxCollider>())
                    {
                        transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                        if (isTrigger)
                        {
                            transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
                        }
                    }
                    
                }
            }
            if (timeIsTrigger)
            {
                DOVirtual.DelayedCall(timerIsTrigger, () => {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
                    }
                });
            }
            if (close)
            {
                DOVirtual.DelayedCall(closeTime, () => {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                });
                
            }
            if (parentDisactive)
            {
                DOVirtual.DelayedCall(1, () => GetComponent<BoxCollider>().isTrigger = true);
                DOVirtual.DelayedCall(3, () => gameObject.SetActive(false));
            }
        }
    }
}
