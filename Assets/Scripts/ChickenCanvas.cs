using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class ChickenCanvas : MonoBehaviour
{
    public Transform root;
    public Camera cam;
    public ParticleSystem ps;
    public TextMeshProUGUI textRight, textLeft;

   
    public void OnEnable()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DOVirtual.DelayedCall(3, () => { 
            textLeft.gameObject.SetActive(false);
            textRight.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }
    private void Update()
    {
        transform.position = new Vector3(root.transform.position.x, 1.5f, root.transform.position.z);
        transform.LookAt(cam.transform.position);
    }
}
