using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task3
{
    abstract class LightNode
    {
        public abstract string OuterHTML { get; }
    }
    interface IEventListener
    {
        void Update(string tagName, string eventType);
    }

    class EventManager
    {
        private Dictionary<string, List<IEventListener>> _listeners = new Dictionary<string, List<IEventListener>>();

        public void Subscribe(string eventType, IEventListener listener)
        {
            if (!_listeners.ContainsKey(eventType))
                _listeners[eventType] = new List<IEventListener>();

            _listeners[eventType].Add(listener);
        }

        public void Unsubscribe(string eventType, IEventListener listener)
        {
            if (_listeners.ContainsKey(eventType))
                _listeners[eventType].Remove(listener);
        }

        public void Notify(string eventType, string tagName)
        {
            if (_listeners.ContainsKey(eventType))
            {
                foreach (var listener in _listeners[eventType])
                {
                    listener.Update(tagName, eventType);
                }
            }
        }
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
        public EventManager Events { get; } = new EventManager();

        public LightElementNode(string tagName, bool isBlock = true, bool isSelfClosing = false)
        {
            TagName = tagName;
            IsBlock = isBlock;
            IsSelfClosing = isSelfClosing;
        }

        public void AddClass(string className) => CssClasses.Add(className);
        public void AddChild(LightNode child)
        {
            if (!IsSelfClosing)
                Children.Add(child);
        }

        public void DispatchEvent(string eventType)
        {
            Console.WriteLine($"{TagName} dispatching '{eventType}' event.");
            Events.Notify(eventType, TagName);
        }

        public override string OuterHTML
        {
            get
            {
                var sb = new StringBuilder();
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

        public string InnerHTML => string.Concat(Children.Select(child => child.OuterHTML));
    }
    class LoggingListener : IEventListener
    {
        public void Update(string tagName, string eventType)
        {
            Console.WriteLine($"Log event '{tagName}' received by <{eventType}>");
        }
    }
     

    internal class Program
    {
        static void Main(string[] args)
        {
            var div = new LightElementNode("div");
            div.AddClass("container");

            var ul = new LightElementNode("ul");
            ul.AddClass("list");

            var li1 = new LightElementNode("li", false);
            li1.AddChild(new LightTextNode("Перший елемент"));

            var li2 = new LightElementNode("li", false);
            li2.AddChild(new LightTextNode("Другий елемент"));

            var logger = new LoggingListener();

            li1.Events.Subscribe("click", logger);
            li2.Events.Subscribe("mouseover", logger);

            ul.AddChild(li1);
            ul.AddChild(li2);
            div.AddChild(ul);

            Console.WriteLine(div.OuterHTML);

            li1.DispatchEvent("click");
            li2.DispatchEvent("mouseover");
            li2.DispatchEvent("click");  

        }
    }
}
