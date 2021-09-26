using System.Collections.Generic;
using System.Linq;

public interface GraphNode<T> where T : GraphNode<T>
{
    List<T> GetNeighbours();
}

public class Graph<T> where T : class, GraphNode<T>
{
    private List<T> nodes;
    public Graph(List<T> nodes)
    {
        this.nodes = nodes;
    }

    public bool ContainsCycle()
    {
        List<T> visited = new List<T>();
        Dictionary<T, T> parentOf = new Dictionary<T, T>();
        foreach (var node in nodes)
        {
            parentOf[node] = default(T);
        }
        while (nodes.Except(visited).Any())
        {
            Queue<T> waitingForVisit = new Queue<T>();
            T startNode = nodes.Except(visited).First();
            waitingForVisit.Enqueue(startNode);
            while (waitingForVisit.Count > 0)
            {
                T visitingNext = waitingForVisit.Dequeue();
                visited.Add(visitingNext);
                List<T> neighbours = visitingNext.GetNeighbours();
                foreach (var neighbour in neighbours)
                {
                    if (!visited.Contains(neighbour))
                    {
                        parentOf[neighbour] = visitingNext;
                        waitingForVisit.Enqueue(neighbour);
                    }
                    else if (parentOf[visitingNext] != neighbour)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}