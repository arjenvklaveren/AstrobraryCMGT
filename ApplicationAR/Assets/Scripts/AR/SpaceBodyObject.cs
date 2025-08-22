using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBodyObject : MonoBehaviour
{
    [SerializeField] Transform visualsTransform;
    [SerializeField] Transform bodyTransform;
    [SerializeField] GameObject childrenHolder;
    [SerializeField] GameObject orbitLinesHolder;

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

    public Transform GetVisualsTransform() => visualsTransform;
    public GameObject GetOrbitLineRenderersHolder() => orbitLinesHolder;
    public GameObject GetChildrenHolder() => childrenHolder;
}
