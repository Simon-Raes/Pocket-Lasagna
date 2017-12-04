using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPlate : MonoBehaviour
{
    public float rotationSpeed = 10;
    public GameObject lasagna;

    public float heightScale;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        detectKeys();

        DetectDrag();
    }

    private void detectKeys()
    {
        if (Input.GetKey("up"))
        {
            Vector3 previousScale = lasagna.transform.localScale;
            lasagna.transform.localScale = new Vector3(previousScale.x, previousScale.y * 1.1f, previousScale.z);
        }
        else if (Input.GetKey("down"))
        {
            Vector3 previousScale = lasagna.transform.localScale;
            lasagna.transform.localScale = new Vector3(previousScale.x, previousScale.y * .9f, previousScale.z);
        }
    }

    private void DetectDrag()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float yMovement = Input.GetTouch(0).deltaPosition.y;

                Vector3 previousScale = lasagna.transform.localScale;
                float adjustment = yMovement > 0 ? 1.05f : .95f;
                heightScale = previousScale.y * adjustment;
                lasagna.transform.localScale = new Vector3(previousScale.x, heightScale, previousScale.z);
            }
        }
    }
}
