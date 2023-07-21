using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI collectAllText,turnBackToSafeText;
    public List< HoldedObject> holdableObjects;
    public int collectedPropCount;
    public GameObject particle;
    public MoveObject moveObject;
    public Transform arrow;
    public Transform cash;
    int state;
    void Start()
    {
        foreach (HoldedObject holdable in FindObjectsOfType<HoldedObject>())
        {
            holdableObjects.Add(holdable);
        }
        collectAllText.gameObject.SetActive(true);
    }
    private void Update()
    {
        arrow.transform.position = moveObject.transform.position;
        if (state == 0)
        {
            arrow.LookAt(particle.transform);
        }
        else
        {
            arrow.LookAt(cash);
        }
       

      
    }
    public void TurnBackToSafe()
    {
        collectAllText.gameObject.SetActive(false);
        turnBackToSafeText.gameObject.SetActive(true);
        particle.SetActive(false);
        state = 1;
    }
   
}
