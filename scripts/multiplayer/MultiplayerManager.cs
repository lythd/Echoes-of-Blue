using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Numerics;
using System;
using System.Collections.Generic;
using Godot;

public partial class MultiplayerManager : Node
{
	private static bool _initialized = false;
	public static MultiplayerManager Instance { get; private set; }
	
	public bool HostModeEnabled = false;
	public bool MultiplayerModeEnabled { get; set; } = false;
	public Godot.Vector2 RespawnPoint = new Godot.Vector2(30, 20);
	
	public string GAME_VERSION { get; private set; } = "v0.0.2d";
	public string GAME_HASH { get; private set; }
	
	public override void _Ready() {
		if(_initialized) return;
		Instance = this;
		_initialized = true;
		GAME_HASH = CalculateGameHash();
	}
	
	public string CalculateGameHash()
	{
		using (SHA256 sha256Hash = SHA256.Create())
		{
			byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("AAAAAAAA"));
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				builder.Append(bytes[i].ToString("x2"));
			}
			return builder.ToString();
		}
	}

	public bool CompareVersions(string gameVersion, string gameHash)
	{
		var theirVersion = ParseVersion(gameVersion);
		var ourVersion = ParseVersion(GAME_VERSION);
		bool alike = theirVersion["phase"].Equals(ourVersion["phase"]);
		alike = alike && theirVersion["major"].Equals(ourVersion["major"]);
		alike = alike && gameHash.Equals(GAME_HASH);
		return alike;
	}

	public Dictionary<string, object> ParseVersion(string version)
	{
		string versionStrip = version.Trim().Substring(version.StartsWith("v") ? 1 : 0).TrimEnd('d');
		string[] parts = versionStrip.Split('.');
		string phase = "";
		int major = 0;
		int minor = 0;
		bool dev = version.Contains('d');
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
