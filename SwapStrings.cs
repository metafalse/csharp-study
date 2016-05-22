using System;

class Test {
public static void SwapStrings(ref string s1, ref string s2) {
    string tempStr = s1;
    s1 = s2;
    s2 = tempStr;
}
static void Main (string[] args) {
    string s1 = "Flip";
    string s2 = "Flop";
    Console.WriteLine("Before:{0},{1},", s1, s2);
    SwapStrings(ref s1, ref s2);
    Console.WriteLine("After:{0},{1}", s1, s2);
    Console.ReadLine();
}
}
