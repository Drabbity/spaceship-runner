public class ScorePrinter : Printer
{
    protected override string GetInformationToDisplay()
    {
        return GameManager.Instance.Score.ToString();
    }
}
