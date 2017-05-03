using UnityEngine;
using System.Collections;

public class PlayerGravity : MonoBehaviour {

	private float G;
	private float Distance;
	public float PlanetMass;
	public float PlayerMass;

	private float force;

	public Vector2 ForceDirection;
	private Vector2 ForceVector;

	private Rigidbody2D rb;
	private Renderer PlanetRenderer;
	private GameObject Player;
	private Rigidbody2D PlayerRB;

	//public GameObject OrbitField;
	private Quaternion targetRotation;





	// Use this for initialization

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Player = GameObject.FindGameObjectWithTag("Player");

		G = 50f;
		PlanetMass = 100f;
		PlanetRenderer = GetComponent<Renderer>();
		PlayerRB = Player.GetComponent<Rigidbody2D>();
		PlayerMass = PlayerRB.mass;

	}
	void Start () {
	}




	// Update is called once per frame
	void Update () {

		//PlayerDistance();
		//DetermineGravity();
		//ApplyGravity();
		//print(ForceDirection + " forcedirection");
	}



	//apply gravity towards planets core F(g) = G(M1M2)/r2
	void DetermineGravity(Transform affectedObject)
	{
		ForceDirection = (this.transform.position - Player.transform.position).normalized;
		force = (G*PlanetMass* affectedObject.gameObject.GetComponent<Rigidbody2D>().mass)/(Distance* Distance);

		ForceVector = ForceDirection * force;

	}

	void ApplyGravity(Transform affectedObject)
	{
		affectedObject.gameObject.GetComponent<Rigidbody2D>().AddForce(ForceVector);//PlayerRB.AddForce(ForceVector);
	}

	void PlayerDistance()
	{
		Distance = 11f;//Vector2.Distance(PlanetRenderer.bounds.center, Player.transform.position);
		//Debug.Log("distancd: " + Distance);
	}

	void Rotate(Transform body)
	{
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;


		targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
	}

	public void Attract(Transform body)
	{
		PlayerDistance();
		DetermineGravity(body);
		ApplyGravity(body);
		Rotate(body);
	}

}
