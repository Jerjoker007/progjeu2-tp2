using UnityEngine;

public class GreetingState : BaseState
{
    void OnEnable()
    {
        character.GreetCharacter(blackboard.LastFriendSeen);
    }

    protected override void UpdateCharacter()
    {
        vitals.LowerLoneliness();
    }

    protected override void ManageStateChange()
    {
        if (character.IsGreetingCharacter()) return;

        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
