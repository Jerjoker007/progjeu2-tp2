using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    private CharacterBlackboard blackboard;

    void Awake()
    {
        blackboard = GetComponent<CharacterBlackboard>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        if (other.CompareTag("Trash") && blackboard.ShouldThrowTrash)
        {
            Trash trash = other.GetComponent<Trash>();
            if (!trash.IsAvailable) return;
            
            blackboard.LastTrashSeen = trash;
        }
        else if (other.CompareTag("Character"))
        {
            Character friend = other.GetComponent<Character>();
            if (!blackboard.IsFriend(friend)) return;

            blackboard.LastFriendSeen = friend;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;

        if (other.CompareTag("Trash"))
        {
            Trash trash = other.GetComponent<Trash>();
            if (blackboard.LastTrashSeen != trash) return;

            blackboard.LastTrashSeen = null;
        }
        else if (other.CompareTag("Character"))
        {
            Character friend = other.GetComponent<Character>();
            if (blackboard.LastFriendSeen != friend) return;
            
            blackboard.LastFriendSeen = null;
        }
    }
}
