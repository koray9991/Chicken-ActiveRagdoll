using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterCollider : MonoBehaviour
{
   
    public static CharacterCollider instance;
    public bool right, left;
    public Transform leftArmChild, rightArmChild;
    public GameManager gm;
    public GameObject leftObject, rightObject;
    public ChickenCanvas chickenCanvas;
    public bool forTutorial;
    private void Awake()
    {
        if (instance == null) { instance = this; }
  
    }

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>() && (!right || !left))
        {
            if (other.GetComponent<Collectable>().enabled)
            {
                if (!other.GetComponent<Collectable>().isBigObject)
                {
                    other.gameObject.layer = 9;
                    other.GetComponent<Rigidbody>().mass = 0.1f;
                }
                gm.audioS.PlayOneShot(gm.voices[3]);
                other.GetComponent<HoldedObject>().enabled = true;
                other.GetComponent<HoldedObject>().holdChicken = this;
                if (FindObjectOfType<Tutorial>() != null)
                {
                    var tutorial = FindObjectOfType<Tutorial>();
                    tutorial.collectedPropCount += 1;
                    if (tutorial.collectedPropCount == tutorial.holdableObjects.Count)
                    {
                        tutorial.TurnBackToSafe();
                    }
                }
                other.GetComponent<Rigidbody>().drag = 10;

                other.GetComponent<Collectable>().enabled = false;

                
                if (!right && left)
                {
                    right = true;
                    other.GetComponent<HoldedObject>().meRight = true;
                    rightObject = other.transform.gameObject;
                    other.gameObject.AddComponent<ConfigurableJoint>();
                    other.GetComponent<ConfigurableJoint>().connectedBody = rightArmChild.GetComponent<Rigidbody>();
                    other.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().autoConfigureConnectedAnchor = false;
                    other.GetComponent<ConfigurableJoint>().anchor = Vector3.zero;
                    other.GetComponent<ConfigurableJoint>().connectedAnchor = Vector3.zero;
                }
                if (right && !left)
                {
                    left = true;
                    other.GetComponent<HoldedObject>().meLeft = true;
                    leftObject = other.transform.gameObject;
                    other.gameObject.AddComponent<ConfigurableJoint>();
                    other.GetComponent<ConfigurableJoint>().connectedBody = leftArmChild.GetComponent<Rigidbody>();
                    other.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().autoConfigureConnectedAnchor = false;
                    other.GetComponent<ConfigurableJoint>().anchor = Vector3.zero;
                    other.GetComponent<ConfigurableJoint>().connectedAnchor = Vector3.zero;
                }
                if (!right && !left)
                {
                    right = true;
                    other.GetComponent<HoldedObject>().meRight = true;
                    rightObject = other.transform.gameObject;
                    other.gameObject.AddComponent<ConfigurableJoint>();
                    other.GetComponent<ConfigurableJoint>().connectedBody = rightArmChild.GetComponent<Rigidbody>();
                    other.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
                    other.GetComponent<ConfigurableJoint>().autoConfigureConnectedAnchor = false;
                    other.GetComponent<ConfigurableJoint>().anchor = Vector3.zero;
                    other.GetComponent<ConfigurableJoint>().connectedAnchor = Vector3.zero;
                }

             
                gm.holding = true;
                gm.open = false;
            }
          
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SafeArea>())
        {
            if (leftObject != null)
            {

                chickenCanvas.gameObject.SetActive(true);
                chickenCanvas.textLeft.gameObject.SetActive(true);
                chickenCanvas.textLeft.color = new Color(1, 1, 1, 1);
                chickenCanvas.ps.Play();
                chickenCanvas.textLeft.text = "$" + leftObject.GetComponent<Collectable>().price;
                chickenCanvas.textLeft.transform.DOMoveY(2, 1);
                chickenCanvas.textLeft.DOFade(0, 1);


                gm.audioS.PlayOneShot(gm.voices[0]);
                gm.EarnMoney(leftObject.GetComponent<Collectable>().price);
                gm.SetSoldObjectCount(1);

                leftObject.SetActive(false);
                leftObject = null;
                left = false;


            }
            if (rightObject != null)
            {
                chickenCanvas.gameObject.SetActive(true);
                chickenCanvas.textRight.gameObject.SetActive(true);
                chickenCanvas.textRight.color = new Color(1,1,1,1);
                chickenCanvas.ps.Play();
                chickenCanvas.textRight.text = "$" + rightObject.GetComponent<Collectable>().price;
                chickenCanvas.textRight.transform.DOMoveY(2, 1);
                chickenCanvas.textRight.DOFade(0, 1);

                gm.EarnMoney(rightObject.GetComponent<Collectable>().price);
                gm.SetSoldObjectCount(1);
                rightObject.SetActive(false);
                rightObject = null;
                right = false;


            }


        }
    }
}
