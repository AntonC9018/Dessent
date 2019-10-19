public abstract class SpellBase
{
    public abstract SpellName spellName
    {
        get;
    }

    public abstract int manacost {
        get; set;
    }

    public virtual int requiredNumberPhases
    {
        get;
    } = 1;

    public abstract SpellType spellType
    {
        get;
    }

    // Construct and make a request to gm 
    // This function is put in ApplyActionStruct inside gm
    // as a callback to clicking a cell with this spell selected
    public abstract void Request(Cell cell, StateManager sm);

    // Receive a response after the request and 
    // do something based on the response
    public abstract void RealizeResponse(Response response, StateManager sm, bool animate);

}