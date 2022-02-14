using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dodgeSpeed;

    private CharacterController controller;
    private float horizontal, vertical, mouseX, turnSpeed;

    private bool canMove = true;

    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * Time.deltaTime * speed * 2);
        }


        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            anim.SetFloat("SideMoveSpeed", horizontal);
            anim.SetFloat("MovementSpeed", vertical);

            if (horizontal != 0)
            {
                controller.Move(transform.right * horizontal * Time.deltaTime * speed / 1.5f);
            }
            if (vertical > 0)
            {
                controller.Move(transform.forward * vertical * Time.deltaTime * speed);
            }
            else if (vertical < 0)
            {
                controller.Move(transform.forward * vertical * Time.deltaTime * speed / 1.5f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetFloat("Dodge", 1);
                canMove = false;
            } 
        }

    }

    // Turns the player with mouse smoothly
    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X");
        Quaternion turn = Quaternion.Euler(0, mouseX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, turn, 4 * Time.deltaTime);

        turnSpeed = Mathf.Lerp(turnSpeed, Input.GetAxis("Mouse X"), 2 * Time.deltaTime);
        turnSpeed = Mathf.Clamp(turnSpeed, -1, 1);

        anim.SetFloat("TurnSpeed", turnSpeed);
    }

    //Function animation calls direction being -1,0,1
    public void Dodge(float direction)
    {
        StartCoroutine(RealDodge());

        //function where dodge is made smoothly
        IEnumerator RealDodge()
        {
            for (int i = 0; i < 10; i++)
            {
                controller.Move(-transform.forward * Time.deltaTime * dodgeSpeed);
                controller.Move(transform.right * Time.deltaTime * dodgeSpeed * direction);
                yield return new WaitForSeconds(0.01f);
            }

            anim.SetFloat("Dodge", 0);
            canMove = true;
        }
    }
}
