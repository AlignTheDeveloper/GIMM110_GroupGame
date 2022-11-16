using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
    private void FixedUpdate() 
    {
        rb.velocity = (transform.forward * moveSpeed);
    }
}
