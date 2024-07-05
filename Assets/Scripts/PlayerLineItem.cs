using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLineItem : MonoBehaviour
{
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text userNameText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject vipImage;
    [SerializeField] private GameObject selectedBorder;
    [SerializeField] private Image avatarBackgrpound;
    [SerializeField] private Image avatarImage;
    [SerializeField] private Image flagImage;
    [SerializeField] private Image badgeImage;

    [SerializeField] private List<Sprite> badgeSprites = new();

    public Action OnPress;

    public void Init(RankingEntry ranking, Sprite avatar, Sprite flag)
    {
        rankText.text = ranking.ranking.ToString();
        userNameText.text = ranking.player.username;
        pointsText.text = ranking.points.ToString("N0", new System.Globalization.CultureInfo("ru-RU"));

        vipImage.SetActive(ranking.player.isVip);

        if (ColorUtility.TryParseHtmlString(ranking.player.characterColor, out Color characterColor))
            avatarBackgrpound.color = characterColor;

        avatarImage.sprite = avatar;
        flagImage.sprite = flag;

        if(ranking.ranking <= badgeSprites.Count)
        {
            badgeImage.sprite = badgeSprites[ranking.ranking - 1];
            badgeImage.color = Color.white;
        }
        else
        {
            badgeImage.color = Color.clear;
        }
    }

    public void Press()
    {
        OnPress?.Invoke();
        selectedBorder.SetActive(true);
    }

    public void Unselect()
    {
        selectedBorder.SetActive(false);
    }
}
