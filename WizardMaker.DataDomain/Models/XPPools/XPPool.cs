using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using WizardMaker.DataDomain.Models.CharacterPersist;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

// This class expects the caller to manage the XP expenditures in a pool and make sure that it is not overdrawn.
public abstract class XPPool
{
    public string Name        { get; }
    public string Description { get; }
    public int    InitialXP   { get; set; }
    public int    RemainingXP { get; set; }

    public XPPool(string name, string description, int initialXP)
    {
        this.Name        = name;
        this.Description = description;
        this.InitialXP   = initialXP;
        this.RemainingXP = initialXP;
    }

    public virtual void Reset()
    {
        RemainingXP = InitialXP; 
    }

    public virtual void SpendXP(int xp)
    {
        if (this.RemainingXP < xp)
        {
            throw new XPPoolOverdrawnException("XP Pool " + Name + " was asked for too many XP: " + xp + " > " + this.RemainingXP);
        }
        this.RemainingXP -= xp;
    }

    // TODO: Do we need to be able to serialize and deserialize XP Pools?
    public static XPPool? DeserializeJson(string json)
    {
        var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
        var result   = JsonConvert.DeserializeObject(json, settings) as XPPool;
        return result;
    }

    public virtual string SerializeJson()
    {
        var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
        var result   = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        return result;
    }

    abstract public bool CanSpendOnAbility(ArchAbility archAbility);

    abstract public int SortOrder();

    // TODO: This seems more difficult than it should be.
    // TODO: If we do not need to serialize and deserialize XPPools, do we need this method?  It was meant for testing.
    // This method is only used for testing.
    // We do not want to override the Equals behavior,
    // because we typically do not want two XPPools with the same stats to be considered equal.  
    public virtual bool IsSameSpecs(XPPool other)
    {
        if (other == null) return false;
        if (this.GetType()   != other.GetType())         return false;
        if (this.Name        != other.Name)              return false;
        if (this.Description != other.Description)       return false;
        if (!this.RemainingXP.Equals(other.RemainingXP)) return false;
        if (this.InitialXP   != other.InitialXP)         return false;
        
        return true;
    }
}
