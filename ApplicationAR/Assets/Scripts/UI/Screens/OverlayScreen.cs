using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayScreen : ScreenUI
{
    [SerializeField] private GameObject hierarchylistContentHolder;
    [SerializeField] private SpaceBodyListItem spaceBodyItemMiniPrefab;

    protected override void OnOpen(object data = null)
    {

    }

    protected override void OnClose()
    {
        SpaceBodyManager.Instance.ClearUpScene();
    }


    void InstantiateHierarchyElements(List<SpaceBody> children, int depth)
    {
        foreach (SpaceBody child in children)
        {
            SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemMiniPrefab, hierarchylistContentHolder.transform);
            newSpaceBodyListItem.InstantiateItem(child);
            newSpaceBodyListItem.SetPaddingLeft(100 * depth);
            InstantiateHierarchyElements(child.Children, depth + 1);
        }
    }
}
