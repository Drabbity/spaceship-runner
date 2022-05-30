public class HighScorePrinter : Printer
{
    protected override string GetInformationToDisplay()
    {
        return GameManager.Instance.HighScore.ToString();
    }
}
