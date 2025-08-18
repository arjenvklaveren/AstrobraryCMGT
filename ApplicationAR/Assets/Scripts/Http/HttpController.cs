using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class HttpController : MonoBehaviour
{
    public static HttpController Instance { get; private set; }

    private string baseUrl = "http://localhost:5025/api/";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public async Task<List<SpaceBody>> GetSpaceBodies()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl + "spacebody"))
        {
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                return null;
            }
            else
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                List<SpaceBody> spaceBodies = JsonConvert.DeserializeObject<List<SpaceBody>>(webRequest.downloadHandler.text, settings);
                return spaceBodies;
            }
        }
    }

    public async Task<SpaceBody> GetHierarchyOfSpaceBody(SpaceBody spaceBody)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl + "spacebody/" + spaceBody.Id.ToString() + "/hierarchy"))
        {
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                return null;
            }
            else
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                SpaceBody hierarchySpaceBody = JsonConvert.DeserializeObject<SpaceBody>(webRequest.downloadHandler.text, settings);
                return hierarchySpaceBody;
            }
        }
    }
}
