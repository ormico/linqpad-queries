<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

BigInteger i = BigInteger.Parse("1042617929129312294946332267952920");
BigInteger.ModPow(i,0,97).Dump();

//why would you need this?
//see https://www.bankersonline.com/regulations/12-1003-appc
