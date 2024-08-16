using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Godot;

namespace EchoesofBlue.scripts.multiplayer;

public partial class MultiplayerManager : Node
{
	private static bool _initialized;
	public static MultiplayerManager Instance { get; private set; }
	
	public bool HostModeEnabled = false;
	public bool MultiplayerModeEnabled { get; set; }
	public Godot.Vector2 RespawnPoint = new(30, 20);
	
	public string GAME_VERSION = "v0.0.2";
	public string GAME_HASH { get; private set; }
	
	public override void _Ready() {
		if(_initialized) return;
		Instance = this;
		_initialized = true;
		GAME_HASH = CalculateGameHash();
	}
	
	public string CalculateGameHash()
	{
		using var sha256Hash = SHA256.Create();
		var bytes = sha256Hash.ComputeHash("AAAAAAAA"u8.ToArray());
		var builder = new StringBuilder();
		foreach (var t in bytes) builder.Append(t.ToString("x2"));
		return builder.ToString();
	}

	public bool CompareVersions(string gameVersion, string gameHash)
	{
		var theirVersion = ParseVersion(gameVersion);
		var ourVersion = ParseVersion(GAME_VERSION);
		var alike = theirVersion["phase"].Equals(ourVersion["phase"]);
		alike = alike && theirVersion["major"].Equals(ourVersion["major"]);
		alike = alike && gameHash.Equals(GAME_HASH);
		return alike;
	}

	public Dictionary<string, object> ParseVersion(string version)
	{
		var versionStrip = version.Trim().Substring(version.StartsWith("v") ? 1 : 0).TrimEnd('d');
		var parts = versionStrip.Split('.');
		string phase;
		int major;
		var minor = 0;
		var dev = version.Contains('d');
		if (versionStrip.StartsWith("0.0"))
		{
			phase = "alpha";
			major = int.Parse(parts[2]);
			if (parts.Length > 3)
			{
				minor = int.Parse(parts[3]);
			}
		}
		else if (versionStrip.StartsWith("0"))
		{
			phase = "beta";
			major = int.Parse(parts[1]);
			if (parts.Length > 2)
			{
				minor = int.Parse(parts[2]);
			}
		}
		else
		{
			phase = "release";
			major = int.Parse(parts[1]);
			if (parts.Length > 2)
			{
				minor = int.Parse(parts[2]);
			}
		}
		return new Dictionary<string, object>()
		{
			{ "phase", phase },
			{ "major", major },
			{ "minor", minor },
			{ "development", dev }
		};
	}
}