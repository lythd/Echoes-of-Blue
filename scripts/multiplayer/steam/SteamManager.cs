using GodotSteam;
using Godot;

namespace EchoesofBlue.scripts.multiplayer.steam;

public partial class SteamManager : Node
{
	// TODO : Convert all gdscript code into c#, really annoying feeling like the project is arbitrarily split in two and having to work like inbetween like ugh
	
	private static bool _initialized;
	public static SteamManager Instance { get; private set; }
	
	public bool IsOwned;
	public int SteamAppId = 480; // Test game app id
	public ulong SteamId;
	public string SteamUsername = "";

	public int LobbyId = 0;
	public int LobbyMaxMembers = 10;

	public SteamManager()
	{
		if(_initialized) return;
		_initialized = true;
		Instance = this;
		GD.Print("Init Steam");
		OS.SetEnvironment("SteamAppId", SteamAppId.ToString());
		OS.SetEnvironment("SteamGameId", SteamAppId.ToString());
	}

	public override void _Process(double delta)
	{
		Steam.RunCallbacks();
	}
		
	public void InitializeSteam()
	{
		var initializeResponse = Steam.SteamInitEx(true);
		GD.Print($"Did Steam Initialize?: {initializeResponse.Verbal}");
		
		if(initializeResponse.Status != SteamInitExStatus.SteamworksActive)
		{
			if(initializeResponse.Status == SteamInitExStatus.Failed)
				GD.Print($"Failed to init Steam! Shutting down. Verbal: {initializeResponse.Verbal}.");
			else if(initializeResponse.Status == SteamInitExStatus.CannotConnectToSteam)
				GD.Print($"Cannot connect to Steam! Shutting down. Verbal: {initializeResponse.Verbal}.");
			else if(initializeResponse.Status == SteamInitExStatus.SteamClientOutOfDate)
				GD.Print($"Steam client out of date! Shutting down. Verbal: {initializeResponse.Verbal}.");
			else // should never run
				GD.Print($"Error trying to init Steam! Shutting down. Verbal: {initializeResponse.Verbal}.");
			GetTree().Quit();
			return;
		}
			
		IsOwned = Steam.IsSubscribed();
		SteamId = Steam.GetSteamID();
		SteamUsername = Steam.GetPersonaName();

		GD.Print($"Steam Id is {SteamId}");
		GD.Print($"Steam Username is {SteamUsername}");

		if (IsOwned) return;
		GD.Print("User does not own game!");
		GetTree().Quit();
	}
}