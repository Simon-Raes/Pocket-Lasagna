using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    public Text flingText;

    public GameObject lasagnasAnchor;
    public GameObject[] lasagnas;

    public Transform positionLeft;
    public Transform positionMiddle;
    public Transform positionRight;

    // private float fingerStartTime = 0.0f;
    // private Vector2 fingerStartPos = Vector2.zero;

    // private bool isSwipe = false;
    // private float minSwipeDist = 50.0f;
    // private float maxSwipeTime = 0.5f;

    private bool draggingMouse;
    private bool wasDraggingFinger;
    private Vector3? previousMousePosition = null;
    private bool flinging; // Continued, decelerating motion after letting go during a drag
    private bool snapping; // Moving to place the nearest lasagna in the center of the screen after a fling ends
    private Vector3 snapTargetPosition;
    private float flingSpeed;

    private float snapSpeed = 3;

    void Start()
    {

    }

    void Update()
    {
        DetectTap();
        DetectDrag();
    }

    private void DetectTap()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2)
        {
            LoadArScene();
        }
    }

    private void DetectDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            draggingMouse = true;
            flinging = false;
            snapping = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            draggingMouse = false;
            previousMousePosition = null;

            StartFling();
        }

        if (draggingMouse)
        {
            if (previousMousePosition != null)
            {
                Vector2 currentPosition = Input.mousePosition;

                float xDiff = currentPosition.x - previousMousePosition.Value.x;
                flingSpeed = xDiff;
                MoveLasagnasForDrag(xDiff);
            }

            // 0,0 is bottom left
            previousMousePosition = Input.mousePosition;
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                wasDraggingFinger = true;
                float xDiff = Input.GetTouch(0).deltaPosition.x;

                // TODO find a cleane rsolution for the difference between mouse and fling events?
                flingSpeed = xDiff * 5;
                MoveLasagnasForDrag(xDiff);
            }
        }
        else if (wasDraggingFinger)
        {
            wasDraggingFinger = false;
            StartFling();
        }

        if (flinging)
        {
            flingSpeed = flingSpeed * .75f;
            print("flinging at " + flingSpeed);
            print("deltatime" + Time.deltaTime);
            lasagnasAnchor.transform.position = new Vector3(lasagnasAnchor.transform.position.x + flingSpeed / 800,
                            lasagnasAnchor.transform.position.y,
                            lasagnasAnchor.transform.position.z);

            if (Mathf.Abs(flingSpeed) < 40)
            {
                flinging = false;
                StartSnapping();
            }
        }

        if (snapping)
        {
            float step = snapSpeed * Time.deltaTime;
            lasagnasAnchor.transform.position = Vector3.MoveTowards(lasagnasAnchor.transform.position, snapTargetPosition, step);
        }
    }

    private void MoveLasagnasForDrag(float xDistance)
    {
        lasagnasAnchor.transform.position = new Vector3(
                            lasagnasAnchor.transform.position.x + xDistance / 800,
                            lasagnasAnchor.transform.position.y,
                            lasagnasAnchor.transform.position.z);
    }

    //TODO just make the fling check where the next lasagna is and move towards that location, starting with the fling velocity
    // Don't need this weird overshooting
    private void StartFling()
    {
        flingText.text = flingSpeed.ToString();
        flinging = true;
    }

    private void StartSnapping()
    {
        GameObject closestLasagna = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject lasagna in lasagnas)
        {
            print("las pos " + lasagna + ": " + lasagna.transform.position.x);
            float xPos = lasagna.transform.position.x;
            float distance = Mathf.Abs(xPos);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                closestLasagna = lasagna;
            }
            else
            {
                // Moving farther away again which means we already have our closest lasagna.
                break;
            }
        }

        float distanceToMove = -closestLasagna.transform.position.x;

        snapTargetPosition = new Vector3(
                    lasagnasAnchor.transform.position.x + distanceToMove,
                    lasagnasAnchor.transform.position.y,
                    lasagnasAnchor.transform.position.z);
        snapping = true;
    }

    private void LoadArScene()
    {
        SceneManager.LoadScene(1);
    }
}
