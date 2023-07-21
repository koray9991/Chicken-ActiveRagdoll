using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Root : MonoBehaviour
{
    public float obstacleTimer;
    public bool obstacleBool;
    public bool setJoint;
    public ConfigurableJoint[] joints;
    public CharacterController cc;
    public ConfigurableJoint head;
    public GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (obstacleBool)
        {
            obstacleTimer += Time.deltaTime;
            if (obstacleTimer > 0.3f)
            {
                if (!setJoint)
                {
                    setJoint = true;
                    for (int i = 0; i < joints.Length; i++)
                    {
                        JointDrive jointDrive = joints[i].angularXDrive;
                        jointDrive.positionSpring = 0f;
                        joints[i].angularXDrive = jointDrive;
                    }
                    cc.hipJoint = head.GetComponent<ConfigurableJoint>();
                    cc.hip = head.GetComponent<Rigidbody>();
                    DOVirtual.DelayedCall(0.5f, () => gm.audioS.PlayOneShot(gm.voices[1]));
                  
                }

            }
            if (obstacleTimer > 2)
            {
                for (int i = 0; i < joints.Length; i++)
                {
                    JointDrive jointDrive = joints[i].angularXDrive;
                    jointDrive.positionSpring = 400f;
                    joints[i].angularXDrive = jointDrive;
                }
                obstacleTimer = 0;
                obstacleBool = false;
                setJoint = false;
                cc.hipJoint = GetComponent<ConfigurableJoint>();
                cc.hip = GetComponent<Rigidbody>();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<Obstacle>())
        {
            obstacleBool = true;
        }
    }
  
}
