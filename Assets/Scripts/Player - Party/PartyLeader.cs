using System.Collections.Generic;
using UnityEngine;

public class PartyLeader : MonoBehaviour
{
    public enum PartyMemebers { First }
    public PartyMemebers partyMemebers = PartyMemebers.First;

    [Header("Trail Settings")]
    public int maxPositions = 1000; 
    public List<Vector2> positions = new List<Vector2>();

    void Update()
    {        
        positions.Insert(0, transform.position);

        if (positions.Count > maxPositions)
            positions.RemoveAt(positions.Count - 1);
    }
}
