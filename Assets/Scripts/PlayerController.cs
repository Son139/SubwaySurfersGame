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

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject tapToStartPanel;
    [SerializeField] GameObject guiPlayGame;

    [Header("Android Control")]
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;

    int currentPos = 0;

    public float sideSpeed;
    public float runningSpeed;
    public float jumpForce;

    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;


    bool isGameStarted;
    bool isGameOver;

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

        dragDistance = Screen.height * 15 / 100;
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
                playerAnimator.speed = 1.2f;
                tapToStartPanel.SetActive(false);
                guiPlayGame.SetActive(true);
            }
        }

        if (isGameStarted)
        {
            MoveForward();
#if UNITY_EDITOR
            MoveHorizontal();
#elif UNITY_ANDROID
            androidControl();
#endif
            MoveToCenter();
            Jump();
        }

        if (isGameOver)
        {
            if (!gameOverPanel.activeSelf)
            {
                AudioManager.instance.StopMusic();
                gameOverPanel.SetActive(true);
            }
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

    private void androidControl()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            if (currentPos == 0) currentPos = 2;
                            else if(currentPos == 1) currentPos = 0;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            if(currentPos ==0) currentPos = 1;
                            else if( currentPos == 2) currentPos = 0;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            rb.velocity = Vector3.up * jumpForce;
                            StartCoroutine(ChangeToJump());
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }


    private void MoveForward()
    {
        transform.position += transform.forward * runningSpeed * Time.deltaTime;
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            rb.velocity = Vector3.up * jumpForce ;
            StartCoroutine(ChangeToJump());
        }
    }

    IEnumerator ChangeToJump()
    {
        playerAnimator.SetInteger("isJump", 1);
        yield return new WaitForSeconds(0.1f); 
        playerAnimator.SetInteger("isJump", 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.death);
            isGameStarted = false;
            isGameOver = true;
            playerAnimator.applyRootMotion = true;
            playerAnimator.SetInteger("isDied", 1);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    bool GroundCheck()
    {
        if(Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
