using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        private List<Node> nodes = new List<Node>();

        public List<Node> ShortestPath(string begin, string end)
        {
            List<Node> solucao = new List<Node>();
            Node shortto = new Node(), shortfrom = new Node(),nn;
            int count;
            double menor = 0;

            solucao.Add(copiaNode(Find(begin, nodes),0));
            while (Find(end, solucao) == null)
            {
                menor = 0;
                foreach(Node n in solucao)
                {
                    nn = Find(n.Name, nodes);
                    count = 0;
                    if (!n.Visited)
                    {
                        foreach(Edge ed in nn.Edges)
                        {
                            if (Find(ed.To.Name, solucao) != null)
                                count++;
                            if ((n.value + ed.Cost < menor || menor == 0) && Find(ed.To.Name,solucao) == null)
                            {
                                menor = n.value + ed.Cost;
                                shortfrom = ed.From;
                                shortto = ed.To;
                            }
                        }
                        if (count == nn.Edges.Count)
                            n.Visited = true;
                    }
                }
                solucao.Add(copiaNode(Find(shortto.Name, nodes), menor));
                Find(shortto.Name, solucao).Parent = Find(shortfrom.Name, solucao);
                AddEdge2(shortfrom.Name, shortto.Name,1,solucao);
            }
            Find(end, solucao).Parent = Find(shortfrom.Name, solucao);
            return solucao;
        }
        public Node copiaNode(Node n,double value)
        {
            Node nn = new Node(n.Name, n.Info);
            nn.value = value;
            return nn;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            Queue<Node> fila = new Queue<Node>();
            List<Node> lista = new List<Node>();
            Node n;
            Find(begin,nodes).Visited = true;
            fila.Enqueue(Find(begin,nodes));
            lista.Add(Find(begin,nodes));
            while (fila.Count != 0)
            {
                n = fila.Dequeue();
                foreach(Edge ed in n.Edges)
                {
                    if(ed.To.Visited == false)
                    {
                        ed.To.Visited = true;
                        fila.Enqueue(ed.To);
                        lista.Add(ed.To);
                        ed.To.Parent = n;
                    }
                    
                }
            }

            return lista;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            bool count;
            Stack<Node> pilha = new Stack<Node>();
            List<Node> lista = new List<Node>();
            Node n;
            Find(begin,nodes).Visited = true;
            lista.Add(Find(begin,nodes));
            pilha.Push(Find(begin,nodes));
            while (pilha.Count != 0)
            {
                count = false;
                n = pilha.Peek();
                foreach (Edge ed in n.Edges)
                {
                    if(ed.To.Visited == false)
                    {
                        count = true;
                        ed.To.Visited = true;
                        pilha.Push(ed.To);
                        lista.Add(ed.To);
                        ed.To.Parent = n;
                        break;
                    }

                }
                if(!count)
                {
                    pilha.Pop();
                }
            }
            return lista;
        }

        public void AddNode(string name, object info)
        {
            if (Find(name,nodes) == null)
            {
                Node n = new Node();
                n.Name = name;
                n.Info = info;
                nodes.Add(n);
            }
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node f = Find(nameFrom,nodes);
            Node t = Find(nameTo,nodes);
            if (f != null && t != null)
            {
                f.AddEdge(t, cost);
            }
        }

        public void AddEdge2(string nameFrom, string nameTo, int cost,List<Node> lista)
        {
            Node f = Find(nameFrom, lista);
            Node t = Find(nameTo, lista);
            if (f != null && t != null)
            {
                f.AddEdge(t, cost);
            }
        }

        private Node Find(string name,List<Node> list)
        {
            foreach (Node n in list)
            {
                if (n.Name.Equals(name))
                    return n;
            }
            return null;
        }

    }
}
