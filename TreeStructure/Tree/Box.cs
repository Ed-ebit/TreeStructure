using TreeStructure.UI;

namespace TreeStructure.Tree
{
    internal class Box : INode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private INode? Parent { get; } = null;

        private readonly List<INode> children = new List<INode>();

        public Box() { }
        public Box(INode parent)
        {
            Parent = parent;
        }


        public void AppendChild(INode child)
        {
            children.Add(child);
        }

        public void Render()
        {
            Console.Clear();
            Console.WriteLine($"You see box: {Id}");
            Console.WriteLine(GetChildrenText());

            Console.WriteLine("What do you want to do?");

            var options = GetUIOptions();

            UIOption selectedOption = WaitForInput(options);
            selectedOption.Handler();

            Render();
        }

        public string GetChildrenText()
        {
            return children.Aggregate("Children: ", (current, child) => current + $"{child.Id}, ");
        }

        public UIOption WaitForInput(IList<UIOption> options)
        {
            var optionsText = options.Aggregate("", (current, option) => current + $"[{option.Key}] {option.Text} \n");
            Console.WriteLine(optionsText);

            while (true)
            {
                var input = Console.ReadLine();
                var selectedOption = options.FirstOrDefault((option) => option.Key == input, null);

                if (selectedOption != null)
                {
                    return selectedOption;
                }

                Console.WriteLine("Invalid input");
            }
        }

        private void SelectChild()
        {
            var options = children.Select((child, id) =>
            {
                return new UIOption()
                {
                    Key = (id + 1).ToString(),
                    Text = child.Id.ToString(),
                    Handler = child.Render
                };
            }).ToList();

            var selectedOption = WaitForInput(options);

            selectedOption.Handler();
        }
        public uint getPrice()
        {
            uint addedContentPrices = 0;
            if (children != null)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    addedContentPrices += children[i].getPrice();
                }
            }
            return addedContentPrices;
        }

        public void getPriceHandler()
        {
            Console.WriteLine(getPrice());
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private IList<UIOption> GetUIOptions()
        {
            var options = new List<UIOption>()
            {
                new UIOption()
                {
                    Key = "1",
                    Text = "Add Box",
                    Handler = ()=> {AppendChild(new Box(this)); }
                },
                new UIOption()
                {
                    Key = "2",
                    Text = "Add Product",
                    Handler = () => {AppendChild(new Product(this)); }
                }
            };

            if (children.Count > 0)
            {
                options.Add(new UIOption()
                {
                    Key = "3",
                    Text = "Navigate to child",
                    Handler = SelectChild
                });
                options.Add(new UIOption()
                {
                        Key = "4",
                        Text = "Show combined Price of all products within all contained boxes",
                        Handler = getPriceHandler
                });
            }

            if (Parent != null)
            {
                options.Add(new UIOption()
                {
                    Key = "5",
                    Text = "Navigate to parent",
                    Handler = Parent.Render
                });
            }

            return options;
        }
    }
}
