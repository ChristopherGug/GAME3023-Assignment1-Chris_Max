using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator anim;

    private Rigidbody2D rb;
    private Vector2 moveVector;
    private bool lookLeft = false;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            lookLeft = false;
            SoundManager.moving = true;
            isMoving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            lookLeft = true;
            SoundManager.moving = true;
            isMoving = true;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            SoundManager.moving = true;
            isMoving = true;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            SoundManager.moving = true;
            isMoving = true;
        }
        else
        {
            SoundManager.moving = false;
            isMoving = false;
        }

        anim.SetFloat("Horizontal", moveVector.x);
        anim.SetFloat("Vertical", moveVector.y);
        Debug.Log(SoundManager.moving);
        anim.SetFloat("Speed", moveVector.sqrMagnitude);

        if (lookLeft)
            transform.localScale = new Vector3 (-0.75f, 0.75f, 1);
        else
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }
}
