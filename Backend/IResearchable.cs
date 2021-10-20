namespace Edison
{
    public interface IResearchable
    {
        ResearchPointsValue CurrentPrice { get; }
        void Get();
    }
}
