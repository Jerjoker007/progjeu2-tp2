using System.Linq;
using UnityEngine;

public class MovingState : BaseState
{
    void OnEnable()
    {
        if (blackboard.LastDestination == null) SetNewDestination();
        character.NavigateTo(blackboard.LastDestination);
    }

    protected override void UpdateCharacter()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();

        if (blackboard.LastTrashSeen.Count <= 0) return;
        character.NavigateTo(blackboard.LastTrashSeen.First());
    }

    protected override void ManageStateChange()
    {
        if (blackboard.LastFriendSeen != null)
        {
            stateManager.ChangeCharacterState(CharacterNextState.GreetingFriend);
            return;
        }

        if (stateManager.TrashBehaviour == CityCharacterTrashBehaviour.PickUp && blackboard.LastTrashSeen.Count > 0)
        {
            if (!character.IsCloseTo(blackboard.LastTrashSeen.First())) return;
            stateManager.ChangeCharacterState(CharacterNextState.PickingTrash);
            return;
        }

        if (stateManager.TrashBehaviour == CityCharacterTrashBehaviour.Throw && blackboard.ShouldThrowTrash)
        {
            stateManager.ChangeCharacterState(CharacterNextState.ThrowingTrash);
            return;
        }

        if (character.IsCloseTo(blackboard.LastDestination))
        {
            CharacterNextState nextState;

            switch (blackboard.LastDestination.Type)
            {
                case Building.CityBuildingType.Food:
                    nextState = CharacterNextState.Eating;
                    break;
                case Building.CityBuildingType.House:
                    nextState = CharacterNextState.Sleeping;
                    break;
                case Building.CityBuildingType.Social:
                    nextState = CharacterNextState.Sleeping;
                    break;
                default:
                    nextState = CharacterNextState.Working;
                    break;
            }
            blackboard.LastDestination = null;
            stateManager.ChangeCharacterState(nextState);
        }
    }
}
