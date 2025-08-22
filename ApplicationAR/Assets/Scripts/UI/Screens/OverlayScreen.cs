using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayScreen : ScreenUI
{
    [SerializeField] private CanvasGroup contentCanvasGroup;
    [SerializeField] private GameObject hierarchyMainHolder;
    [SerializeField] private GameObject hierarchylistContentHolder;
    [SerializeField] private SpaceBodyListItem spaceBodyItemMiniPrefab;

    SpaceBody refSpaceBody;

    protected override void OnOpen(object data = null)
    {
        if (data != null && data is SpaceBody body)
        {
            refSpaceBody = body;
        }
        if (refSpaceBody != null) SetContent();

        contentCanvasGroup.alpha = 0;
        contentCanvasGroup.interactable = false;
        AugmentedRealityManager.Instance.OnSelectPlane += EnableControls;
    }

    protected override void OnClose()
    {
        SpaceBodyManager.Instance.ClearUpScene();
    }

    async void SetContent()
    {
        SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemMiniPrefab, hierarchylistContentHolder.transform);
        await HttpController.Instance.LoadSpaceBodyImage(refSpaceBody);
        newSpaceBodyListItem.InstantiateItem(refSpaceBody, true);
        InstantiateHierarchyElements(refSpaceBody.Children, 1);
    }

    async void InstantiateHierarchyElements(List<SpaceBody> children, int depth)
    {
        foreach (SpaceBody child in children)
        {
            SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemMiniPrefab, hierarchylistContentHolder.transform);
            await HttpController.Instance.LoadSpaceBodyImage(child);
            newSpaceBodyListItem.InstantiateItem(child, true);
            newSpaceBodyListItem.SetPaddingLeft(75 * depth);
            InstantiateHierarchyElements(child.Children, depth + 1);
        }
    }

    void EnableControls()
    {
        contentCanvasGroup.alpha = 1;
        contentCanvasGroup.interactable = true;
    }

    public void DisableHierarchy() { hierarchyMainHolder.SetActive(false); }
}
