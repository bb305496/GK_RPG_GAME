using System.Collections.Generic;
using UnityEngine;

public class DialogueHistoryTracker : MonoBehaviour
{
    public static DialogueHistoryTracker Instance;
    private readonly List<ActorSO> spokenNPCs = new List<ActorSO>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    public void RecordNpc(ActorSO actorSo)
    {
        spokenNPCs.Add(actorSo);

        Debug.Log($"NPC {actorSo.actorName} has been recorded in dialogue history.");
    }

    public bool HasSpokneWith(ActorSO actorSo)
    {
        return spokenNPCs.Contains(actorSo);
    }
}
