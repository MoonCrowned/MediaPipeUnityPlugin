using System.Collections.Generic;
using Mediapipe;
using UnityEngine;

public interface IDrawableHandGraph
{
    void Draw(Transform screenTransform, List<NormalizedLandmarkList> handLandmarkLists, List<ClassificationList> handednesses,
        List<Detection> palmDetections, List<NormalizedRect> handRects, bool isFlipped = false);
}
