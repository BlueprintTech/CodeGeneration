# CodeGeneration

Various static helpers to assist with runtime generation of type-safe C# code. The helpers wrap the `Microsoft.CodeAnalysis.CSharp.SyntaxFactory` class provided by Roslyn.

This is still an early release of an internal helper tool so there might be some inconsistencies or improvements to be made.

# Example

Create an instance of a `CodeFile`, define the class and write it to a file:

```
var codeFile = new CodeFile
{
    DefinedInNamespace = "BlueprintTech.CodeGeneration.Test",
    UsingNamespaces = new[]
    {
        "System"
    },
    ClassName = "ACodeGenerationTestClass",
    AccessModifiers = new[]
    {
        SyntaxKind.PublicKeyword,
        SyntaxKind.StaticKeyword
    },
    Members = () => new[]
    {
        Methods.Header("Test", Types.Void)
            .WithModifiers(Tokens.StaticModifier)
            .WithBody(
                Invocations.Invoke("Console", "WriteLine", "Hello world!".AsLiteral().AsArgument())
                .AsStatement().AsBlock())
    }
};

File.WriteAllText("ACodeGenerationTestClass.cs", codeFile.Definition());
```

Resulting in:

```
namespace BlueprintTech.CodeGeneration.Test
{
    using System;

    public static class ACodeGenerationTestClass
    {
        static void Test()
        {
            Console.WriteLine("Hello world!");
        }
    }
}
```

# See also:

* https://github.com/dotnet/roslyn
* https://roslynquoter.azurewebsites.net/
