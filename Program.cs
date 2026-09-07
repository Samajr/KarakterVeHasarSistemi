using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Program()
{
   
    static void Main()
    {
        
         string baglanti = "Server=localhost;Database=karakterhasarDb;Uid=root;Pwd='';";
        Karakter kahraman = null;
        while (true)
        {
            using (MySqlConnection baglan = new MySqlConnection(baglanti))
        {
            baglan.Open();
            string kliste = "select * from karakterler";
            using (MySqlCommand klistele= new MySqlCommand(kliste,baglan))
            {
                using (MySqlDataReader drliste =klistele.ExecuteReader())
                {

                    while (drliste.Read())
                    {
                        int id = Convert.ToInt16(drliste["id"]);
                        string isim = drliste["isim"].ToString();
                        string tur = drliste["tur"].ToString();
                        Console.WriteLine($"{id}- {isim}: {tur}");
                    }
                }
                    
            }
        }
        Console.Write("Savaşmak istediğiniz karakterin numarasını girin: ");
        int secilenID = Convert.ToInt16(Console.ReadLine());
        
        using (MySqlConnection baglan = new MySqlConnection(baglanti))
            {
                baglan.Open();
                string seckarakter = "select * from karakterler where id=" +secilenID;
                using (MySqlCommand kmt = new MySqlCommand(seckarakter, baglan))
                {
                    using (MySqlDataReader kdr= kmt.ExecuteReader())
                    {
     
                        if (kdr.Read())
                        {
                            string tur = kdr["tur"].ToString();
                            if (tur == "Savasci")
                            {
                                kahraman = new Savasci();
                                kahraman.Id = Convert.ToInt16(kdr["id"]);
                                kahraman.Isim = kdr["isim"].ToString();
                                kahraman.Seviye = Convert.ToInt16(kdr["Seviye"]);
                                kahraman.Can = Convert.ToInt16(kdr["Can"]);
                                ((Savasci)kahraman).Ofke = Convert.ToInt32(kdr["enerji_kaynagi"]);
                                Console.WriteLine($"Savaş başlıyor! Seçilen kahraman: {kahraman.Isim}");
                                break;
                            }
                            else if (tur == "Buyucu")
                            {
                                kahraman = new Buyucu();
                                kahraman.Id = Convert.ToInt16(kdr["id"]);
                                kahraman.Isim = kdr["isim"].ToString();
                                kahraman.Seviye = Convert.ToInt16(kdr["Seviye"]);
                                kahraman.Can = Convert.ToInt16(kdr["Can"]);
                                ((Buyucu)kahraman).Mana = Convert.ToInt32(kdr["enerji_kaynagi"]);
                                Console.WriteLine($"Savaş başlıyor! Seçilen kahraman: {kahraman.Isim}");
                                break;
                            }
                            else { Console.WriteLine("geçersiz karakter lütfen doğru tuşlayınız..");
                                Console.ReadLine();
                            }
                               
                        }
                        else
                        {
                            Console.WriteLine("Girilen ID bulunamadı! Devam etmek için Enter'a basın...");
                            Console.ReadLine();
                        }




                    }
                }
            }
            if (kahraman != null)
            {
                break;
            }
        }
        Console.Clear();
        Console.WriteLine($"Savaş başlıyor! Seçilen kahraman: {kahraman.Isim}");

        Console.WriteLine("\nDevam etmek için Enter'a basın...");
        Console.ReadLine();
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