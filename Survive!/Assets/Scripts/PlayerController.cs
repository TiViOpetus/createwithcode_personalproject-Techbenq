using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Item item;
    public float speed;
    public float dodgeSpeed;

    private CharacterController controller;
    private float horizontal, vertical, mouseX, turnSpeed;

    private bool canMove = true;

    private Animator anim;

    private int slotNum;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

            anim.SetFloat("SideSpeed", horizontal);
            anim.SetFloat("MovementSpeed", vertical);

            if (horizontal != 0)
            {
                controller.Move(transform.right * horizontal * Time.deltaTime * speed);
            }
            if (vertical != 0)
            {
                controller.Move(transform.forward * vertical * Time.deltaTime * speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.applyRootMotion = true;
                anim.SetTrigger("Dodge");
                canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(SelectObject.current != null)
                    SelectObject.current.Interact();
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            slotNum = Mathf.Clamp(slotNum - (int)Input.mouseScrollDelta.y, 0, 8);
            InventoryManager.instance.SetActiveSlot(slotNum);
        }
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Punch");
            canMove = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
            InventoryManager.instance.RemoveItems(item, 1);
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

    public void AllowMovement()
    {
        anim.applyRootMotion = false;
        canMove = true;
    }
}
