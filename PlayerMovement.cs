using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 12f;
	public float runSpeed = 16f;

	public float jumpForce = 2000f;

	private Vector2 moveDir;
	private Vector2 jumpDir;
	private bool running;
	private bool jumping;
	public bool grounded;
	private bool facingRight = true;
    private bool jetPackOn = false;
    public float jetPackForce;
    public float jetPackFuelMax;
    private float jetPackFuel;

    public bool canControl = true;

    public CoinSound coinSoundRef;
    //public Transform groundCheckbox;
    //public float groundCheckRadius;
    //public LayerMask whatIsGround;

    private Rigidbody2D rb;

	private PlayerGravity playerGravity;
	private Vector2 gravityVector;
	private bool inOrbit;

	private float horMov;

	public GameObject planet;

	private Animator anim;

    public Canvas shop;
		private WeaponManager weaponManager;

	void Awake()
	{
		running = false;
		grounded = false;
        jetPackFuel = jetPackFuelMax;
        rb = GetComponent<Rigidbody2D>();
        //coinSoundRef = GetComponent<CoinSound>();
		inOrbit = false;

		playerGravity = planet.GetComponent<PlayerGravity>();

		anim = GetComponent<Animator>();
		weaponManager = GetComponent<WeaponManager>();
		//groundCheckRadius = 1f;
	}
    // Update is called once per frame


    void Update()
    {
        if (canControl)
        {
            CheckRunning();
            CheckJump();
            HandleJetpack();
        }
        CheckShop();
        DetermineVectors();
        //groundCheck();
        //print(grounded);
        //moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f).normalized;//Input.GetAxisRaw("Vertical"),0f).normalized;
        //jumpDir = new Vector3(0f,Input.GetAxisRaw("Vertical"),0f).normalized;

        playerGravity.Attract(transform);
        inOrbit = true;

        SetAnims();
        if (Input.GetButtonUp("Fire1")) {
            coinSoundRef.StopWeaponSound();
        }
    }

	void FixedUpdate()
	{


		if(inOrbit&& canControl)
		{

			if(running && horMov > 0f)
			{
				//rb.MovePosition(rb.position + (transform.TransformDirection(moveDir) * runSpeed * Time.deltaTime));
				rb.AddForce(moveDir * horMov * runSpeed, ForceMode2D.Impulse);// * Input.GetAxis("Horizontal"));
			}
			else if(running && horMov <= 0f)
			{
				rb.AddForce(moveDir * horMov * runSpeed, ForceMode2D.Impulse);
			}

			else if(!running && horMov > 0f)
			{
				rb.AddForce(moveDir * horMov * moveSpeed, ForceMode2D.Impulse);

				//rb.MovePosition(rb.position + rb.transform.TransformDirection(moveDir) * horMov * moveSpeed * Time.deltaTime);
			}

			else if(!running && horMov <= 0f)
			{
				rb.AddForce(moveDir * horMov * moveSpeed, ForceMode2D.Impulse);
				//rb.MovePosition(rb.position + rb.transform.TransformDirection(moveDir) * horMov * moveSpeed * Time.deltaTime);
			}

			else if(jumping)
			{
				jump();
			}

			if(horMov > 0 && !facingRight)
			{
				Flip();
			}
			if(horMov < 0 && facingRight)
			{
				Flip();
			}
            if (grounded && !(horMov == 0))
            {
                coinSoundRef.makeWalkSound = true;

            }
            else
            {
                coinSoundRef.makeWalkSound = false;
            }
			//GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

		}

		else if(!inOrbit)
		{
			moveDir = new Vector2(Input.GetAxis("Horizontal"),0f);

			if(running)
			{

				rb.velocity = (rb.transform.TransformDirection(moveDir) * runSpeed * Time.deltaTime);
			}

			/*
			else if(!running)
			{
				rb.velocity = (rb.position + (rb.transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime)));
			}
			*/

		}

	}

    public void CheckShop()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            shop.enabled = !shop.enabled;
            //canControl = !shop.enabled;
						canControl = !canControl;
						weaponManager.canControl = canControl;
        }

    }

	/*
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}


	void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Orbit")
		{
			//collision.gameObject.transform.parent.GetComponent<PlayerGravity>().Attract(transform);

			playerGravity = collision.gameObject.transform.parent.GetComponent<PlayerGravity>();
			playerGravity.Attract(transform);
			inOrbit = true;

		}
		else
		{
			inOrbit = false;
		}
	}


	void OntriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}
	*/

	void CheckRunning()
	{
	//	if(Input.GetKey(KeyCode.LeftShift) && grounded)
		//{
		//	running = true;
        //    coinSoundRef.makeWalkSound = true;
		//}
		//else
		//{
		//	running = false;
        //    coinSoundRef.makeWalkSound = false;
        //}

		//Debug.Log("velocity vector: " + rb.velocity);
		//Debug.Log("velocity magnitude: " + rb.velocity.magnitude);
	}


	void CheckJump()
	{
		if(inOrbit && grounded)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				jumping = true;
				grounded = false;
				jump();
                coinSoundRef.PlayJump();
			}
		}
		else
		{
			jumping = false;
		}
	}

	void SetAnims()
	{
		anim.SetFloat("Speed", Mathf.Abs(horMov));
		if(jumping)
		{
			anim.SetFloat("Vspeed", 1f);
		}
		else if(!jumping && !grounded)
		{
			anim.SetFloat("Vspeed", -1f);
		}
		anim.SetBool("Grounded", grounded);

	}


	void DetermineVectors()
	{
		gravityVector = playerGravity.ForceDirection;
		moveDir = new Vector2(-gravityVector.y, gravityVector.x).normalized;
		jumpDir = new Vector2(0f, -gravityVector.y).normalized;//-gravityVector;
		horMov = Input.GetAxis("Horizontal");

		if(horMov <= 0)
		{
			moveDir = new Vector2(gravityVector.y, -gravityVector.x).normalized;
		}
		else
		{
			moveDir *= -1;
		}

		moveDir *= -1;
	}

	void jump()
	{
		rb.AddForce(-gravityVector * jumpForce, ForceMode2D.Force);//jumpDir.y * jumpForce), ForceMode2D.Force);
		jumping = false;
	}

	void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	/*
	void groundCheck()
	{
		if (Physics.CheckSphere(groundCheckbox.position, groundCheckRadius, whatIsGround))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}
	}
	*/
    void HandleJetpack()
    {
        if (grounded)
        {
            jetPackFuel = jetPackFuelMax;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            jetPackOn = true;
            coinSoundRef.StartJetSound();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            jetPackOn = false;
            coinSoundRef.StopJetSound();
        }

        if (jetPackOn)
        {
            if (jetPackFuel > 0)
            {
                jetPackFuel -= Time.deltaTime;
                rb.AddForce(-gravityVector * jetPackForce, ForceMode2D.Force);
                grounded = false;
            }

        }
    }
}
