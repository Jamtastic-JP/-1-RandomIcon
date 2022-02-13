using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] int speed;
    [SerializeField] float radiusCheck;
    [SerializeField] int jumpForce;
    [SerializeField] LayerMask groundLayer;
    [Header("Shoot")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform shootSpawn;
    Rigidbody2D myRigid;
    bool onGround, facingRigth = true;
    bool positiveCooldown = false, negativeCooldown = false;

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
        if (Input.GetMouseButtonDown(0) && !positiveCooldown) {
            Shoot(true);
        } else if(Input.GetMouseButtonDown(1) && !negativeCooldown) {
            Shoot(false);
        }
        
        if ((move < 0 && facingRigth) || (move > 0 && !facingRigth)) Flip();
    }

    void Shoot(bool typeShoot) {
        if (typeShoot) positiveCooldown = true;
        else negativeCooldown = true;
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.StartBullet(typeShoot, shootSpawn.position, facingRigth);
        StartCoroutine(ShootCooldown(typeShoot));
    }

    IEnumerator ShootCooldown(bool typeShoot) {
        yield return new WaitForSeconds(.5f);
        if (typeShoot) positiveCooldown = false;
        else negativeCooldown = false;
    }

    void Flip() {
        facingRigth = !facingRigth;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
