using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject {
    [SerializeField] private List<GameObject> terrainKinds;
    [SerializeField] private int maxInSuccession;
    [SerializeField] private int minInSucession;

    
    public List<GameObject> getTerrainKinds()
    {
        return this.terrainKinds;
    }

    public int getMaxInSuccession()
    {
        return this.maxInSuccession;
    }

    public int getMinInSuccession()
    {
        return this.minInSucession;
    }


}
