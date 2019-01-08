using System;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Shared.CodeGeneration
{
    public static class Fields
    {
        public static FieldDeclarationSyntax PublicConstField(string name, Type type, ExpressionSyntax initialValue)
        {
            return Field(name, type, new[] { SyntaxKind.PublicKeyword, SyntaxKind.ConstKeyword }, initialValue);
        }

        public static FieldDeclarationSyntax Field(string name, Type type, SyntaxKind[] modifiers, ExpressionSyntax initialValue)
        {
            var typeDefinition = Types.Type(type);

            return FieldDeclaration(
                VariableDeclaration(typeDefinition)
                    .WithVariables(
                                SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    VariableDeclarator(
                                            Identifier(name))
                                        .WithInitializer(
                                            EqualsValueClause(initialValue)))))
                    .WithModifiers(
                        TokenList(modifiers.AsList())
            );
        }
    }
}
