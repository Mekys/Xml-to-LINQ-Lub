
using System.Linq;
using System.Net.Http.Headers;
using System.Xml.Linq;


//1 задача
new XDocument(new XElement("root", File.ReadAllLines("../../../input/input_1.txt").Select(x => new XElement("line", x)))).Save("../../../output/output_1.xml");

//11 задача
XElement.Load("../../../input/input_2.xml").Descendants().GroupBy(x => x.Name).ToList().ForEach(item =>
{
   Console.WriteLine(item.Key + ":" + item.Count());
});

//21 задача
var s = "line3";
var newEl = XElement.Load("../../../input/input_3.xml");
newEl.Elements().Where(x => x.Name.ToString().Equals(s)).Remove();
new XDocument(newEl).Save("../../../output/output_3.xml");

//31 задача
var s_1 = "line";
var s_2 = "line4";
var newEl_31 = XElement.Load("../../../input/input_4.xml");
newEl_31.Elements().Where(x => x.Name.ToString().Equals(s_1)).ToList().ForEach(t => t.AddAfterSelf(new XElement(s_2, t.Attributes(), t.Nodes())));
new XDocument(newEl_31).Save("../../../output/output_4.xml");

//41 задача
var newEl_41 = XElement.Load("../../../input/input_5.xml");
newEl_41.Descendants().
    Where(x => x.Elements().Count() > 0).
    ToList().
    ForEach(item => item.Add(new XAttribute("sum", Math.Round(item.Descendants().
        Where(child => child.Descendants().Count() == 0).
        Sum(x => (double)x), 2)
    )));
new XDocument(newEl_41).Save("../../../output/output_5.xml");

//51 Задача

var newEl_51 = XElement.Load("../../../input/input_6.xml");
newEl_51.Descendants().
    Where(x => x.Elements().Count() == 0).
    ToList().
    ForEach(item =>
    {
        DateTime date = DateTime.Parse(item.Value);
        item.Add(new XAttribute("year", date.Year));
        item.SetValue("");
        item.Add(new XElement("day", date.Day));
    });
new XDocument(newEl_51).Save("../../../output/output_6.xml");

//61 задача

var newEl_61 = XElement.Load("../../../input/input_7.xml");
newEl_61.Elements().
    ToList().
    ForEach(item =>
    {
        DateTime date = DateTime.Parse(item.Element("date").Value);
        var id = item.Element("id").Value;
        var time = item.Element("time").Value;
        item.Add(new XAttribute("id", id));
        item.Add(new XAttribute("year", date.Year));
        item.Add(new XAttribute("month", date.Month));
        item.Name = "time";
        item.Value = time;
    });
new XDocument(newEl_61).Save("../../../output/output_7.xml");

//71 задача

var newEl_71 = XElement.Load("../../../input/input_8.xml");
var a = (from brand in newEl_71.Elements()
         group brand by brand.Element("info").Attribute("brand").Value
                       into g
         orderby int.Parse(g.Key) descending
         select new XElement("b" + g.Key, from p in g
                                          group p by p.Element("info").Attribute("price").Value
                                     into f
                                          orderby int.Parse(f.Key) descending
                                          select new XElement("p" + f.Key, from i in f
                                                                           orderby i.Attribute("street").Value, i.Attribute("company").Value
                                                                           select new XElement("info", i.Attributes()))));
new XDocument(new XElement("root", a)).Save("../../../output/output_8.xml");


//81 задача


var newEl_81 = XElement.Load("../../../input/input_9.xml");
a = from houses in newEl_81.Elements()
        orderby int.Parse(houses.Name.ToString().Replace("house", ""))
        select new XElement("house", new XAttribute("number", houses.Name.ToString().Replace("house", "")),
    from flats in houses.Elements()
    group flats by ((int.Parse(flats.Name.ToString().Replace("flat", "")) - 1) / 4)
    into g
    orderby g.Key
    select new XElement("entrance", new XAttribute("number", g.Key + 1),
                        new XAttribute("count", g.Count()),
                        new XAttribute("avr-debt", g.Average(x => double.Parse(x.Attribute("debt").Value.Replace(".", ",")))),
                        g.Where(x => double.Parse(x.Attribute("debt").Value.Replace(".", ",")) >= g.Average(x => double.Parse(x.Attribute("debt").Value.Replace(".", ",")))).Select(x => new XElement("debt", new XAttribute("flat", x.Name.ToString().Replace("flat", "")),
                        x.Attribute("name"), x.Attribute("debt").Value.ToString())).OrderBy(x => int.Parse(x.Attribute("flat").Value)))
    );
new XDocument(new XElement("root", a)).Save("../../../output/output_9.xml");

//69 задача

var newEl_69 = XElement.Load("../../../input/input_10.xml");
a = newEl_69.Elements().Select(x => new XElement("b" + x.Element("info").Element("brand").Value,
                                        new XAttribute("company", x.Attribute("company").Value),
                                        new XAttribute("street", x.Element("info").Attribute("street").Value),
                                        new XAttribute("price", x.Element("info").Element("price").Value))).
                                            OrderBy(x => x.Name.ToString().Remove(0, 1)).
                                            ThenBy(x => x.Attribute("company").Value).
                                            ThenBy(x => x.Attribute("street").Value);
new XDocument(new XElement("root", a)).Save("../../../output/output_10.xml");