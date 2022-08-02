using WizardMakerPrototype.Models;

namespace WizardMakerTestbed.Models
{
    public interface Journalable: ICommand
    {
        string getText();

        // TODO: We should have a class for season.
        SeasonYear getDate();
    }
}
