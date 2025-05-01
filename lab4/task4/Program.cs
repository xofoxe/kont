using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task4
{
    interface IImageLoaderStrategy
    {
        string Load(string href);
    }
    class FileImageLoader : IImageLoaderStrategy
    {
        public string Load(string href)
        {
            return $"Завантажено з файлу: '{href}'";
        }
    }
    class NetworkImageLoader : IImageLoaderStrategy
    {
        public string Load(string href)
        {
            return $"Завантажено з мережі: '{href}'";
        }
    }

    class LightImageNode : LightNode
    {
        public string Href { get; }
        private IImageLoaderStrategy _loader;
        public LightImageNode(string href)
        {
            Href = href;
            _loader = ChooseStrategy(href);
        }
        private IImageLoaderStrategy ChooseStrategy(string href)
        {
            if (href.StartsWith("http://") || href.StartsWith("https://"))
            {
                return new NetworkImageLoader();
            }
            return new FileImageLoader();
        }
        public override string OuterHTML
        {
            get
            {
                string loaded = _loader.Load(Href);
                return $"<img src=\"{Href}\"/> <!-- {loaded} -->\n";
            }
        }
    }

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

        public void AddClass(string className) => CssClasses.Add(className);
        public void AddChild(LightNode child)
        {
            if (!IsSelfClosing)
                Children.Add(child);
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
        public string InnerHTML => string.Concat(Children.Select(child => child.OuterHTML));
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            LightElementNode div = new LightElementNode("div");
            div.AddClass("container");

            LightElementNode ul = new LightElementNode("ul");
            ul.AddClass("list");

            LightElementNode li1 = new LightElementNode("li", false);
            li1.AddChild(new LightTextNode("Перший елемент"));

            LightElementNode li2 = new LightElementNode("li", false);
            li2.AddChild(new LightTextNode("Другий елемент"));

            LightElementNode div2 = new LightElementNode("div");
            div2.AddClass("container");

            LightImageNode localImg = new LightImageNode("images/photo.jpg");
            LightImageNode netImg = new LightImageNode("https://example.com/photo.jpg");

            div2.AddChild(localImg);
            div2.AddChild(netImg);

            Console.WriteLine(div2.OuterHTML);
        }
    }


}
