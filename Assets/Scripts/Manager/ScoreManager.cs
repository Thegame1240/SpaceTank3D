using System;
using UnityEngine;
using Utilities;

namespace Manager
{
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public int Score = 0;

        public event Action ScoreUpdated;

        public void Init()
        {
            Reset();
        }

        public void AddScore(int score)
        {
            Score += score;
            ScoreUpdated?.Invoke();
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}
