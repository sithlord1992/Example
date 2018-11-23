using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CityDataContainer : ScriptableObject
{
    public RestaurantDataContainer[] restaurants;
}

[CreateAssetMenu]
public class RestaurantDataContainer : ScriptableObject
{
    public LevelDataContainer[] restaurants;
}

[CreateAssetMenu]
public class LevelDataContainer : ScriptableObject
{
    public string[] levels;
}