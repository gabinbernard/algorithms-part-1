// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

QuickFindUF quickFindUF = new QuickFindUF(10);
quickFindUF.union(0, 2);
quickFindUF.union(0, 4);
quickFindUF.union(0, 8);
quickFindUF.union(1, 5);
quickFindUF.union(4, 3);
Console.WriteLine(quickFindUF.find(2, 4));
Console.WriteLine(quickFindUF.find(3, 8));
Console.WriteLine(quickFindUF.find(0, 9));

QuickUnionUF quickUnionUF = new QuickUnionUF(10);
quickUnionUF.union(0, 2);
quickUnionUF.union(0, 4);
quickUnionUF.union(0, 8);
quickUnionUF.union(1, 5);
quickUnionUF.union(4, 3);
Console.WriteLine(quickUnionUF.find(2, 4));
Console.WriteLine(quickUnionUF.find(3, 8));
Console.WriteLine(quickUnionUF.find(0, 9));