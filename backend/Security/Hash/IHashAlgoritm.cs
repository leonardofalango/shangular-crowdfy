namespace backend.Security.Hash;

public interface IHashAlgoritm
{
    public string ToHash(string str);
}