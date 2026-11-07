using System.Linq;
using UnityEngine;

public class PickingTrashState : BaseState
{
    void OnEnable()
    {
        character.PickUpTrash(blackboard.LastTrashSeen.First());
    }

    protected override void UpdateCharacter()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }

    protected override void ManageStateChange()
    {
        if (character.IsPickingTrash()) return;

        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
