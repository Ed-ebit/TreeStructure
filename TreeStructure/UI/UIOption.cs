namespace TreeStructure.UI
{
    internal class UIOption
    {
        public string Key { get; set; }

        public string Text { get; set; }

        public Action Handler { get; set; }
    }
}
