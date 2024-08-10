using Godot;
using System;
using GodotSteam;

public partial class SteamManager : Node
{
	private static bool _initialized = false;
	public static SteamManager Instance { get; private set; }
	
	public bool IsOwned = false;
	public int SteamAppId = 480; // Test game app id
	public ulong SteamId = 0;
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
		
		if(!IsOwned)
		{
			GD.Print("User does not own game!");
			GetTree().Quit();
		}
	}
}
