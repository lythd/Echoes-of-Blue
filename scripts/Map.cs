using Godot;
using System;

public partial class Map : Node2D
{
	private TileMap _tileMap;
	private Sprite2D _cursor;
	private Sprite2D _flag;
	private Timer _transitionTimer;
	private AtlasTexture _atlasTexture;
	private Vector2 _tileSize;
	
	private Vector2I _cursorPosition;
	private bool _cursorEnabled;
	private bool _cursorOverTown;
	private string _locationName;
	
	private Vector2 _lastDirection;
	private float _keyRepeatTimer = 0;
	
	private const float _keyRepeatDelay = 0.5f;
	private const float _keyRepeatInterval = 0.1f;
	
	private Rect2 _cursorDefault = new Rect2(240, 224, 16, 16);
	private Rect2 _cursorReady = new Rect2(224, 224, 16, 16);
	private Rect2 _cursorSelected = new Rect2(208, 224, 16, 16);
	
	private GameData _gameData;
	
	public GodotObject Controller;
	
	public override void _Ready()
	{
		//GD.Print("Map ready!");
		//GD.Print($"Path: '{GetPath()}'");
		_gameData = GetNode<GameData>("/root/GameData");
		_tileMap = GetNode<TileMap>("/root/Game/Map/Canvas/TileMap");
		_cursor = GetNode<Sprite2D>("/root/Game/Map/Canvas/Cursor");
		_flag = GetNode<Sprite2D>("/root/Game/Map/Canvas/Flag");
		_transitionTimer = GetNode<Timer>("/root/Game/Map/TransitionTimer");
		GetNode<Camera2D>("/root/Game/Map/Canvas/Cursor/Camera2D").MakeCurrent();
		//GD.Print($"Game Data: '{_gameData}'");
		//GD.Print($"Tile Map: '{_tileMap}'");
		_atlasTexture = (AtlasTexture)_cursor.Texture;
		_tileSize = _tileMap.TileSet.TileSize;
		_cursorPosition = Vector2I.Zero;
		_lastDirection = Vector2.Zero;
		_cursorEnabled = true;
		_cursorOverTown = false;
		
		SetFlag();
		UpdateCursor();
	}

	public override void _Process(double delta)
	{
		_keyRepeatTimer -= (float)delta;
		Vector2 direction = Input.GetAxis("gui_left","gui_right") * Vector2.Right + Input.GetAxis("gui_up","gui_down") * Vector2.Down;
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
			_gameData.PlayerLocation = _locationName;
		}
	}
	
	public void DoSceneTransition() {
		//GD.Print("transition");
		Controller.Call("come_back_from_map");
	}

	private void SetFlag() {
		//set _flag.Position to whatever tile has the same "LocationName" custom data layer as _gameData.PlayerLocation
		
		for (int x = -_tileMap.GetUsedRect().Size.X/2; x < _tileMap.GetUsedRect().Size.X/2; x++)
		{
			for (int y = -_tileMap.GetUsedRect().Size.Y/2; y < _tileMap.GetUsedRect().Size.Y/2; y++)
			{
				Vector2I tileCoords = new Vector2I(x, y);
				TileData tileData = _tileMap.GetCellTileData(0, tileCoords);
				if(tileData == null || ((string)tileData.GetCustomData("LocationName")).Length == 0) continue;
				//GD.Print($"'{_gameData.PlayerLocation}' ~ '{(string)tileData.GetCustomData("LocationName")}'");
				if (_gameData.PlayerLocation.Equals((string)tileData.GetCustomData("LocationName")))
				{
					//GD.Print("found");
					_flag.Position = MapToGlobal(tileCoords)+_tileSize*2.4f-new Vector2(0,9.6f); //_tileMap.MapToLocal(tileCoords) + _tileMap.GlobalPosition + _tileSize/2;
					//GD.Print(_flag.Position);
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

		//_cursor.Position = new Vector2((float)(_cursorPosition.X * _tileSize.X * 4.8 + _tileMap.Position.X),(float)(_cursorPosition.Y * _tileSize.Y * 4.8 + _tileMap.Position.Y));
		//_cursor.Position = _tileMap.ToGlobal(_tileMap.MapToLocal(_cursorPosition*2)); // for some reason a bit off?
		_cursor.Position = MapToGlobal(_cursorPosition*2);
		
		//GD.Print($"cursor.Pos: ({_cursor.Position.X}, {_cursor.Position.Y})");
	}
	
	private Vector2 MapToGlobal(Vector2 mapPos) {
		return mapPos * _tileSize * 2.4f + _tileMap.Position;
	}
	
	private void UpdateCursorTexture() {
		Vector2I tileCoords = _cursorPosition*2; //_tileMap.LocalToMap(_cursor.GlobalPosition-_tileMap.GlobalPosition);
		//tileCoords.X = (int)((tileCoords.X+1)/2.4);
		//tileCoords.Y = (int)((tileCoords.Y+1)/2.4);
		//GD.Print($"tileCoords: ({tileCoords.X}, {tileCoords.Y})");
		//GD.Print($"cursorPos: ({_cursorPosition.X}, {_cursorPosition.Y})");
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
