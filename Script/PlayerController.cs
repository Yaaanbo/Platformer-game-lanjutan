using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    bool isJump = true;
    bool isDead = false;
    int idMove = 0;
    Animator anim;
    public GameObject bullet;
    public Vector2 bulletVelocity;
    public Vector2 bulletOffset;
    public float cooldown = 0.5f;
    public bool isCanShoot = true;
    public int score;
    public Text scoreUI;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        anim = GetComponent<Animator>();
        isCanShoot = false;
        EnemyController.enemyKilled = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            idle();
        }
        move();
        dead();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isJump)
        {
            //Ketika menyentuh tanah
            anim.ResetTrigger("Jump");
            if (idMove == 0) anim.SetTrigger("Idle");
            isJump = false;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Kondisi ketika menyentuh tanah
        anim.SetTrigger("Jump");
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Idle");
        isJump = true;
    }
    public void moveRight()
    {
        idMove = 1;
    }
    public void moveLeft()
    {
        idMove = 2;
    }
    void move()
    {
        if (idMove == 1 && !isDead)
        {
            //Kondisi ketika bergerak ke kanan
            if (!isJump) anim.SetTrigger("Run");
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (idMove == 2 && !isDead)
        {
            //Kondisi ketika bergerak ke kiri
            if (!isJump) anim.SetTrigger("Run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void jump()
    {
        if(!isJump)
        {
            //Kondisi ketika loncat
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
        }
    }
    public void idle()
    {
        if (!isJump)
        {
            anim.ResetTrigger("Jump");
            anim.ResetTrigger("Run");
            anim.SetTrigger("Idle");
        }
        idMove = 0;
    }
    public void dead()
    {
        if (!isDead)
        {
            if(transform.position.y < -10f)
            {
                //Kondisi ketika jatuh
                isDead = true;
            }
        }
    }

    //Coin collect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Coin"))
        {
            score++;
            scoreUI.text = $"{score.ToString()}";
            Destroy(collision.gameObject);
        }
    }
    
    void shoot()
    {
        if (isCanShoot)
        {
            //Membuat peluru baru
            GameObject projectile = Instantiate(bullet, (Vector2)transform.position - bulletOffset * transform.localScale.x,
                Quaternion.identity);

            //Membuat kecepatan dari peluru
            Vector2 velocity = new Vector2(bulletVelocity.x * transform.localScale.x, bulletVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * -1;

            //Menyesuaikan scale dari peluru dengan scale karakter
            Vector3 scale = transform.localScale;
            bullet.transform.localScale = scale * -1;

            StartCoroutine(CanShootCoroutine());
            anim.SetTrigger("shoot");
        }
    }
    IEnumerator CanShootCoroutine()
    {
        anim.SetTrigger("shoot");
        isCanShoot = false;
        yield return new WaitForSeconds(cooldown);
        isCanShoot = true;
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.transform.tag.Equals("Peluru"))
        {
            isCanShoot = true;
        }
        if (collision.transform.tag.Equals("Enemy"))
        {
            SceneManager.LoadScene("Game Over");
            isDead = true;
        }
    }
}
