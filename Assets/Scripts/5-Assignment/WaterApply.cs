using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterApply : MonoBehaviour
{
    [SerializeField] TileBase[] waterTiles = null;

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

            KeyboardMoverByTile mover = collision.gameObject.GetComponent<KeyboardMoverByTile>();
            if (mover != null)
            {
                for (int i = 0; i < waterTiles.Length; i++)
                {
                    mover.addInAllowedTiles(waterTiles[i]);
                }
            }
        }
    }
}
