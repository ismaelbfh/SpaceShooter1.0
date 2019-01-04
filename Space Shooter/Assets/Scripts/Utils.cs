using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2 GetHalfDimensionsInWorldUnits(this Camera camera)
    {
        float width, height;
        
        float ratio = camera.pixelWidth / (float) camera.pixelHeight;
        height = camera.orthographicSize * 2;
        width = height * ratio;
        return new Vector2(width, height) / 2;
    }
}