using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Data", menuName = "Terrain Data")]
public class TerrainData : ScriptableObject {
    [SerializeField] private GameObject terrain;
    [SerializeField] private int maxInSuccession;
    [SerializeField] private int minInSucession;



    
    public GameObject getTerrain()
    {
        return this.terrain;
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
