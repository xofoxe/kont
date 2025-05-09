﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface ILightNodeVisitor
    {
        void VisitTextNode(LightTextNode textNode);
        void VisitElementNode(LightElementNode elementNode);
    }

    interface ICommand
    {
        void Undo();
        void Execute();
    }
    class AddClassCommand : ICommand
    {
        private LightElementNode _target;
        private string _className;

        public AddClassCommand(LightElementNode target, string className)
        {
            _target = target;
            _className = className;
        }

        public void Execute()
        {
            _target.AddClass(_className);
        }

        public void Undo()
        {
            _target.CssClasses.Remove(_className);
        }
    }
    class AddChildCommand : ICommand
    {
        private LightElementNode _parent;
        private LightNode _child;

        public AddChildCommand(LightElementNode parent, LightNode child)
        {
            _parent = parent;
            _child = child;
        }

        public void Execute()
        {
            _parent.AddChild(_child);
        }

        public void Undo()
        {
            _parent.Children.Remove(_child);
        }
    }
    interface IElementState
    {
        void Apply(LightElementNode element);
        bool CanAddChild { get; }
        string StateClass { get; }
    }
    class NormalState : IElementState
    {
        public void Apply(LightElementNode element)
        {
        }

        public bool CanAddChild => true;
        public string StateClass => "";
    }
    class HighlightedState : IElementState
    {
        public void Apply(LightElementNode element)
        {
            element.AddClass("highlighted");
        }

        public bool CanAddChild => true;
        public string StateClass => "highlighted";
    }


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
        public abstract void Accept(ILightNodeVisitor visitor);
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
        public override void Accept(ILightNodeVisitor visitor)
        {
            visitor.VisitTextNode(this);
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
        private IElementState _state = new NormalState();
        public void SetState(IElementState state)
        {
            _state = state;
            state.Apply(this);
        }
        public override void Accept(ILightNodeVisitor visitor)
        {
            visitor.VisitElementNode(this);

            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
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
    class ElementCountingVisitor : ILightNodeVisitor
    {
        public Dictionary<string, int> TagCounts { get; } = new Dictionary<string, int>();

        public void VisitTextNode(LightTextNode textNode)
        {

        }

        public void VisitElementNode(LightElementNode elementNode)
        {
            if (!TagCounts.ContainsKey(elementNode.TagName))
                TagCounts[elementNode.TagName] = 0;

            TagCounts[elementNode.TagName]++;
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
