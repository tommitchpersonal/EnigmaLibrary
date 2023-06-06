public class EnigmaSettings
{
    public WheelSetting[]? WheelSettings {get; set;}

    public bool IsValid()
    {
        if (WheelSettings == null)
        {
            return false;
        }

        foreach (var setting in WheelSettings)
        {
            if (!setting.IsValid())
            {
                return false;
            }
        }

        return true;
    }

}

public class WheelSetting
{
    public int[]? Mappings {get; set;}

    public bool IsValid()
    {
        if (Mappings == null)
        {
            return false;
        }

        if (Mappings.Count() != 26)
        { 
            return false;    
        }

        if (Mappings.Any(s => s < 0 || s > 25))
        {
            return false;
        }

        if (Mappings.Distinct().Count() != Mappings.Count())
        {
            return false;
        }    

        return true;
    }
}