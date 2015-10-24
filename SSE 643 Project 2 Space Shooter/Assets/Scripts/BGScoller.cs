using UnityEngine;
using System.Collections;

public class BGScoller : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;

	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}
