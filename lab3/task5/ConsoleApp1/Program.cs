using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
     
    abstract class LightNode
    {
        public abstract string OuterHTML { get; }
    }

    class LightTextNode : LightNode
    {
        public string Text { get; }

        public LightTextNode(string text)
        {
            Text = text;
        }

        public override string OuterHTML => Text;
    }

    class LightElementNode : LightNode
    {
        public string TagName { get; }
        public bool IsBlock { get; }
        public bool IsSelfClosing { get; }
        public List<string> CssClasses { get; } = new List<string>();
        public List<LightNode> Children { get; } = new List<LightNode>();

        public LightElementNode(string tagName, bool isBlock = true, bool isSelfClosing = false)
        {
            TagName = tagName;
            IsBlock = isBlock;
            IsSelfClosing = isSelfClosing;
        }

        public void AddClass(string className)
        {
            CssClasses.Add(className);
        }

        public void AddChild(LightNode child)
        {
            if (!IsSelfClosing)
                Children.Add(child);
        }

        public string InnerHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var child in Children)
                {
                    sb.Append(child.OuterHTML);
                }
                return sb.ToString();
            }
        }

        public override string OuterHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"<{TagName}");

                if (CssClasses.Count > 0)
                {
                    sb.Append(" class=\"");
                    sb.Append(string.Join(" ", CssClasses));
                    sb.Append("\"");
                }

                if (IsSelfClosing)
                {
                    sb.Append(" />");
                }
                else
                {
                    sb.Append(">");
                    sb.Append(InnerHTML);
                    sb.Append($"</{TagName}>");
                }
                return sb.ToString();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            LightElementNode div = new LightElementNode("div");
            div.AddClass("container");

            LightElementNode ul = new LightElementNode("ul");
            ul.AddClass("list");

            LightElementNode li1 = new LightElementNode("li", false);
            li1.AddChild(new LightTextNode("Перший елемент"));

            LightElementNode li2 = new LightElementNode("li", false);
            li2.AddChild(new LightTextNode("Другий елемент"));

            ul.AddChild(li1);
            ul.AddChild(li2);
            div.AddChild(ul);

            Console.WriteLine(div.OuterHTML);
        }
    }

    
}
