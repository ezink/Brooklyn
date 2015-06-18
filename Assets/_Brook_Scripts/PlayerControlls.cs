using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class PlayerControlls : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody PlayerRigidbody;

	public Transform GunFireingPoint;
	public GameObject shot;
	public float fireRate;
	private float nextFire;


	void Awake(){
		PlayerRigidbody = GetComponent<Rigidbody> ();
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, GunFireingPoint.position, GunFireingPoint.rotation);
		}
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		PlayerRigidbody.velocity = movement * speed;

		GetComponent<Rigidbody>().position = new Vector3
			(Mathf.Clamp (PlayerRigidbody.position.x,boundary.xMin, boundary.xMax),
			 Mathf.Clamp (PlayerRigidbody.position.y, boundary.yMin, boundary.yMax),
			 0.0f
		);
		PlayerRigidbody.rotation = Quaternion.Euler (0.0f, 0.0f,PlayerRigidbody.velocity.y * -tilt);
	}
}
