using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedSection : MonoBehaviour
{
    [SerializeField] private Image[] _leds;
    public int Combination { get; set; }

    public Image[] Leds => _leds;
}
