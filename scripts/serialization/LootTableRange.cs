using System;
using System.Collections.Generic;
using System.Linq;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class LootTableRange<T>
{
    [JsonProperty("COMMON")] public Dictionary<T, Range> Common { get; set; } = new();
    [JsonProperty("UNCOMMON")] public Dictionary<T, Range> Uncommon { get; set; } = new();
    [JsonProperty("RARE")] public Dictionary<T, Range> Rare { get; set; } = new();
    [JsonProperty("VERYRARE")] public Dictionary<T, Range> VeryRare { get; set; } = new();

    [JsonIgnore]
    public (T,long) Drop => new Random().NextSingle() switch
    {
        >= .00f and < .59f => DropSpecific(Common),         // 59% chance, with the standard number that's a ~20% chance per item
        >= .59f and < .89f => DropSpecific(Uncommon),       // 30% chance, with the standard number that's a  10% chance per item
        >= .89f and < .99f => DropSpecific(Rare),           // 10% chance, with the standard number that's a   5% chance per item
        _ => DropSpecific(VeryRare),                        //  1% chance, with the standard number that's a   1% chance per item
    };

    private static (T, long) DropSpecific(Dictionary<T, Range> dict)
    {
        if (dict.Count == 0) return (default, 0);
        var ind = new Random().Next(dict.Count);
        return (dict.Keys.ToArray()[ind], dict.Values.ToArray()[ind].Value);
    }
}