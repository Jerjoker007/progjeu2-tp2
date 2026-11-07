using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    private Character character;
    private CharacterBlackboard blackboard;
    private CharacterStateMachine stateManager;

    void Awake()
    {
        character = GetComponent<Character>();
        blackboard = GetComponent<CharacterBlackboard>();
        stateManager = GetComponent<CharacterStateMachine>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!character.IsAvailable) return;

        if (other.CompareTag("Trash") && stateManager.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            Trash trash = other.GetComponent<Trash>();
            if (!trash.IsAvailable || blackboard.LastTrashSeen.Contains(trash)) return;
            
            blackboard.LastTrashSeen.Add(trash);
        }
        else if (!other.isTrigger && other.CompareTag("Character"))
        {
            Character friend = other.GetComponent<Character>();
            if (!friend.IsAvailable ||!blackboard.IsFriend(friend)) return;

            blackboard.LastFriendSeen = friend;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash") && stateManager.TrashBehaviour == CityCharacterTrashBehaviour.PickUp)
        {
            Trash trash = other.GetComponent<Trash>();
            if (blackboard.LastTrashSeen.Contains(trash)) return;

            blackboard.LastTrashSeen.Remove(trash);
        }
        else if (!other.isTrigger && other.CompareTag("Character"))
        {
            Character friend = other.GetComponent<Character>();
            if (blackboard.LastFriendSeen != friend) return;
            
            blackboard.LastFriendSeen = null;
        }
    }
}
