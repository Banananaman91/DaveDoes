using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DaveDoesLogic : MonoBehaviour
{
    [SerializeField] private LedSection[] _ledSections;

    [SerializeField] private Image[] _mainLeds;

    [SerializeField] private Color[] _colorOptions;

    private List<Color[]> _colorCombinations = new List<Color[]>();

    private Color[] _colorCombination1 = new Color[4];
    private Color[] _colorCombination2 = new Color[4];
    private Color[] _colorCombination3 = new Color[4];
    private Color[] _colorCombination4 = new Color[4];

    private Color[] _mainColors = new Color[4];
    
    private LedSection[] _selection = new LedSection[2];
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _mainLeds.Length; i++)
        {
            var colourChoice = Random.Range(0, _colorOptions.Length);
            _mainLeds[i].color = _colorOptions[colourChoice];
            _mainColors[i] = _colorOptions[colourChoice];
        }
        
        GenerateColours();
        _colorCombinations.Add(_colorCombination1);
        _colorCombinations.Add(_colorCombination2);
        _colorCombinations.Add(_colorCombination3);
        _colorCombinations.Add(_colorCombination4);
        
        SelectLedSection(0);
    }

    private void GenerateColours()
    {
        int colourValue = 0;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination1[i] = _mainColors[colourValue];
            _ledSections[0].Leds[i].color = _mainColors[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }

        _ledSections[0].Combination = 0;
        
        colourValue = 1;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination2[i] = _mainColors[colourValue];
            _ledSections[1].Leds[i].color = _mainColors[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[1].Combination = 1;
        
        colourValue = 2;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination3[i] = _mainColors[colourValue];
            _ledSections[2].Leds[i].color = _mainColors[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[2].Combination = 2;
        
        colourValue = 3;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination4[i] = _mainColors[colourValue];
            _ledSections[3].Leds[i].color = _mainColors[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[3].Combination = 3;
    }

    public void SelectLedSection(int selected)
    {
        _selection[0] = _ledSections[selected];
        var addition = selected + 1;
        if (addition > 3) addition = 0;
        _selection[1] = _ledSections[addition];
    }

    public void RotateLeft()
    {
        foreach (var selected in _selection)
        {
            if (selected.Combination >= 3) selected.Combination = 0;
            else selected.Combination++;
            for (int i = 0; i < selected.Leds.Length; i++)
            {
                selected.Leds[i].color = _colorCombinations[selected.Combination][i];
            }
        }
    }

    public void RotateRight()
    {
        foreach (var selected in _selection)
        {
            if (selected.Combination <= 0) selected.Combination = 3;
            else selected.Combination--;
            for (int i = 0; i < selected.Leds.Length; i++)
            {
                selected.Leds[i].color = _colorCombinations[selected.Combination][i];
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
