using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListScreen : ScreenUI
{
    [SerializeField] private GameObject listContentHolder; 
    [SerializeField] private SpaceBodyListItem spaceBodyItemPrefab;

    private List<SpaceBody> spaceBodies;

    #region Open / Close
    protected async override void OnOpen(object data = null)
    {
        if (spaceBodies == null)
        {
            spaceBodies = await HttpController.Instance.GetSpaceBodies();
            InstantiateSpaceBodyElements();
        }
    }

    protected override void OnClose()
    {

    }
    #endregion


    void InstantiateSpaceBodyElements()
    {
        foreach(SpaceBody body in spaceBodies)
        {
            SpaceBodyListItem newSpaceBodyListItem = Instantiate(spaceBodyItemPrefab, listContentHolder.transform);
            newSpaceBodyListItem.InstantiateItem(body);
        }
    }
}
