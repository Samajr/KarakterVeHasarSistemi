using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Program()
{

    static void main()
    {
        
    }
}
public class Karakter
{ 
  public int Id { get; set; }
  public string Isim { get; set; }
  public int Can { get; set; }
  public int Seviye { get; set; }
    public virtual int Saldir()
    {
        int temelhasar = 10*Seviye;
        Console.WriteLine($"{Isim} temel bir vuruş yaptı ve {temelhasar} hasar verdi");
        return temelhasar;
    }
}

public class Savasci : Karakter
{
    public int Ofke { get; set; }
    public override int Saldir()
    {
        Ofke += 15;
        int hasar = 0;
        if (Ofke >= 50)
        {
            hasar = 40 * Seviye;
            Ofke = 0;
            Console.WriteLine($"{Isim} ÖFKE PATLAMASI yaşadı! {hasar} KRİTİK hasar verdi!");
        }
        else
        {
            hasar = 20 * Seviye;
            Console.WriteLine($"{Isim} baltasını savurdu, {hasar} hasar verdi. (Mevcut Öfke: {Ofke}");
        }
        return hasar;       
    }
}

public class Buyucu : Karakter
{
    public int Mana { get; set; }
    public override int Saldir()
    {
        
        int hasar = 0;
        if (Mana >= 35)
        {
            hasar = 50 * Seviye;
            Console.WriteLine($"{Isim} {Mana} kullanarak {hasar} vurdu");
            Mana -= 15;
        }
        else if (Mana == 0)
        {
            Console.WriteLine($"Manan kalmadı. {hasar} vurdu");
            hasar = 0;
        }
        else
        {
            hasar = 25 * Seviye;
            Mana -= 15;
        }
        return hasar;
    }
}

public class Boss
{
    public int Id { get; set; }
    public string Isim { get; set; }
    public int Can { get; set; }
    public int Maxhasar { get; set; }
    public int Rastgelesaldir()
    {
        Random rast = new Random();
        int vurulanhasar = rast.Next(5, Maxhasar + 1);
        return vurulanhasar;
    }
}