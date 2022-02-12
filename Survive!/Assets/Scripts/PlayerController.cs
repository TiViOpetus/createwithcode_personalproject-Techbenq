using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;

    private CharacterController controller;
    private float horizontal, vertical;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            controller.Move(transform.forward * vertical * Time.deltaTime *speed);
            controller.Move(transform.right * horizontal * Time.deltaTime * speed);
        }
    }
    private void LateUpdate()
    {
        horizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, horizontal);
    }
}
