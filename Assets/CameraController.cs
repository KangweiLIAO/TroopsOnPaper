using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float camSize = 7;
    [SerializeField] private float zoomFactor = 3f;

    public float zoomSpeed = 5f;
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        camera = (Camera)gameObject.GetComponent("Camera");
        camSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Zoom control:
        if (camSize <= 15) {
            // Limit the camera size within 0 to 15
            if (camSize < 3) camSize = 3;
            else {
                float scrollInput;
                scrollInput = Input.GetAxis("Mouse ScrollWheel");
                camSize -= scrollInput * zoomFactor;
            }
        } else camSize = 15;
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camSize, Time.deltaTime * zoomSpeed);     // Smooth zooming of camera

        // Keyboard Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        gameObject.transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime,
            verticalInput * moveSpeed * Time.deltaTime, 0);     // Add vector3 to camera position

        // Edge Scrolling
        if (Input.mousePosition.x > Screen.width - 10f)
            gameObject.transform.position += new Vector3(moveSpeed *Time.deltaTime,0,0);
        if (Input.mousePosition.x < 10f) gameObject.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.y > Screen.height - 10f)
            gameObject.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        if (Input.mousePosition.y < 10f) gameObject.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
