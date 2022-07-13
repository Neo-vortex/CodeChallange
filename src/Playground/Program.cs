var x = new List<int>();
x.Add(5);
x.Add(6);
x.Add(5);
x.Sort((i, i1) =>  i.CompareTo(i1));
var xx = 5;