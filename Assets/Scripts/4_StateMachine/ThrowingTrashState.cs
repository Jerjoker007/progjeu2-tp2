using UnityEngine;

public class ThrowingTrashState : BaseState
{
    void OnEnable()
    {
        character.ThrowTrash();
    }

    protected override void UpdateCharacter()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }

    protected override void ManageStateChange()
    {
        if (character.IsThrowingTrash()) return;

        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
