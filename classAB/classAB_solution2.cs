/**
Problem Description
        You are given two s: N and K. Lun the dog is interested in strings that satisfy the following conditions:
        The string has exactly N characters, each of which is either 'A' or 'B'.
        The string s has exactly K pairs (i, j) (0 <= i < j <= N-1) such that s[i] = 'A' and s[j] = 'B'.
        If there exists a string that satisfies the conditions, find and return any such string. Otherwise, return an empty string.
        Definition
        Class: AB
        Method: createString
        Parameters: int, int
        Returns: string
        Method signature: string createString(int N, int K)
        (be sure your method is public)
        Limits
        Time limit (s): 2.000
        Memory limit (MB): 256
        Constraints
        - N will be between 2 and 50, inclusive.
        - K will be between 0 and N(N-1)/2, inclusive.
        
Programming Language Used: C#
Notes: Brute Force

The code checks the possible combinations of 2^N numbers in binary form. The zero value represents the 'A' letter 
and 1's represent the 'B'.
It seems that 2^(N-1) contains all the possible cases. Thus there is no need for checking all the combinations. Just the half of it.
*/

class AB
{
    //!< Simulates the Math "Power" function
    public double MathPow(double aNum, double aPower)
    {
        double MathPowerRes = 1;
        for (int i = 0; i < aPower; ++i)
            MathPowerRes *= aNum;
        return MathPowerRes;
    }

    //!< Convert a number to its binary form and presented as string, left padded as the given length
    public string ConvertToString(int aNum, int aLength)
    {
        string iResult = "";
        string RetRes = "";
        int Num = aNum;
        int numb = aNum;
        int Remainder = 0;
        do
        {
            Num = numb / 2;
            Remainder = numb % 2;
            iResult += Remainder.ToString();
            numb = Num;
        } while (Num > 0);

        for (int x = 0; x < aLength - iResult.Length; ++x)
            RetRes += '0';

        for (int x = iResult.Length - 1; x >= 0; x--)
            RetRes += iResult[x];

        return RetRes;
    }

    //!< Creates the string of N digits with K pairs, using brute force.
    public string createString(int N, int K)
    {
        string Result = "";
        double PossibleCases = 0;
        int CountPairs = 0;

        if ((N >= 2 && N <= 50) || (K >= 0 && K <= (N-1)/2))
        {
            //!< Check the half of the possible combinations.
            PossibleCases = MathPow(2, N-1);
            for (int i = 0; i < PossibleCases; ++i)
            {
                Result = ConvertToString(i, N);
                CountPairs = 0;

                for (int x=0; x < Result.Length; ++x)
                {
                    for (int y = x+1; y < Result.Length; ++y)
                    {
                        if (Result[x] < Result[y])
                            ++CountPairs;
                    }
                }
                if (CountPairs == K)
                    return Result.Replace('0', 'A').Replace('1', 'B');
            }
            return "";
        }
        else
            return "";
    }
}
