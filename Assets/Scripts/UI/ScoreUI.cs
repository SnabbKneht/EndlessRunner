using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // cached references

    private TextMeshProUGUI Text { get; set; }

    private void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Text.text = Gameplay.Instance.CurrentScore.ToString();
    }
}
