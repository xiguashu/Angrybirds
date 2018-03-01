using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour {

    private bool isClicked = false;
    public Transform rightpos;
    public Transform leftpos;

    public float maxDis=2;
    public SpringJoint2D sp;
    bool released = true;
    bool start = false;
    bool outofborder = false;
    private Rigidbody2D rg;
    public LineRenderer left;
    public LineRenderer right;

    public boom boom;

    private TestMyTrail trail;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        trail = GetComponent<TestMyTrail>();
        outofborder = false;
    }
 

	// Use this for initialization
	void Start () {
        gameManager.instance.initialize();
    }
	
    

	// Update is called once per frame
	void Update () {
            line();
        if (isClicked) //如果鼠标按下
        {
            start = true;
            //拖着走
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position);
            if (Vector3.Distance(transform.position, rightpos.position) > maxDis && released)
            {
                Vector3 pos = (transform.position - rightpos.position).normalized;
                transform.position = pos * maxDis + rightpos.position;
            }
        }
        //结束条件判定
        else if( transform.position.x > 13 || transform.position.x < -13) //超出边界
        {
            outofborder = true;
            nextbird();
        }
        else if((rg.velocity.magnitude<0.05&&start==true)) //速度为零
        {
            nextbird();
           
        }

	}
    private void OnMouseDown()
    {
        if (start == false)
        {
            isClicked = true;
            released = true;
            rg.isKinematic = true;
        }

    }
    private void OnMouseUp()
    {
        isClicked = false;
        released = false;
        rg.isKinematic = false;
        Invoke("fly", 0.1f);
        Invoke("line", 0.5f);
       
        



    }
    private void fly()
    {
        trail.trailStart();
        sp.enabled = false;
    }

    private void line()
    {
        if (released)
        {
            right.SetPosition(0, rightpos.position);
            right.SetPosition(1, transform.position);
            left.SetPosition(0, leftpos.position);
            left.SetPosition(1, transform.position);
        }
        else
        {
            right.SetPosition(0, rightpos.position);
            right.SetPosition(1, leftpos.position);
            left.SetPosition(0, leftpos.position);
            left.SetPosition(1, rightpos.position);
        }
    }

    private void nextbird()
    {
        gameManager.instance.birds.Remove(this);
        if(!outofborder)
            Instantiate(boom, transform.position, Quaternion.identity);

        if(gameManager.instance.birds.Count>0)
            gameManager.instance.birds[0].transform.position = gameManager.instance.temppos;

        gameManager.instance.nextbirds();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trail.trailEnd();
    }
    
    
    
}
