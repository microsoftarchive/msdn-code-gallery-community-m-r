using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;

namespace com.techphernalia.windows.forms.techieUI
{
    public class TreeDataList : IEnumerable<TreeData>
    {
        public void RunAllTest()
        {
            frmOutput Out = new frmOutput();
            Out.T.Text = "";
            StreamWriter writer = ConsoleWriter;
            TextWriter oldConsoleOut = Console.Out;
            Console.SetOut(writer);
            MemoryStream stream = (MemoryStream)writer.BaseStream;
            stream.SetLength(0);
            foreach (TreeData _current in this)
            {
                _current.Execute();
            }
            writer.Flush();
            Console.SetOut(oldConsoleOut);
            Out.T.Text += writer.Encoding.GetString(stream.ToArray());
            Out.ShowDialog();
        }
        private readonly IDictionary<int, TreeData> tree = new Dictionary<int, TreeData>();
        private readonly String _Title;
        private readonly StreamWriter _ConsoleWriter = new StreamWriter(new MemoryStream());
        public TreeDataList()
        {
            Type treeType = this.GetType();
            _Title = "Durgesh";
            string prefix = "Tutorial";
            foreach (Attribute a in treeType.GetCustomAttributes(false))
            {
                if (a is TitleAttribute)
                    _Title = ((TitleAttribute)a).Title;
                else if (a is PrefixAttribute)
                    prefix = ((PrefixAttribute)a).Prefix;
            }

            var methods =
                from sm in treeType.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly | BindingFlags.Static)
                where sm.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                orderby sm.Name
                select sm;
            int m = 1;
            foreach (var method in methods)
            {
                string methodCategory = "techPhernalia";
                string methodTitle = prefix + " Tutorial " + m;
                string methodDescription = "No Description Available.";
                List<CodeBlock> code = new List<CodeBlock>();
                foreach (Attribute a in method.GetCustomAttributes(false))
                {
                    if (a is CategoryAttribute)
                        methodCategory = ((CategoryAttribute)a).Category;
                    else if (a is TitleAttribute)
                        methodTitle = ((TitleAttribute)a).Title;
                    else if (a is DescriptionAttribute)
                        methodDescription = ((DescriptionAttribute)a).Description;
                    else if (a is CodeAttribute)
                        code.Add(new CodeBlock(((CodeAttribute)a).Language, ((CodeAttribute)a).CodeBlock));
                }

                tree.Add(m, new TreeData(this, method, methodCategory, methodTitle, methodDescription, code));
                m++;
            }
        }
        public String Title { get { return _Title; } }
        public StreamWriter ConsoleWriter { get { return _ConsoleWriter; } }
        public TreeData this[int index]
        {
            get { return tree[index]; }
        }

        IEnumerator<TreeData> IEnumerable<TreeData>.GetEnumerator()
        {
            return tree.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tree.Values.GetEnumerator();
        }
    }
    public class CodeBlock
    {
        private readonly String _Code;
        private readonly String _Lang;
        public CodeBlock(String lang,String code)
        {
            this._Code = code;
            this._Lang = lang;
        }
        public String Lang { get { return _Lang; } }
        public String Code { get { return _Code; } }
        public override string ToString()
        {
            return _Lang;
        }
    }
    public class TreeData
    {
        private readonly TreeDataList _ParentList;
        private readonly MethodInfo _Method;
        private readonly string _Category;
        private readonly string _Title;
        private readonly string _Description;
        private readonly List<CodeBlock> _Code;

        public TreeData(TreeDataList ParentList, MethodInfo Method, string Category, string Title, string Description, List<CodeBlock> Code)
        {
            _ParentList = ParentList;
            _Method = Method;
            _Category = Category;
            _Title = Title;
            _Description = Description;
            _Code = Code;
        }
        public TreeDataList ParentList { get { return _ParentList; } }
        public MethodInfo Method { get { return _Method; } }
        public string Category { get { return _Category; } }
        public string Title { get { return _Title; } }
        public string Description { get { return _Description; } }
        public List<CodeBlock> Code { get { return _Code; } }
        public void Execute()
        {
            _Method.Invoke(_ParentList, null);
        }
        public override string ToString()
        {
            return Title;
        }
    }
    [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TitleAttribute : Attribute
    {
        public TitleAttribute(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }
    [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PrefixAttribute : Attribute
    {
        public PrefixAttribute(string prefix)
        {
            this.Prefix = prefix;
        }

        public string Prefix { get; set; }
    }
    [global::System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string category)
        {
            this.Category = category;
        }

        public string Category { get; set; }
    }
    [global::System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            this.Description = description.Replace("\n",Environment.NewLine);
        }

        public string Description { get; set; }
    }
    [global::System.AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CodeAttribute : Attribute
    {
        public CodeAttribute(string language, string code)
        {
            this.Language = language;
            this.CodeBlock = code.Replace("\n",Environment.NewLine);
        }

        public CodeAttribute(string language, string[] code)
        {
            this.Language = language;
            foreach (string c in code)
                CodeBlock += c + Environment.NewLine;
        }
        public string Language { get; set; }
        public string CodeBlock { get; set; }
    }
}
