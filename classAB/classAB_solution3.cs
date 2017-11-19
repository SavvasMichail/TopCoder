using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/********************************************************************************************************************************************
Problem Description - SRM - 648
=============================================================================================================================================
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
*********************************************************************************************************************************************
Solution Description
=============================================================================================================================================
    The logic is to try finding in which range the given number of pairs falls into. The below cases are identified:
     a) The number of pairs are less than the number N. This is found immediately by adding k number of B on the right side of an A. And the 
        remaining number (N-k-1) of B's to the left side of the A. Example (8,5): BBABBBBB.

     b) The number of pairs are equal to the maximum number of pairs. The maximum number of pairs is found by multiplying the quotient and remainder
        when the N is divided by 2.

     c) The number of pairs falls in between a range of possible pairs having two A's, three A's and so on (one A is covered by (a)).
        For example, for N=8 then:
            - for two A's we have the range:
                AABBBBBB = 2*6 = 12 - Upper Limit
                BAABBBBB = 2*5 = 10 - Lower Limit (The lower limit is found by moving a B to the left most side)
         If the number of pairs is less than the Lower Limit then we add two a B on the left most side (BAABBBBB) and we find again the upper
         and lower limit. If the number of pairs is greater than the Upper limit, we increase the number of A's (AAABBBBB) and so on.
**********************************************************************************************************************************************/

namespace TopCoderArena
{
    class ClassABEnhanced
    {
        int InitialNumOfBs;
        int NumOfAs;
        int NumOfBs;

        bool StopSearch;
        string Output;
        int OutputLength;

        int UpperBound;
        int LowerBound;

        /*
            Set's the upper bound and lower bound based on:
                - The initial number of B's (at the beginning of the string)
                - The number of A's
                - The number of B's beside the starting B's
        */
        public void SetBounds()
        {
            this.UpperBound = this.NumOfAs * this.NumOfBs;
            this.LowerBound = this.NumOfAs * (this.NumOfBs -1);

            if (this.LowerBound <= 0)
                this.StopSearch = true;
        }

        /*
            Initializes the class's members
        */
        public void Initialize(int aLength)
        {
            StopSearch = false;
            Output = System.String.Empty;
            OutputLength = aLength;
        }

        /*
            Sets the number of A's and B's and initial B's (at the beginning of the string).
            In case of any invalid number the search is stopped.
        */
        public void SetABs(int aInitialNumOfBs, int aNumOfAs)
        {
            this.InitialNumOfBs = aInitialNumOfBs;
            this.NumOfAs = aNumOfAs;
            this.NumOfBs = this.OutputLength - aNumOfAs - aInitialNumOfBs;

            if (this.NumOfAs >= OutputLength || this.NumOfBs <= 0)
                this.StopSearch = true;
        }

        /*
            Builds the output string
        */
        public void BuildOutputSring(int aPairs)
        {
            this.Output = new String('B', this.InitialNumOfBs) + new String('A', this.NumOfAs) + new String('B', this.NumOfBs);
            if (aPairs < this.UpperBound)
            {
                StringBuilder dOutput = new StringBuilder(Output);
                dOutput[this.InitialNumOfBs + this.NumOfAs] = 'A';
                dOutput[this.InitialNumOfBs + this.NumOfAs - (this.UpperBound - aPairs)] = 'B';
                Output = dOutput.ToString();
            }
        }

        /**
            createString
        */
        public string createString(int aLength, int aPairs)
        {

            int MaxAs = (aLength / 2);
            int MaxBs = (aLength - aLength / 2);
            int MaxPairs =  MaxAs * MaxBs;

            /*
                Case 1: Check the problem's constraints
                Case 2: Number of pairs <= Length -1
                Case 3: Number of Pairs >= Length and <= MaxPairs
                Case 4: Invalid cases
            */

            if (aLength <= 1 || aLength > 51 || aPairs < 0 || aPairs > aLength * (aLength - 1) / 2)
            {
                //!< Case 1: Intentionally left blank
                Output = System.String.Empty;
            }
            else if (aPairs <= aLength -1)
            {
                //!< Case 2: pairs <= length - 1
                Output = new String('B', aLength - aPairs - 1) + new string('A', aPairs) + new String('B', 1);
            }
            else if (aPairs == MaxPairs)
            {
                Output = new string('A', MaxAs) + new string('B', MaxBs);
            }
            else if (aPairs >= aLength && aPairs < MaxPairs)
            {
                //!< Case 3: Number of Pairs >= Length and <= MaxPairs
                this.Initialize(aLength);
                this.SetABs(0, 2);
                this.SetBounds();

                while (!this.StopSearch)
                {

                    if (aPairs < this.LowerBound)
                    {
                        this.SetABs(this.InitialNumOfBs + 1, this.NumOfAs);
                        this.SetBounds();
                    }
                    else if (aPairs >= LowerBound && aPairs <= this.UpperBound)
                    {
                        this.BuildOutputSring(aPairs);
                        this.StopSearch = true;

                    }
                    else if (aPairs > this.UpperBound)
                    {
                        this.SetABs(this.InitialNumOfBs, this.NumOfAs + 1);
                        this.SetBounds();
                    }
                    else
                    {
                        this.StopSearch = true;
                    }
                }
            }
            else
            {
                //!< Case 4: Invalid cases
                Output = System.String.Empty;
            }

            return Output;
        }
    }
}
