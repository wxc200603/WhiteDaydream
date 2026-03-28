using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterMove : MonoBehaviour
{
    public float speed = 30;
    public Rigidbody2D rb;
    public Animator anim;
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical);

        rb.velocity = new Vector2(horizontal, vertical) * speed;
    }
}
