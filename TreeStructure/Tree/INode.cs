namespace TreeStructure.Tree
{
    internal interface INode
    {
        public Guid Id { get; }

        public INode? Parent { get; }

        public void Render();

        public void AppendChild(INode child);
    }
}
