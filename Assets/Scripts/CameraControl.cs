using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	[SerializeField] private BuilderMode _builderMode;
	public float speed = 5;
	public float zoomSpeed = 5;

	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;

	public Transform startPoint;
	public int rotationX = 70;
	public float maxHeight = 15;
	public float minHeight = 5;
	public int rotationLimit = 240;

	private float camRotation;
	private float height;
	private float tmpHeight;
	private float h, v;
	private bool L, R, U, D;

	void Start()
	{
		height = (maxHeight + minHeight) / 2;
		tmpHeight = height;
		camRotation = rotationLimit / 2;
		transform.position = new Vector3(startPoint.position.x, height, startPoint.position.z);
	}

	public void CursorTriggerEnter(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = true;
				break;
			case "R":
				R = true;
				break;
			case "U":
				U = true;
				break;
			case "D":
				D = true;
				break;
		}
	}

	public void CursorTriggerExit(string triggerName)
	{
		switch (triggerName)
		{
			case "L":
				L = false;
				break;
			case "R":
				R = false;
				break;
			case "U":
				U = false;
				break;
			case "D":
				D = false;
				break;
		}
	}

	void Update()
	{
		if (Input.GetKey(left) || L) h = -1; else if (Input.GetKey(right) || R) h = 1; else h = 0;
		if (Input.GetKey(down) || D) v = -1; else if (Input.GetKey(up) || U) v = 1; else v = 0;

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (height < maxHeight) tmpHeight += zoomSpeed;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (height > minHeight) tmpHeight -= zoomSpeed;
		}

		tmpHeight = Mathf.Clamp(tmpHeight, minHeight, maxHeight);
		height = Mathf.Lerp(height, tmpHeight, 3 * Time.deltaTime);

		Vector3 direction = new Vector3(h, v, 0);
		transform.Translate(direction * speed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, height, transform.position.z);
		transform.rotation = Quaternion.Euler(rotationX, camRotation, 0);

		if(Input.GetMouseButtonDown(0))
        {
			_builderMode.ConstructBlock();
        }
		else if(Input.GetMouseButtonDown(1))
        {
			_builderMode.DestroyBlock();
		}

		if(Input.GetKeyDown(KeyCode.E))
        {
			_builderMode.Rotate(0, 90, 0);
        }
		if (Input.GetKeyDown(KeyCode.Q))
		{
			_builderMode.Rotate(0, -90, 0);
		}
		if (Input.GetKeyDown(KeyCode.PageDown))
		{
			_builderMode.Rotate(0, 0, 90);
		}
		if (Input.GetKeyDown(KeyCode.PageUp))
		{
			_builderMode.Rotate(0, 0, -90);
		}
		if (Input.GetKeyDown(KeyCode.Home))
		{
			_builderMode.Rotate(90, 0, 0);
		}
		if (Input.GetKeyDown(KeyCode.End))
		{
			_builderMode.Rotate(-90, 0, 0);
		}
		if(Input.GetKeyDown(KeyCode.R))
        {
			_builderMode.ChangeBuilding();
        }
	}
}