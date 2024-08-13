using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [SerializeField] private GameObject Player_Camera;
    [SerializeField] private Transform anchor_transform;

    public void Update()
    {
        Player_Camera.transform.position = anchor_transform.position + anchor_transform.forward * -7f;
        Player_Camera.transform.LookAt(anchor_transform.position);
    }
}
