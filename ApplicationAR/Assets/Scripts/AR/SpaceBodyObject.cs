using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBodyObject : MonoBehaviour
{
    [SerializeField] Transform visualsTransform;
    [SerializeField] Transform bodyTransform;
    [SerializeField] GameObject childrenHolderObject;
    [SerializeField] LineRenderer orbitLineRender;

    SpaceBody refSpaceBody;

    public void OnInitialize(SpaceBody sourceSpaceBody)
    {
        refSpaceBody = sourceSpaceBody;
        SetVisuals();
    }

    void SetVisuals()
    {
        ObjectMappingObject mapper = SpaceBodyManager.Instance.GetMapperObjectByType(refSpaceBody.Type);

        Renderer renderer = bodyTransform.GetComponent<Renderer>();
        Material bodyTransformMaterial = Instantiate(mapper.material);
        renderer.material = bodyTransformMaterial;

        Texture2D randomMapperTex = mapper.textures[Random.Range(0, mapper.textures.Count)];
        bodyTransformMaterial.mainTexture = randomMapperTex;
    }

    public void SetOrbitLineRenderer(Vector3 parentBasePos)
    {
        List<Vector3> linePositions = new List<Vector3>();
        float lineRendererScale = refSpaceBody.Mass / 50;
        orbitLineRender.startWidth = lineRendererScale;
        orbitLineRender.endWidth = lineRendererScale;

        int lineDetail = 36;
        orbitLineRender.positionCount = lineDetail + 1;

        for(int i = 0; i < lineDetail + 1; i++)
        {
            Vector3 parentOffsetVec = transform.position - parentBasePos;
            float rotateAngle = ((360 / lineDetail) * i) * Mathf.Deg2Rad;
            float rotatedVecX = (parentOffsetVec.x * Mathf.Cos(rotateAngle)) - (parentOffsetVec.z * Mathf.Sin(rotateAngle));
            float rotatedVecY = (parentOffsetVec.x * Mathf.Sin(rotateAngle)) + (parentOffsetVec.z * Mathf.Cos(rotateAngle));
            Vector3 rotatedOffsetVec = new Vector3(rotatedVecX, i == 0 ? 0.0000001f : 0, rotatedVecY);
            Vector3 linePosWorld = parentBasePos + rotatedOffsetVec;
            Vector3 linePosLocal = orbitLineRender.transform.InverseTransformPoint(linePosWorld);
            linePositions.Add(linePosLocal);
        }
        orbitLineRender.SetPositions(linePositions.ToArray());
    }

    public Transform GetVisualsTransform() => visualsTransform;
    public LineRenderer GetLineRenderer() => orbitLineRender;
    public GameObject GetChildrenHolder() => childrenHolderObject;
}
