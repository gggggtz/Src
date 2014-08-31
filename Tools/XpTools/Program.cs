using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.GroupPolicy;

namespace XpTools
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                ListGroupPolicies(args[0]);
            }
            else
            {
                Console.WriteLine("Usage : XpTools.exe domainName");
            }
        }

        static void ListGroupPolicies(string domainName)
        {
            GPDomain domain = new GPDomain(domainName);
            foreach(var gpo in domain.GetAllGpos())
            {
                Console.WriteLine("GPO : " + gpo.DisplayName + " : " + gpo.Path);
                GPSearchCriteria searchCriteria = new GPSearchCriteria();
                searchCriteria.Add(SearchProperty.SomLinks, SearchOperator.Contains, gpo);
                foreach(var som in domain.SearchSoms(gpo))
                {
                    Console.WriteLine("SOM " + som.Name + " : " + som.Path);
                    foreach(var link in som.GpoLinks)
                    {
                        Console.WriteLine("LINK : " + link.DisplayName + " Enabled : " + link.Enabled.ToString());
                    }
                }
            }
            
        }
    }
}
