using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] int speed;
    [SerializeField] float radiusCheck;
    [SerializeField] int jumpForce;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D myRigid;
    bool onGround, facingRigth = true;

    void Start() {
        myRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        float move = Input.GetAxis("Horizontal");
        myRigid.velocity = new Vector2(move * speed, myRigid.velocity.y);
        onGround = Physics2D.OverlapCircle(transform.position, radiusCheck, groundLayer);
        if (Input.GetKeyDown(KeyCode.W) && onGround) {
            myRigid.AddForce(new Vector2(0f, jumpForce));
        }
        if ((move < 0 && facingRigth) || (move > 0 && !facingRigth)) Flip();
    }

    void Flip() {
        facingRigth = !facingRigth;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
