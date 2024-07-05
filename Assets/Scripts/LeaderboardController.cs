using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private PropertiesData properties;
    [SerializeField] private PlayerLineItem playerLinePrefab;
    [SerializeField] private Transform playerLinesContainer;
    [SerializeField] private Animator leaderboardAnimator;
    [SerializeField] private UIAdaptation uiAdaptation;

    private string[] _fileNames;
    private int _currentFileNum;
    private readonly List<PlayerLineItem> _playerLines = new();

    const string LeaderBoardAnimationKey = "open";

    private void Start()
    {
        _fileNames = DataLoader.GetAllJsonFiles(properties.jsonFolderName);
    }

    public void OpenLeaderboard()
    {
        if (_fileNames.Length <= 0) return;

        var leaderboardData = DataLoader.GetLeaderboardData(properties.jsonFolderName, _fileNames[_currentFileNum]);

        _currentFileNum++;

        if (_currentFileNum >= _fileNames.Length)
        {
            _currentFileNum = 0;
        }

        UpdateLeaderboard(leaderboardData);
    }

    private void UpdateLeaderboard(LeaderboardData leaderboardData)
    {
        foreach (var line in _playerLines)
        {
            Destroy(line.gameObject);
        }

        _playerLines.Clear();

        foreach (var ranking in leaderboardData.ranking)
        {
            var playerLine = Instantiate(playerLinePrefab, playerLinesContainer);
            playerLine.Init(ranking, 
                DataLoader.GetSprite(properties.avatarFolderName, ((Avatars)ranking.player.characterIndex).ToString()),
                DataLoader.GetSprite(properties.flagFolderName, ranking.player.countryCode));
            playerLine.OnPress += OnPressPlayerLine;
            _playerLines.Add(playerLine);
        }

        leaderboardAnimator.SetBool(LeaderBoardAnimationKey, true);
        StartCoroutine(uiAdaptation.UIUpdate());
    }

    private void OnPressPlayerLine()
    {
        foreach (var line in _playerLines)
        {
            line.Unselect();
        }
    }

    public void CloseLeaderboard()
    {
        leaderboardAnimator.SetBool(LeaderBoardAnimationKey, false);
    }
}
