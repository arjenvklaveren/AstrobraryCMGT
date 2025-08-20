using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TestSetup : MonoBehaviour
{
    string fakeObject = @"{ 
        ""id"": 7,
        ""name"": ""Earth"",
        ""discoveryDate"": ""2000-01-01"",
        ""discovererId"": 4,
        ""age"": 299,
        ""imageUrl"": ""https://cdn.britannica.com/27/152027-050-ED6C422A/Vesta-asteriod-image-spacecraft-Dawn-July-24-2011.jpg"",
        ""parentId"": null,
        ""type"": 3,
        ""mainInfo"": ""Its a cool rock"",
        ""subInfo"": ""Its very cool"",
        ""mass"": 1000,
        ""luminosity"": 2000,
        ""diameter"": 3000,
        ""velocity"": 4000,
        ""temperature"": 300,
        ""distanceFromParent"": 0,
        ""rotationAngle"": 45,
        ""rotationSpeed"": 43,
        ""atmosphereThickness"": 88,
        ""mainColorHex"": ""#000000"",
        ""subColorHex"": ""#000000"",
        ""ringSystem"": {
            ""id"": 1,
            ""ringDistance"": 50,
            ""ringSize"": 100,
            ""ringMainColorHex"": ""#000000"",
            ""ringSubColorHex"": ""#000000"",
            ""ringDetailColorHex"": ""#000000"",
            ""spaceBodyId"": 7
        },
        ""children"": [
            {
                ""id"": 11,
                ""name"": ""Moon"",
                ""discoveryDate"": ""2002-01-01"",
                ""discovererId"": 4,
                ""age"": 50,
                ""imageUrl"": ""https://cdn.britannica.com/27/152027-050-ED6C422A/Vesta-asteriod-image-spacecraft-Dawn-July-24-2011.jpg"",
                ""parentId"": 7,
                ""type"": 7,
                ""mainInfo"": ""Just a cool moon"",
                ""subInfo"": ""Moon do be cool"",
                ""mass"": 500,
                ""luminosity"": 400,
                ""diameter"": 320,
                ""velocity"": 400,
                ""temperature"": 41,
                ""distanceFromParent"": 200,
                ""rotationAngle"": 90,
                ""rotationSpeed"": 12,
                ""atmosphereThickness"": 44,
                ""mainColorHex"": ""#000000"",
                ""subColorHex"": ""#000000"",
                ""ringSystem"": null,
                ""children"": [
                    {
                        ""id"": 13,
                        ""name"": ""Submoon"",
                        ""discoveryDate"": ""2003-01-01"",
                        ""discovererId"": 4,
                        ""age"": 40,
                        ""imageUrl"": ""https://cdn.britannica.com/27/152027-050-ED6C422A/Vesta-asteriod-image-spacecraft-Dawn-July-24-2011.jpg"",
                        ""parentId"": 11,
                        ""type"": 8,
                        ""mainInfo"": ""Cool child moon"",
                        ""subInfo"": ""How cool he is"",
                        ""mass"": 200,
                        ""luminosity"": 500,
                        ""diameter"": 160,
                        ""velocity"": 200,
                        ""temperature"": 40,
                        ""distanceFromParent"": 100,
                        ""rotationAngle"": 0,
                        ""rotationSpeed"": 40,
                        ""atmosphereThickness"": 11,
                        ""mainColorHex"": ""#000000"",
                        ""subColorHex"": ""#000000"",
                        ""ringSystem"": null,
                        ""children"": []
                    }
                ]
            },
            {
                ""id"": 14,
                ""name"": ""Coolname"",
                ""discoveryDate"": ""2000-01-01"",
                ""discovererId"": 10,
                ""age"": 0,
                ""imageUrl"": null,
                ""parentId"": 7,
                ""type"": 7,
                ""mainInfo"": """",
                ""subInfo"": """",
                ""mass"": 0,
                ""luminosity"": 0,
                ""diameter"": 0,
                ""velocity"": 0,
                ""temperature"": 0,
                ""distanceFromParent"": 0,
                ""rotationAngle"": 0,
                ""rotationSpeed"": 0,
                ""atmosphereThickness"": 0,
                ""mainColorHex"": ""#000000"",
                ""subColorHex"": ""#000000"",
                ""ringSystem"": null,
                ""children"": []
            }
        ]
    }";

    private void Start()
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        SpaceBody spaceBody = JsonConvert.DeserializeObject<SpaceBody>(fakeObject, settings);
        SpaceBodyManager.Instance.SetupScene(spaceBody, spaceBody);
    }
}
