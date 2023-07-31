using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] 
    private Color _enabledColor = Color.white;

    [SerializeField]
    private Color _disabledColor = Color.black;

	[SerializeField]
	private List<Image> _images = new();

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