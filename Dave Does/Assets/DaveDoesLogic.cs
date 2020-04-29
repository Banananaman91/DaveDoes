using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DaveDoesLogic : MonoBehaviour
{
    [SerializeField] private LedSection[] _ledSections;

    [SerializeField] private AudioSource[] _audioClips;

    [SerializeField] private Image[] _mainLeds;

    [SerializeField] private Color[] _colorOptions;

    private List<Color[]> _colorCombinations = new List<Color[]>();
    private List<AudioSource[]> _audioCombinations = new List<AudioSource[]>();

    private Color[] _colorCombination1 = new Color[4];
    private Color[] _colorCombination2 = new Color[4];
    private Color[] _colorCombination3 = new Color[4];
    private Color[] _colorCombination4 = new Color[4];
    
    private AudioSource[] _audioCombination1 = new AudioSource[4];
    private AudioSource[] _audioCombination2 = new AudioSource[4];
    private AudioSource[] _audioCombination3 = new AudioSource[4];
    private AudioSource[] _audioCombination4 = new AudioSource[4];

    private Color[] _mainColors = new Color[4];
    private AudioSource[] _mainAudio = new AudioSource[4];
    
    private LedSection _selection;
    
    private Vector3 _newScale = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 _oldScale = new Vector3(1, 1, 1);

    private bool _fail;

    [HideInInspector] public bool _useAudio;
    [HideInInspector] public bool _useVisual;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateColours();
        _colorCombinations.Add(_colorCombination1);
        _colorCombinations.Add(_colorCombination2);
        _colorCombinations.Add(_colorCombination3);
        _colorCombinations.Add(_colorCombination4);
        
        _audioCombinations.Add(_audioCombination1);
        _audioCombinations.Add(_audioCombination2);
        _audioCombinations.Add(_audioCombination3);
        _audioCombinations.Add(_audioCombination4);
        
        SelectLedSection(0);

        if (_useVisual) return;
        foreach (var led in _mainLeds)
        {
            led.gameObject.SetActive(false);
        }
    }

    private void GenerateColours()
    {
        for (int i = 0; i < _mainLeds.Length; i++)
        {
            var colourChoice = Random.Range(0, _colorOptions.Length);
            _mainColors[i] = _colorOptions[colourChoice];
            _mainAudio[i] = _audioClips[colourChoice];
            _mainLeds[i].color = _mainColors[i];
        }
        
        int colourValue = 0;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination1[i] = _mainColors[colourValue];
            _ledSections[0].Leds[i].color = _mainColors[colourValue];
            _audioCombination1[i] = _mainAudio[colourValue];
            _ledSections[0]._audio[i] = _mainAudio[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }

        _ledSections[0].Combination = 0;
        
        colourValue = 1;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination2[i] = _mainColors[colourValue];
            _ledSections[1].Leds[i].color = _mainColors[colourValue];
            _audioCombination2[i] = _mainAudio[colourValue];
            _ledSections[1]._audio[i] = _mainAudio[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[1].Combination = 1;
        
        colourValue = 2;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination3[i] = _mainColors[colourValue];
            _ledSections[2].Leds[i].color = _mainColors[colourValue];
            _audioCombination3[i] = _mainAudio[colourValue];
            _ledSections[2]._audio[i] = _mainAudio[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[2].Combination = 2;
        
        colourValue = 3;

        for (int i = 0; i < 4; i++)
        {
            _colorCombination4[i] = _mainColors[colourValue];
            _ledSections[3].Leds[i].color = _mainColors[colourValue];
            _audioCombination4[i] = _mainAudio[colourValue];
            _ledSections[3]._audio[i] = _mainAudio[colourValue];
            if (colourValue < 3) colourValue++;
            else colourValue = 0;
        }
        
        _ledSections[3].Combination = 3;
    }

    public void SelectLedSection(int selected)
    {
        _selection = _ledSections[selected];
    }

    public void RotateLeft()
    {
        if (_selection.Combination >= 3) _selection.Combination = 0;
        else _selection.Combination++;
        for (int i = 0; i < _selection.Leds.Length; i++)
        {
            _selection.Leds[i].color = _colorCombinations[_selection.Combination][i];
            _selection._audio[i] = _audioCombinations[_selection.Combination][i];
        }

    }

    public void RotateRight()
    {

        if (_selection.Combination <= 0) _selection.Combination = 3;
        else _selection.Combination--;
        for (int i = 0; i < _selection.Leds.Length; i++)
        {
            _selection.Leds[i].color = _colorCombinations[_selection.Combination][i];
            _selection._audio[i] = _audioCombinations[_selection.Combination][i];
        }

    }

    public void PlaySequence()
    {
        StartCoroutine(PlayBackSequence());
    }

    public void ConfirmSequence()
    {
        _fail = false;
        StartCoroutine(Confirmation());
    }

    private IEnumerator Confirmation()
    {
        StartCoroutine(PlayBackSequence());
        yield return new WaitForSeconds(5f);
        foreach (var section in _ledSections)
        {
            for (int i = 0; i < _mainLeds.Length; i++)
            {
                if (_mainLeds[i].color != section.Leds[i].color) _fail = true;
            }
        }
        if (!_fail) GenerateColours();
        yield return null;
    }

    private IEnumerator PlayBackSequence()
    {
        var ledSection1 = _ledSections[0];
        var ledSection2 = _ledSections[1];
        var ledSection3 = _ledSections[2];
        var ledSection4 = _ledSections[3];
        for (int i = 0; i < _mainLeds.Length; i++)
        {
            if (_useVisual) ledSection1.Leds[i].transform.localScale = _newScale;
            if (_useAudio) ledSection1._audio[i].Play();
            if (_useVisual) ledSection2.Leds[i].transform.localScale = _newScale;
            if (_useAudio) ledSection2._audio[i].Play();
            if (_useVisual) ledSection3.Leds[i].transform.localScale = _newScale;
            if (_useAudio) ledSection3._audio[i].Play();
            if (_useVisual) ledSection4.Leds[i].transform.localScale = _newScale;
            if (_useAudio) ledSection4._audio[i].Play();
            if (_useVisual) _mainLeds[i].transform.localScale = _newScale;
            if (_useAudio) _mainAudio[i].Play();
            yield return new WaitForSeconds(1f);
            ledSection1.Leds[i].transform.localScale = _oldScale;
            ledSection2.Leds[i].transform.localScale = _oldScale;
            ledSection3.Leds[i].transform.localScale = _oldScale;
            ledSection4.Leds[i].transform.localScale = _oldScale;
            _mainLeds[i].transform.localScale = _oldScale;
        }

        yield return null;
    }

    public void UseAudio()
    {
        if (!_useAudio) _useAudio = true;
        else _useAudio = false;
    }
    
    public void UseVisual()
    {
        if (!_useVisual) _useVisual = true;
        else _useVisual = false;

        if (!_useVisual)
        {
            foreach (var led in _mainLeds)
            {
                led.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var led in _mainLeds)
            {
                led.gameObject.SetActive(true);
            }
        }
    }
}
