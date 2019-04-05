
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public float panSpeed = 20f;
	public float panBoarderThickness = 10f;
	public Vector2 panLimit;

	public float scrollSpeed = 20f;
	public float minY=30;
	public float maxY = 180;



    // Update is called once per frame
    void Update()
    {

		Vector3 pos = transform.position;

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
		{
			pos.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
		{
			pos.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
		{
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
		{
			pos.x -= panSpeed * Time.deltaTime;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y -= scroll * 5000f * Time.deltaTime;


		pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
		pos.y = Mathf.Clamp(pos.y, minY, maxY);
		pos.z = Mathf.Clamp(pos.z, 20, panLimit.y);



		transform.position = pos;

    }
}
