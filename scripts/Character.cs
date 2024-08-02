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
	
	public int SpecialInd {
		get { return _specialInd; }
		set
		{
			_specialInd = value % SpritesheetSpecial.Length;
			_special.Texture = SpritesheetSpecial[_specialInd];
		}
	}
	
	public int HairInd {
		get { return _hairInd; }
		set
		{
			_hairInd = value % SpritesheetHair.Length;
			_hair.Texture = SpritesheetHair[_hairInd];
		}
	}
	
	public int EyesInd {
		get { return _eyesInd; }
		set
		{
			_eyesInd = value % SpritesheetEyes.Length;
			_eyes.Texture = SpritesheetEyes[_eyesInd];
		}
	}
	
	public int HandsInd {
		get { return _handsInd; }
		set
		{
			_handsInd = value % SpritesheetHands.Length;
			_hands.Texture = SpritesheetHands[_handsInd];
		}
	}
	
	public int ShirtInd {
		get { return _shirtInd; }
		set
		{
			_shirtInd = value % SpritesheetShirt.Length;
			_shirt.Texture = SpritesheetShirt[_shirtInd];
		}
	}
	
	public int PantsInd {
		get { return _pantsInd; }
		set
		{
			_pantsInd = value % SpritesheetPants.Length;
			_pants.Texture = SpritesheetPants[_pantsInd];
		}
	}
	
	public int ShoesInd {
		get { return _shoesInd; }
		set
		{
			_shoesInd = value % SpritesheetShoes.Length;
			_shoes.Texture = SpritesheetShoes[_shoesInd];
		}
	}
	
	public int SkinInd {
		get { return _skinInd; }
		set
		{
			_skinInd = value % SpritesheetSkin.Length;
			_skin.Texture = SpritesheetSkin[_skinInd];
		}
	}
	
	public float HairHue {
		get { return (float)(_hair.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hair.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float HairSat {
		get { return (float)(_hair.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hair.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float HairVal {
		get { return (float)(_hair.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hair.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float EyesHue {
		get { return (float)(_eyes.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_eyes.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float EyesSat {
		get { return (float)(_eyes.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_eyes.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float EyesVal {
		get { return (float)(_eyes.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_eyes.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float HandsHue {
		get { return (float)(_hands.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hands.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float HandsSat {
		get { return (float)(_hands.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hands.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float HandsVal {
		get { return (float)(_hands.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_hands.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float ShirtHue {
		get { return (float)(_shirt.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shirt.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float ShirtSat {
		get { return (float)(_shirt.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shirt.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float ShirtVal {
		get { return (float)(_shirt.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shirt.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float PantsHue {
		get { return (float)(_pants.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_pants.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float PantsSat {
		get { return (float)(_pants.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_pants.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float PantsVal {
		get { return (float)(_pants.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_pants.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float ShoesHue {
		get { return (float)(_shoes.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shoes.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float ShoesSat {
		get { return (float)(_shoes.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shoes.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float ShoesVal {
		get { return (float)(_shoes.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_shoes.Material as ShaderMaterial).SetShaderParameter("value", value);
		}
	}
	
	public float SkinHue {
		get { return (float)(_skin.Material as ShaderMaterial).GetShaderParameter("hue"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_skin.Material as ShaderMaterial).SetShaderParameter("hue", value);
		}
	}
	
	public float SkinSat {
		get { return (float)(_skin.Material as ShaderMaterial).GetShaderParameter("saturation"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_skin.Material as ShaderMaterial).SetShaderParameter("saturation", value);
		}
	}
	
	public float SkinVal {
		get { return (float)(_skin.Material as ShaderMaterial).GetShaderParameter("value"); }
		set 
		{ 
			if(value <= 1.0f && value >= 0.0f)
				(_skin.Material as ShaderMaterial).SetShaderParameter("value", value);
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
