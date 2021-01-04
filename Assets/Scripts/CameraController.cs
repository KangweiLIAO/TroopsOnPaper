using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private float camSize = 7f;
    private float zoomFactor = 3f;
    private bool toCenter = true;
    private Vector3 mapCenter;
    private MapController map;

    public float zoomSpeed = 5f;        // The zooming speed of camera
    public float moveSpeed = 8f;        // The speed of camera movement
    public float maxCamSize = 10f;      // Maximum value of zooming
    public float minCamSize = 3f;       // Minimum value of zooming
    public Tilemap envTileMap;          // Environment Tilemap

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        camera = (Camera)gameObject.GetComponent("Camera");
        camSize = camera.orthographicSize;  
        map = GameObject.Find("MapController").GetComponent<MapController>();
        mapCenter = envTileMap.CellToWorld(new Vector3Int((int)map.getWidth() / 2, (int)map.getHeight() / 2, 0)) 
            + new Vector3 (0,0,-10);        // -10 on z-axis to avoid the map out of the camera's view
    }

    // Update is called once per frame
    void Update()
    {
        // Set camera position to the center of the map
        if (toCenter) {
            transform.position = mapCenter;
            Debug.LogWarning("W:" + map.getWidth() + " H:" + map.getHeight());
            toCenter = false;
        }

        // Zoom control:
        if (camSize <= maxCamSize) {
            // Limit the camera size within maxCamSize to minCamSize
            if (camSize < minCamSize) camSize = minCamSize;
            else {
                float scrollInput;
                scrollInput = Input.GetAxis("Mouse ScrollWheel");
                camSize -= scrollInput * zoomFactor;
            }
        } else camSize = maxCamSize;
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camSize, Time.deltaTime * zoomSpeed);     // Smooth zooming of camera

        Vector3Int camCellPosition = envTileMap.WorldToCell(transform.position);            // Convert camera transform to cell position
        //Debug.Log("X:" + camCellPosition.x + " Y:" + camCellPosition.y);
        bool checkWidth = (camCellPosition.x > map.getWidth() || camCellPosition.x < 0);    // True if camera outside the width of map
        bool checkHeight = (camCellPosition.y > map.getHeight() || camCellPosition.y < 0);  // True if camera outside the height of map

        // Keyboard Movement
        // TODO: Limit camera area
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        gameObject.transform.position += new Vector3(horizontalInput * moveSpeed * Time.deltaTime,
            verticalInput * moveSpeed * Time.deltaTime, 0);     // Add vector3 to camera position

        // Edge Scrolling
        // TODO: Improve the camera area limiting
        if (Input.mousePosition.x > Screen.width - 10f && !checkWidth)
            gameObject.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.x < 10f && !checkWidth) gameObject.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.y > Screen.height - 10f && !checkHeight)
            gameObject.transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        if (Input.mousePosition.y < 10f && !checkHeight) gameObject.transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
