using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Move With Arrows")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
    [Header("Input keys")]
    public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

    [Header("Movement")]
    [Tooltip("Speed of movement")]
    public float speed = 5f;
    public Enums.MovementType movementType = Enums.MovementType.AllDirections;

    [Header("Orientation")]
    public bool orientToDirection = false;
    // The direction that will face the player
    public Enums.Directions lookAxis = Enums.Directions.Up;

    private Vector2 movement, cachedDirection;
    private float moveHorizontal;
    private float moveVertical;
    bool FastMove = true;
    Animator anim;
    Transform Mytransform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Mytransform = transform;
        OringinScale = Mytransform.localScale;
    }

    // Update gets called every frame
    void Update()
    {
        // Moving with the arrow keys
        if (typeOfControl == Enums.KeyGroups.ArrowKeys)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = Input.GetAxis("Vertical2");
        }

        if (FastMove)
        {
            bool isWalk = false;
            if (Mathf.Abs(moveHorizontal) > 0.01f)
            {
                moveHorizontal = Mathf.Sign(moveHorizontal);
                isWalk = true;
            }
                
            if (Mathf.Abs(moveVertical) > 0.01f)
            {
                moveVertical = Mathf.Sign(moveVertical);
                isWalk = true;
            }
            if (isWalk)
                anim.SetBool("Walk", true);
            else
                anim.SetBool("Walk", false);
            //Player face left or right
            if (moveHorizontal < -0.01f)
                Mytransform.rotation = Quaternion.Euler(0, 180, 0);
            else if(moveHorizontal > 0.01f)
                Mytransform.rotation = Quaternion.Euler(0, 0, 0);
        }


        //zero-out the axes that are not needed, if the movement is constrained
        switch (movementType)
        {
            case Enums.MovementType.OnlyHorizontal:
                moveVertical = 0f;
                break;
            case Enums.MovementType.OnlyVertical:
                moveHorizontal = 0f;
                break;
        }

        movement = new Vector2(moveHorizontal, moveVertical);


        //rotate the gameObject towards the direction of movement
        //the axis to look can be decided with the "axis" variable
        if (orientToDirection)
        {
            if (movement.sqrMagnitude >= 0.01f)
            {
                cachedDirection = movement;
            }
            Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
        }
    }

    Vector3 OringinScale;
    float OringinSpeed = 5;

    public void ResetSpeedAndScale()
    {
        Mytransform.localScale = OringinScale;
        speed = OringinSpeed;
    }

    public void ChangeSpeedAndScale(int n)
    {
        speed = OringinSpeed * (1 - 0.2f * n);
        Mytransform.localScale = OringinScale * (1 + 0.2f * n);
    }

    // FixedUpdate is called every frame when the physics are calculated
    void FixedUpdate()
    {
        // Apply the force to the Rigidbody2d
        rigidbody2D.AddForce(movement * speed * 10f);
    }
}