var tree0 = new BinaryTree("0", 16);
var tree1 = new BinaryTree("1", 16);
tree0.BuildTree();
tree1.BuildTree();

Console.WriteLine();

tree0.PrintCombinations();
tree1.PrintCombinations();
Console.WriteLine(tree0.CombinationsGenerated + tree1.CombinationsGenerated);

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
    public int CombinationsGenerated { get; set; } = 0;
    public int NodesAmount { get; set; } = 0;

    private int _currentDepth = 0;
    private List<Node> markedNodes = new List<Node>();

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

    public void PrintCombinations()
    {
        Step(Root, "");
    }

    private void Do(Node current)
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

    private void PrintNode(Node current) // just to check the tree
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
        PrintNode(current.RightChild);
        _currentDepth--;
    }

    private void Step(Node current, string combination)
    {
        combination += current.Data;

        if (current.LeftChild == null && current.RightChild == null)
        {
            Console.WriteLine(combination);
            CombinationsGenerated += 1;
            return;
        }

        _currentDepth++;
        Step(current.LeftChild, combination);
        Step(current.RightChild, combination);
        _currentDepth--;
    }
}
