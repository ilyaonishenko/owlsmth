using System;
using System.Windows.Forms;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using System.Collections.Generic;

namespace owl
{
    public partial class Form1 : Form
    {
        // функция, которая убирает URI вид. И все элементы трипла в нормальном. читабельном виде
        string GetNodeString(INode node)
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
        ListBox list1 = new ListBox();
        ListBox list2 = new ListBox();
        ListBox list3 = new ListBox();

        public Form1()
        {
            InitializeComponent();
            //загружаем данные из файла в граф
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"rdf.txt");
            ListBox listBox1 = new ListBox();
            ListBox listBox2 = new ListBox();
            ListBox listBox3 = new ListBox();
            // распределяем элементы трипла по спискам
            foreach (Triple triple in graph.Triples)
            {
                listBox1.Items.Add(GetNodeString(triple.Subject));
                listBox2.Items.Add(GetNodeString(triple.Object));
                listBox3.Items.Add(GetNodeString(triple.Predicate));
            }
            //выводит все объекты, субъекты и индивиды в комбобокс без повторений
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (list1.Items.IndexOf(listBox1.Items[i]) == -1)
                {
                    list1.Items.Add(listBox1.Items[i]);
                }
            }
            for (int i = 0; i < list1.Items.Count; i++)
            {
                comboBox1.Items.Add(list1.Items[i]);
            }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (list2.Items.IndexOf(listBox2.Items[i]) == -1)
                {
                    list2.Items.Add(listBox2.Items[i]);
                }
            }
            for (int i = 0; i < list2.Items.Count; i++)
            {
                comboBox2.Items.Add(list2.Items[i]);
            }
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                if (list3.Items.IndexOf(listBox3.Items[i]) == -1)
                {
                    list3.Items.Add(listBox3.Items[i]);
                }
            }
            for (int i = 0; i < list3.Items.Count; i++)
            {
                comboBox3.Items.Add(list3.Items[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"rdf.txt");
            int number_triplets;
            number_triplets = graph.Triples.Count;
            dataGridView1.RowCount = number_triplets;
            Column1.HeaderText = "Subject";
            Column2.HeaderText = "Property";
            Column3.HeaderText = "Object";
            ListBox listbox1 = new ListBox();
            ListBox listbox2 = new ListBox();
            ListBox listbox3 = new ListBox();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                foreach (Triple triple in graph.Triples)
                {
                    listbox1.Items.Add(GetNodeString(triple.Subject));
                    listbox2.Items.Add(GetNodeString(triple.Predicate));
                    listbox3.Items.Add(GetNodeString(triple.Object));
                    dataGridView1.Rows[i].Cells[0].Value = listbox1.Items[i];
                    dataGridView1.Rows[i].Cells[1].Value = listbox2.Items[i];
                    dataGridView1.Rows[i].Cells[2].Value = listbox3.Items[i];
                }
            }
            textBox2.Text = Convert.ToString(number_triplets);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1_1.Items.Clear();
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"rdf.txt");
            Column1.HeaderText = "Subject";
            Column2.HeaderText = "Property";
            Column3.HeaderText = "Object";
            label4.Visible = true;
            ListBox listBox1 = new ListBox();
            ListBox listBox2 = new ListBox();
            ListBox listBox3 = new ListBox();
            foreach (Triple triple in graph.Triples)
            {
                listBox1.Items.Add(GetNodeString(triple.Subject));
                listBox2.Items.Add(GetNodeString(triple.Predicate));
                listBox3.Items.Add(GetNodeString(triple.Object));
            }
            ListBox class_property4 = new ListBox();
            ListBox class_sub4 = new ListBox();
            ListBox class_obj4 = new ListBox();
            int i = 0;
            while (i < listBox1.Items.Count)
            {
                if (Convert.ToString(listBox3.Items[i]) == "ObjectProperty")
                {
                    class_property4.Items.Add(listBox2.Items[i]);
                    class_sub4.Items.Add(listBox1.Items[i]);
                    class_obj4.Items.Add(listBox3.Items[i]);
                }
                i++;
            }
            textBox2.Text = Convert.ToString(class_property4.Items.Count);
            for (int J = 0; J < class_property4.Items.Count; J++)
            {
                listBox1_1.Items.Add(class_sub4.Items[J]);
            }
            listBox1_1.Items.Add("subClassOf");
            listBox1_1.Items.Add("type");
            listBox1_1.Items.Add("name");
            label5.Visible = true;
            label5.Text = "ObjectProperty";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1_1.Items.Clear();
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"rdf.txt");
            Column1.HeaderText = "Subject";
            Column2.HeaderText = "Property";
            Column3.HeaderText = "Object";
            ListBox listBox1 = new ListBox();
            ListBox listBox2 = new ListBox();
            ListBox listBox3 = new ListBox();
            foreach (Triple triple in graph.Triples)
            {
                listBox1.Items.Add(GetNodeString(triple.Subject));
                listBox2.Items.Add(GetNodeString(triple.Predicate));
                listBox3.Items.Add(GetNodeString(triple.Object));
            }
            ListBox class_property4 = new ListBox();
            ListBox class_sub4 = new ListBox();
            ListBox class_obj4 = new ListBox();
            int i = 0;
            while (i < listBox1.Items.Count)
            {
                if (Convert.ToString(listBox2.Items[i]) == "subClassOf" || Convert.ToString(listBox3.Items[i]) == "Class")
                {
                    class_property4.Items.Add(listBox2.Items[i]);
                    class_sub4.Items.Add(listBox1.Items[i]);
                    class_obj4.Items.Add(listBox3.Items[i]);
                }
                i++;
            }
            textBox2.Text = Convert.ToString(class_property4.Items.Count);
            for (int J = 0; J < class_property4.Items.Count; J++)
            {
                listBox1_1.Items.Add(class_sub4.Items[J]);
            }
            label5.Visible = true;
            label5.Text = "Classes";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1_1.Items.Clear();
            var parser = new Notation3Parser();
            var graph = new Graph();
            parser.Load(graph, @"rdf.txt");
            Column1.HeaderText = "Subject";
            Column2.HeaderText = "Property";
            Column3.HeaderText = "Object";
            label4.Visible = true;
            ListBox listBox1 = new ListBox();
            ListBox listBox2 = new ListBox();
            ListBox listBox3 = new ListBox();
            foreach (Triple triple in graph.Triples)
            {
                listBox1.Items.Add(GetNodeString(triple.Subject));
                listBox2.Items.Add(GetNodeString(triple.Predicate));
                listBox3.Items.Add(GetNodeString(triple.Object));
            }
            ListBox class_property4 = new ListBox();
            ListBox class_sub4 = new ListBox();
            ListBox class_obj4 = new ListBox();
            int i = 0;
            while (i < listBox1.Items.Count)
            {
                if (Convert.ToString(listBox3.Items[i]) == "Individual")
                {
                    class_property4.Items.Add(listBox2.Items[i]);
                    class_sub4.Items.Add(listBox1.Items[i]);
                    class_obj4.Items.Add(listBox3.Items[i]);
                }
                i++;
            }
            textBox2.Text = Convert.ToString(class_property4.Items.Count);
            for (int J = 0; J < class_property4.Items.Count; J++)
            {
                listBox1_1.Items.Add(class_sub4.Items[J]);
            }
            label5.Visible = true;
            label5.Text = "Individuals";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var graph = new Graph();
            var parser = new Notation3Parser();
            parser.Load(graph, @"rdf.txt");
            ListBox listBox1 = new ListBox();
            ListBox listBox2 = new ListBox();
            ListBox listBox3 = new ListBox();
            foreach (Triple triple in graph.Triples)
            {
                listBox1.Items.Add(triple.Subject);
                listBox2.Items.Add(triple.Object);
                listBox3.Items.Add(triple.Predicate);
            }
            // ищет все триплы для заданного субъекта
          if (comboBox1.Text != "" && comboBox3.Text == "" && comboBox2.Text == "")
            {
                listBox1_1.Items.Clear();
                int sel1 = comboBox1.SelectedIndex;
                Object sel11 = comboBox1.SelectedItem;
                string sub = sel11.ToString();
                sub = " :" + sub;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
PREFIX mypref: <https://ddddtryyaiafarjfka.blogspot.ru/mypref/>
PREFIX mn: <https://ddddtryyaiafarjfka.blogspot.ru/mn/>
PREFIX cs: <https://ddddtryyaiafarjfka.blogspot.ru/cs/>
PREFIX icb: <https://ddddtryyaiafarjfka.blogspot.ru/icb/>
PREFIX iot: <https://ddddtryyaiafarjfka.blogspot.ru/iot/>
PREFIX hp: <https://ddddtryyaiafarjfka.blogspot.ru/hp/>
PREFIX call: <https://ddddtryyaiafarjfka.blogspot.ru/call/>
PREFIX bg: <https://ddddtryyaiafarjfka.blogspot.ru/bg/>
PREFIX schema:<http://schema.org/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
	        SELECT  ?prop ?obj
	        WHERE 
	        { 
                 " + sub + @" ?prop ?obj .
            }";
                string s = "";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[0].Value = "";
                        dataGridView1.Rows[i].Cells[1].Value = GetNodeString(result["prop"]);
                        dataGridView1.Rows[i].Cells[2].Value = GetNodeString(result["obj"]);
                    }
                }
            }
            // ищет все триплы для заданного свойства
            if (comboBox3.Text != "" && comboBox1.Text == "" && comboBox2.Text == "")
            {
                listBox1_1.Items.Clear();
                int sel2 = comboBox3.SelectedIndex;
                Object sel22 = comboBox3.SelectedItem;
                string prop = sel22.ToString();
                if (prop == "causes") prop = "cs:" + prop;
                else if (prop.StartsWith("means"))
                    prop = "mn:" + prop;
                else if (prop.StartsWith("inOrderTo"))
                    prop = "iot:" + prop;
                else if (prop.StartsWith("type"))
                    prop = "rdf:" + prop;
                else if (prop.StartsWith("subClassOf"))
                    prop = "rdfs:" + prop;
                else if (prop.StartsWith("isCausedBy"))
                    prop = "icb:" + prop;
                else if (prop.StartsWith("hasProperty"))
                    prop = "hp:" + prop;
                else if (prop.StartsWith("belong"))
                    prop = "bg:" + prop;
                else if (prop.StartsWith("isPartOf"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("canCall"))
                    prop = "call:" + prop;
                else if (prop.StartsWith("manage"))
                    prop = ":" + prop;
                else if (prop.StartsWith("actionStatus"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("name"))
                    prop = "schema:" + prop;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
PREFIX mypref: <https://ddddtryyaiafarjfka.blogspot.ru/mypref/>
PREFIX mn: <https://ddddtryyaiafarjfka.blogspot.ru/mn/>
PREFIX cs: <https://ddddtryyaiafarjfka.blogspot.ru/cs/>
PREFIX icb: <https://ddddtryyaiafarjfka.blogspot.ru/icb/>
PREFIX iot: <https://ddddtryyaiafarjfka.blogspot.ru/iot/>
PREFIX hp: <https://ddddtryyaiafarjfka.blogspot.ru/hp/>
PREFIX call: <https://ddddtryyaiafarjfka.blogspot.ru/call/>
PREFIX bg: <https://ddddtryyaiafarjfka.blogspot.ru/bg/>
PREFIX schema:<http://schema.org/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
	        SELECT  ?sub ?obj
	        WHERE 
	        { 
                 ?sub " + prop + @" ?obj 
            .}";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[1].Value = "";
                        dataGridView1.Rows[i].Cells[0].Value = GetNodeString(result["sub"]);
                        dataGridView1.Rows[i].Cells[2].Value = GetNodeString(result["obj"]);
                    }
                }
            }
            // ищет все триплы для заданного объекта
            if (comboBox2.Text != "" && comboBox1.Text == "" && comboBox3.Text == "")
            {
                listBox1_1.Items.Clear();
                int sel1 = comboBox2.SelectedIndex;
                Object sel11 = comboBox2.SelectedItem;
                string obj = sel11.ToString();
                if (obj == "Individual")
                    obj = "rdf:type " + obj;
                else if (obj == "Class")
                    obj = ":" + obj;
                else if (obj == "ObjectProperty")
                    obj = ":" + obj;
                else if (!obj.StartsWith("?"))
                    obj = ":" + obj;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
PREFIX mypref: <https://ddddtryyaiafarjfka.blogspot.ru/mypref/>
PREFIX mn: <https://ddddtryyaiafarjfka.blogspot.ru/mn/>
PREFIX cs: <https://ddddtryyaiafarjfka.blogspot.ru/cs/>
PREFIX icb: <https://ddddtryyaiafarjfka.blogspot.ru/icb/>
PREFIX iot: <https://ddddtryyaiafarjfka.blogspot.ru/iot/>
PREFIX hp: <https://ddddtryyaiafarjfka.blogspot.ru/hp/>
PREFIX call: <https://ddddtryyaiafarjfka.blogspot.ru/call/>
PREFIX bg: <https://ddddtryyaiafarjfka.blogspot.ru/bg/>
PREFIX schema:<http://schema.org/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
	        SELECT  ?sub ?prop
	        WHERE 
	        { 
                 ?sub ?prop " + obj + @".
            }";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[2].Value = "";
                        dataGridView1.Rows[i].Cells[1].Value = GetNodeString(result["prop"]);
                        dataGridView1.Rows[i].Cells[0].Value = GetNodeString(result["sub"]);
                    }
                }
            }            
            // ищет свойства связывающее для субъекта и объекта
            if (comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text == "")
            {
                int sel1 = comboBox1.SelectedIndex;
                Object sel11 = comboBox1.SelectedItem;
                string sub = sel11.ToString();
                if (!sub.StartsWith("?"))
                    sub = ":" + sub;
                int sel2 = comboBox2.SelectedIndex;
                Object sel22 = comboBox2.SelectedItem;
                string obj = sel22.ToString();
                if (!obj.StartsWith("?"))
                    obj = ":" + obj;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
	        SELECT   ?prop
	        WHERE 
	        { 
                 " + sub + @"?prop " + obj + @".
            }";
                string s = "";
                label5.Visible = true;
                label5.Text = "Properties";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[2].Value = "";
                        dataGridView1.Rows[i].Cells[1].Value = GetNodeString(result["prop"]);
                        dataGridView1.Rows[i].Cells[0].Value = "";
                    }
                }
            }            
            // ищет все триплы для заданного субъекта и свойства         
            if (comboBox1.Text != "" && comboBox3.Text != "" && comboBox2.Text == "")
            {
                int sel1 = comboBox1.SelectedIndex;
                Object sel11 = comboBox1.SelectedItem;
                string sub = sel11.ToString();
                if (!sub.StartsWith("?"))
                { sub = " :" + sub; }       
                int sel2 = comboBox3.SelectedIndex;
                Object sel22 = comboBox3.SelectedItem;
                string prop = sel22.ToString();
                if (prop == "causes") prop = "cs:" + prop;
                else if (prop.StartsWith("means"))
                    prop = "mn:" + prop;
                else if (prop.StartsWith("inOrderTo"))
                    prop = "iot:" + prop;
                else if (prop.StartsWith("type"))
                    prop = "rdf:" + prop;
                else if (prop.StartsWith("subClassOf"))
                    prop = "rdfs:" + prop;
                else if (prop.StartsWith("isCausedBy"))
                    prop = "icb:" + prop;
                else if (prop.StartsWith("hasProperty"))
                    prop = "hp:" + prop;
                else if (prop.StartsWith("belong"))
                    prop = "bg:" + prop;
                else if (prop.StartsWith("isPartOf"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("canCall"))
                    prop = "call:" + prop;
                else if (prop.StartsWith("manage"))
                    prop = ":" + prop;
                else if (prop.StartsWith("actionStatus"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("name"))
                    prop = "schema:" + prop;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
PREFIX mypref: <https://ddddtryyaiafarjfka.blogspot.ru/mypref/>
PREFIX mn: <https://ddddtryyaiafarjfka.blogspot.ru/mn/>
PREFIX cs: <https://ddddtryyaiafarjfka.blogspot.ru/cs/>
PREFIX icb: <https://ddddtryyaiafarjfka.blogspot.ru/icb/>
PREFIX iot: <https://ddddtryyaiafarjfka.blogspot.ru/iot/>
PREFIX hp: <https://ddddtryyaiafarjfka.blogspot.ru/hp/>
PREFIX call: <https://ddddtryyaiafarjfka.blogspot.ru/call/>
PREFIX bg: <https://ddddtryyaiafarjfka.blogspot.ru/bg/>
PREFIX schema:<http://schema.org/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
	        SELECT  ?prop ?obj
	        WHERE 
	        { 
                 " + sub + " " + prop + @"?obj .
            }";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[0].Value = "";
                        dataGridView1.Rows[i].Cells[1].Value = "";
                        dataGridView1.Rows[i].Cells[2].Value = GetNodeString(result["obj"]);
                    }
                }
            }
            // ищет все триплы для заданного объекта и свойства
            if (comboBox2.Text != "" && comboBox3.Text != "" && comboBox1.Text == "")
            {
                int sel2 = comboBox2.SelectedIndex;
                Object sel22 = comboBox2.SelectedItem;
                string obj = sel22.ToString();
                if (obj == "Individual")
                    obj = "rdf:type" + obj;
                else if (obj == "Class")
                    obj = "rdf:type" + obj;
                else if (obj == "ObjectProperty")
                    obj = "rdf:type:" + obj;
                else if (!obj.StartsWith("?"))
                    obj = ":" + obj;
                int sel3 = comboBox3.SelectedIndex;
                Object sel33 = comboBox3.SelectedItem;
                string prop = sel33.ToString();
                if (prop == "causes") prop = "cs:" + prop;
                else if (prop.StartsWith("means"))
                    prop = "mn:" + prop;
                else if (prop.StartsWith("inOrderTo"))
                    prop = "iot:" + prop;
                else if (prop.StartsWith("type"))
                    prop = "rdf:" + prop;
                else if (prop.StartsWith("subClassOf"))
                    prop = "rdfs:" + prop;
                else if (prop.StartsWith("isCausedBy"))
                    prop = "icb:" + prop;
                else if (prop.StartsWith("hasProperty"))
                    prop = "hp:" + prop;
                else if (prop.StartsWith("belong"))
                    prop = "bg:" + prop;
                else if (prop.StartsWith("isPartOf"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("canCall"))
                    prop = "call:" + prop;
                else if (prop.StartsWith("manage"))
                    prop = ":" + prop;
                else if (prop.StartsWith("actionStatus"))
                    prop = "schema:" + prop;
                else if (prop.StartsWith("name"))
                    prop = "schema:" + prop;
                string getInsomnia = @"
PREFIX :<https://ddddtryyaiafarjfka.blogspot.ru/>
PREFIX mypref: <https://ddddtryyaiafarjfka.blogspot.ru/mypref/>
PREFIX mn: <https://ddddtryyaiafarjfka.blogspot.ru/mn/>
PREFIX cs: <https://ddddtryyaiafarjfka.blogspot.ru/cs/>
PREFIX icb: <https://ddddtryyaiafarjfka.blogspot.ru/icb/>
PREFIX iot: <https://ddddtryyaiafarjfka.blogspot.ru/iot/>
PREFIX hp: <https://ddddtryyaiafarjfka.blogspot.ru/hp/>
PREFIX call: <https://ddddtryyaiafarjfka.blogspot.ru/call/>
PREFIX bg: <https://ddddtryyaiafarjfka.blogspot.ru/bg/>
PREFIX schema:<http://schema.org/>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
	        SELECT  ?sub 
	        WHERE 
	        { 
                 ?sub " + prop + " " + obj + @".
            }";
                SparqlResultSet resultSet = graph.ExecuteQuery(getInsomnia) as SparqlResultSet;
                dataGridView1.RowCount = resultSet.Count + 1;
                if (resultSet != null)
                {
                    for (int i = 0; i < resultSet.Count; i++)
                    {
                        SparqlResult result = resultSet[i];
                        dataGridView1.Rows[i].Cells[2].Value = "";
                        dataGridView1.Rows[i].Cells[1].Value = "";
                        dataGridView1.Rows[i].Cells[0].Value = GetNodeString(result["sub"]);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            dataGridView1.Rows.Clear();
        }
    }
}
