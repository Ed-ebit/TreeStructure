using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeStructure.Tree
{
    internal class Product : INode
    {
        readonly uint price = 6000;

        public Guid Id { get; set; } = Guid.NewGuid();

        private INode? Parent { get; } = null;

        public Product (INode parent)
        {
            Parent = parent;
        }
        public void AppendChild(INode child)
        {
            throw new NotImplementedException();
        }

         public uint getPrice()
        {
            return price;
        }

        public void Render()
        {
            Console.WriteLine("Product price is: " + price);
            Console.WriteLine("To return, press any key");
            Console.ReadKey();
            Parent.Render();
        }

       
    }
}
