using UnityEngine;
using System.Collections;

public class bodyControlTESTScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var newPosition = new Vector3(transform.position.x + horizontal, transform.position.y + vertical, 0);

        transform.position = newPosition;
	}
}
