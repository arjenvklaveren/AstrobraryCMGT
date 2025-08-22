using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpaceBodyManager : MonoBehaviour
{
    public static SpaceBodyManager Instance { get; private set; }

    [SerializeField] float scaleDivider = 100;

    [SerializeField] private SpaceBodyObject spaceBodyPrefab;
    [SerializeField] private LineRenderer lineRendererPrefab;
    [SerializeField] private GameObject spaceBodyObjectHolder;
    [SerializeField] private SpaceBodyObjectMapper objectMapper;

    private Dictionary<SpaceBody, SpaceBodyObject> spaceBodyObjects = new Dictionary<SpaceBody, SpaceBodyObject>();
    private SpaceBodyObject rootSpaceBodyObject;
    private SpaceBodyObject selectedSpaceBody;

    bool isSimulating = true;

    #region Singleton instantiation
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    #region Spacebody simulation

    private void FixedUpdate()
    {
        if(isSimulating) SimulateSpaceBodies();
    }

    void SimulateSpaceBodies()
    {
        MoveSpadeBodies();
        RotateSpadeBodies();
    }

    void MoveSpadeBodies()
    {
        foreach (KeyValuePair<SpaceBody, SpaceBodyObject> spaceBody in spaceBodyObjects)
        {
            if (selectedSpaceBody != null && (spaceBody.Value == selectedSpaceBody || !selectedSpaceBody.GetComponentsInChildren<SpaceBodyObject>().Contains(spaceBody.Value))) continue;

            KeyValuePair<SpaceBody, SpaceBodyObject> parentObject = spaceBodyObjects.FirstOrDefault(sb => sb.Key.Id == spaceBody.Key.ParentId);
            if (parentObject.Key == null || parentObject.Value == null) continue;

            Vector3 parentOffsetVec = spaceBody.Value.transform.position - parentObject.Value.transform.position;
            float rotateAngle = spaceBody.Key.RotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
            float rotatedVecX = (parentOffsetVec.x * Mathf.Cos(rotateAngle)) - (parentOffsetVec.z * Mathf.Sin(rotateAngle));
            float rotatedVecY = (parentOffsetVec.x * Mathf.Sin(rotateAngle)) + (parentOffsetVec.z * Mathf.Cos(rotateAngle));
            Vector3 rotatedOffsetVec = new Vector3(rotatedVecX, 0, rotatedVecY);
            spaceBody.Value.transform.position = parentObject.Value.transform.position + rotatedOffsetVec;
        }
    }

    void RotateSpadeBodies()
    {
        foreach(KeyValuePair<SpaceBody, SpaceBodyObject> spaceBody in spaceBodyObjects)
        {
            spaceBody.Value.GetVisualsTransform().Rotate(new Vector3(0, 1, 0) * (spaceBody.Key.RotationSpeed / 10));
            spaceBody.Value.GetOrbitLineRenderersHolder().transform.localRotation = Quaternion.Inverse(spaceBody.Value.GetVisualsTransform().rotation);
        }
    }

    #endregion

    #region Object creation / deletion
    void GenerateSpaceBodyObjects(SpaceBody spaceBody, SpaceBodyObject parentObject)
    {
        var parentTransform = parentObject == null ? spaceBodyObjectHolder.transform : parentObject.GetChildrenHolder().transform;
        SpaceBodyObject spaceBodyObject = Instantiate(spaceBodyPrefab, parentTransform);
        if (parentObject == null) rootSpaceBodyObject = spaceBodyObject;
        spaceBodyObjects.Add(spaceBody, spaceBodyObject);

        spaceBodyObject.transform.name = spaceBody.Name;
        spaceBodyObject.transform.localPosition = Vector3.right * (spaceBody.DistanceFromParent / (scaleDivider / 20));
        spaceBodyObject.GetVisualsTransform().localScale = Vector3.one * (spaceBody.Mass / scaleDivider);
        float rotation = spaceBody.RotationAngle - (Mathf.FloorToInt(spaceBody.RotationAngle / 360) * 360);
        spaceBodyObject.GetVisualsTransform().transform.Rotate(0, 0, rotation);

        spaceBodyObject.OnInitialize(spaceBody);

        //Instantiate orbit lines
        if (parentObject != null)
        {
            LineRenderer orbitline = GetOrbitLineRenderer(parentObject, spaceBodyObject);
            orbitline.startWidth = (float)spaceBody.Mass / 35000.0f;
            orbitline.endWidth = (float)spaceBody.Mass / 35000.0f;
            float parentVisualScale = parentObject.GetVisualsTransform().transform.localScale.x;
            parentObject.GetOrbitLineRenderersHolder().transform.localScale = new Vector3(1.0f / parentVisualScale, 1.0f / parentVisualScale, 1.0f / parentVisualScale);
        }

        foreach (SpaceBody child in spaceBody.Children)
        {
            GenerateSpaceBodyObjects(child, spaceBodyObject);
        }
    }

    LineRenderer GetOrbitLineRenderer(SpaceBodyObject parent, SpaceBodyObject child)
    {
        LineRenderer lineRenderer = Instantiate(lineRendererPrefab, parent.GetOrbitLineRenderersHolder().transform);

        List<Vector3> linePositions = new List<Vector3>();
        int lineDetail = 36;
        lineRenderer.positionCount = lineDetail + 1;

        for (int i = 0; i < lineDetail + 1; i++)
        {
            Vector3 parentOffsetVec = parent.transform.position - child.transform.position;
            float rotateAngle = ((360 / lineDetail) * i) * Mathf.Deg2Rad;
            float rotatedVecX = (parentOffsetVec.x * Mathf.Cos(rotateAngle)) - (parentOffsetVec.z * Mathf.Sin(rotateAngle));
            float rotatedVecY = (parentOffsetVec.x * Mathf.Sin(rotateAngle)) + (parentOffsetVec.z * Mathf.Cos(rotateAngle));
            Vector3 rotatedOffsetVec = new Vector3(rotatedVecX, i == 0 ? 0.0000001f : 0, rotatedVecY);
            Vector3 linePos = rotatedOffsetVec;
            linePositions.Add(linePos);
        }
        lineRenderer.SetPositions(linePositions.ToArray());
        return lineRenderer;
    }

    void DestroySpaceBodyObjects()
    {
        foreach (Transform child in spaceBodyObjectHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region Public methods
    public void SetupScene(SpaceBody hierarchyRootSpaceBody, SpaceBody selectedSpaceBody = null)
    {
        isSimulating = true;
        ClearUpScene();
        GenerateSpaceBodyObjects(hierarchyRootSpaceBody, null);
        rootSpaceBodyObject.gameObject.SetActive(false);
        SelectSpaceBody(selectedSpaceBody);
        AugmentedRealityManager.Instance.SetupAR(spaceBodyObjects);
    }

    public void ClearUpScene()
    {
        spaceBodyObjects.Clear();
        DestroySpaceBodyObjects();
    }

    public void SelectSpaceBody(SpaceBody spaceBody)
    {
        SpaceBodyObject selectedSpaceBodyObject = spaceBodyObjects.First(x => x.Key.Id == spaceBody.Id).Value;
        if(selectedSpaceBodyObject != null) SelectSpaceBody(selectedSpaceBodyObject);
    }

    public void SelectSpaceBody(SpaceBodyObject spaceBodyObject)
    {
        selectedSpaceBody = spaceBodyObject;

        if (!AugmentedRealityManager.Instance.IsPlaneTracking()) return;

        rootSpaceBodyObject.gameObject.SetActive(true);

        foreach (KeyValuePair<SpaceBody, SpaceBodyObject> sb in spaceBodyObjects)
        {
            if (sb.Value != selectedSpaceBody) sb.Value.GetVisualsTransform().gameObject.SetActive(false);
        }
        foreach (SpaceBodyObject sb in selectedSpaceBody.GetComponentsInChildren<SpaceBodyObject>())
        {
            sb.GetVisualsTransform().gameObject.SetActive(true);
        }

        Vector3 rootPos = AugmentedRealityManager.Instance.GetPlaneTrackingPos();
        rootSpaceBodyObject.transform.position = rootPos;
        Vector3 diffVec = selectedSpaceBody.transform.position - rootPos;
        rootSpaceBodyObject.transform.position -= diffVec;

        selectedSpaceBody.GetVisualsTransform().gameObject.SetActive(true);
    }

    public void SetRootObjectScale(float amount)
    {
        rootSpaceBodyObject.transform.localScale = new Vector3(amount, amount, amount);
    }

    public void SetSimulationState(bool state)
    {
        isSimulating = state;
    }

    public void SetOrbitLineRendersActive(bool state)
    {
        foreach (KeyValuePair<SpaceBody, SpaceBodyObject> spaceBody in spaceBodyObjects)
        {
            spaceBody.Value.GetOrbitLineRenderersHolder().SetActive(state);
        }
    }

    public ObjectMappingObject GetMapperObjectByType(SpaceBodyType type)
    {
        return objectMapper.mapperObjects.Find((x) => x.type == type);
    }

    public SpaceBodyObject GetRootSpaceBodyObject() => rootSpaceBodyObject;
    public SpaceBodyObject GetSelectedSpaceBodyObject() => selectedSpaceBody;
    #endregion
}
