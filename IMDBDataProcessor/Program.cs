using IMDBDataProcessor;

var input = -1;

while (input != 0)
{
    Console.WriteLine("Main Menu");
    Console.WriteLine("----------------------");
    Console.WriteLine("1. Import Movies");
    Console.WriteLine("2. Import Crew");
    Console.WriteLine("3. Export Data to Flat Files");
    Console.WriteLine("0. Exit");
    Console.Write("> ");
    Exception e;
    try
    {
        input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                Console.WriteLine("Importing Movies...");
                MovieImporter.Import();
                break;
            case 2:
                Console.WriteLine("Importing Crew...");
                CrewImporter.Import();
                break;
            case 3:
                Console.WriteLine("Exporting Data to Flat Files...");
                FlatFileExporter.Export();
                break;
            case 0:
                Console.WriteLine("Exiting...");
                break;
            default:
                Console.WriteLine("Invalid Selection");
                break;
        }
    }
    catch (Exception _)
    {
        Console.WriteLine("Invalid Selection");
    }
}