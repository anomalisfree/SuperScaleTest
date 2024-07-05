using UnityEngine;
using UnityEngine.UI;

public class UIAdaptation : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private float minBackgroundAspectRatio;
    [SerializeField] private float minLeaderboardAspectRatio;
    [SerializeField] private RectTransform leaderboardTransform;
    [SerializeField] private RectTransform container;

    void Start()
    {

    }

    void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        var backgroundTransform = background.GetComponent<RectTransform>();

        if (Screen.width / (float)Screen.height > minBackgroundAspectRatio)
        {
            backgroundTransform.sizeDelta = new Vector2(background.sprite.rect.width * (Screen.height / background.sprite.rect.height), Screen.height);
        }
        else
        {
            backgroundTransform.sizeDelta = new Vector2(background.sprite.rect.width * (Screen.width / minBackgroundAspectRatio / background.sprite.rect.height), Screen.width / minBackgroundAspectRatio);
        }

        var leaderboardWidth = Screen.width - 40;

        float leaderboardHeight = container.sizeDelta.y + 270;

        if (leaderboardHeight > Screen.height + 40)
        {
            leaderboardHeight = Screen.height - 40;
        }

        if (leaderboardWidth / (float)leaderboardHeight > minLeaderboardAspectRatio)
        {
            leaderboardWidth = (int)(leaderboardHeight * minLeaderboardAspectRatio);
        }

        leaderboardTransform.sizeDelta = new Vector2(leaderboardWidth, leaderboardHeight);
    }
}
