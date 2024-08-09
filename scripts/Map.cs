using Godot;
using System;

public partial class Map : Node2D
{
	private TileMap _tileMap;
	private Sprite2D _cursor;
	private AnimatedSprite2D _flag;
	private Camera2D _camera;
	private Timer _transitionTimer;
	private AtlasTexture _atlasTexture;
	private Vector2 _tileSize;
	
	private Vector2I _cursorPosition;
	private bool _cursorEnabled;
	private bool _cursorOverTown;
	private string _locationName;
	
	private Vector2 _lastDirection;
	private float _keyRepeatTimer = 0;
	
	private const float _keyRepeatDelay = 0.25f;
	private const float _keyRepeatInterval = 0.1f;
	
	private Rect2 _cursorDefault = new Rect2(240, 896, 16, 16);
	private Rect2 _cursorReady = new Rect2(224, 896, 16, 16);
	private Rect2 _cursorSelected = new Rect2(208, 896, 16, 16);
	
	public GodotObject Controller;
	
	public override void _Ready()
	{
		_tileMap = GetNode<TileMap>("TileMap");
		_cursor = GetNode<Sprite2D>("Canvas/Cursor");
		_flag = GetNode<AnimatedSprite2D>("Flag");
		_transitionTimer = GetNode<Timer>("TransitionTimer");
		_camera = GetNode<Camera2D>("Canvas/Cursor/MapCamera2D");
		_camera.MakeCurrent();
		_atlasTexture = (AtlasTexture)_cursor.Texture;
		_tileSize = _tileMap.TileSet.TileSize;
		_cursorPosition = Vector2I.Zero;
		_lastDirection = Vector2.Zero;
		_cursorEnabled = true;
		_cursorOverTown = false;
		
		SetFlag();
		UpdateCursor();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta)
	{
		_keyRepeatTimer -= (float)delta;
		Vector2 direction = Input.GetAxis("gui_left","gui_right")*Vector2.Right+Input.GetAxis("gui_up","gui_down")*Vector2.Down;
		if(_lastDirection == Vector2.Zero || _keyRepeatTimer <= 0) {
			_keyRepeatTimer = _lastDirection == direction ? _keyRepeatInterval : _keyRepeatDelay;
			if(_cursorEnabled) MoveCursor(direction);
			_lastDirection = direction;
		}
		
		if (Input.IsActionJustPressed("gui_accept") && _cursorOverTown)
		{
			_atlasTexture.Region = _cursorSelected;
			_cursor.Texture = _atlasTexture;
			_cursorEnabled = false;
			_transitionTimer.Start();
			GameData.Instance.PlayerLocation = GameLocation.Get(_locationName);
		}
	}
	
	public void DoSceneTransition() {
		Controller.Call("come_back_from_map");
	}

	private void SetFlag() {
		//sets _flag.Position to whatever tile has the same "LocationName" custom data layer as GameData.Instance.PlayerLocation
		for (int x = -_tileMap.GetUsedRect().Size.X/2; x < _tileMap.GetUsedRect().Size.X/2; x++)
		{
			for (int y = -_tileMap.GetUsedRect().Size.Y/2; y < _tileMap.GetUsedRect().Size.Y/2; y++)
			{
				Vector2I tileCoords = new Vector2I(x, y);
				TileData tileData = _tileMap.GetCellTileData(0, tileCoords);
				if(tileData == null || ((string)tileData.GetCustomData("LocationName")).Length == 0) continue;
				if (GameData.Instance.PlayerLocation.Id.Equals((string)tileData.GetCustomData("LocationName")))
				{
					_flag.Position = MapToGlobal(tileCoords*2) - _tileSize*new Vector2(2.8f,-0.8f); // proof by i fiddled around with the values until it was perfect
					return;
				}
			}
		}
	}

	private void MoveCursor(Vector2 direction)
	{
		_cursorPosition += (Vector2I)direction;
		UpdateCursor();
		UpdateCursorTexture();
	}

	private void UpdateCursor()
	{
		_cursorPosition.X = Mathf.Clamp(_cursorPosition.X, -_tileMap.GetUsedRect().Size.X/2, _tileMap.GetUsedRect().Size.X/2-1);
		_cursorPosition.Y = Mathf.Clamp(_cursorPosition.Y, -_tileMap.GetUsedRect().Size.Y/2, _tileMap.GetUsedRect().Size.Y/2-1);
		_cursor.Position = MapToGlobal(_cursorPosition*2);
	}
	
	private Vector2 MapToGlobal(Vector2 mapPos) {
		return (mapPos + new Vector2I(4,0)) * _tileSize * 1.2f + _tileMap.Position + new Vector2(0.75f*2.4f, -2.5f*2.4f);
	}
	
	private void UpdateCursorTexture() {
		Vector2I tileCoords = _cursorPosition*2;
		TileData tileData = _tileMap.GetCellTileData(0, tileCoords);
		if(tileData != null) _cursorOverTown = ((bool)tileData.GetCustomData("TownTile"));
		else _cursorOverTown = false;
		if(tileData != null) _locationName = ((string)tileData.GetCustomData("LocationName"));
		else _locationName = "";
		_atlasTexture.Region = _cursorOverTown ? _cursorReady : _cursorDefault;
		_cursor.Texture = _atlasTexture;
	}
	
	private void _on_transition_timer_timeout()
	{
		DoSceneTransition();
	}
}
