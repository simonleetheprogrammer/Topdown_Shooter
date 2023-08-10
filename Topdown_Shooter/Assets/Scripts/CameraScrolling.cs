using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Currently, this code is strictly coupled with the sceneTilemap. Once the character goes to another screen, then the camera breaks.
/// Only relevant on maps that are big enough.
/// </summary>
public class CameraScrolling : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Tilemap sceneTilemap;

    private float cameraWidth;
    private float cameraHeight;
    private Vector3 minWorld;
    private Vector3 maxWorld;

    void Start()
    {
        // Trim off Empty Tiles From Edges
        sceneTilemap.CompressBounds();

        // Define Camera Dimensions
        Vector3 cameraDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        cameraWidth= cameraDimensions.x;
        cameraHeight= cameraDimensions.y;

        // Define World Size
        BoundsInt sceneTilemapBounds = sceneTilemap.cellBounds;

        Vector3Int minCell = sceneTilemapBounds.min;
        Vector3Int maxCell = sceneTilemapBounds.max;
        minWorld = sceneTilemap.CellToWorld(minCell);
        maxWorld = sceneTilemap.CellToWorld(maxCell);
    }

    void Update()
    {
        CameraFollowPlayer();
    }
    /// <summary>
    /// If player moves out of the bounds of the tilemap, the camera doesn't follow.
    /// </summary>
    private void CameraFollowPlayer()
    {
        if (playerTransform != null)
        {
            float xOffset = cameraWidth;
            float yOffset = cameraHeight;
            // Position is at player. But cannot go past bounds.
            Vector3 newCameraPosition = new Vector3(
                Mathf.Clamp(playerTransform.position.x, minWorld.x + xOffset, maxWorld.x - xOffset),
                Mathf.Clamp(playerTransform.position.y, minWorld.y + yOffset, maxWorld.y - yOffset),
                transform.position.z
                );
            //Vector3 newCameraPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

            transform.position = newCameraPosition;
        }
    }
}
