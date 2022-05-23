using System.Text;


var tree = new BinaryTree("0", 4);
tree.BuildTree();

tree.PrintTree();

Console.WriteLine();
Console.WriteLine(tree.NodesAmount);

Console.ReadKey();

class Node
{
    public string Data { get; set; }
    public Node LeftChild { get; set; }
    public Node RightChild { get; set; }
    public Node Parent { get; set; }

    public void AddRight()
    {
        RightChild = new Node() { Data = "1", Parent = this };
    }
    public void AddLeft()
    {
        LeftChild = new Node() { Data = "0", Parent = this };
    }

    public override string ToString()
    {
        return $"{LeftChild.Data} <- {Data} -> {RightChild.Data}";
    }
}

class BinaryTree
{
    public Node Root { get; private set; }
    public int TreeDepth { get; private set; }

    private int _currentDepth = 0;
    private List<Node> markedNodes = new List<Node>();
    public int NodesAmount { get; set; } = 0;

    public BinaryTree(string data, int keyLength)
    {
        Root = new Node() { Data = data };
        TreeDepth = keyLength - 1;
        NodesAmount++;
    }

    public void BuildTree()
    {
        var current = Root;
        Do(current);
        _currentDepth = 0;
    }

    public void PrintTree()
    {
        PrintNode(Root);
        _currentDepth = 0;
        markedNodes.Clear();
    }

    public void Do(Node current)
    {
        if (_currentDepth == TreeDepth)
            return;

        if (current.LeftChild == null)
        {
            current.AddLeft();
            _currentDepth++;
            NodesAmount++;
            Do(current.LeftChild);
            _currentDepth--;
        }
            
        if (current.RightChild == null)
        {
            current.AddRight();
            NodesAmount++;
            _currentDepth++;
            Do(current.RightChild);
            _currentDepth--;
        }
    }

    public void PrintNode(Node current) // just to check the tree
    {
        if (current == null)
            return;

        for (int i = 0; i < _currentDepth; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine(current.Data);

        _currentDepth++;
        PrintNode(current.LeftChild);
        _currentDepth--;
        _currentDepth++;
        PrintNode(current.RightChild);
        _currentDepth--;
    }
}