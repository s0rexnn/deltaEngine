using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [Header("Current Party Members")]
    public GameObject member1; // Leader
    public GameObject member2;
    public GameObject member3;

    [Header("Party Info")]
    public int currentPartySize = 3; // Default to 3

    void Start()
    {
        UpdatePartySize();
    }

    void UpdatePartySize()
    {
        int partySize = 0;
        if (member1 != null) partySize++;
        if (member2 != null) partySize++;
        if (member3 != null) partySize++;
        currentPartySize = partySize;
    }
  
}
