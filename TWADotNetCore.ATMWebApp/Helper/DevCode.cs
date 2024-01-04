using Microsoft.EntityFrameworkCore;

namespace TWADotNetCore.ATMWebApp.Helper;

public static class DevCode
{
    public static string GenerateAtmCode()
    {
        var random = new Random();
        var digits = new List<long>(16);
    regenerate:
        for (var i = 0; i < 16; i++)
        {
            digits.Add(random.Next(0, 9));
        }

        var oddList = GetOddListLong(digits).ToList();
        var evenList = GetEvenListLong(digits).ToList();
        var newEvenList = GetNewEvenListLong(evenList).ToList();
        var sum = oddList.Sum() + newEvenList.Sum();
        if (sum % 10 != 0)
        {
            digits.Clear();
            goto regenerate;
        }
        var uniqueNumber = string.Join("", digits.Select((i, index) => (index + 1) % 4 == 0 ? i + " " : i.ToString()));
        return uniqueNumber;
    }

    public static bool IsValidAtmCode(string cardNumber)
    {
        var isLong = long.TryParse(cardNumber, out var longCardNumber);
        if (!isLong) return false;
        if (longCardNumber.ToString().Length != 16) return false;
        var cardNumberList = GetListLong(longCardNumber.ToString()).ToList();
        var oddNumber = GetOddListLong(cardNumberList).ToList();
        var evenNumber = GetEvenListLong(cardNumberList).ToList();
        var newEvenNumber = GetNewEvenListLong(evenNumber);
        var oddValue = oddNumber.Sum();
        var evenValue = newEvenNumber.Sum();
        return (oddValue + evenValue) % 10 == 0;
    }

    public static string SplitSpace(this string cardNumber)
    {
        var splitCardNumber = new string[] { };
        if (!cardNumber.Contains(" "))
            return splitCardNumber.Aggregate(cardNumber, (current, item) => current + item);
        splitCardNumber = cardNumber.Split(" ");
        cardNumber = string.Empty;
        return splitCardNumber.Aggregate(cardNumber, (current, item) => current + item);
    }

    private static IEnumerable<long> GetListLong(string cardNumber)
    {
        var cardNumberList = new List<long>(cardNumber.Length);
        cardNumberList.AddRange(cardNumber.Select(digitChar => long.Parse(digitChar.ToString())));
        return cardNumberList;
    }

    private static IEnumerable<long> GetEvenListLong(IReadOnlyList<long> listLong)
    {
        var evenListLong = new List<long>();
        for (var i = 0; i < listLong.Count; i += 2)
        {
            evenListLong.Add(listLong[i]);
        }

        return evenListLong;
    }

    private static IEnumerable<long> GetOddListLong(IReadOnlyList<long> listLong)
    {
        var oddListLong = new List<long>();
        for (var i = 1; i < listLong.Count; i += 2)
        {
            oddListLong.Add(listLong[i]);
        }

        return oddListLong;
    }

    private static IEnumerable<long> GetNewEvenListLong(List<long> evenList)
    {
        var newEvenNumber = new List<long>();
        foreach (var item in evenList)
        {
            var even = item * 2;
            var even1 = 0L;
            if (even > 9)
            {
                var stringEven = even.ToString();
                even1 += stringEven.Sum(evenChar => long.Parse(evenChar.ToString()));
                newEvenNumber.Add(even1);
            }
            else
            {
                newEvenNumber.Add(even);
            }
        }

        return newEvenNumber;
    }

    public static string FormatAtmCard(string card)
    {
        return $"{card.Substring(0, 4)} {card.Substring(4, 4)} {card.Substring(8, 4)} {card.Substring(12)}";
    }
}
