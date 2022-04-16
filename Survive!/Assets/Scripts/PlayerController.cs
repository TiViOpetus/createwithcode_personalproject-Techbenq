using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rollStaminaNeed;
    public float punchStaminaNeed;

    public float rollLength;

    private CharacterController controller;
    private float horizontal, vertical, mouseX, turnSpeed;

    private bool canMove = true;
    private bool canPunch = true;
    private bool canRoll = true;

    private Animator anim;

    private int slotNum;

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
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

            anim.SetFloat("Side", horizontal);
            anim.SetFloat("Forward", vertical);

            if (horizontal != 0 || vertical != 0)
            {
                Vector3 dir = transform.right * horizontal + transform.forward * vertical;
                controller.Move(dir * Time.deltaTime * speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canRoll)
                {
                    if (SurvivalNeeds.instance.DrainStamina(rollStaminaNeed))
                    {
                        anim.applyRootMotion = true;

                        anim.SetTrigger("Roll");
                        canMove = false;
                        canRoll = false;

                        Invoke("AllowMovement", rollLength);
                        Invoke("AllowRoll", rollLength * 1.25f);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(SelectObject.current != null)
                    SelectObject.current.Interact();

                else
                    InventoryManager.instance.activeSlot.Use();
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            slotNum = Mathf.Clamp(slotNum - (int)Input.mouseScrollDelta.y, 0, 8);
            InventoryManager.instance.SetActiveSlot(slotNum);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(canPunch)
                if (SurvivalNeeds.instance.DrainStamina(punchStaminaNeed))
                {
                    anim.SetTrigger("Punch");
                    canMove = false;
                    canPunch = false;
                }
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.C))
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                InventoryManager.instance.ToggleCrafting(false);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                InventoryManager.instance.ToggleCrafting(true);
            }
        }
    }

    private void allowPunch()
    {
        canPunch = true;
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

    private void AllowRoll()
    {
        canRoll = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            SurvivalNeeds.withinRadius = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Campfire"))
        {
            SurvivalNeeds.withinRadius = false;
        }
    }
}
