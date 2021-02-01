using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HandTrackingFilterController : MonoBehaviour
{

    [SerializeField] GameObject graphToHandle = null;
    SceneDirector sceneDirector;

    public Text filterStatusText;

    // Start is called before the first frame update
    void Start()
    {
        sceneDirector = FindObjectOfType<SceneDirector>();
        if( sceneDirector != null )
        {

            // Включаем дефолтную камеру
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Permission.RequestUserPermission(Permission.Camera);
            yield return new WaitForSeconds(0.1f);
        }
#elif UNITY_IOS
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        }
#endif

#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera)) {
            Debug.LogWarning("Not permitted to use Camera");
            yield break;
        }
#elif UNITY_IOS
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
            Debug.LogWarning("Not permitted to use WebCam");
            yield break;
        }
#endif

            sceneDirector.ChangeWebCamDevice(WebCamTexture.devices[0]);

            // запускаем граф
            sceneDirector.ChangeGraph(Instantiate(graphToHandle));

            SetFilterStatus(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            SetFilterStatus(!filter);
        }
    }


    bool filter = false;
    void SetFilterStatus(bool status)
    {
        filter = status;
        var filters = FindObjectsOfType<MonoBehaviour>().OfType<IFilterableAnnotation>();
        foreach (IFilterableAnnotation f in filters)
        {
            f.FilterStatus = filter;
        }
        filterStatusText.text = "Filter: " + (filter ? "ON" : "OFF");
    }



}
