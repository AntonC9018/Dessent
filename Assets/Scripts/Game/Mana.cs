using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public StateManager gameState;
    public int maxMana;
    public int defaultMaxMana = 1;
    public int currentMana = 0;
    public int mps; // mana per second
    public int defaultMPS;

    public void GatherManaFromBuildings() {
        int mana = defaultMPS;

        // loop through the player's buildings
        // let them all produce their mana
        foreach (Cell cell in gameState.publicGrid.cells)
        {
            mana += cell.building.GetMana();
        }

        // calculate how much mana the player
        // will receive from the opponent's buildings
        // (assume they are iluminated by default)
        foreach (Cell cell in gameState.privateGrid.cells)
        {
            mana += cell.building.GetMana();
        }

        currentMana += mana;        
    }

    public void RecalculateMPS() {

        int mps = defaultMPS;

        // loop through the player's buildings
        // calculate how much mana they are going to produce
        foreach (Cell cell in gameState.publicGrid.cells)
        {
            mps += cell.building.GetMPS();
        }

        // calculate how much mana the player
        // will receive from the opponent's buildings
        // (assume they are iluminated by default)
        foreach (Cell cell in gameState.privateGrid.cells)
        {
            mps += cell.building.GetMPS();
        }

        this.mps = mps;
    }


    public void RecalculateMaxMana() {
        int maxMana = defaultMaxMana;

        // loop through the player's buildings
        // let them all produce their mana
        foreach (Cell cell in gameState.publicGrid.cells)
        {
            maxMana += cell.building.GetManaCapacity();
        }

        // calculate how much mana the player
        // will receive from the opponent's buildings
        // (assume they are iluminated by default)
        foreach (Cell cell in gameState.privateGrid.cells)
        {
            maxMana += cell.building.GetManaCapacity();
        }

        this.maxMana = maxMana;
    }


    public void UseMana(int amount) {
        if (currentMana < amount) {
            Debug.LogError("Not enough mana");
        } else {
            currentMana -= amount;
        }
    }


    public void LimitMana() {
        currentMana = currentMana > maxMana ? maxMana : currentMana;
    }


    public void UpdateMana(TurnPhase phase) {
        if (phase == TurnPhase.Start) {
            RecalculateMPS();
            GatherManaFromBuildings();
        }
        else if (phase == TurnPhase.End) {
            RecalculateMaxMana();
            LimitMana();
        }
    }
}