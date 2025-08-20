using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpaceBodyManager : MonoBehaviour
{
    public static SpaceBodyManager Instance { get; private set; }

    [SerializeField] float speedMag = 0.1f;

    [SerializeField] private SpaceBodyObject spaceBodyPrefab;
    [SerializeField] private GameObject spaceBodyObjectHolder;
    [SerializeField] private SpaceBodyObjectMapper objectMapper;

    private SpaceBody hierarchyRootSpaceBody;
    private Dictionary<SpaceBody, SpaceBodyObject> spaceBodyObjects = new Dictionary<SpaceBody, SpaceBodyObject>();
    private SpaceBodyObject selectedSpaceBody;

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
        SimulateSpaceBodies();
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
            //spaceBody.Value.GetLineRenderer().transform.localRotation = Quaternion.Inverse(spaceBody.Value.GetVisualsTransform().rotation);
        }
    }

    #endregion

    #region Object creation / deletion
    void GenerateSpaceBodyObjects(SpaceBody spaceBody, GameObject parentObject)
    {
        SpaceBodyObject spaceBodyObject = Instantiate(spaceBodyPrefab, parentObject.transform);
        spaceBodyObjects.Add(spaceBody, spaceBodyObject);

        spaceBodyObject.transform.name = spaceBody.Name;
        spaceBodyObject.transform.localPosition = Vector3.right * (spaceBody.DistanceFromParent);
        spaceBodyObject.GetVisualsTransform().localScale = Vector3.one * (spaceBody.Mass / 10);
        float rotation = spaceBody.RotationAngle - (Mathf.FloorToInt(spaceBody.RotationAngle / 360) * 360);
        spaceBodyObject.GetVisualsTransform().transform.Rotate(0, 0, rotation);

        spaceBodyObject.OnInitialize(spaceBody);
        spaceBodyObject.SetOrbitLineRenderer(parentObject.transform.position);

        foreach (SpaceBody child in spaceBody.Children)
        {
            GenerateSpaceBodyObjects(child, spaceBodyObject.GetChildrenHolder());
        }
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
        ClearUpScene();
        GenerateSpaceBodyObjects(hierarchyRootSpaceBody, spaceBodyObjectHolder);
    }

    public void ClearUpScene()
    {
        spaceBodyObjects.Clear();
        DestroySpaceBodyObjects();
    }

    public ObjectMappingObject GetMapperObjectByType(SpaceBodyType type)
    {
        return objectMapper.mapperObjects.Find((x) => x.type == type);
    }

    public SpaceBody GetHierarchyRootSpaceBody() => hierarchyRootSpaceBody;
    #endregion
}
