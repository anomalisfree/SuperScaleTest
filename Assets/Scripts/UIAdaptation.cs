using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIAdaptation : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private float minBackgroundAspectRatio;
    [SerializeField] private float minLeaderboardAspectRatio;
    [SerializeField] private RectTransform leaderboardTransform;
    [SerializeField] private RectTransform container;

    const int StandardBorder = 40;
    const int AdditionalLeaderboardHeight = 270;

    private int _lastScreenWidth;
    private int _lastScreenHeight;

    void Update()
    {
        if (_lastScreenWidth != Screen.width || _lastScreenHeight != Screen.height)
        {
            StartCoroutine(UIUpdate());

            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
        }
    }

    public IEnumerator UIUpdate()
    {
        yield return null;

        container.anchoredPosition = Vector3.zero;

        var backgroundTransform = background.GetComponent<RectTransform>();

        if (Screen.width / (float)Screen.height > minBackgroundAspectRatio)
        {
            backgroundTransform.sizeDelta = new Vector2(background.sprite.rect.width * (Screen.height / background.sprite.rect.height), Screen.height);
        }
        else
        {
            backgroundTransform.sizeDelta = new Vector2(background.sprite.rect.width * (Screen.width / minBackgroundAspectRatio / background.sprite.rect.height), Screen.width / minBackgroundAspectRatio);
        }

        var leaderboardWidth = Screen.width - StandardBorder;

        float leaderboardHeight = container.sizeDelta.y + AdditionalLeaderboardHeight;

        if (leaderboardHeight > Screen.height + StandardBorder)
        {
            leaderboardHeight = Screen.height - StandardBorder;
        }

        if (leaderboardWidth / (float)leaderboardHeight > minLeaderboardAspectRatio)
        {
            leaderboardWidth = (int)(leaderboardHeight * minLeaderboardAspectRatio);
        }

        leaderboardTransform.sizeDelta = new Vector2(leaderboardWidth, leaderboardHeight);
    }
}
