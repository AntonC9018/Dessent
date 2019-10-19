//public class BuffSpellTile : SelectableActionTile
//{
//    public BuffSpell buffSpell;

//    public override void ApplyAction(Cell cell)
//    {
//        if (stateManager.mana.currentMana < buffSpell.manacost)
//        {
//            print("Low Mana");
//        }
//        else if (!cell.parentGrid.IsPublic())
//        {
//            print("Choose opponent's tile");
//        }
//        else if (buffSpell.requiredNumberPhases >= stateManager.selectedAction.phase)
//        {
//            buffSpell.Request(cell, stateManager);
//            stateManager.ResetSelectedAction();
//        }
//        else
//        {
//            stateManager.ProgressSelectedActionPhase(cell);
//        }
//    }
//}