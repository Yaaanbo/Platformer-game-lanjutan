using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isFacingRight = false;
    public Transform batas1;
    public Transform batas2;
    public float speed;
    Rigidbody2D rb;
    Animator anim;
    public int hp = 1;
    bool isDie = false;
    public static int enemyKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && !isDie)
        {
            if (isFacingRight)
            {
                moveRight();
            }
            else
            {
                moveLeft();
            }
            if (transform.position.x >= batas2.position.x && isFacingRight)
            {
                flip();
            }
            else if(transform.position.x <= batas1.position.x && !isFacingRight)
            {
                flip();
            }
        }
    }

    void moveRight()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        if (!isFacingRight)
        {
            flip();
        }
    }
    void moveLeft()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        if (isFacingRight)
        {
            flip();
        }
    }
    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void damageTaken(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDie = true;
            rb.velocity = Vector2.zero;
            anim.SetBool("IsDie", true);
            Destroy(this.gameObject, 2);
            //Data.score += 20;
            enemyKilled++;
        }
        if (enemyKilled == 3)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
