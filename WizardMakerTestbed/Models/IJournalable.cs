using WizardMakerPrototype.Models;

namespace WizardMakerTestbed.Models
{
    // TODO: Give it an ID field.  This way we can support searching and easier deletion.
    public interface IJournalable: ICommand
    {
        string getText();

        SeasonYear getDate();

        string getId();
    }
}
