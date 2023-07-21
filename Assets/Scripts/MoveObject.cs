using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    public float moveSpeed;
    public Rigidbody rb;
    public GameManager gm;
    public bool canOpenable;
    float timer;
    float voiceTimer;
    private void Start()
    {
        canOpenable = false;
        gm.upgradePanel.SetActive(true);
    }
    void FixedUpdate()
    {
        float horizontal = joystick.Horizontal; 
        float vertical = joystick.Vertical;
        rb.velocity = new Vector3(horizontal, 0, vertical) * moveSpeed;
        //transform.position += new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        if (rb.velocity != Vector3.zero)
        {
            voiceTimer += Time.deltaTime;
            if (voiceTimer > 0.3f)
            {
                voiceTimer = 0;
                gm.audioS.PlayOneShot(gm.voices[2]);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OpenUpgradePanel>() && timer > 4f)
        {
            canOpenable = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<OpenUpgradePanel>() && canOpenable)
        {
          
             //   gm.upgradePanel.SetActive(true);
                canOpenable = false;
            


        }
    }
    private void OnTriggerExit(Collider other)
    {
       
            canOpenable = true;
        gm.upgradePanel.SetActive(false);

    }
}
