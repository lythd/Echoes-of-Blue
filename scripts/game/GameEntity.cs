namespace EchoesofBlue.scripts.game;

public class GameEntity
{
	public string Id { get; protected set; }
	
	public virtual string Name { get; protected set; }
	
	public virtual string Desc { get; protected set; }
	
	public override string ToString() => Id;
}