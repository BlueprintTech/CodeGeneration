using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Shared.CodeGeneration
{
    public static class Collections
    {
        public static ImplicitArrayCreationExpressionSyntax AsArraySyntax(this IEnumerable<ExpressionSyntax> expressions)
        {
            var withCommas = new List<SyntaxNodeOrToken>();
            var expressionsAr = expressions.ToArray();

            for (int i = 0; i < expressionsAr.Length; i++)
            {
                withCommas.Add(expressionsAr[i]);
                if (i < expressionsAr.Length - 1)
                {
                    withCommas.Add(Tokens.Comma);
                }
            }

            return ImplicitArrayCreationExpression(
                InitializerExpression(
                    SyntaxKind.ArrayInitializerExpression,
                    SeparatedList<ExpressionSyntax>(withCommas)));
        }

        public static ObjectCreationExpressionSyntax InstantiateDictionary(TypeSyntax keyType, TypeSyntax valueType, IEnumerable<ExpressionSyntax> expressions)
        {
            var withCommas = new List<SyntaxNodeOrToken>();
            var expressionsAr = expressions.ToArray();

            for (int i = 0; i < expressionsAr.Length; i++)
            {
                withCommas.Add(expressionsAr[i]);
                if (i < expressionsAr.Length - 1)
                {
                    withCommas.Add(Tokens.Comma);
                }
            }

            var initializer = InitializerExpression(SyntaxKind.CollectionInitializerExpression, SeparatedList<ExpressionSyntax>(withCommas));

            return ObjectCreationExpression(
                    GenericName(
                            Identifier("Dictionary"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SeparatedList<TypeSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        keyType,
                                        Tokens.Comma,
                                        valueType
                                    }))))
                .WithInitializer(initializer);
        }

        public static InitializerExpressionSyntax KeyValuePairInitializer(ExpressionSyntax key, ExpressionSyntax value)
        {
            return InitializerExpression(
                SyntaxKind.ComplexElementInitializerExpression,
                SeparatedList<ExpressionSyntax>(
                    new SyntaxNodeOrToken[]
                    {
                        key,
                        Tokens.Comma,
                        value
                    }));
        }
    }
}
