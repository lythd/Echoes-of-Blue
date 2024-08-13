using Godot;

namespace EchoesofBlue.scripts;

public partial class Character : Node2D
{
	private static Texture2D[] _spritesheetSpecial;
	private static Texture2D[] _spritesheetHair;
	private static Texture2D[] _spritesheetEyes;
	private static Texture2D[] _spritesheetHands;
	private static Texture2D[] _spritesheetShirt;
	private static Texture2D[] _spritesheetPants;
	private static Texture2D[] _spritesheetShoes;
	private static Texture2D[] _spritesheetSkin;

	private static bool _loadedSheets;
	
	private Sprite2D _special;
	private Sprite2D _hair;
	private Sprite2D _eyes;
	private Sprite2D _hands;
	private Sprite2D _shirt;
	private Sprite2D _pants;
	private Sprite2D _shoes;
	private Sprite2D _skin;
	
	private AnimationPlayer _ani;
	
	private int _specialInd;
	private int _hairInd;
	private int _eyesInd;
	private int _handsInd;
	private int _shirtInd;
	private int _pantsInd;
	private int _shoesInd;
	private int _skinInd;
	
	private RandomNumberGenerator _random = new();
	
	[Export]
	public int SpecialInd {
		get => _specialInd;
		set
		{
			if(_special == null) return;
			_specialInd = value % _spritesheetSpecial.Length;
			_special.Texture = _spritesheetSpecial[_specialInd];
		}
	}
	
	[Export]
	public int HairInd {
		get => _hairInd;
		set
		{
			if(_hair == null) return;
			_hairInd = value % _spritesheetHair.Length;
			_hair.Texture = _spritesheetHair[_hairInd];
		}
	}
	
	[Export]
	public int EyesInd {
		get => _eyesInd;
		set
		{
			if(_eyes == null) return;
			_eyesInd = value % _spritesheetEyes.Length;
			_eyes.Texture = _spritesheetEyes[_eyesInd];
		}
	}
	
	[Export]
	public int HandsInd {
		get => _handsInd;
		set
		{
			if(_hands == null) return;
			_handsInd = value % _spritesheetHands.Length;
			_hands.Texture = _spritesheetHands[_handsInd];
		}
	}
	
	[Export]
	public int ShirtInd {
		get => _shirtInd;
		set
		{
			if(_shirt == null) return;
			_shirtInd = value % _spritesheetShirt.Length;
			_shirt.Texture = _spritesheetShirt[_shirtInd];
		}
	}
	
	[Export]
	public int PantsInd {
		get => _pantsInd;
		set
		{
			if(_pants == null) return;
			_pantsInd = value % _spritesheetPants.Length;
			_pants.Texture = _spritesheetPants[_pantsInd];
		}
	}
	
	[Export]
	public int ShoesInd {
		get => _shoesInd;
		set
		{
			if(_shoes == null) return;
			_shoesInd = value % _spritesheetShoes.Length;
			_shoes.Texture = _spritesheetShoes[_shoesInd];
		}
	}
	
	[Export]
	public int SkinInd {
		get => _skinInd;
		set
		{
			if(_skin == null) return;
			_skinInd = value % _spritesheetSkin.Length;
			_skin.Texture = _spritesheetSkin[_skinInd];
		}
	}
	
	[Export]
	public float HairHue {
		get => _hair.SelfModulate.H;
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(value,_hair.SelfModulate.S,_hair.SelfModulate.V);
		}
	}
	
	[Export]
	public float HairSat {
		get => _hair.SelfModulate.S;
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(_hair.SelfModulate.H,value,_hair.SelfModulate.V);
		}
	}
	
	[Export]
	public float HairVal {
		get => _hair.SelfModulate.V;
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(_hair.SelfModulate.H,_hair.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float EyesHue {
		get => _eyes.SelfModulate.H;
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(value,_eyes.SelfModulate.S,_eyes.SelfModulate.V);
		}
	}
	
	[Export]
	public float EyesSat {
		get => _eyes.SelfModulate.S;
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(_eyes.SelfModulate.H,value,_eyes.SelfModulate.V);
		}
	}
	
	[Export]
	public float EyesVal {
		get => _eyes.SelfModulate.V;
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(_eyes.SelfModulate.H,_eyes.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float HandsHue {
		get => _hands.SelfModulate.H;
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(value,_hands.SelfModulate.S,_hands.SelfModulate.V);
		}
	}
	
	[Export]
	public float HandsSat {
		get => _hands.SelfModulate.S;
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(_hands.SelfModulate.H,value,_hands.SelfModulate.V);
		}
	}
	
	[Export]
	public float HandsVal {
		get => _hands.SelfModulate.V;
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(_hands.SelfModulate.H,_hands.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float ShirtHue {
		get => _shirt.SelfModulate.H;
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(value,_shirt.SelfModulate.S,_shirt.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShirtSat {
		get => _shirt.SelfModulate.S;
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(_shirt.SelfModulate.H,value,_shirt.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShirtVal {
		get => _shirt.SelfModulate.V;
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(_shirt.SelfModulate.H,_shirt.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float PantsHue {
		get => _pants.SelfModulate.H;
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(value,_pants.SelfModulate.S,_pants.SelfModulate.V);
		}
	}
	
	[Export]
	public float PantsSat {
		get => _pants.SelfModulate.S;
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(_pants.SelfModulate.H,value,_pants.SelfModulate.V);
		}
	}
	
	[Export]
	public float PantsVal {
		get => _pants.SelfModulate.V;
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(_pants.SelfModulate.H,_pants.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float ShoesHue {
		get => _shoes.SelfModulate.H;
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(value,_shoes.SelfModulate.S,_shoes.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShoesSat {
		get => _shoes.SelfModulate.S;
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(_shoes.SelfModulate.H,value,_shoes.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShoesVal {
		get => _shoes.SelfModulate.V;
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(_shoes.SelfModulate.H,_shoes.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float SkinHue {
		get => _skin.SelfModulate.H;
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(value,_skin.SelfModulate.S,_skin.SelfModulate.V);
		}
	}
	
	[Export]
	public float SkinSat {
		get => _skin.SelfModulate.S;
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(_skin.SelfModulate.H,value,_skin.SelfModulate.V);
		}
	}
	
	[Export]
	public float SkinVal {
		get => _skin.SelfModulate.V;
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(_skin.SelfModulate.H,_skin.SelfModulate.S,value);
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	public override void _Ready()
	{
		if(!_loadedSheets) LoadSheets();
		_special = GetNode<Sprite2D>("Special");
		_hair = GetNode<Sprite2D>("Hair");
		_eyes = GetNode<Sprite2D>("Eyes");
		_hands = GetNode<Sprite2D>("Hands");
		_shirt = GetNode<Sprite2D>("Shirt");
		_pants = GetNode<Sprite2D>("Pants");
		_shoes = GetNode<Sprite2D>("Shoes");
		_skin = GetNode<Sprite2D>("Skin");
		_ani = GetNode<AnimationPlayer>("AnimationPlayer");
		
		SpecialInd = 0;
		HairInd = 0;
		EyesInd = 0;
		HandsInd = 0;
		ShirtInd = 0;
		PantsInd = 0;
		ShoesInd = 0;
		SkinInd = 0;
		
		HairHue = 9.0f/360.0f;
		HairSat = 52.0f/100.0f;
		HairVal = 40.0f/100.0f;
		
		EyesHue = 24.0f/360.0f;
		EyesSat = 83.0f/100.0f;
		EyesVal = 87.0f/100.0f;
		
		HandsHue = 29.0f/360.0f;
		HandsSat = 35.0f/100.0f;
		HandsVal = 93.0f/100.0f;
		
		ShirtHue = 319.0f/360.0f;
		ShirtSat = 42.0f/100.0f;
		ShirtVal = 27.0f/100.0f;
		
		PantsHue = 246.0f/360.0f;
		PantsSat = 38.0f/100.0f;
		PantsVal = 20.0f/100.0f;
		
		ShoesHue = 34.0f/360.0f;
		ShoesSat = 8.0f/100.0f;
		ShoesVal = 35.0f/100.0f;
		
		SkinHue = 29.0f/360.0f;
		SkinSat = 35.0f/100.0f;
		SkinVal = 93.0f/100.0f;
	}
	
	// just was here to test
	/*public override void _Process(double delta) {
	if(Input.IsActionJustPressed("gui_accept")) {
		PantsInd++;
	}
}*/
	/*public override void _Process(double delta) {
	if(Input.IsActionJustPressed("gui_accept")) {
		Randomize();
	}
}*/
	
	public void Randomize() {
		_random.Randomize();
		
		SpecialInd = _random.RandiRange(0,_spritesheetSpecial.Length-1);
		HairInd = _random.RandiRange(0,_spritesheetHair.Length-1);
		EyesInd = _random.RandiRange(0,_spritesheetEyes.Length-1);
		HandsInd = _random.RandiRange(0,_spritesheetHands.Length-1);
		ShirtInd = _random.RandiRange(0,_spritesheetShirt.Length-1);
		PantsInd = _random.RandiRange(0,_spritesheetPants.Length-1);
		ShoesInd = _random.RandiRange(0,_spritesheetShoes.Length-1);
		SkinInd = _random.RandiRange(0,_spritesheetSkin.Length-1);
		
		HairHue = _random.Randf();
		HairSat = _random.Randf();
		HairVal = _random.Randf();
		
		EyesHue = _random.Randf();
		EyesSat = _random.Randf();
		EyesVal = _random.Randf();
		
		HandsHue = _random.Randf();
		HandsSat = _random.Randf();
		HandsVal = _random.Randf();
		
		ShirtHue = _random.Randf();
		ShirtSat = _random.Randf();
		ShirtVal = _random.Randf();
		
		PantsHue = _random.Randf();
		PantsSat = _random.Randf();
		PantsVal = _random.Randf();
		
		ShoesHue = _random.Randf();
		ShoesSat = _random.Randf();
		ShoesVal = _random.Randf();
		
		SkinHue = _random.Randf();
		SkinSat = _random.Randf();
		SkinVal = _random.Randf();
	}
	
	public void SetInd(string name, int ind) {
		switch(name) {
			case "Special":
				SpecialInd = ind;
				break;
			case "Hair":
				HairInd = ind;
				break;
			case "Eyes":
				EyesInd = ind;
				break;
			case "Hands":
				HandsInd = ind;
				break;
			case "Shirt":
				ShirtInd = ind;
				break;
			case "Pants":
				PantsInd = ind;
				break;
			case "Shoes":
				ShoesInd = ind;
				break;
			case "Skin":
				SkinInd = ind;
				break;
		}
	}
	
	public void AdjInd(string name, int ind) {
		switch(name) {
			case "Special":
				SpecialInd += ind;
				break;
			case "Hair":
				HairInd += ind;
				break;
			case "Eyes":
				EyesInd += ind;
				break;
			case "Hands":
				HandsInd += ind;
				break;
			case "Shirt":
				ShirtInd += ind;
				break;
			case "Pants":
				PantsInd += ind;
				break;
			case "Shoes":
				ShoesInd += ind;
				break;
			case "Skin":
				SkinInd += ind;
				break;
		}
	}
	
	public Color GetHSV(string name) {
		switch(name) {
			case "Hair":
				return Color.FromHsv(HairHue, HairSat, HairVal);
			case "Eyes":
				return Color.FromHsv(EyesHue, EyesSat, EyesVal);
			case "Hands":
				return Color.FromHsv(HandsHue, HandsSat, HandsVal);
			case "Shirt":
				return Color.FromHsv(ShirtHue, ShirtSat, ShirtVal);
			case "Pants":
				return Color.FromHsv(PantsHue, PantsSat, PantsVal);
			case "Shoes":
				return Color.FromHsv(ShoesHue, ShoesSat, ShoesVal);
			case "Skin":
				return Color.FromHsv(SkinHue, SkinSat, SkinVal);
		}
		return new Color(0,0,0);
	}
	
	public void SetHSV(string name, float hue, float sat, float val) {
		switch(name) {
			case "Hair":
				HairHue = hue;
				HairSat = sat;
				HairVal = val;
				break;
			case "Eyes":
				EyesHue = hue;
				EyesSat = sat;
				EyesVal = val;
				break;
			case "Hands":
				HandsHue = hue;
				HandsSat = sat;
				HandsVal = val;
				break;
			case "Shirt":
				ShirtHue = hue;
				ShirtSat = sat;
				ShirtVal = val;
				break;
			case "Pants":
				PantsHue = hue;
				PantsSat = sat;
				PantsVal = val;
				break;
			case "Shoes":
				ShoesHue = hue;
				ShoesSat = sat;
				ShoesVal = val;
				break;
			case "Skin":
				SkinHue = hue;
				SkinSat = sat;
				SkinVal = val;
				break;
		}
	}
	
	public void Play(string anim) {
		_ani.Play($"character/{anim}");
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	public void LoadSheets() {
		_loadedSheets = true;
		_spritesheetSpecial =
		[
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_special.png"),
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_special2.png")
		];
		_spritesheetHair = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_hair.png")
		};
		_spritesheetEyes = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_eyes.png")
		};
		_spritesheetHands = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_hands.png")
		};
		_spritesheetShirt = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_shirt.png")
		};
		_spritesheetPants = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_pants.png"),
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_pants2.png")
		};
		_spritesheetShoes = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_shoes.png")
		};
		_spritesheetSkin = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_skin.png")
		};
	}
}