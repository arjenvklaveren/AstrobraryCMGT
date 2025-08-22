using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AugmentedRealityManager : MonoBehaviour
{
    public static AugmentedRealityManager Instance { get; private set; }

    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] ARRaycastManager raycastManagerAR;
    [SerializeField] ARPlaneManager planeManagerAR;

    Dictionary<SpaceBody, SpaceBodyObject> spaceBodies;
    Dictionary<string, SpaceBodyObject> prefabImageObjects = new Dictionary<string, SpaceBodyObject>();

    bool isTrackingOnPlane;
    Vector2 planeTrackingPos;

    public Action OnSelectPlane;

    #region Singleton instantiation + event initiation
    private void Awake()
    {
        trackedImageManager.enabled = false;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    #endregion

    private void Update()
    {
        CheckTouchInput();
    }

    public void SetupAR(Dictionary<SpaceBody, SpaceBodyObject> spaceBodies)
    {
        ClearAR();
        this.spaceBodies = spaceBodies;
        SetImageLibrary();
    }

    void ClearAR()
    {
        planeManagerAR.requestedDetectionMode = PlaneDetectionMode.Horizontal;
        isTrackingOnPlane = false;
        prefabImageObjects.Clear();
    }

    #region AR image tracking
    void SetImageLibrary()
    {
        trackedImageManager.enabled = true;
        var runtimeLibrary = trackedImageManager.CreateRuntimeLibrary();
        if (runtimeLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            trackedImageManager.referenceLibrary = mutableLibrary;
            trackedImageManager.enabled = true;
            foreach (KeyValuePair<SpaceBody, SpaceBodyObject> spaceBody in spaceBodies)
            {
                if (spaceBody.Key.ImageTexture == null) continue;
                float imageScale = 0.20f;
                string imageName = spaceBody.Key.Name + "_" + spaceBody.Key.Id.ToString();

                byte[] pngData = spaceBody.Key.ImageTexture.EncodeToPNG();
                Texture2D newTex = new Texture2D(spaceBody.Key.ImageTexture.width, spaceBody.Key.ImageTexture.height, TextureFormat.RGBA32, false);
                newTex.LoadImage(pngData);

                var jobHandle = mutableLibrary.ScheduleAddImageWithValidationJob(newTex, imageName, imageScale);
                jobHandle.jobHandle.Complete();
                var referenceImage = mutableLibrary[mutableLibrary.count - 1];
                prefabImageObjects.Add(imageName, spaceBody.Value);
            }
            trackedImageManager.referenceLibrary = mutableLibrary;
        }
        trackedImageManager.enabled = true;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            continue;


            if (trackedImage.referenceImage.name == null || prefabImageObjects.Count == 0) continue;
            if (!prefabImageObjects.TryGetValue(trackedImage.referenceImage.name, out var prefabImageObject)) continue;
            SpaceBodyObject spaceObject = Instantiate(prefabImageObject, trackedImage.transform);
            spaceObject.gameObject.SetActive(true);
            spaceObject.transform.localPosition = new Vector3(0, 0.25f, 0);
        }

        foreach(var trackedImage in eventArgs.updated)
        {
            if(trackedImage.trackingState == TrackingState.None)
            {
                foreach(Transform child in trackedImage.gameObject.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
    #endregion


    #region AR plane tracking

    void CheckTouchInput()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            if(Input.touchCount > 0)
            {
                SetRootSpaceBodyPos(Input.GetTouch(0).position);
            }
            else
            {
                SetRootSpaceBodyPos(Input.mousePosition);
            }
        }
    }

    void SetRootSpaceBodyPos(Vector2 touchPos)
    {
        if (isTrackingOnPlane) return;

        var rayHits = new List<ARRaycastHit>();
        raycastManagerAR.Raycast(touchPos, rayHits, TrackableType.PlaneWithinPolygon);
        if(rayHits.Count > 0)
        {
            isTrackingOnPlane = true;
            Vector3 hitPosePosition = rayHits[0].pose.position + new Vector3(0, 1f, 0);
            planeTrackingPos = hitPosePosition;

            foreach (var plane in planeManagerAR.trackables)
            {
                plane.gameObject.SetActive(false);
            }
            planeManagerAR.requestedDetectionMode = PlaneDetectionMode.None;

            OnSelectPlane.Invoke();
            SpaceBodyManager.Instance.SelectSpaceBody(SpaceBodyManager.Instance.GetSelectedSpaceBodyObject());
        }
    }

    public bool IsPlaneTracking() => isTrackingOnPlane;
    public Vector3 GetPlaneTrackingPos() => planeTrackingPos;

    #endregion
}
