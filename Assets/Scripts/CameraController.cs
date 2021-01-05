using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private float zoomFactor = 3f;          // Zoom distance for each scroll
    private float maxX, maxY;               // Boundaries of camera position
    private bool toCenter = true;           // Set camera position to center
    private Vector3 mapCenter;              // Store the center world position of map
    private MapController map;              // Store the map controller

    public float zoomSpeed = 5f;            // The zooming speed of camera
    public float moveSpeed = 8f;            // The speed of camera movement
    public float maxCamSize = 10f;          // Maximum value of zooming
    public float minCamSize = 3f;           // Minimum value of zooming
    public Tilemap environmentMap;              // Environment Tilemap

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        map = GameObject.Find("MapController").GetComponent<MapController>();
        mapCenter = environmentMap.CellToWorld(new Vector3Int(map.mapWidth / 2, map.mapHeight / 2, 0))
            + new Vector3(0, 0, -10);        // -10 on z-axis to avoid the map out of the camera's view
        CalculateLimitation();
        maxCamSize = Camera.main.orthographicSize * Screen.height / Screen.width * 2.0f;
        Debug.LogWarning("MinCam: " + minCamSize + " MaxCam: " + maxCamSize);
    }

    // Update is called once per frame
    void Update()
    {
        // Set camera position to the center of the map
        if (toCenter) {
            transform.position = mapCenter;
            CalculateLimitation();              // Update the limitation for camera after map generated
            toCenter = false;
        }

        // Zoom control:
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scrollInput) > 0.01f) {
            Camera.main.orthographicSize -= scrollInput * zoomFactor;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCamSize, maxCamSize);
        }

        // Keyboard Movement
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        gameObject.transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime,
            verticalInput * moveSpeed * Time.deltaTime, 0);     // Add vector3 to camera position

        // Edge Scrolling
        if (Input.mousePosition.x > Screen.width - 10f)     // scroll right
            gameObject.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.x < 10f)                    // scroll left
            gameObject.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.y > Screen.height - 10f)    // scroll up
            gameObject.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        if (Input.mousePosition.y < 10f)                    // scroll down
            gameObject.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);

        // Clamping the position of camera
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, 0, maxX);
        pos.y = Mathf.Clamp(pos.y, 0, maxY);
        transform.position = pos;
    }

    /// <summary>
    /// Calculate the limitation of camera transform
    /// </summary>
    void CalculateLimitation()
    {
        maxX = environmentMap.CellToWorld(new Vector3Int(map.mapWidth, map.mapHeight, 0)).x;
        maxY = environmentMap.CellToWorld(new Vector3Int(map.mapWidth, map.mapHeight, 0)).y;
        Debug.LogWarning("maxX: " + maxX + " maxY: " + maxY);
    }
}
