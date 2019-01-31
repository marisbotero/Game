using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Basic Properties....")]
    [SerializeField]
    float moveSpeed = 0.0f;
    public int attackDamage = 0;
    public Collider2D hitTrigger;
    [SerializeField]
    bool isAttacking = false;
    Animator anim;

    [Space(10.0f)]
    [Header("Buff Options....")]
    public float speedTime = 0.0f;
    public float speedAmount = 0.0f;
    public float instaKillTime = 0.0f;
    public bool isRange = false;
    public float bulletSpeed = 0.0f;
    public float bulletLife = 0.0f;
    public int bulletsToStart = 0;
    public Rigidbody2D bulletPrefab;
    public Transform bulletSpawn;
    int bulletAmount = 0;
    Vector3 bulletDirection = Vector3.zero;

    [Space(10.0f)]
    [Header("Buff Options....")]
    public AudioClip[] clips;
    AudioSource source;

    enum WalkingDirection
    {
        left,
        right,
        up,
        down
    }
    [SerializeField]
    WalkingDirection direction = WalkingDirection.down;

    Rigidbody2D rb;
    SpriteRenderer renderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();

        hitTrigger.transform.localPosition = new Vector3(0.0f, -0.74f, 0.0f);
        hitTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladron")
        {
            Enemy2 tempEnemy = collision.gameObject.GetComponent<Enemy2>();
            tempEnemy.ReceiveDamage(attackDamage + ManagerLevelsNight.buffAttack);
            Debug.Log("Hit Enemy");
        }

        if(collision.CompareTag("Buff"))
        {
            ActivateRangeAttack();
        }
    }

    //tesoro shield, tesoro stamina, mermar velocidad de enemigos, vuelve torres rapido, velocidad, instakill
    void ActivateRangeAttack()
    {
        isRange = true;
        bulletAmount += bulletsToStart;
        Debug.Log("Recharged");
    }

    void FireBullet()
    {
        Rigidbody2D tempBullet = Instantiate(bulletPrefab.gameObject, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        tempBullet.velocity = bulletDirection * bulletSpeed;
        Destroy(tempBullet.gameObject, bulletLife);

        bulletAmount--;

        if (bulletAmount <= 0)
        {
            isRange = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ActivateRangeAttack();           
        }

        if (isAttacking == false)
        {
            if (Input.GetAxis("Horizontal") > 0.01f)
            {
                rb.velocity = new Vector3(moveSpeed, 0.0f, 0.0f);
                anim.Play("Walking");
                direction = WalkingDirection.right;

                hitTrigger.transform.localPosition = new Vector3(0.42f, 0.0f, 0.0f);
                hitTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            }
            else if (Input.GetAxis("Horizontal") < -0.01f)
            {
                rb.velocity = new Vector3(-moveSpeed, 0.0f, 0.0f);
                anim.Play("Walking");
                direction = WalkingDirection.left;
                hitTrigger.transform.localPosition = new Vector3(0.42f, 0.0f, 0.0f);
                hitTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                transform.localScale = new Vector3(-1.0f, 1.0f, 0.0f);
            }
            else if (Input.GetAxis("Vertical") > 0.01f)
            {
                rb.velocity = new Vector3(0.0f, moveSpeed, 0.0f);
                anim.Play("WalkingVerticalUp");
                direction = WalkingDirection.up;

                hitTrigger.transform.localPosition = new Vector3(0.0f, 0.74f, 0.0f);
                hitTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            }
            else if (Input.GetAxis("Vertical") < -0.01f)
            {
                rb.velocity = new Vector3(0.0f, -moveSpeed, 0.0f);
                anim.Play("WalkingVerticalDown");
                direction = WalkingDirection.down;
                hitTrigger.transform.localPosition = new Vector3(0.0f, -0.74f, 0.0f);
                hitTrigger.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
            }
            else
            {
                rb.velocity = Vector3.zero;

                switch (direction)
                {
                    case WalkingDirection.left:
                        anim.Play("Idle");
                        break;
                    case WalkingDirection.right:
                        anim.Play("Idle");
                        break;
                    case WalkingDirection.up:
                        anim.Play("IdleUp");
                        break;
                    case WalkingDirection.down:
                        anim.Play("IdleDown");
                        break;
                }
            }
        }

        if (isRange && Input.GetButtonDown("Fire1"))
        {
            int rnd = Random.Range(0, 2);
            source.PlayOneShot(clips[rnd]);
            switch (direction)
            {
                case WalkingDirection.left: anim.Play("RangeAttack"); bulletDirection = Vector3.left; break;
                case WalkingDirection.right: anim.Play("RangeAttack"); bulletDirection = Vector3.right; break;
                case WalkingDirection.up: anim.Play("RangeAttackUp"); bulletDirection = Vector3.up; break;
                case WalkingDirection.down: anim.Play("RangeAttackDown"); bulletDirection = -Vector3.up; break;
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector3.zero;
            int rnd = Random.Range(0, 2);
            source.PlayOneShot(clips[rnd]);
            switch (direction)
            {
                case WalkingDirection.left: anim.Play("Attack"); break;
                case WalkingDirection.right: anim.Play("Attack"); break;
                case WalkingDirection.up: anim.Play("AttackUp"); break;
                case WalkingDirection.down:  anim.Play("AttackDown"); break;
            }
        }
    }
}
