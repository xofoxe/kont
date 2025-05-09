using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    abstract class LightNode
    {
        public virtual IEnumerable<LightNode> TraverseDepthFirst()
        {
            yield return this;
        }

        public virtual IEnumerable<LightNode> TraverseBreadthFirst()
        {
            yield return this;
        }

        public string Render()
        {
            OnCreated();
            var html = OuterHTML;
            OnRendered();
            return html;
        }

        public abstract string OuterHTML { get; }
        protected virtual void OnCreated() { }
        protected internal virtual void OnInserted() { }
        protected virtual void OnRemoved() { }
        protected virtual void OnStylesApplied() { }
        protected virtual void OnClassListApplied() { }
        protected virtual void OnTextRendered() { }
        protected virtual void OnRendered() { }
    }

    class LightTextNode : LightNode
    {
        public string Text { get; }

        public LightTextNode(string text)
        {
            Text = text;
        }

        public override string OuterHTML
        {
            get
            {
                OnTextRendered();
                return Text;
            }
        }

        protected override void OnTextRendered()
        {
            Console.WriteLine($"Text rendered: \"{Text}\"");
        }
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
            OnClassListApplied();
        }

        public void AddChild(LightNode child)
        {
            if (!IsSelfClosing)
            {
                Children.Add(child);
                child.OnInserted();
            }
        }
        public override IEnumerable<LightNode> TraverseDepthFirst()
        {
            yield return this;
            foreach (var child in Children)
            {
                foreach (var descendant in child.TraverseDepthFirst())
                {
                    yield return descendant;
                }
            }
        }

        public override IEnumerable<LightNode> TraverseBreadthFirst()
        {
            var queue = new Queue<LightNode>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current;

                if (current is LightElementNode elementNode)
                {
                    foreach (var child in elementNode.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public string InnerHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var child in Children)
                {
                    sb.Append(child.Render());
                }
                return sb.ToString();
            }
        }

        public override string OuterHTML
        {
            get
            {
                OnStylesApplied();

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

        protected override void OnStylesApplied()
        {
            Console.WriteLine($"Styles applied to <{TagName}> with classes: {string.Join(", ", CssClasses)}");
        }

        protected override void OnClassListApplied()
        {
            Console.WriteLine($"Class list changed on <{TagName}>: {string.Join(", ", CssClasses)}");
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
