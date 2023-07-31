using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TextVariant {
	public string text;
	public float width;

	public TextVariant(string text, float width) {
		this.text = text;
		this.width = width;
	}
}

public class GameView : MonoBehaviour {

	[SerializeField]
	private GameObject _loseView;

	[SerializeField]
	private float _loseViewToggleDelay = 2;

	[SerializeField]
	private GameObject _pauseView;

	[SerializeField]
	private GameObject _winView;

	[SerializeField]
	private Text _winButtonText;

	[SerializeField]
	private GameObject _tutorialView;

	[SerializeField]
	TextVariant[] _winButtonVariants = {
		new("You did it!", 750),
		new("Well done!", 750),
		new("Hooray!", 600),
		new("Victory!", 650),
		new("Amazing!", 650),
		new("Bravo!", 550),
		new("You're a hero!", 800),
		new("Fantastic!", 750)
	};

	private bool _tutorialShown;

	public bool Paused { get; private set; }

	private void Start() {
		_loseView.SetActive(false);
		_pauseView.SetActive(false);
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			TogglePause(!Paused);

		} else if (Input.GetKeyDown(KeyCode.F1)) {
			ToggleTutorial(!_tutorialShown);
		}
	}

	public void TogglePause(bool isPaused) {
		if (_tutorialShown)
			return;
		Paused = isPaused;
		Time.timeScale = isPaused ? 0 : 1;
		_pauseView.SetActive(isPaused);
	}

	public void OnLose() {
		StartCoroutine(ToggleShownCoroutine(true));
	}

	private IEnumerator ToggleShownCoroutine(bool show) {
		yield return new WaitForSeconds(_loseViewToggleDelay);
		_loseView.SetActive(show);
	}

	private void OnEnable() {
		Time.timeScale = 1;
	}

	private void OnDestroy() {
		Time.timeScale = 1;
	}

	public void OnWinGame() {
		var randomText = _winButtonVariants[Random.Range(0, _winButtonVariants.Length)];
		_winButtonText.text = randomText.text;
		var _rectTransform = _winButtonText.GetComponent<RectTransform>();
		_rectTransform.sizeDelta = new Vector2(randomText.width, _rectTransform.sizeDelta.y);
	}

	public void ToggleTutorial(bool shown) {
		if (Paused) {
			_pauseView.SetActive(!shown);

		} else {
			Time.timeScale = shown ? 0 : 1;
		}

		_tutorialShown = shown;
		_tutorialView.SetActive(shown);
	}
}