using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class MapController : MonoBehaviour
{
    // private vars:
    private int[,] mapMatrix;
    private Vector3Int prevTilePosition = new Vector3Int();
    private Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();

    [SerializeField] private string seed;
    [SerializeField] private bool useRandomSeed = true;
    [SerializeField] [Range(0, 100)] private int landFillPercent = 50;
    [SerializeField] private TileBase highlightTile;

    // public vars:
    public Grid mapGrid;        // Container that holds all tilemaps
    public int width;           // To help displaying map width in inspector
    public int height;          // To help displaying map height in inspector
    public int mapWidth { get; set; }
    public int mapHeight { get; set; }
    public List<TileBase> oceanTiles, landTiles, mountainTiles;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        mapWidth = width;
        mapHeight = height;
        Vector3Int worldCellPosition = mapGrid.WorldToCell(mapGrid.transform.position);
        foreach (var tilemap in mapGrid.GetComponentsInChildren<Tilemap>()) {
            // Loop through tilemaps in grid object:
            tilemaps.Add(tilemap.name,tilemap);      // Save tilemaps in an array
            Debug.Log(tilemap.name + " size: " + tilemap.size);
            if (tilemap.cellBounds.Contains(worldCellPosition)) {
                // If tilemap is not empty:

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap(tilemaps["EnvironmentMap"]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GenerateMap(tilemaps["EnvironmentMap"]);
            // DisplayMapCoord(tileMaps[0], Color.red);
        }
        // Highlighting the tile at mouse pos
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = tilemaps["HighlightMap"].WorldToCell(mouseWorldPos);
        if (tileCoordinate != prevTilePosition) {
            tilemaps["HighlightMap"].SetTile(prevTilePosition, null);
            tilemaps["HighlightMap"].SetTile(tileCoordinate, highlightTile);
            prevTilePosition = tileCoordinate;
            // Debug.Log("Mouse at " + tileCoordinate);
        }
    }

    /// <summary>
    /// Generate the map on a given tilemap
    /// </summary>
    /// <param name="envMap"></param>
    void GenerateMap(Tilemap envMap)
    {
        List<TileBase> tiles = new List<TileBase>();
        tiles.AddRange(oceanTiles);
        tiles.AddRange(landTiles);
        tiles.AddRange(mountainTiles);

        envMap.ClearAllTiles();                     // Clear all old tiles

        mapMatrix = new int[mapWidth, mapHeight];
        RandomFillMap(0, oceanTiles.Count, tiles);  // fill the map randomly using seed

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                // create random map base on map matrix
                Vector3Int pos = new Vector3Int(x, y, 1);
                envMap.SetTile(pos, tiles[mapMatrix[x, y]]);
            }
        }
    }

    /// <summary>
    /// Randomly generate not smoothed map
    /// </summary>
    /// <param name="defaultOcean"> List index of default ocean tile in tiles list </param>
    /// <param name="defaultLand"> List index of default land tile in tiles list </param>
    void RandomFillMap(int defaultOcean, int defaultLand, List<TileBase> tiles)
    {
        if (useRandomSeed) {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                // loop through all tiles in map
                mapMatrix[x, y] = (pseudoRandom.Next(0, 100) < landFillPercent) ? defaultLand : defaultOcean;
            }
        }
        SmoothMap(defaultOcean, defaultLand, tiles);
    }

    /// <summary>
    /// Smooth map base on some rules
    /// </summary>
    /// <param name="defaultOcean"> List index of default ocean tile in tiles list </param>
    /// <param name="defaultLand"> List index of default land tile in tiles list </param>
    void SmoothMap(int defaultOcean, int defaultLand, List<TileBase> tiles, int smoothTimes = 3)
    {
        for (int i = 0; i < smoothTimes; i++) {
            for (int x = 0; x < mapWidth; x++) {
                for (int y = 0; y < mapHeight; y++) {
                    int neibourDefaultLandTiles = CountTilesAround(x, y, defaultLand);
                    // smoothing rules:
                    if (neibourDefaultLandTiles > 4)
                        mapMatrix[x, y] = defaultLand;
                    else if (neibourDefaultLandTiles < 4)
                        mapMatrix[x, y] = defaultOcean;
                }
            }
        }
        for (int x = 0; x < mapWidth; x++) {
            for (int y = 0; y < mapHeight; y++) {
                if (mapMatrix[x, y] == defaultLand)
                    mapMatrix[x, y] = Random.Range(defaultLand, defaultLand + landTiles.Count);
                else if (mapMatrix[x, y] == defaultOcean)
                    mapMatrix[x, y] = Random.Range(defaultOcean, defaultOcean + oceanTiles.Count);
            }
        }
    }

    /// <summary>
    /// Count the number of indicated type of tiles surrounding the given tile position
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <param name="type"> type of tile (List index of the tile in tiles) </param>
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
                        count += mapMatrix[neighbourX, neighbourY] == type ? 1 : 0;     // count += num of tiles with given type
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
    void DisplayMapCoord(Tilemap map, Color color, int fontSize = 15)
    {
        foreach (var pos in map.cellBounds.allPositionsWithin) {
            // loop through tiles in tileMaps[0]
            List<Vector3> tileWorldLocations = new List<Vector3>();
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemaps["EnvironmentMap"].CellToWorld(localPlace);
            if (tilemaps["EnvironmentMap"].HasTile(localPlace)) {
                tileWorldLocations.Add(place);
                TextMesh txt = UtilsClass.CreateWorldText(pos.x.ToString() + ", " + pos.y.ToString(), map.transform,
                    place, fontSize, color, TextAnchor.MiddleCenter);
                txt.transform.localScale += new Vector3(-0.7f, -0.7f, -0.7f);
            }
        }
    }
}