using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MountainCarveApply : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] GameObject player = null;
    [SerializeField] TileBase mountainTile = null;
    [SerializeField] TileBase grassTile = null;
    bool canCarve = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (canCarve)
            {
                // Get the player's position in the tilemap
                Vector3Int playerTilePosition = tilemap.WorldToCell(player.transform.position);

                // Get the tile at the player's position
                TileBase tile = tilemap.GetTile(playerTilePosition);

                // Check if the tile is the mountain tile
                if (tile == mountainTile)
                {
                    // Set the tile at the player's position to the grass tile
                    tilemap.SetTile(playerTilePosition, grassTile);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Get the SpriteRenderer component attached to the same GameObject as this component
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            // Destroy the SpriteRenderer component
            if (spriteRenderer != null)
            {
                Destroy(spriteRenderer);
            }

            canCarve = true;
        }
    }
}
