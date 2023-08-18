using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Camera follows player around while not veering offscreen.
/// This code is strictly coupled with the sceneTilemap.
/// 
/// BUG: any change in screensize will be uhh bad.
/// BUG: I created the game based off of MY resolution. So yeah... There'll be consequences
/// </summary>
public class CameraScrolling : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private Tilemap sceneTilemap;

    private float cameraWidth;
    private float cameraHeight;

    private Vector3 minSceneTilemapBounds;
    private Vector3 maxSceneTilemapBounds;

    // Max and min bounds of the camera
    private float cameraMinX;
    private float cameraMaxX;
    private float cameraMinY;
    private float cameraMaxY;

    /// <summary>
    /// Gets map and camera dimensions
    /// Disables if there's no player, tilemap, or if the map too small. 
    /// </summary>
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        if (playerTransform is null || sceneTilemap is null) 
        { 
            enabled = false; 
        }
        else
        {
            // Trim off Empty Tiles From Edges
            sceneTilemap.CompressBounds();

            // Define Camera Dimensions
            Vector3 cameraDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            cameraWidth = cameraDimensions.x;
            cameraHeight = cameraDimensions.y;

            // Define TileMap Size
            BoundsInt sceneTilemapBounds = sceneTilemap.cellBounds;
            Vector3Int minCell = sceneTilemapBounds.min;
            Vector3Int maxCell = sceneTilemapBounds.max;
            minSceneTilemapBounds = sceneTilemap.CellToWorld(minCell);
            maxSceneTilemapBounds = sceneTilemap.CellToWorld(maxCell);

            ToggleCameraHorizonalVerticalMovements();
        }
    }
    /// <summary>
    /// Disable horizontal and/or vertical scrolling if map too small.
    /// </summary>
    private void ToggleCameraHorizonalVerticalMovements()
    {
        //print($"MinWorld X: {minWorld.x} MaxWorld X: {maxWorld.x}");
        //print($"MinWorld Y: {minWorld.y} MaxWorld Y: {maxWorld.y}");
        //print($"CamWidth: {cameraWidth} CamHeight: {cameraHeight}");

        float TilemapWidth = Mathf.Abs(minSceneTilemapBounds.x - maxSceneTilemapBounds.x) / 2;
        float TilemapHeight = Mathf.Abs(minSceneTilemapBounds.y - maxSceneTilemapBounds.y) / 2;

        if (cameraWidth >= TilemapWidth && cameraHeight >= TilemapHeight)
        {
            print("CAMERASCROLL: map too small, disabled");
            enabled = false;
        }
        else
        {
            float XOffset = cameraWidth * 0.75f; 
            if (cameraWidth < TilemapWidth)
            {
                // Enabled: Bounds of camera
                cameraMinX = minSceneTilemapBounds.x + XOffset;
                cameraMaxX = maxSceneTilemapBounds.x - XOffset;
            }
            else
            {
                print("CAMERASCROLL: world X too small. X scroll disabled");
                float CameraStartPositionX = minSceneTilemapBounds.x + maxSceneTilemapBounds.x;
                cameraMinX = CameraStartPositionX;
                cameraMaxX = CameraStartPositionX;
            }

            float YOffset = cameraHeight * 0.75f;
            if (cameraHeight < TilemapHeight)
            {
                // Bounds of camera
                cameraMinY = minSceneTilemapBounds.y + YOffset;
                cameraMaxY = maxSceneTilemapBounds.y + YOffset;
            }
            else
            {
                print("CAMERASCROLL: world Y too small. Y scroll disabled");
                float CameraStartPositionY = minSceneTilemapBounds.y + maxSceneTilemapBounds.y;
                cameraMinY = CameraStartPositionY;
                cameraMaxY = CameraStartPositionY;
            }
        }
    }

    void Update()
    {
        if (playerTransform!= null)
        {
            CameraFollowPlayer();
        }
    }
    /// <summary>
    /// If player moves out of the bounds of the tilemap, the camera doesn't follow.
    /// </summary>
    private void CameraFollowPlayer()
    {
            // Position is at player. But cannot go past bounds.
            Vector3 newCameraPosition = new Vector3(
                Mathf.Clamp(playerTransform.position.x, cameraMinX, cameraMaxX),
                Mathf.Clamp(playerTransform.position.y, cameraMinY, cameraMaxY),
                transform.position.z
                );
            //Vector3 newCameraPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

            transform.position = newCameraPosition;
    }
}
