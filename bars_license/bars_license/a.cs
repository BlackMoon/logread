using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
public static class a
{
    // Methods
    public static string a_(string A_0, string A_1)
    {
        List<string> list = new List<string>();
        a.c c = new a.c();
        a.b b = new a.b();
        list.AddRange(c.a2(A_0 + A_1));
        list.AddRange(b.a2(A_0 + A_1));
        List<byte> list2 = new List<byte>();
        for (int i = 0; i < list.Count; i++)
        {
            int num2 = 1;                    
            for (int j = 0; j < list[i].Length; j++)
            {                
                num2 = ((3 * num2) + list[i][j]) + 7;
            }
            list2.Add((byte)(num2 % 0xfd));            
        }
       
        string res = Convert.ToBase64String(list2.ToArray());
        return res;
    }

    public static bool a_(string A_0, string A_1, string A_2)
    {
        if ((string.IsNullOrEmpty(A_0) || string.IsNullOrEmpty(A_1)) || string.IsNullOrEmpty(A_2))
        {
            //throw new ArgumentException("Invalid argumends, invokation failed..");
        }
        return (A_1 == a_(A_0, A_2));
    }

    // Nested Types
    private class a1
    {
        // Fields
        protected int a;

        // Methods
        internal List<string> a2(string A_0)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrEmpty(A_0))
            {
                throw new ArgumentException("The string is not given..");
            }
            for (string str = A_0; str.Length > 0; str = str.Substring(this.a - 3))
            {
                if (str.Length <= this.a)
                {
                    list.Add(str);
                    return list;
                }
                list.Add(str.Substring(0, this.a));
            }
            return list;
        }
    }

    private class b : a.a1
    {
        // Methods
        public b()
        {
            base.a = 7;
        }
        ~b()
        {
        }
    }

    private class c : a.a1
    {
        // Methods
        public c()
        {
            base.a = 5;            
        }
    }
}

 

