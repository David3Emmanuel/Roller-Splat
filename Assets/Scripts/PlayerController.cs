using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Swipe swipe;
    private Material material;
    private AudioSource audioSource;

    public float speed = 15.0f;
    public float minCollisionDistance = 2.5f;

    private bool isMoving;
    private Vector3 direction;
    private Vector3 nextCollisionPosition;
    public Color solveColor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        swipe = GetComponent<Swipe>();
        audioSource = GetComponent<AudioSource>();
        solveColor = Random.ColorHSV(0.5f, 1);
        material = GetComponent<MeshRenderer>().material;
        material.color = solveColor;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = direction * speed;
        }

        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < minCollisionDistance)
            {
                isMoving = false;
                audioSource.Stop();
                direction = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }

        if (isMoving) return;

        switch (swipe.swipeDirection)
        {
            case SwipeDirection.Up:
                SetDestination(Vector3.forward);
                break;
            case SwipeDirection.Down:
                SetDestination(Vector3.back);
                break;
            case SwipeDirection.Left:
                SetDestination(Vector3.left);
                break;
            case SwipeDirection.Right:
                SetDestination(Vector3.right);
                break;
            case SwipeDirection.None:
            default:
                break;
        }
    }

    void SetDestination(Vector3 direction)
    {
        this.direction = direction;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPosition = hit.point;
        }
        isMoving = true;
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            GroundPiece groundPiece = other.GetComponent<GroundPiece>();
            if (!groundPiece.isColored)
            {
                groundPiece.ChangeColor(solveColor);
            }
        }
    }
}
