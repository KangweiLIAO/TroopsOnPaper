using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class MapController : MonoBehaviour
{
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [Range(0, 100)] public int randomFillPercent = 50;
    [SerializeField] private Grid mapGrid;

    public string seed;
    public bool useRandomSeed = true;
    public List<TileBase> tiles = new List<TileBase>();

    private List<Tilemap> tileMaps = new List<Tilemap>();
    private int[,] mapMatrix;

    // Awake is called when the script is loaded
    void Awake()
    {
        Debug.Log(mapGrid.transform.position);
        Vector3Int worldCellPosition = mapGrid.WorldToCell(mapGrid.transform.position);
        foreach (var tilemap in mapGrid.GetComponentsInChildren<Tilemap>()) {
            // loop through tilemaps in grid
            tileMaps.Add(tilemap);
            Debug.Log(tilemap.name + " size: " + tilemap.size);
            if (tilemap.cellBounds.Contains(worldCellPosition)) {
                // if tilemap is not empty
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap(tileMaps[0]);
            DisplayMapCoord(tileMaps[0], Color.red);
        }
    }

    /// <summary>
    /// Generate the map on a given tilemap
    /// </summary>
    /// <param name="tileMap"></param>
    void GenerateMap(Tilemap tileMap)
    {
        mapMatrix = new int[mapWidth, mapHeight];
        RandomFillMap();    // fill the map randomly using seed

        for (int i = 0; i < 3; i++) {
            SmoothMap();
        }

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                // create random map base on map matrix
                Vector3Int pos = new Vector3Int(x,y,1);
                if (mapMatrix[x, y] == 0) {
                    tileMap.SetTile(pos, tiles[0]);
                } else if (mapMatrix[x, y] == 1) {
                    tileMap.SetTile(pos, tiles[1]);
                } else {
                    tileMap.SetTile(pos, tiles[2]);
                }
            }
        }
    }

    /// <summary>
    /// Randomly fill the map with some tiles 
    /// </summary>
    void RandomFillMap()
    {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                // loop through all tiles in map
                if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1) {
                    // set the edge to 0
                    mapMatrix[x, y] = 0;
                } else {
                    // if random < threshold then it's a wall, else it's blank
                    mapMatrix[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 0 : 1;
                }
            }
        }
    }

    /// <summary>
    /// Smooth map using some rules
    /// </summary>
    void SmoothMap()
    {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                int neibourDefaultTiles = CountTilesAround(x, y, 1);
                // smoothing rules: 
                if (neibourDefaultTiles > 4)
                    mapMatrix[x, y] = 1;
                else if (neibourDefaultTiles < 4)
                    mapMatrix[x, y] = 0;
            }
        }
    }

    /// <summary>
    /// Count the number of indicated type of tiles surrounding the given tile position
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    int CountTilesAround(int gridX, int gridY, int type)
    {
        // How many tiles around this tile are walls
        int count = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                // loop through the tiles around the tile at (gridX,gridY)
                if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight) {
                    // If (gridX,gridY) in the map
                    if (neighbourX != gridX || neighbourY != gridY) {
                        // if not looking at given tile position
                        count += mapMatrix[neighbourX, neighbourY]==type ? 1 : 0;     // count += tile index (i.e. 0,1,2...)
                    }
                } else {
                    // if looking outside/edge of the map
                    count++;
                }
            }
        }
        return count;
    }

    /// <summary>
    /// Display coordinates on given tilemap for each tile
    /// </summary>
    /// <param name="map"></param>
    /// <param name="fontSize"></param>
    /// <param name="color"></param>
    void DisplayMapCoord(Tilemap map, Color color, int fontSize= 15)
    {
        foreach (var pos in map.cellBounds.allPositionsWithin) {
            // loop through tiles in tileMaps[0]
            List<Vector3> tileWorldLocations = new List<Vector3>();
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tileMaps[0].CellToWorld(localPlace);
            if (tileMaps[0].HasTile(localPlace)) {
                tileWorldLocations.Add(place);
                TextMesh txt = UtilsClass.CreateWorldText(pos.x.ToString() + ", " + pos.y.ToString(), map.transform, 
                    place, fontSize, color, TextAnchor.MiddleCenter);
                txt.transform.localScale += new Vector3(-0.7f, -0.7f, -0.7f);
            }
        }
    }
}
