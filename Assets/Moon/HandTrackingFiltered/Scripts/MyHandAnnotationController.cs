using Mediapipe;
using System.Collections.Generic;
using UnityEngine;

public class MyHandAnnotationController : AnnotationController, IDrawableHandGraph, IFilterableAnnotation
{
    [SerializeField] GameObject handLandmarkListsPrefab = null;

    private GameObject handLandmarkListsAnnotation;

    bool filter;
    public bool FilterStatus { get { return filter; } set { filter = value; } }

    void Awake()
    {
        handLandmarkListsAnnotation = Instantiate(handLandmarkListsPrefab);
    }

    void OnDestroy()
    {
        Destroy(handLandmarkListsAnnotation);
    }

    public override void Clear()
    {
        handLandmarkListsAnnotation.GetComponent<MultiHandLandmarkListAnnotationController>().Clear();
    }

    public void Draw(Transform screenTransform, List<NormalizedLandmarkList> handLandmarkLists, List<ClassificationList> handednesses,
        List<Detection> palmDetections, List<NormalizedRect> handRects, bool isFlipped = false)
    {
        //Debug.Log(handLandmarkLists.Count);
        /*if (filter)
        {

        }
        else*/
        {
            handLandmarkListsAnnotation.GetComponent<MultiHandLandmarkListAnnotationController>().Draw(screenTransform, handLandmarkLists, isFlipped, filter);
        }
    }
}
