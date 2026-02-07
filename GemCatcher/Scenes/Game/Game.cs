using Godot;
using System;
using System.Threading;

public partial class Game : Node2D
{
	[Export] private PackedScene _diamondScene;
	[Export] private Godot.Timer _timer;
	[Export] private Label _scoreLabel;
	[Export] private AudioStreamPlayer _backGroundMusic;
	[Export] private AudioStreamPlayer2D _effectMusic;

	private static readonly AudioStream EXPLODE_SOUND = GD.Load<AudioStream>("res://assets/explode.wav");

	private Rect2 _vpr; // 画布矩形
	private int _score; // 得分

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_vpr = GetViewportRect();  // 获取画布矩形
		_timer.Timeout += OnTimeOut;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnDiamondScene()
	{
		Diamond diamond = (Diamond)_diamondScene.Instantiate();
		AddChild(diamond);
		float x = (float)GD.RandRange(_vpr.Position.X + diamond.MARGIN, _vpr.End.X - diamond.MARGIN);
		diamond.Position = new Vector2(x, -100);
		diamond.OnScroed += OnScroed;
		diamond.OnGameOffScreen += OnGameOffScreen;
	}

	private void OnScroed()
	{
		GD.Print("Recived OnScored Signal");
		_score++;
		_scoreLabel.Text = $"{_score:0000}";
		_effectMusic.Play();
	}

	private void OnTimeOut()
	{
		SpawnDiamondScene();
	}

	private void OnGameOffScreen()
	{
		GD.Print("-----GameOver-----");
		foreach (Node node in GetChildren())
		{
			node.SetProcess(false);
		}
		_timer.Stop();
		_backGroundMusic.Stop();
		_effectMusic.Stop();
		_effectMusic.Stream = EXPLODE_SOUND;
		_effectMusic.Play();
	}
}
