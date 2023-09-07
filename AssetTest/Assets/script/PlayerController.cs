using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    private int playerHP = 10;

    private float speed = 0;
    private Rigidbody rb;
    private Animator animator;
    public static bool isJumping = false;

    private float horizontal;
    private float vertical;

    private Vector3 moveVec;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        PlayerMove();

        PLayerIsClick();
    }


    private void PlayerMove()           // 플레이어 움직임
    {
        // 좌표이동
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(horizontal, 0, vertical).normalized;

        transform.position += moveVec * 5 * Time.deltaTime;

        transform.LookAt(transform.position + moveVec);
        // 좌표이동

        // 애니메이션
        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            speed = Mathf.MoveTowards(speed, 1, Time.deltaTime * 3);
        }
        else
        {
            speed = Mathf.MoveTowards(speed, 0, Time.deltaTime * 3);
        }
        animator.SetFloat("Speed", speed);
        // 애니메이션
    }

    private void PLayerIsClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndAttack();
        }
    }

    private IEnumerator Attack()
    {
        //animator.SetLayerWeight(1, 1.0f);
        Debug.Log("1");

        animator.SetBool("attack", true);

        yield return new WaitForSeconds(0.5f);
        string secondLayerName = animator.GetLayerName(1);

        animator.SetFloat(secondLayerName, 0f);
        animator.speed = 0f;

        Debug.Log("두 번째 레이어의 이름: " + secondLayerName);
        //animation["name"].layer = 0;
    } 

    private void EndAttack()
    {
        animator.SetBool("attack", false);

        Debug.Log("2");
        animator.speed = 1;
    }
}
//animator.SetLayerWeight(1, 0.0f);       // 두번째(1) 레이어의 애니메이션을 멈춤
