using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ZabwazIO
{
    [Serializable]
    class BST<T> : IEnumerable
        where T: IComparable
    {
        public class Node<T>
        {
            public T element { get; set; }
            [XmlIgnore]
            public Node<T> left, right;
            public Node(T elem)
            {
                this.element = elem;
                left = right = null;
            }
            public override string ToString() => element.ToString();
        }
        private Node<T> root;
        public Func<T, T, int> Sort_Function { get; private set; }
        public BST(Func<T,T,int> sortfunction = null)
        {
            if(sortfunction==null)
            {
                this.Sort_Function = (person1, person2) => person1.CompareTo(person2);
                return;
            }
            this.Sort_Function = sortfunction;
            root = null;
        }
        public void Add(T elem)
        {
            if(root == null)
            {
                root = new Node<T>(elem);
                return;
            }
            else
            {
                Node<T> tmp = root;
                Node<T> prev = null;
                while (tmp != null)
                {
                    prev = tmp;
                    //int x = elem.CompareTo(tmp.element);
                    int x = Sort_Function(elem, tmp.element);
                    if (x < 0)
                    {
                        tmp = tmp.left;
                    }
                    else
                        tmp = tmp.right;
                }
                int y = Sort_Function(elem, prev.element);
                if (y < 0)
                {
                    prev.left =new Node<T>(elem);
                }
                else
                    prev.right = new Node<T>(elem);
            }
        }
        private void Inorder(Node<T> tmp, ref List<T> lista)
        {
            if (tmp == null) return;
            Inorder(tmp.left, ref lista);
            lista.Add(tmp.element);
            Inorder(tmp.right, ref lista);
        }
        public static explicit operator List<T>(BST<T> tree)
        {
            List<T> lista = new List<T>();
            tree.Inorder(tree.root,ref lista);
            return lista;
        }
        public IEnumerator GetEnumerator()
        {
            List<T> lista = ((List<T>)this);
            foreach (var elem in lista)
            {
                yield return elem;
            }
        }
        public T Find(Func<T,int> f)
        {
            Node<T> tmp = this.root;
            while(tmp!=null && f(tmp.element)!=0)
            {
                if (f(tmp.element) < 0)
                    tmp = tmp.left;
                else if (f(tmp.element) > 0)
                    tmp = tmp.right;
            }
            if (tmp == null) throw new MyException("no item found!");
            return tmp.element;
        }
        public static void SerilizeToDirectory(string path, BST<T> obj)
        {
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            foreach(Person elem in obj)
            {
                var file_name = Path.Combine(path, elem.Name+"_"+elem.Surname+".txt");
                //if (File.Exists(file_name)) file_name += "1";
                using (FileStream file = new FileStream(file_name, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(file, elem);
                }
            }
        }
        public static BST<T> DeserilizeDirectory(string path)
        {
            if (Directory.Exists(path) == false) throw new MyException("Not direcoty found!");
            var EnumFile = Directory.EnumerateFiles(path);
            BST<T> ReturnTree = new BST<T>();
            foreach(var FilePath in EnumFile)
            {
                using (FileStream file = new FileStream(FilePath, FileMode.Open))
                {
                    var deserializer = new XmlSerializer(typeof(T));
                    ReturnTree.Add((T)(deserializer.Deserialize(file)));
                }
            }
            return ReturnTree;
        }
    }
    public class MyException : Exception
    {
        public MyException(string msg) : base(msg)
        {
        }
    }
}
