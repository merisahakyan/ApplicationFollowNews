# ApplicationFollowNews
Namespace **Following** has two main classes:**MyNews** and _static class_ **Server**.</br>
**Server** class contains a static method **SendGetRequest(string url)**, which returns the source code of a web page that given by _url_.</br>
![how to get source code of a web page](https://github.com/merisahakyan/ApplicationFollowNews/blob/master/sourcecode.gif)</br>
**MyNews** class constructor allows to choose in which language you want to get the most read news(arenian or russian).In default </br>
it is armenian.
```cs
public MyNews(string name, string language = "arm")
{
     AgencyName = name;
     this.language = language;
}
```
The _private_ **void NewsListCreator()** method fills the list with received news.News with their properties we receive </br>
with help System.Text.RegularExpressions namespace.
```cs
MatchCollection viewes = Regex.Matches(s, s1, RegexOptions.Singleline);
foreach (Match x in viewes)
{
      GroupCollection Group = x.Groups;
      var m = new NewsProp { Views = int.Parse(Group[1].Value.Trim()) };
      newslist.Add(m);
}
MatchCollection titleslinks = Regex.Matches(s, s3, RegexOptions.Singleline);
foreach (Match x in titleslinks)
{
    if (i < newslist.Count)
    {
       GroupCollection Group = x.Groups;
       newslist[i].Link = "http://blognews.am" + Group[1].Value.Trim();
       newslist[i].Title = Group[2].Value.Trim();
       i++;
    }
    else
       break;
}
```
And finnaly the class send received news with utilizing _events_ and _delegates_.
```cs
public event EventHandler<NewsProp> DailyNews;
public void BroadcastNews()
{
    NewsListCreator();
    foreach (var item in newslist)
    {
        DailyNews?.Invoke(this, item);
    }
}
```
##How to use
News are printing in the text docuent with name _BlogNews.txt_, which situetid on the Desktop.If there isn't file with </br>
name _BlogNews_,the compiler creats new file with sae name.

```cs
static void Main(string[] args)
{
    MyNews mn = new MyNews("BlogNews","russian");
    mn.DailyNews += ShowNews;
    mn.BroadcastNews();
}
public static void ShowNews(object sender, EventArgs e)
{
    var agency = (MyNews)sender;
    if (agency != null)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"/BlogNews.txt";
        using (StreamWriter r = new StreamWriter(path,true))
        {
           r.WriteLine(e.ToString());
        }
    }
}
```






##Most read news ( BlogNews daily )
![](https://github.com/marysahakyan/ApplicationFollowNews/blob/master/mostreadennews.gif)
