using HashidsNet;

namespace ToDoListBk.Utils;

public static class AppHelpers
{
    public const string HashIdsSalt = "todolist*Cusatomer¨@";
    public const string key = "todolist*Cusatomer¨@";

    public static string ToHashId(this int number) =>
        GetHasher().Encode(number);

    public static int FromHashId(this string encoded) =>
        GetHasher().Decode(encoded).FirstOrDefault();

    private static Hashids GetHasher() => new(HashIdsSalt, 8);
}
