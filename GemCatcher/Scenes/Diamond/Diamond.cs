using Godot;
using System;
using System.Diagnostics.CodeAnalysis;

public partial class Diamond : Area2D
{
	public readonly float MARGIN = 50.0f;
	private Rect2 _vpr;                                             // 画布矩形

	[Export] private float _speed = 100.0f;
	[Signal] public delegate void OnScroedEventHandler();           // 得分信号
	[Signal] public delegate void OnGameOffScreenEventHandler();    // 触达屏幕底部信号		

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// 订阅AreaEntered信号,当其他Area/PhysicsBody进入时调用OnAreaEntered方法
		AreaEntered += OnAreaEntered;
		_vpr = GetViewportRect();  // 获取画布矩形
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0, _speed * (float)delta);	// 钻石下落
		CheckHitBottom();
	}

	// 碰撞检测
	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Send OnScored Signal");
		EmitSignal(SignalName.OnScroed);    // 发送得分信号
		QueueFree();
	}

	// 当钻石到达屏幕底部时,发送OnGameOffScreen信号
	private void CheckHitBottom()
	{
		if (Position.Y >= _vpr.End.Y)
		{
			GD.Print("Send OnGameOffScreen Signal");
			EmitSignal(SignalName.OnGameOffScreen);	// 发送钻石触达底部信号
		}
	}
}
