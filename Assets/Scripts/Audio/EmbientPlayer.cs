using Controllers.Sound;
using UnityEngine;

public class EmbientPlayer : MonoBehaviour
{

	public void Start()
	{
		SoundController.Instance.PlayEmbient(true);
	}
}