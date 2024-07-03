using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeedMultiplier = 2f;
    public float rotationSpeed = 5f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 获取键盘输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 检测是否有移动输入
        bool isWalking = horizontalInput != 0 || verticalInput != 0;

        // 检测是否按住Shift键
        bool isRunning = isWalking && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        // 设置Animator参数
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);

        // 检测是否按下攻击键（例如空格键）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isAttacking", false);
        }

        // 计算移动方向和移动速度（使用角色本地坐标系）
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        float currentMoveSpeed = isRunning ? walkSpeed * runSpeedMultiplier : walkSpeed;
        Vector3 moveAmount = transform.TransformDirection(moveDirection) * currentMoveSpeed * Time.deltaTime;

        // 移动角色
        transform.Translate(moveAmount, Space.World);

        // 获取鼠标在屏幕上的位置变化
        float mouseX = Input.GetAxis("Mouse X");

        // 根据鼠标移动来旋转角色
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);
    }
}
