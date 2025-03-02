using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text scoreText;
    public static int score;

    private void Awake() => score = 0;

    private void Update() => scoreText.text = "Score: " + score;
}