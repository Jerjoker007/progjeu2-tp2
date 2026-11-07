using UnityEngine;

public class SocializingState : BuildingState
{
    protected override void UpdateCharacter()
    {
        vitals.LowerLoneliness();
    }

    protected override void ManageStateChange()
    {
        if (!vitals.IsLonelinessBellowTarget) return;

        SetNewDestination();
        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
