// See https://aka.ms/new-console-template for more information

// 1. iterate over all dirs & subdirs
// 2. write each directory to sqlite db

if (args.Length < 1){
    Console.WriteLine("Please provide a starting path."); 
    return;
}

var startPath = args[0];

var startDirs = Directory.GetDirectories(args[0]);

Dictionary<string,string> targetDirs = new();
Int64 byteCountViaPathLengths = 0;
Int32 dirCount =0;
foreach (string d in startDirs){
    // add all dir names to sqlite
    Console.WriteLine(d);
    targetDirs.TryAdd(d,d);
    byteCountViaPathLengths += d.Length;
    dirCount++;
    Console.Write($"{dirCount} ");
}

while (targetDirs.Count > 0){
    var currentWorkingDir = targetDirs.ElementAt(0).Value;
    var allSubs = Directory.GetDirectories(currentWorkingDir);
    foreach (string s in allSubs){
        targetDirs.TryAdd(s,s);
        Console.Write($"{dirCount} ");
        Console.WriteLine(s);
        byteCountViaPathLengths += s.Length;
        dirCount++;
    }
    targetDirs.Remove(currentWorkingDir);
    
}

Console.WriteLine($"\nThe paths took {byteCountViaPathLengths} bytes.");
