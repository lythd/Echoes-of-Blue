using System;
using System.Collections.Generic;
using System.Linq;
using EchoesofBlue.scripts.game;
using Newtonsoft.Json;

namespace EchoesofBlue.scripts.serialization;

public class LootTable<T>
{
    [JsonProperty("COMMON")] public List<T> Common { get; set; } = [];
    [JsonProperty("UNCOMMON")] public List<T> Uncommon { get; set; } = [];
    [JsonProperty("RARE")] public List<T> Rare { get; set; } = [];
    [JsonProperty("VERYRARE")] public List<T> VeryRare { get; set; } = [];

    [JsonIgnore]
    public T Drop => new Random().NextSingle() switch
    {
        >= .00f and < .59f => DropSpecific(Common),         // 59% chance, with the standard number that's a ~20% chance per item
        >= .59f and < .89f => DropSpecific(Uncommon),       // 30% chance, with the standard number that's a  10% chance per item
        >= .89f and < .99f => DropSpecific(Rare),           // 10% chance, with the standard number that's a   5% chance per item
        _ => DropSpecific(VeryRare),                        //  1% chance, with the standard number that's a   1% chance per item
    };

    private static T DropSpecific(List<T> list) => list.Count > 0 ? list[new Random().Next(list.Count)] : default;
}