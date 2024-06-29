using System;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static event System.EventHandler LevelUp;

    public delegate void EventHandler(EventArgs e);

    public AsteroidSpawner Spawner;
    public Player Player;

    public TMP_Text ScoreLabel;
    public GameObject CountDown;
    public TMP_Text CountDownText;
    public LevelUpMenu LevelUpMenu;
    public GameObject GameOverButton;

    public Tuple<int, int> ScoreToLevelUp = new Tuple<int, int>(5, 8);
    public int Score = 0;
    public int Level = 1;

    public Timer countDownTimer = new Timer(1000);
    public int CountDownSeconds = 3;

    private void Start()
    {
        Asteroid.AsteroidDestroyed += HandleAsteroidDestroyed;
        ItemCard.PlayerItemSelected += HandlePlayerItemSelected;
        Player.PlayerDestroyed += HandlePlayerDestroyed;

        countDownTimer.Elapsed += (sender, e) => ProcessCountdown();

        UpdateScore(0);
    }

    private void OnDestroy()
    {
        // Need to unsubscribe as well otherwise we get null references on re-load
        // Could also be avoided by not using static handlers.
        Asteroid.AsteroidDestroyed -= HandleAsteroidDestroyed;
        ItemCard.PlayerItemSelected -= HandlePlayerItemSelected;
        Player.PlayerDestroyed -= HandlePlayerDestroyed;
    }

    private void Update()
    {
        if (countDownTimer.Enabled)
        {
            CountDownText.text = CountDownSeconds.ToString();
        }
        else if (!countDownTimer.Enabled && CountDown.activeSelf)
        {
            CountDownText.text = CountDownSeconds.ToString();
            CountDown.SetActive(false);
            ResumeGame();
        }
    }

    private void ProcessCountdown()
    {
        CountDownSeconds--;

        if (CountDownSeconds <= 0)
        {
            countDownTimer.Stop();
            CountDownSeconds = 3;
        }
    }

    private void HandleAsteroidDestroyed(object sender, AsteroidDestroyedEventArgs e) =>
        UpdateScore(e.Score);

    private void HandlePlayerItemSelected(object sender, PlayerItemSelectedEventArgs e)
    {
        LevelUpMenu.ShowMenu(false);;
        CountDown.SetActive(true);
        countDownTimer.Start();
    }

    public void HandleGameOverClicked() =>
        SceneManager.LoadScene(0);

    public void HandlePlayerDestroyed(object sender, EventArgs e)
    {
        GameOverButton.SetActive(true);
        Destroy(Player);
        // Could consider pausing here, but asteroids moving around still could be cool?
    }


    private void UpdateScore(int scoreIncrease)
    {
        Score += scoreIncrease;
        ScoreLabel.text = $"{Score} / {ScoreToLevelUp.Item2}";

        if (Score >= ScoreToLevelUp.Item2)
        {
            ProcessLevelUp();
        }
    }

    private void ProcessLevelUp()
    {
        var nextLevel = ScoreToLevelUp.Item1 + ScoreToLevelUp.Item2;

        ScoreToLevelUp = new Tuple<int, int>(ScoreToLevelUp.Item2, nextLevel);

        Level++;

        var levelUp = LevelUp;

        if (levelUp != null)
        {
            PauseGame();
            levelUp(this, new EventArgs());
            LevelUpMenu.ShowMenu(true);
        }
    }

    private void PauseGame() =>
        Time.timeScale = 0;

    private void ResumeGame() =>
        Time.timeScale = 1;
}
