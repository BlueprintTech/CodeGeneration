using System;
using System.IO;
using BlueprintTech.CodeGeneration;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneration.Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
