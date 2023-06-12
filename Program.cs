Console.WriteLine("enter expected Path:");
var path = Console.ReadLine();

var directories = GetDirectories(path);

var files = new List<string>();

foreach (var d in directories)
{
    files.AddRange(Directory.GetFiles(d).Where(file => file.EndsWith(".cshtml")));
}

foreach (var file in files)
{
    var newLines = new List<string>();
    var changed = false;

    foreach (var line in File.ReadAllLines(file))
    {
        if(line.Contains("<link"))
        {
            if(!line.Contains("asp-append-version")){
                newLines.Add(line.Insert(line.IndexOf("<link") + 5, " asp-append-version=\"true\""));
                changed = true;
            }
        }
        else if(line.Contains("<script"))
        {
            if(!line.Contains("asp-append-version")){
                newLines.Add(line.Insert(line.IndexOf("<script") + 7, " asp-append-version=\"true\""));
                changed = true;
            }
        }
        else
        {
            newLines.Add(line);
        }
    }

    if(changed)
    {
        File.WriteAllLines(file, newLines);
    }
}


List<string> GetDirectories(string path)
{

    var directories1 = new List<string>();

    directories1.Add(path);

    var newDiectory = new List<string>();
    newDiectory.AddRange(directories1);

    var tempDirectory = new List<string>();

    while (true)
    {
        foreach (var item in newDiectory)
        {
            tempDirectory.AddRange(Directory.GetDirectories(item));
        }

        if (tempDirectory.Count == 0) break;

        newDiectory.Clear();
        newDiectory.AddRange(tempDirectory);
        directories1.AddRange(tempDirectory);
        tempDirectory.Clear();
    }

    return directories1;
}

