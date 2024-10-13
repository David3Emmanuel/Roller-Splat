using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Swipe : MonoBehaviour
{
    public int minSwipeRecognition = 500;
    public SwipeDirection swipeDirection;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = Input.mousePosition;
            if (swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;
            }
            swipePosLastFrame = swipePosCurrentFrame;

            if (currentSwipe.sqrMagnitude < minSwipeRecognition) return;

            currentSwipe.Normalize();

            // Vertical
            if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                swipeDirection = currentSwipe.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }
            // Horizontal
            if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                swipeDirection = currentSwipe.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
            swipeDirection = SwipeDirection.None;
        }
    }
}
