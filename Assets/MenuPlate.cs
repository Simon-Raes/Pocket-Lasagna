using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public float rotationSpeed = 10;
    public float bobDistance = .1f;
    public float bobSpeed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);

        float positionY = transform.position.y + bobDistance * Mathf.Sin(bobSpeed * Time.time);
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
    }
}
