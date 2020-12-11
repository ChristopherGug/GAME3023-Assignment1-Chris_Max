using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Animator anim;

    private Rigidbody2D rb;
    private Vector2 moveVector;
    private bool lookLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetAxisRaw("Horizontal") > 0)
            lookLeft = false;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            lookLeft = true;

        anim.SetFloat("Horizontal", moveVector.x);
        anim.SetFloat("Vertical", moveVector.y);
        anim.SetFloat("Speed", moveVector.sqrMagnitude);

        if (lookLeft)
            transform.localScale = new Vector3 (-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }
}
