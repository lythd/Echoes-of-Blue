using Godot;
using System;

public partial class Character : Node2D
{
	
	public static Texture2D[] SpritesheetSpecial;
	public static Texture2D[] SpritesheetHair;
	public static Texture2D[] SpritesheetEyes;
	public static Texture2D[] SpritesheetHands;
	public static Texture2D[] SpritesheetShirt;
	public static Texture2D[] SpritesheetPants;
	public static Texture2D[] SpritesheetShoes;
	public static Texture2D[] SpritesheetSkin;
	
	public static bool loaded_sheets = false;
	
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
	
	private RandomNumberGenerator random = new RandomNumberGenerator();
	
	[Export]
	public int SpecialInd {
		get { return _specialInd; }
		set
		{
			if(_special == null) return;
			_specialInd = value % SpritesheetSpecial.Length;
			_special.Texture = SpritesheetSpecial[_specialInd];
		}
	}
	
	[Export]
	public int HairInd {
		get { return _hairInd; }
		set
		{
			if(_hair == null) return;
			_hairInd = value % SpritesheetHair.Length;
			_hair.Texture = SpritesheetHair[_hairInd];
		}
	}
	
	[Export]
	public int EyesInd {
		get { return _eyesInd; }
		set
		{
			if(_eyes == null) return;
			_eyesInd = value % SpritesheetEyes.Length;
			_eyes.Texture = SpritesheetEyes[_eyesInd];
		}
	}
	
	[Export]
	public int HandsInd {
		get { return _handsInd; }
		set
		{
			if(_hands == null) return;
			_handsInd = value % SpritesheetHands.Length;
			_hands.Texture = SpritesheetHands[_handsInd];
		}
	}
	
	[Export]
	public int ShirtInd {
		get { return _shirtInd; }
		set
		{
			if(_shirt == null) return;
			_shirtInd = value % SpritesheetShirt.Length;
			_shirt.Texture = SpritesheetShirt[_shirtInd];
		}
	}
	
	[Export]
	public int PantsInd {
		get { return _pantsInd; }
		set
		{
			if(_pants == null) return;
			_pantsInd = value % SpritesheetPants.Length;
			_pants.Texture = SpritesheetPants[_pantsInd];
		}
	}
	
	[Export]
	public int ShoesInd {
		get { return _shoesInd; }
		set
		{
			if(_shoes == null) return;
			_shoesInd = value % SpritesheetShoes.Length;
			_shoes.Texture = SpritesheetShoes[_shoesInd];
		}
	}
	
	[Export]
	public int SkinInd {
		get { return _skinInd; }
		set
		{
			if(_skin == null) return;
			_skinInd = value % SpritesheetSkin.Length;
			_skin.Texture = SpritesheetSkin[_skinInd];
		}
	}
	
	[Export]
	public float HairHue {
		get { return _hair.SelfModulate.H; }
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(value,_hair.SelfModulate.S,_hair.SelfModulate.V);
		}
	}
	
	[Export]
	public float HairSat {
		get { return _hair.SelfModulate.S; }
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(_hair.SelfModulate.H,value,_hair.SelfModulate.V);
		}
	}
	
	[Export]
	public float HairVal {
		get { return _hair.SelfModulate.V; }
		set 
		{ 
			if(_hair == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hair.SelfModulate = Color.FromHsv(_hair.SelfModulate.H,_hair.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float EyesHue {
		get { return _eyes.SelfModulate.H; }
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(value,_eyes.SelfModulate.S,_eyes.SelfModulate.V);
		}
	}
	
	[Export]
	public float EyesSat {
		get { return _eyes.SelfModulate.S; }
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(_eyes.SelfModulate.H,value,_eyes.SelfModulate.V);
		}
	}
	
	[Export]
	public float EyesVal {
		get { return _eyes.SelfModulate.V; }
		set 
		{ 
			if(_eyes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_eyes.SelfModulate = Color.FromHsv(_eyes.SelfModulate.H,_eyes.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float HandsHue {
		get { return _hands.SelfModulate.H; }
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(value,_hands.SelfModulate.S,_hands.SelfModulate.V);
		}
	}
	
	[Export]
	public float HandsSat {
		get { return _hands.SelfModulate.S; }
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(_hands.SelfModulate.H,value,_hands.SelfModulate.V);
		}
	}
	
	[Export]
	public float HandsVal {
		get { return _hands.SelfModulate.V; }
		set 
		{ 
			if(_hands == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_hands.SelfModulate = Color.FromHsv(_hands.SelfModulate.H,_hands.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float ShirtHue {
		get { return _shirt.SelfModulate.H; }
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(value,_shirt.SelfModulate.S,_shirt.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShirtSat {
		get { return _shirt.SelfModulate.S; }
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(_shirt.SelfModulate.H,value,_shirt.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShirtVal {
		get { return _shirt.SelfModulate.V; }
		set 
		{ 
			if(_shirt == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shirt.SelfModulate = Color.FromHsv(_shirt.SelfModulate.H,_shirt.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float PantsHue {
		get { return _pants.SelfModulate.H; }
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(value,_pants.SelfModulate.S,_pants.SelfModulate.V);
		}
	}
	
	[Export]
	public float PantsSat {
		get { return _pants.SelfModulate.S; }
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(_pants.SelfModulate.H,value,_pants.SelfModulate.V);
		}
	}
	
	[Export]
	public float PantsVal {
		get { return _pants.SelfModulate.V; }
		set 
		{ 
			if(_pants == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_pants.SelfModulate = Color.FromHsv(_pants.SelfModulate.H,_pants.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float ShoesHue {
		get { return _shoes.SelfModulate.H; }
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(value,_shoes.SelfModulate.S,_shoes.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShoesSat {
		get { return _shoes.SelfModulate.S; }
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(_shoes.SelfModulate.H,value,_shoes.SelfModulate.V);
		}
	}
	
	[Export]
	public float ShoesVal {
		get { return _shoes.SelfModulate.V; }
		set 
		{ 
			if(_shoes == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_shoes.SelfModulate = Color.FromHsv(_shoes.SelfModulate.H,_shoes.SelfModulate.S,value);
		}
	}
	
	[Export]
	public float SkinHue {
		get { return _skin.SelfModulate.H; }
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(value,_skin.SelfModulate.S,_skin.SelfModulate.V);
		}
	}
	
	[Export]
	public float SkinSat {
		get { return _skin.SelfModulate.S; }
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(_skin.SelfModulate.H,value,_skin.SelfModulate.V);
		}
	}
	
	[Export]
	public float SkinVal {
		get { return _skin.SelfModulate.V; }
		set 
		{ 
			if(_skin == null) return;
			if(value <= 1.0f && value >= 0.0f)
				_skin.SelfModulate = Color.FromHsv(_skin.SelfModulate.H,_skin.SelfModulate.S,value);
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	public override void _Ready()
	{
		if(!loaded_sheets) LoadSheets();
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
		random.Randomize();
		
		SpecialInd = random.RandiRange(0,SpritesheetSpecial.Length-1);
		HairInd = random.RandiRange(0,SpritesheetHair.Length-1);
		EyesInd = random.RandiRange(0,SpritesheetEyes.Length-1);
		HandsInd = random.RandiRange(0,SpritesheetHands.Length-1);
		ShirtInd = random.RandiRange(0,SpritesheetShirt.Length-1);
		PantsInd = random.RandiRange(0,SpritesheetPants.Length-1);
		ShoesInd = random.RandiRange(0,SpritesheetShoes.Length-1);
		SkinInd = random.RandiRange(0,SpritesheetSkin.Length-1);
		
		HairHue = random.Randf();
		HairSat = random.Randf();
		HairVal = random.Randf();
		
		EyesHue = random.Randf();
		EyesSat = random.Randf();
		EyesVal = random.Randf();
		
		HandsHue = random.Randf();
		HandsSat = random.Randf();
		HandsVal = random.Randf();
		
		ShirtHue = random.Randf();
		ShirtSat = random.Randf();
		ShirtVal = random.Randf();
		
		PantsHue = random.Randf();
		PantsSat = random.Randf();
		PantsVal = random.Randf();
		
		ShoesHue = random.Randf();
		ShoesSat = random.Randf();
		ShoesVal = random.Randf();
		
		SkinHue = random.Randf();
		SkinSat = random.Randf();
		SkinVal = random.Randf();
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
		loaded_sheets = true;
		SpritesheetSpecial = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_special.png"),
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_special2.png")
		};
		SpritesheetHair = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_hair.png")
		};
		SpritesheetEyes = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_eyes.png")
		};
		SpritesheetHands = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_hands.png")
		};
		SpritesheetShirt = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_shirt.png")
		};
		SpritesheetPants = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_pants.png"),
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_pants2.png")
		};
		SpritesheetShoes = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_shoes.png")
		};
		SpritesheetSkin = new Texture2D[]
		{
			(Texture2D)ResourceLoader.Load("res://assets/sprites/character_skin.png")
		};
	}
}
