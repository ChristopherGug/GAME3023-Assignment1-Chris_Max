using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (Input.GetAxis("Horizontal") > 0)
        //{
        //    anim.SetFloat("Horizontal", 1);
        //}
        anim.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        transform.Translate(moveVector * moveSpeed);
    }
}
