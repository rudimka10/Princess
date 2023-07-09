using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] List<Image> _images = new();

    [Space]

    [SerializeField] 
    private Color _enabledColor = Color.white;

    [SerializeField]
    private Color _disabledColor = Color.black;

    public int Value
    {
        set
        {
            for (int i = 0; i < _images.Count; i++)
            {
                if (_images[i] != null)
                {
                    _images[i].color = i < value ? _enabledColor : _disabledColor;
                }
            }
        }
    }
}