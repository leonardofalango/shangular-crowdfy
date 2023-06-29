namespace backend.Security;

public class TextSalt : ISalt
{
    private StreamReader sr;
    public string GetSalt()
    {
        sr = new StreamReader("../../Salts.txt");

        int counter = 0;
        string line = string.Empty;
        int lineNumber = (int)(new Random().NextInt64(0, 100));

        while (!sr.EndOfStream)
        {
            line = sr.ReadLine();
            if (counter == lineNumber)
                break;
            counter += 1;
        }

        return line??"";
    }
}