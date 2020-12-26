using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class MapController : MonoBehaviour
{
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [SerializeField] private Grid mapGrid;
    [Range(0, 100)] public int randomFillPercent;
    public bool useRandomSeed;
    [SerializeField] private string seed;
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
                int index = 0;
                List<Vector3> tileWorldLocations = new List<Vector3>();

                foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
                    // loop through tiles in tilemap
                    Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                    Vector3 place = tilemap.CellToWorld(localPlace);
                    if (tilemap.HasTile(localPlace)) {
                        tileWorldLocations.Add(place);
                        //if (tilemap.name == "EnvironmentMap") {
                        //    TextMesh txt = UtilsClass.CreateWorldText(index++.ToString(), null, place, 15, Color.white, TextAnchor.MiddleCenter);
                        //    txt.transform.localScale += new Vector3 (-0.7f,-0.7f,-0.7f);
                        //}
                    }
                }
            }
            if (tilemap.name == "EnvironmentMap") {
                GenerateMap(tilemap);
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
        }
    }

    void GenerateMap(Tilemap tileMap)
    {
        mapMatrix = new int[mapWidth, mapHeight];
        RandomFillMap();    // fill the map randomly using seed

        for (int i = 0; i < 5; i++) {
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

    void SmoothMap()
    {
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    mapMatrix[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    mapMatrix[x, y] = 0;
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++) {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++) {
                if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight) {
                    if (neighbourX != gridX || neighbourY != gridY) {
                        wallCount += mapMatrix[neighbourX, neighbourY];
                    }
                } else {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
}
