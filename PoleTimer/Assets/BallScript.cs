using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {

public Rigidbody rb;
public float thrustX = 3.0f;
public float thrustY = 10.0f;

public Text timer;
private double time;

public Text GroundReport;

public double winningTime;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log("Update time: " + Time.deltaTime);

		time += Time.deltaTime;
		timer.text = time.ToString();

		RaycastHit hitInfo;
		if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3.0f))	//Only report if at most 3 meters (3.0f) from ground
		{																																			//transform.position gets the center of the ball.***** Would need to change if want to account for bottom of ball.
			GroundReport.text = hitInfo.distance.ToString();
			//From slide 14 below till Debug.DrawRey.
			if (hitInfo.collider.tag == "Finish") {
				GroundReport.text = "HOLD...";
				winningTime += Time.deltaTime;
			} else {
				winningTime = 0.0;
			}
			if (winningTime > 5.0) {
				Time.timeScale = 0.0f; // "pauses" game
				// https://docs.unity3d.com/ScriptReference/Time-timeScale.html
				// https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
				if (PlayerPrefs.GetFloat ("besttime", 100f) > winningTime) {
					PlayerPrefs.SetFloat ("besttime", (float)winningTime);
					Debug.Log ("New best time: " +
					PlayerPrefs.GetFloat ("besttime", 0f));
				}
}


		}
		//Debug.DrawRay(eyeLine.position, Vector2.right, Color.magenta, 0.1f);  //******Figure out why eyeLine.position doesnt work.



	}

	void FixedUpdate()
	{
		//Debug.Log("FixedUpdate time: " + Time.deltaTime);
		rb.AddForce(Input.GetAxis("Horizontal") * thrustX, 0, 0);
		rb.AddForce(0, Input.GetAxis("Vertical") * thrustY, 0);


	}
}
