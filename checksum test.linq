<Query Kind="Statements" />

List<byte> a = new List<byte>();
a.Add(6);
a.Add(2);
a.Add(0);
a.Add(55);
a.Add(54);
a.Add(54);
a.Add(52);
a.Add(48);
a.Add(48);
a.Add(53);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(0);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);
a.Add(93);
a.Add(129);
a.Add(89);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);
a.Add(93);
a.Add(129);
a.Add(89);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);
a.Add(93);
a.Add(129);
a.Add(89);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);
a.Add(93);
a.Add(129);
a.Add(89);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);
a.Add(93);
a.Add(129);
a.Add(89);
a.Add(47);
a.Add(35);
a.Add(221);
a.Add(34);
a.Add(39);
a.Add(20);
a.Add(200);
a.Add(181);
a.Add(143);
a.Add(8);
a.Add(73);
a.Add(68);
a.Add(92);

byte checksum = 0;

for(int i=0;i<a.Count -2;i++)
{
	checksum += a[i];
}

Console.WriteLine("checksum = {0} (hex 0x{0:X})", checksum);