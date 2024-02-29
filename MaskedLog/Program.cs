// See https://aka.ms/new-console-template for more information
using MaskedLog;
using System;

Console.WriteLine("Hello, World!");
var res = SensitiveDataMasking.Logger<Atm>.MaskSensitiveData(new Atm { CardNumber = "11111222223", PinBlock = "TBNA5862", TerminalId = "1312312", Track2 = "AKMDAL545ADAAD513AS##121312 213" });
Console.WriteLine(res);
var res2 = SensitiveDataMasking.Logger<Card>.MaskSensitiveData(new Card { Name = "ALICANYILMAZ",Surname = "YILMAZ",Age = "25", CityName = "HATAY" });
Console.WriteLine(res2);
Console.ReadLine();
//5295455295455295