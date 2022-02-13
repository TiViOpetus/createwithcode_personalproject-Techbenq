using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;

    private CharacterController controller;
    private float horizontal, vertical;

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

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("SideMoveSpeed", horizontal);
        anim.SetFloat("MovementSpeed", vertical);

        if (horizontal != 0)
        {
            controller.Move(transform.right * horizontal * Time.deltaTime * speed / 1.5f);
        }
        if(vertical > 0)
        {
            controller.Move(transform.forward * vertical * Time.deltaTime * speed);
        }
        else if(vertical < 0)
        {
            controller.Move(transform.forward * vertical * Time.deltaTime * speed / 1.5f);
        }
    }
    private void LateUpdate()
    {
        horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, horizontal);
    }
}
