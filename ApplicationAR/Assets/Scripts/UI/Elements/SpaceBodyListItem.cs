using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceBodyListItem : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI bodyNameText;
    [SerializeField] private TextMeshProUGUI astronomerNameText;
    [SerializeField] private TextMeshProUGUI childrenAmountText;
    [SerializeField] private LayoutElement paddingElement;

    SpaceBody refSpaceBody;

    public void InstantiateItem(SpaceBody refBody)
    {
        refSpaceBody = refBody;
        bodyNameText.text = refSpaceBody.Name;
        selectButton.onClick.AddListener(OnSelect);
    }

    public void SetPaddingLeft(int amount)
    {
        paddingElement.preferredWidth = amount;
    }

    void OnSelect()
    {
        ObjectScreen objectScreen = ScreenManager.Instance.GetComponentInChildren<ObjectScreen>(true);
        objectScreen.Open(refSpaceBody);
    }
}
