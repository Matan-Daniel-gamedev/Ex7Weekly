using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileVisibilityManager : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] TileBase mountainTile = null;
    [SerializeField] Tilemap tilemap = null;

    // Dictionary to store the original tiles of each position
    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();

    void Update()
    {
        // Get the tile that the player is standing on
        Vector3 playerPosition = player.transform.position;
        TileBase playerTile = GetTileAtWorldPosition(playerPosition);
        if (playerTile != mountainTile)
        {
            // Check visibility for all other tiles in the tilemap
            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                Vector3 tilePosition = tilemap.CellToWorld(position);
                TileBase tile = tilemap.GetTile(position);
                if (tile != mountainTile)
                {
                    // Check if there's a mountainTile between the player and this tile
                    if (IsTileVisible(playerPosition, tilePosition))
                    {
                        // Enable the sprite tile image
                        if (originalTiles.ContainsKey(position))
                        {
                            tilemap.SetTile(position, originalTiles[position]);
                            //tilemap.SwapTile(tile, originalTiles[position]);
                        }
                    }
                    else
                    {
                        // Disable the sprite tile image
                        if (!originalTiles.ContainsKey(position))
                        {
                            originalTiles[position] = tile;
                        }
                        //tilemap.SetColor(position, Color.black);
                        tilemap.SetTile(position, null);
                    }
                }
            }
        }
        else
        {
            // Check visibility for all other tiles in the tilemap
            foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
            {
                // Enable the sprite tile image
                if (originalTiles.ContainsKey(position))
                {
                    tilemap.SetTile(position, originalTiles[position]);
                    //tilemap.SwapTile(tile, originalTiles[position]);
                }
            }
        }
    }

    bool IsTileVisible(Vector3 playerPosition, Vector3 tilePosition)
    {
        // Get the positions of all the tiles between the player and the tile
        Vector3Int playerCell = tilemap.WorldToCell(playerPosition);
        Vector3Int tileCell = tilemap.WorldToCell(tilePosition);
        Vector3Int[] positions = GetCellsOnLine(playerCell, tileCell);

        // Check each tile for mountainTile
        foreach (Vector3Int position in positions)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile == mountainTile)
            {
                return false; // Mountain tile blocks the view
            }
        }
        return true; // No mountain tile between the player and the tile
    }


    TileBase GetTileAtWorldPosition(Vector3 position)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(position);
        return tilemap.GetTile(cellPosition);
    }

    Vector3Int[] GetCellsOnLine(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> positions = new List<Vector3Int>(); // A list to hold the cell positions on the line

        int x = start.x; // Starting x position
        int y = start.y; // Starting y position
        int dx = end.x - start.x; // Delta x
        int dy = end.y - start.y; // Delta y
        int xi = dx > 0 ? 1 : -1; // Step direction for x
        int yi = dy > 0 ? 1 : -1; // Step direction for y
        int d = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy)); // Maximum steps needed

        // Calculate step sizes for x and y
        float xStep = dx != 0 ? xi * Mathf.Abs(dx) / d : 0;
        float yStep = dy != 0 ? yi * Mathf.Abs(dy) / d : 0;

        // Loop through the steps
        for (int i = 0; i < d; i++)
        {
            Vector3Int position = new Vector3Int(x, y, start.z); // Create a new cell position
            positions.Add(position); // Add the position to the list

            float ratio = (float)i / (float)d; // Calculate the current progress along the line
            x = start.x + Mathf.RoundToInt(ratio * dx); // Interpolate x coordinate
            y = start.y + Mathf.RoundToInt(ratio * dy); // Interpolate y coordinate
        }

        positions.Add(end); // Add the end position to the list

        return positions.ToArray(); // Return the positions as an array
    }
}