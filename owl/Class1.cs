using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;


namespace WindowsFormsApp1
{
    public class Class1
    {
        public string GetNodeString(INode node)
        {
            string s = node.ToString();
            switch (node.NodeType)
            {
                case NodeType.Uri:
                    int lio = s.LastIndexOf('#');
                    int lik = s.LastIndexOf('/');
                    if (lio == -1)
                    {
                        if (lik == -1)
                        {
                            return s;
                        }
                        else
                            return s.Substring(lik + 1);
                    }
                    else
                        return s.Substring(lio + 1);
                case NodeType.Literal:
                    return string.Format("\"{0}\"", s);
                default:
                    return s;
            }
        }
    }
}
