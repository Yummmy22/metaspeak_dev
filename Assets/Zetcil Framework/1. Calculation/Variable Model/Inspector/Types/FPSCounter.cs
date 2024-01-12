﻿using TechnomediaLabs;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
	[SerializeField] public float _updateInterval = 1f;
	[SerializeField] public int _targetFrameRate = 30;
	
	[Separator]
	[SerializeField] public Anchor _anchor;
	[SerializeField] public int _xOffset;
	[SerializeField] public int _yOffset;
	

	private float _elapsed;
	private int _frames;
	private int _quantity;
	private float _fps;
	private float _averageFps;

	private Color _goodColor;
	private Color _okColor;
	private Color _badColor;

	private float _okFps;
	private float _badFps;

	private Rect _rect1;
	private Rect _rect2;

	private void Awake()
	{
		_goodColor = new Color(.4f, .6f, .4f);
		_okColor = new Color(.8f, .8f, .2f, .6f);
		_badColor = new Color(.8f, .6f, .6f);

		var percent = _targetFrameRate / 100;
		var percent10 = percent * 10;
		var percent40 = percent * 40;
		_okFps = _targetFrameRate - percent10;
		_badFps = _targetFrameRate - percent40;
		
		var xPos = 0;
		var yPos = 0;
		var linesHeight = 40;
		var linesWidth = 150;
		if (_anchor == Anchor.LeftBottom || _anchor == Anchor.RightBottom) yPos = Screen.height - linesHeight;
		if (_anchor == Anchor.RightTop || _anchor == Anchor.RightBottom) xPos = Screen.width - linesWidth;
		xPos += _xOffset;
		yPos += _yOffset;
		var yPos2 = yPos + 18;
		_rect1 = new Rect(xPos, yPos, linesWidth, linesHeight);
		_rect2 = new Rect(xPos, yPos2, linesWidth, linesHeight);

		_elapsed = _updateInterval;
	}
	
	private void Update()
	{
		_elapsed += Time.deltaTime;
		++_frames;
		
		if (_elapsed >= _updateInterval)
		{
			_fps = _frames / _elapsed;
			_elapsed = 0;
			_frames = 0;
		}
		
		_quantity++;
		_averageFps += (_fps - _averageFps) / _quantity;
	}

	private void OnGUI()
	{
		var defaultColor = GUI.color;
		var color = _goodColor;
		if (_fps <= _okFps || _averageFps <= _okFps) color = _okColor;
		if (_fps <= _badFps || _averageFps <= _badFps) color = _badColor;
		GUI.color = color;
		GUI.Label(_rect1, "FPS: " + (int)_fps);
		GUI.Label(_rect2, "Avg FPS: " + (int)_averageFps);
		GUI.color = defaultColor;
	}

	public enum Anchor
	{
		LeftTop, LeftBottom, RightTop, RightBottom
	}
}
