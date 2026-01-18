public struct PanelInfo
{
    public string PanelName { get; }
    public string Description { get; }
    public string AttributeText { get; }

    public PanelInfo(string panelName, string description, string attributeText)
    {
        PanelName = panelName;
        Description = description;
        AttributeText = attributeText;
    }
}
