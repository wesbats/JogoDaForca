using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao Jogo da Forca!");

        bool jogarNovamente = true;

        while (jogarNovamente)
        {
            string palavraSecreta = LerPalavraSecreta();

            int tentativasRestantes = 6;
            char[] letrasUsadas = new char[26];

            while (tentativasRestantes > 0)
            {
                Console.Clear();
                Console.WriteLine("Jogo da Forca");
                DesenharBoneco(tentativasRestantes);
                MostrarPalavra(palavraSecreta, letrasUsadas);
                Console.WriteLine($"Vidas Restantes: {tentativasRestantes}");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                char letra = keyInfo.KeyChar;

                if (!Char.IsLetter(letra) || (letra < 'a' || letra > 'z'))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPor favor, digite apenas letras do alfabeto.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                letra = char.ToLower(letra);

                if (letrasUsadas.Contains(letra))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nVocê já usou a letra '{letra}'. Tente novamente.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                letrasUsadas[letra - 'a'] = letra;

                string palavraNormalizada = palavraSecreta.Normalize(NormalizationForm.FormD);
                string letraNormalizada = letra.ToString().Normalize(NormalizationForm.FormD);

                if (palavraNormalizada.Contains(letraNormalizada))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nA letra '{letra}' está na palavra!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nA letra '{letra}' não está na palavra.");
                    tentativasRestantes--;
                    Console.ResetColor();
                }

                if (PalavraCompletada(palavraSecreta, letrasUsadas))
                {
                    Console.Clear();
                    Console.WriteLine("Jogo da Forca");
                    MostrarPalavra(palavraSecreta, letrasUsadas);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nParabéns! Você adivinhou a palavra!");
                    Console.ResetColor();
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine("Jogo da Forca");
            DesenharBoneco(tentativasRestantes);
            MostrarPalavra(palavraSecreta, letrasUsadas);
            Console.WriteLine($"Vidas Restantes: {tentativasRestantes}");

            if (tentativasRestantes == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nVocê perdeu! A palavra secreta era: {palavraSecreta}");
                Console.ResetColor();
            }

            Console.Write("\nDeseja jogar novamente? (S/N): ");
            char escolha = Console.ReadKey().KeyChar;
            jogarNovamente = (escolha == 's' || escolha == 'S');
            Console.Clear();
        }

        Console.WriteLine("Obrigado por jogar o Jogo da Forca!");
    }

    static string LerPalavraSecreta()
    {
        string palavraSecreta;

        do
        {
            Console.Write("Digite a palavra secreta (apenas letras do alfabeto): ");
            palavraSecreta = Console.ReadLine().ToLowerInvariant();

            if (!Regex.IsMatch(palavraSecreta, "^[a-zA-Z]+$"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nA palavra secreta deve conter apenas letras do alfabeto.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
        while (!Regex.IsMatch(palavraSecreta, "^[a-zA-Z]+$"));

        return palavraSecreta;
    }

    static void DesenharBoneco(int tentativasRestantes)
    {
        switch (tentativasRestantes)
        {
            case 6:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 5:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 4:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine("  |   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 3:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine(" /|   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 2:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine(" /|\\  |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 1:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine(" /|\\  |");
                Console.WriteLine(" /    |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
            case 0:
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  O   |");
                Console.WriteLine(" /|\\  |");
                Console.WriteLine(" / \\  |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
                break;
        }
    }

    static void MostrarPalavra(string palavraSecreta, char[] letrasUsadas)
    {
        List<char> palavraAdivinhada = new List<char>();

        foreach (char letra in palavraSecreta)
        {
            if (letrasUsadas.Contains(letra))
                palavraAdivinhada.Add(letra);
            else
                palavraAdivinhada.Add('_');
        }

        Console.WriteLine("Palavra: " + string.Join(" ", palavraAdivinhada));

        List<char> letrasErradas = letrasUsadas
            .Where(letra => !palavraSecreta.Contains(letra))
            .Distinct()
            .ToList();

        if (letrasErradas.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nLetras Erradas ({letrasErradas.Count - 1}): {string.Join(" ", letrasErradas)}");
            Console.ResetColor();
        }
    }

    static bool PalavraCompletada(string palavraSecreta, char[] letrasUsadas)
    {
        foreach (char letra in palavraSecreta)
        {
            if (!letrasUsadas.Contains(letra))
                return false;
        }
        return true;
    }
}
