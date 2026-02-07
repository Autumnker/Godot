using Godot;
using System;
using System.Drawing;

public partial class Paddle : Area2D
{
	public readonly float MARGIN = 50.0F;

	[Export] private float _speed = 500.0f;

	private Rect2 _vpr; // 画布矩形

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_vpr = GetViewportRect();  // 获取画布矩形
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// 键盘输入控制移动
		if (Input.IsActionPressed("Right") == true)
		{
			Position += new Vector2(_speed * (float)delta, 0);
		}
		if (Input.IsActionPressed("Left") == true)
		{
			Position -= new Vector2(_speed * (float)delta, 0);
		}

		// 边界判定
		if (Position.X >= _vpr.End.X - MARGIN)
		{
			Position = new Vector2(_vpr.End.X - MARGIN, Position.Y);
		}
		if (Position.X <= _vpr.Position.X + MARGIN)
		{
			Position = new Vector2(_vpr.Position.X + MARGIN, Position.Y);
		}
	}
}
