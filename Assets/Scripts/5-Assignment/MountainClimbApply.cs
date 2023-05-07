using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MountainClimbApply : MonoBehaviour
{
    [SerializeField] TileBase mountainTile = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

            KeyboardMoverByTile mover = collision.gameObject.GetComponent<KeyboardMoverByTile>();
            if (mover != null)
            {
                mover.addInAllowedTiles(mountainTile);
            }
        }
    }
}