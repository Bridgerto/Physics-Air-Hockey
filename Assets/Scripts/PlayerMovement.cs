using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool wasJustClicked = true; // has the player clicked on the scene
    bool canMove; // can the player move or not
    Vector2 playerSize; //how big the Human player sprite is

    Rigidbody2D rb;

    public Transform BoundaryHolder;

    Boundary playerBoundary;

    struct Boundary
    {
        public float Up, Down, Left, Right;

        public Boundary(float up, float down, float left, float right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the extent of the player, the distance from centre to the egde
        playerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                       BoundaryHolder.GetChild(1).position.y,
                                       BoundaryHolder.GetChild(2).position.x,
                                       BoundaryHolder.GetChild(3).position.x);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (wasJustClicked)
            {
                wasJustClicked = false;

                // check if the mouse position at time of the click, is in the same position as the player sprite
                if ((MousePos.x >= transform.position.x && MousePos.x < transform.position.x + playerSize.x ||
                    MousePos.x <= transform.position.x && MousePos.x > transform.position.x - playerSize.x) &&
                    (MousePos.y >= transform.position.y && MousePos.y < transform.position.y + playerSize.y ||
                    MousePos.y <= transform.position.y && MousePos.y > transform.position.y - playerSize.y))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }


            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(MousePos.x, playerBoundary.Left, playerBoundary.Right),
                                                      Mathf.Clamp(MousePos.y, playerBoundary.Down, playerBoundary.Up));
                rb.MovePosition(MousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }

}
