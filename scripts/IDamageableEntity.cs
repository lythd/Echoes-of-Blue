using Godot;

namespace EchoesofBlue.scripts;

public interface IDamageableEntity
{
	public int Health { get; set; }
	public int MaxHealth { get; set; }
	public int Damage { get; set; }
	public Vector2 Pos { get; }
	public bool Flip { get; set; }
	public int AttackOffset { get; set; }
	public float KbResistance { get; set; }
	public float KbStrength { get; set; }
	public Vector2 Kb { get; set; }
	
	public void TakeAttack(Attack attack);
	public void ClearAttack();
}