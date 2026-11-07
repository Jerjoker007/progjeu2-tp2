using UnityEngine;

public class SleepingState : BuildingState
{
    protected override void UpdateCharacter()
    {
        vitals.LowerSleepiness();
    }

    protected override void ManageStateChange()
    {
        if (!vitals.IsSleepinessBellowTarget) return;

        SetNewDestination();
        stateManager.ChangeCharacterState(CharacterNextState.Moving);
    }
}
