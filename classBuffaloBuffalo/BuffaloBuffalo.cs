using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Problem Statement - SRM 723
    	Did you know that "Buffalo buffalo Buffalo buffalo buffalo buffalo Buffalo buffalo." is a grammatically correct sentence in American English? 



In this problem we call a string good if it satisfies the following constraints: 

The string contains one or more words.
Each word in the string is "buffalo".
Each pair of consecutive words is separated by exactly one space.
There are no spaces at the beginning of the string.
There are no spaces at the end of the string.
For example, the strings "buffalo", "buffalo buffalo" and "buffalo buffalo buffalo" are good but " buffalo", "buffalobuffalo", "buff alo", and "cow" are not. 



You are given a String s that consists of spaces and lowercase letters. Return "Good" if s is a good string. Otherwise, return "Not good". (Note that the return value is case-sensitive.)
 
Definition
    	
Class:	BuffaloBuffalo
Method:	check
Parameters:	String
Returns:	String
Method signature:	String check(String s)
(be sure your method is public)
    
 
Constraints
-	s will contain between 1 and 1,000 characters, inclusive.
-	Each character in s will be a lowercase English letter ('a' - 'z') or a space (' ').
*/

namespace TopCoder_Buffalo
{
    class BuffaloBuffalo
    {
        public String check(String s)
        {
            string Result_Bad = "Not good";
            string Result_Good = "Good";
            string Result = Result_Good;
            string BUFFALO = "buffalo";
            int BUFFALO_LEN = 7;
            string substring;
            int index = 1;
            int pos = 0;

            int NumOfBuffalo = s.Length / BUFFALO_LEN;
            int NumOfSpaces = (NumOfBuffalo - 1);

            if (s.Length < 1 || s.Length > 1000 || s.Length != (NumOfBuffalo*BUFFALO_LEN + NumOfSpaces) || s.Length < BUFFALO_LEN)
            {
                Result = Result_Bad;
            }
            else {
                int NextSubPos = 0;
                while (true)
                {
                    NextSubPos = (index * BUFFALO_LEN) + (index - 1) + 1;
                    substring = s.Substring(pos, BUFFALO_LEN);
 
                    if (!substring.Equals(BUFFALO))
                    {
                        Result = Result_Bad;
                        break;
                    }
                    else
                    {
                        if (s.Length == NextSubPos - 1)
                        {
                            Result = Result_Good;
                            break;
                        }
                        else if (s.Length > NextSubPos - 1 && s[NextSubPos - 1].Equals(' '))
                        {
                            pos = NextSubPos;
                            ++index;
                        }
                        else
                        {
                            Result = Result_Bad;
                            break;
                        }
                    }
 
                }
            }
            return Result;
        }
    }
}
