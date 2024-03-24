using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;

    [SerializeField] Transform centerPos;
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;

    [SerializeField] Rigidbody rb;

    [SerializeField] Animator playerAnimator;

    int currentPos = 0;

    public float sideSpeed;
    public float runningSpeed;
    public float jumpForce;

    bool isGameStarted = false;
    bool isGameOver = false;
    bool isJumping = false;

    private void Awake()
    {
        PlayerController.instance = this;
    }

    void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        // 0 =  center, 1= left, 2 = right
        currentPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted || !isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("start Game");
                isGameStarted = true;
                playerAnimator.SetInteger("isRunning", 1);
                playerAnimator.speed = 1.5f;
            }
        }

        if (isGameStarted)
        {
            MoveHorizontal();
            MoveToCenter();
            Jump();
        }
    }

    private void MoveHorizontal()
    {
        if (currentPos == 0 && Input.GetKeyDown(KeyCode.LeftArrow))
            currentPos = 1;
        else if (currentPos == 0 && Input.GetKeyDown(KeyCode.RightArrow))
            currentPos = 2;
        else if (currentPos == 1 && Input.GetKeyDown(KeyCode.RightArrow))
            currentPos = 0;
        else if (currentPos == 2 && Input.GetKeyDown(KeyCode.LeftArrow))
            currentPos = 0;
    }

    private void MoveToCenter()
    {
        if (currentPos == 0)
        {
            if (Vector3.Distance(transform.position, new Vector3(centerPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(centerPos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World);
            }
        }
        else if (currentPos == 1)
        {
            if (Vector3.Distance(transform.position, new Vector3(leftPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(leftPos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World);
            }
        }
        else if (currentPos == 2)
        {
            if (Vector3.Distance(transform.position, new Vector3(rightPos.position.x, transform.position.y, transform.position.z)) >= 0.1f)
            {
                Vector3 dir = new Vector3(rightPos.position.x, transform.position.y, transform.position.z) - transform.position;
                transform.Translate(dir.normalized * sideSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void Jump()
    {
        // Kiểm tra xem nhân vật có đang trong trạng thái nhảy không
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jumpForce ;
            StartCoroutine(ChangeToJump());
        }
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += transform.forward * runningSpeed * Time.deltaTime;
    }

    IEnumerator ChangeToJump()
    {
        playerAnimator.SetInteger("isJump", 1);
        yield return new WaitForSeconds(0.1f); 
        playerAnimator.SetInteger("isJump", 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "object")
        {
            isGameStarted = false;
            isGameOver = true;
            playerAnimator.applyRootMotion = true;
            playerAnimator.SetInteger("isDied", 1);
        }
    }
}
