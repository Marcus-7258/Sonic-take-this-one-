using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonic : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private bool ground;
	private bool run;
    private float prev;
    private Animator a;
    public float Xspeed;
    public float Yspeed;
	public GameObject loop;
	public Quaternion originalRotation;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = true;
        a = GetComponent<Animator>();
        prev = 0f;
        spr = GetComponent<SpriteRenderer>();
		rb.freezeRotation=true;
		originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.localScale=new Vector3(0.1542039f,0.1683983f,1f);
		if(transform.position.y >= -2)
		{
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxisRaw("Vertical");
        if (movehorizontal < 0)
        {
            spr.flipX = true;
        }
        else if (movehorizontal > 0) {
            spr.flipX = false;
        }
        if ((Mathf.Abs(movehorizontal) > 0 && prev == 0) && ground)
        {
			run = true;
            a.SetBool("RUN",true);
			a.SetBool("Idle",false);
        }else if((movehorizontal == 0 && Mathf.Abs(prev) > 0)&& ground){
			run = false;
            a.SetBool("RUN",false);
			a.SetBool("Idle",true);
		}
        prev = movehorizontal;
        if (movevertical == 1 && ground && !run)
        {
            ground = false;
            a.SetBool("Jump",true);
			a.SetBool("Idle",false);
            transform.localScale = new Vector3(2f, 1.7f, 1f);
        }
		else if (movevertical == 1 && ground && run){
			ground = false;
			a.SetBool("Jump",true);
			a.SetBool("RUN",false);
            transform.localScale = new Vector3(2f, 1.7f, 1f);
		}
        else 
        {
            movevertical = 0;
        }
		if ((movehorizontal == 0)){
			run = false;	
		}else {
			run = true;
		}
		if (transform.position.x <= 0 && movehorizontal<0){
			movehorizontal=0;
		}
        //Vector2 movement = new Vector2(movehorizontal * Xspeed, movevertical * Yspeed);
        //rb.AddForce(movement);
		transform.Translate(Vector3.right*(Time.deltaTime*movehorizontal*Xspeed));
		}




    }

    void OnCollisionEnter2D(Collision2D c2D) {
		if (c2D.gameObject.tag=="loop")
		{
			rb.freezeRotation=false;
		}else if(c2D.gameObject.tag == "end loop"){
			loop.SetActive(false);
			transform.rotation = originalRotation;
			rb.freezeRotation=true;
		}
		else if(!(c2D is PolygonCollider2D)){
			if (!ground && !run)
			{
				a.SetBool("Jump",false);
				a.SetBool("Idle",true);
				ground=true;
			}else if (!ground && run){
				a.SetBool("RUN",true);
				a.SetBool("Jump",false);
				ground=true;
			}
		}  
        //ground = true;
        transform.localScale = new Vector3(0.1350209f, 0.1667312f, 1f);
    }
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag=="end loop")
		{
			transform.rotation = originalRotation;
			rb.freezeRotation=true;
		}
	}
	
}
// Always Save
