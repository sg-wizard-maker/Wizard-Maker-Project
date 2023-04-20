using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

public class XPPoolComparer : IComparer<XPPool>
{
    public int Compare(XPPool xx, XPPool yy)
    {
        int result;
        if (xx.SortOrder() == yy.SortOrder())
        {
            result = new CaseInsensitiveComparer().Compare(xx.Name, yy.Name);
            return result;
        }
        result = xx.SortOrder().CompareTo(yy.SortOrder());
        return result;
    }
}
