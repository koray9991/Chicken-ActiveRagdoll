using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public  ConfigurableJoint hipJoint;
    public  Rigidbody hip;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator targetAnimator;
    public Transform moveObject;
    public Transform root;
    private bool walk = false;
    
    void Start()
    {

    }

   
    void FixedUpdate()
    {
        //float horizontal = joystick.Horizontal; //Input.GetAxisRaw("Horizontal");
        //float vertical = joystick.Vertical;//Input.GetAxisRaw("Vertical");

        float horizontal = -root.transform.position.x+moveObject.transform.position.x;
        if(horizontal<0.1f && horizontal > -0.1f) { horizontal = 0; }
        float vertical = -root.transform.position.z + moveObject.transform.position.z;
        if (vertical < 0.1f && vertical > -0.1f) { vertical = 0; }

       

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            this.hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            this.hip.AddForce(direction * this.speed);

            this.walk = true;
            this.targetAnimator.SetBool("Walk", true);
            this.targetAnimator.SetBool("Idle", false);
        }
        else
        {
            this.walk = false;
            this.targetAnimator.SetBool("Walk", false);
            this.targetAnimator.SetBool("Idle", true);
        }




    }

   
}
