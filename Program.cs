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

FolderInfoContext fic = new();

FolderInfo fi;

var FileForPaths = "allPaths.dat";

foreach (string d in startDirs){
    // add all dir names to sqlite
    dirCount++;
    Console.Write($"{dirCount} ");
    Console.WriteLine(d);
    targetDirs.TryAdd(d,d);
    //WriteToDb(d);
    WriteToFile(dirCount,d);
    byteCountViaPathLengths += d.Length;
    
}

while (targetDirs.Count > 0){
    var currentWorkingDir = targetDirs.ElementAt(0).Value;
    var allSubs = Directory.GetDirectories(currentWorkingDir);
    foreach (string s in allSubs){
        targetDirs.TryAdd(s,s);
        // WriteToDb(s);
        WriteToFile(dirCount,s);
        Console.Write($"{dirCount} ");
        Console.WriteLine(s);
        byteCountViaPathLengths += s.Length;
        dirCount++;
    }
    targetDirs.Remove(currentWorkingDir);
    
}

Console.WriteLine($"\nThe paths took {byteCountViaPathLengths} bytes.");

void WriteToDb(String path){
    fi = new FolderInfo(path,DateTime.Now);
    fic.Add(fi);
    fic.SaveChanges();
}

void WriteToFile(Int32 dirCount, String path){
    File.AppendAllText(FileForPaths,$"{dirCount}|{path}|{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}{Environment.NewLine}");
}

public record FolderInfo(String Path, DateTime Created, Int64 Id=0);
