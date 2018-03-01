using UnityEngine;

public class pig : MonoBehaviour {

    public float deadSpeed = 10;
    public float hurtSpeed = 4;
    public Sprite hurt;
    private SpriteRenderer render;
    public GameObject boom;
    public GameObject score;
    public bool ispig=false;
    private bool ishurted = false;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > deadSpeed)
        {
            dead();
        }
        else if (collision.relativeVelocity.magnitude > hurtSpeed)
        {
            if (ishurted)
                dead();
            else
            {
                ishurted = true;
                render.sprite = hurt;
            }
        }
    }

    private void dead()
    {
        if (ispig)
        {
            gameManager.instance.pigs.Remove(this);
            gameManager.instance.addscore(5000);
        }
        else
        {
            gameManager.instance.addscore(3000);
        }
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(gameObject);

        GameObject Score = Instantiate(score, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        Destroy(Score, 1.5f);
     
    }

}
