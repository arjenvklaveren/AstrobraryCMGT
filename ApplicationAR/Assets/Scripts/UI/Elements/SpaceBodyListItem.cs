using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceBodyListItem : MonoBehaviour
{
    [SerializeField] private Button selectButton;
    [SerializeField] private Image spaceBodyImage;
    [SerializeField] private TextMeshProUGUI bodyNameText;
    [SerializeField] private TextMeshProUGUI astronomerNameText;
    [SerializeField] private TextMeshProUGUI childrenAmountText;
    [SerializeField] private LayoutElement paddingElement;

    SpaceBody refSpaceBody;
    bool isSelectorAR;

    public void InstantiateItem(SpaceBody refBody, bool isSelectorAR = false)
    {
        refSpaceBody = refBody;
        bodyNameText.text = refSpaceBody.Name;
        this.isSelectorAR = isSelectorAR;
        if (refBody.ImageTexture != null)
        {
            Texture2D tex = refBody.ImageTexture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            spaceBodyImage.sprite = sprite;
        }
        selectButton.onClick.AddListener(OnSelect);
    }

    public void SetPaddingLeft(int amount)
    {
        paddingElement.preferredWidth = amount;
    }

    void OnSelect()
    {
        if (!isSelectorAR)
        {
            ObjectScreen objectScreen = ScreenManager.Instance.GetMainCanvasGameObject().GetComponentInChildren<ObjectScreen>(true);
            objectScreen.Open(refSpaceBody);
        }
        else
        {
            OverlayScreen overlayScreen = ScreenManager.Instance.GetMainCanvasGameObject().GetComponentInChildren<OverlayScreen>(true);
            overlayScreen.DisableHierarchy();
            SpaceBodyManager.Instance.SelectSpaceBody(refSpaceBody);
        }
    }
}
