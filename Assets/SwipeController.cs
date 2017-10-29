using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    public Text swipeText;

    public GameObject lasagnasAnchor;
    public GameObject lasagnaSmall;
    public GameObject lasagnaMedium;
    public GameObject lasagnaLarge;

    public Transform positionLeft;
    public Transform positionMiddle;
    public Transform positionRight;



    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    void Update()
    {
        DetectSwipe();
        DetectButton();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {

            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:

                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    OnSwipeRight();
                                }
                                else
                                {
                                    OnSwipeLeft();
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    OnSwipeUp();
                                }
                                else
                                {
                                    swipeText.text = "DOWN!";
                                }
                            }

                        }

                        break;
                }
            }
        }
    }

    private void DetectButton()
    {
        if (Input.GetKey("left"))
            OnSwipeLeft();

        if (Input.GetKey("right"))
            OnSwipeRight();

		if (Input.GetKey("up"))
            OnSwipeUp();
    }

    private void OnSwipeLeft()
    {
        swipeText.text = "LEFT!";
        SlideLeft();
    }

    private void OnSwipeRight()
    {
        swipeText.text = "RIGHT!";
    }

    private void OnSwipeUp()
    {
		swipeText.text = "UP!";
        SceneManager.LoadScene(1);
    }

    void SlideLeft()
    {
        var anim = lasagnasAnchor.GetComponent<Animator>();
        anim.SetTrigger("SlideLeft");
    }
}
