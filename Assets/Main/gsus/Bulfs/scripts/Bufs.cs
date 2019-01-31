using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bufs : MonoBehaviour
{
    private SpriteRenderer img;
    public enum BufsType { shieldStamina, weight, slowTime, fastReturn, velocity, instaKill, range }

    public BufsType type = BufsType.shieldStamina;

    public Sprite[] spritesBufs;

    private int rnd;

    private void Awake()
    {
        rnd = Random.Range(0, 7);

        switch (rnd)
        {
            case 0:
                type = BufsType.shieldStamina;
                break;
            case 1:
                type = BufsType.weight;
                break;
            case 2:
                type = BufsType.slowTime;
                break;
            case 3: type = BufsType.fastReturn;
                break;                
            case 4:
                type = BufsType.velocity;
                break;
            case 5:
                type = BufsType.instaKill;
                break;
            case 6:
                type = BufsType.range;
                break;
            default:
                break;
        }

        img = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case BufsType.shieldStamina:
                img.sprite = spritesBufs[0];
                break;
            case BufsType.weight:
                img.sprite = spritesBufs[1];
                break;
            case BufsType.slowTime:
                img.sprite = spritesBufs[2];
                break;
            case BufsType.fastReturn:
                img.sprite = spritesBufs[3];
                break;
            case BufsType.velocity:
                img.sprite = spritesBufs[5];
                break;
            case BufsType.instaKill:
                img.sprite = spritesBufs[7];
                break;
            default:
                break;
        }
        startCount();
    }

    void startCount()
    {
        StartCoroutine(count());
    }

    IEnumerator count()
    {
        yield return new WaitForSeconds(7.0f);

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player") {
            Destroy(gameObject);
        }
    }
}
