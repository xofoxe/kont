using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        private ElementType _elementType;
        public List<string> CssClasses { get; } = new List<string>();
        public List<LightNode> Children { get; } = new List<LightNode>();

        public LightElementNode(ElementType elementType)
        {
            _elementType = elementType;
        }

        public void AddClass(string className)
        {
            CssClasses.Add(className);
        }

        public void AddChild(LightNode child)
        {
            if (!_elementType.IsSelfClosing)
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
                sb.Append($"<{_elementType.TagName}");

                if (CssClasses.Count > 0)
                {
                    sb.Append(" class=\"");
                    sb.Append(string.Join(" ", CssClasses));
                    sb.Append("\"");
                }

                if (_elementType.IsSelfClosing)
                {
                    sb.Append(" />");
                }
                else
                {
                    sb.Append(">");
                    sb.Append(InnerHTML);
                    sb.Append($"</{_elementType.TagName}>");
                }
                return sb.ToString();
            }
        }
    }

    class ElementType
    {
        public string TagName { get; }
        public bool IsBlock { get; }
        public bool IsSelfClosing { get; }

        public ElementType(string tagName, bool isBlock, bool isSelfClosing)
        {
            TagName = tagName;
            IsBlock = isBlock;
            IsSelfClosing = isSelfClosing;
        }
    }

    class ElementTypeFactory
    {
        private Dictionary<string, ElementType> _types = new Dictionary<string, ElementType>();

        public ElementType GetElementType(string tagName, bool isBlock, bool isSelfClosing)
        {
            string key = $"{tagName}.{isBlock}.{isSelfClosing}";
            if (!_types.ContainsKey(key))
            {
                _types[key] = new ElementType(tagName, isBlock, isSelfClosing);
            }
            return _types[key];
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("text.txt");

            ElementTypeFactory factory = new ElementTypeFactory();
            LightElementNode root = new LightElementNode(factory.GetElementType("div", true, false));

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].TrimEnd('\r', '\n');
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                ElementType type;
                if (i == 0)
                {
                    type = factory.GetElementType("h1", true, false);
                }
                else if (line.Length < 20)
                {
                    type = factory.GetElementType("h2", true, false);
                }
                else if (char.IsWhiteSpace(lines[i][0]))
                {
                    type = factory.GetElementType("blockquote", true, false);
                }
                else
                {
                    type = factory.GetElementType("p", true, false);
                }

                LightElementNode node = new LightElementNode(type);
                node.AddChild(new LightTextNode(line.Trim()));
                root.AddChild(node);
            }
            Console.WriteLine(root.OuterHTML);
            File.WriteAllText("output.txt", root.OuterHTML);


        }
    }
}