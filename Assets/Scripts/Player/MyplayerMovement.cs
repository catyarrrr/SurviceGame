using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyplayerMovement : MonoBehaviour
{
    public float Speed = 5;

    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);

        Rotate();

        Anime(h, v);
    }

    //水平运动
    private void Move(float h, float v)
    {
        Vector3 movementV3 = new Vector3(h, 0, v);
        movementV3 = movementV3.normalized * Speed * Time.deltaTime;

        rb.MovePosition(transform.position + movementV3);
    }

    //转向
    private void Rotate()
    {
        //射线（鼠标位置）
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool isTouchFloor = Physics.Raycast(cameraRay, out hit, 200, LayerMask.GetMask("Floor"));

        if (isTouchFloor)
        {
            Vector3 v3 = hit.point - transform.position;
            v3.y = 0;

            Quaternion quaternion = Quaternion.LookRotation(v3);

            rb.MoveRotation(quaternion);
        }

    }

    private void Anime(float h, float v)
    {
        if (h == 0 && v == 0)
            anim.SetBool("isMove", false);
        else
            anim.SetBool("isMove", true);
    }


}
