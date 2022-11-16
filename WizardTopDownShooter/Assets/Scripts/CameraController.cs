using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform lookAt; // Drop player here in the inspector.
    private Vector3 startOffset;
    private Vector3 moveVector;

    private void Start()
    {
        startOffset = transform.position - lookAt.transform.position;
        Cursor.visible = false;
    }

    private void Update()
    {
        moveVector = lookAt.position + startOffset;
        transform.position = moveVector;
    }
    
}