using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlate : MonoBehaviour
{
    public GameObject lasagna;

    public void SetHeightScale(float heightScale)
    {
        Vector3 previousScale = lasagna.transform.localScale;
        lasagna.transform.localScale = new Vector3(previousScale.x, heightScale, previousScale.z);
    }
}
