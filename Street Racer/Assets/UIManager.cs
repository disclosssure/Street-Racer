using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private List<PoliceCar> _policeCars;

    private void OnEnable()
    {
        _policeCars.ForEach(p => p.OnPlayerCaught += HandleOnPlayerCaught);
    }

    private void HandleOnPlayerCaught()
    {
        Time.timeScale = 0;
        _panel.SetActive(true);
    }
}
