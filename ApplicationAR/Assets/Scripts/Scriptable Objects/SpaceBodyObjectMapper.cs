using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceBodyObjectMapper", menuName = "ScriptableObjects/SpaceBodyObjectMapper", order = 1)]
public class SpaceBodyObjectMapper : ScriptableObject
{
    public List<ObjectMappingObject> mapperObjects = new List<ObjectMappingObject>();
}

[System.Serializable]
public struct ObjectMappingObject
{
    public SpaceBodyType type;
    public List<Texture2D> textures;
    public Material material;
}