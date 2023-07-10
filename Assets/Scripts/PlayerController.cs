using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isJump = false;
    private bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;

    private Vector2 startPosition;
    private Animator animator;
void Start()
{
	startPosition = transform.position;
    animator = GetComponent<Animator>();
}

void Update()
{
	// 달리는 애니메이션으로 설정하기
	animator.SetBool("run", true);
        if (Input.GetKeyDown(KeyCode.Space)) // spacebar 누르면
        {
            isJump = true;
        }
        else if (transform.position.y <= startPosition.y)
        {
            isJump = false;
            isTop = false;
            transform.position = startPosition;
        }

        if (isJump)
        {
            if (transform.position.y <= jumpHeight - 0.1f && !isTop)
            {
                transform.position = Vector2.Lerp(transform.position,
                    new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
            }
            else
            {
                isTop = true;
            }

            if (transform.position.y > startPosition.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    startPosition, jumpSpeed * Time.deltaTime);
            }
        }
    }
}