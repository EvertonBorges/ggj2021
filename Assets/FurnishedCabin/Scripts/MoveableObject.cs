using UnityEngine;
using System.Collections;

public class MoveableObject : MonoBehaviour 
{

	public int objectNumber = 1;

	[SerializeField] private GameObject[] _canvas = null;
	private bool _canInteract = false;

	private bool _hasCanvas => _canvas != null && _canvas.Length > 0;
	private bool _canvasIsActive => _hasCanvas && _canvas[0].activeSelf;

	void Awake()
	{
		foreach (var canvas in _canvas)
			canvas?.SetActive(false);
	}

	void Update()
	{
		if (_canInteract && !_canvasIsActive)
			foreach (var canvas in _canvas)
				canvas?.SetActive(true);
		else if (!_canInteract && _canvasIsActive)
			foreach (var canvas in _canvas)
				canvas?.SetActive(false);
	}

	public void SetCanInteract(bool canInteract)
	{
		_canInteract = canInteract;
	}

}
