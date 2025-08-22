using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectScreen : ScreenUI
{
    [SerializeField] private SpaceBodyListItem spaceBodyItemSmallPrefab;
    [SerializeField] private SetInformationAreaValues setInformationAreaValues;

    [SerializeField] TextMeshProUGUI objectNameText;
    [SerializeField] Image objectBodyImage;

    [SerializeField] Button selectButton;

    [SerializeField] private GameObject informationAreasHolder;
    [SerializeField] private GameObject hierarchylistContentHolder;

    SpaceBody refSpaceBody;
    SpaceBody hierarchySpaceBody;

    #region Open / Close
    protected override void OnOpen(object data = null)
    {
        if (data != null && data is SpaceBody body)
        {
            refSpaceBody = body;
        }
        if (refSpaceBody != null) SetContent();
        selectButton.onClick.AddListener(OnSelectObject);
    }

    protected override void OnClose()
    {
        ClearListContent();
    }
    #endregion

    async void SetContent()
    {
        objectNameText.text = refSpaceBody.Name;
        hierarchySpaceBody = await HttpController.Instance.GetHierarchyOfSpaceBody(refSpaceBody);

        setInformationAreaValues.SetSpaceBodyValues(refSpaceBody);
        //setInformationAreaValues.SetAstronomerValues();

        if(refSpaceBody.ImageTexture != null)
        {
            Texture2D tex = refSpaceBody.ImageTexture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            objectBodyImage.sprite = sprite;
        }

        SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemSmallPrefab, hierarchylistContentHolder.transform);
        await HttpController.Instance.LoadSpaceBodyImage(hierarchySpaceBody);
        newSpaceBodyListItem.InstantiateItem(hierarchySpaceBody);
        InstantiateHierarchyElements(hierarchySpaceBody.Children, 1);
    }

    void OnSelectObject()
    {
        OverlayScreen overlayScreen = ScreenManager.Instance.GetMainCanvasGameObject().GetComponentInChildren<OverlayScreen>(true);
        SpaceBodyManager.Instance.SetupScene(hierarchySpaceBody, refSpaceBody);
        overlayScreen.Open(hierarchySpaceBody);
    }

    void ClearListContent()
    {
        foreach (Transform child in hierarchylistContentHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetAllInformationAreasInactive()
    {
        foreach (Transform child in informationAreasHolder.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    async void InstantiateHierarchyElements(List<SpaceBody> children, int depth)
    {
        foreach(SpaceBody child in children)
        {
            SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemSmallPrefab, hierarchylistContentHolder.transform);
            await HttpController.Instance.LoadSpaceBodyImage(child);
            newSpaceBodyListItem.InstantiateItem(child);
            newSpaceBodyListItem.SetPaddingLeft(100 * depth);
            InstantiateHierarchyElements(child.Children, depth + 1);
        }
    }
}
