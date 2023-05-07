using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour
{
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contain(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get()
    {
        return allowedTiles;
    }
    // function to add a specific tile
    public void AddTile(TileBase tile)
    {
        // Declare a new TileBase array with room for one extra element
        TileBase[] newAllowedTiles = new TileBase[allowedTiles.Length + 1];

        // Copy the existing tiles into the new array
        for (int i = 0; i < allowedTiles.Length; i++)
        {
            newAllowedTiles[i] = allowedTiles[i];
        }

        // Add the new tile to the end of the array
        newAllowedTiles[newAllowedTiles.Length - 1] = tile;

        // Replace the old array with the new one
        allowedTiles = newAllowedTiles;

        Debug.Log("Added tile to allowed tiles: " + tile);
    }
}
